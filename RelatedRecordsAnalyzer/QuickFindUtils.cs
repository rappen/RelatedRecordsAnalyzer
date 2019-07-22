using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Rappen.XTB.RRA
{
    public static class QuickFindUtils
    {
        public static string ComposeQuickFindQuery(this string fetchxml, AttributeMetadata[] attributes, string searchtext)
        {
            searchtext = searchtext.Replace("*", "%");
            var fx = new XmlDocument();
            fx.LoadXml(fetchxml);
            if (fx.SelectSingleNode("fetch/entity") is XmlNode entitynode)
            {
                entitynode.FixQuickFindEntityNode(attributes, searchtext);
            }
            fetchxml = fx.OuterXml;
            return fetchxml;
        }

        private static void FixQuickFindEntityNode(this XmlNode entitynode, AttributeMetadata[] attributes, string searchtext)
        {
            var filters = entitynode.SelectNodes("filter");
            foreach (XmlNode filter in filters)
            {
                filter.FixQuickFindFilterNode(attributes, searchtext);
            }
            var linkentities = entitynode.SelectNodes("link-entity");
            foreach (XmlNode linkentity in linkentities)
            {
                linkentity.FixQuickFindEntityNode(attributes, searchtext);
            }
        }

        private static void FixQuickFindFilterNode(this XmlNode filter, AttributeMetadata[] attributes, string searchtext)
        {
            if (filter.Attributes["type"]?.Value == "or" && filter.Attributes["isquickfindfields"]?.Value == "1")
            {
                var findstr = searchtext + "%";
                var specint = int.TryParse(searchtext, out int findint);
                var specfloat = float.TryParse(searchtext, out float findfloat);
                var specdate = DateTime.TryParse(searchtext, out DateTime finddate);
                var specguid = Guid.TryParse(searchtext, out Guid findguid);
                var specbool = bool.TryParse(searchtext, out bool findbool);
                var conditions = filter.SelectNodes("condition");
                foreach (XmlNode cond in conditions)
                {
                    var attrname = cond.Attributes["attribute"]?.Value;
                    var value = cond.Attributes["value"]?.Value;
                    switch (value.Trim('{').Trim('}'))
                    {
                        case "0":
                            if (attributes.FirstOrDefault(a => a.LogicalName == attrname) is AttributeMetadata attrmeta)
                            {
                                switch (attrmeta.AttributeType)
                                {
                                    case AttributeTypeCode.Customer:
                                    case AttributeTypeCode.Lookup:
                                    case AttributeTypeCode.Owner:
                                        if (!specguid)
                                        {
                                            cond.Attributes["attribute"].Value = attrname + "name";
                                        }
                                        break;

                                    case AttributeTypeCode.Boolean:
                                        if (!specbool)
                                        {
                                            cond.Attributes["attribute"].Value = attrname + "name";
                                        }
                                        break;

                                    case AttributeTypeCode.Picklist:
                                    case AttributeTypeCode.State:
                                    case AttributeTypeCode.Status:
                                        cond.Attributes["attribute"].Value = attrname + "name";
                                        break;
                                }
                            }
                            value = findstr;
                            break;

                        case "1":
                            value = specint ? findint.ToString() : null;
                            break;

                        case "2":
                        case "4":
                            value = specfloat ? findfloat.ToString() : null;
                            break;

                        case "3":
                            value = specdate ? finddate.ToString() : null;
                            break;
                    }
                    if (value == null)
                    {
                        filter.RemoveChild(cond);
                    }
                    else
                    {
                        cond.Attributes["value"].Value = value;
                    }
                }
            }
            var filters = filter.SelectNodes("filter");
            foreach (XmlNode subfilter in filters)
            {
                subfilter.FixQuickFindFilterNode(attributes, searchtext);
            }
        }

        public static string[] GetQueryColumns(this string fetchxml, params string[] requiredattributes)
        {
            var fxdoc = new XmlDocument();
            fxdoc.LoadXml(fetchxml);
            var attributes = fxdoc.SelectNodes("fetch/entity/attribute");
            var result = attributes
                .Cast<XmlNode>()
                .Select(a => a.Attributes["name"].Value)
                .ToList();
            foreach (var req in requiredattributes)
            {
                if (!result.Contains(req))
                {
                    result.Add(req);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Loads quick find query details for given OTC
        /// </summary>
        /// <param name="service"></param>
        /// <param name="objecttypecode"></param>
        /// <returns>ValueTuple<string, List<string>> where Item1 is the fetchxml string and Item2 is a list of attributes to return from the query</returns>
        public static (string, List<string>) GetQuickFind(this IOrganizationService service, int objecttypecode)
        {
            string fetchxml = null;
            List<string> layoutxml = null;
            var qry = new QueryExpression("savedquery");
            qry.ColumnSet.AddColumns("fetchxml", "layoutxml");
            qry.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, objecttypecode);
            qry.Criteria.AddCondition("querytype", ConditionOperator.Equal, 4);
            var view = service.RetrieveMultiple(qry).Entities.FirstOrDefault();
            if (view != null && view.Contains("fetchxml"))
            {
                fetchxml = view["fetchxml"] as string;
                var layout = new XmlDocument();
                layout.LoadXml(view["layoutxml"] as string);
                layoutxml = layout.SelectNodes("grid/row/cell")
                    .Cast<XmlNode>()
                    .Select(c => c.Attributes["name"].Value)
                    .ToList();
            }
            return (fetchxml, layoutxml);
        }
    }
}