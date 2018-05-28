using Cinteros.Xrm.CRMWinForm;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Rappen.XTB.RRA
{
    public partial class RRA : PluginControlBase, IStatusBarMessenger, IGitHubPlugin, IPayPalPlugin
    {
        public string RepositoryName => "RelatedRecordsAnalyzer";

        public string UserName => "rappen";

        public string DonationDescription => "Related Records Analyzer Fan Club";

        public string EmailAccount => "jonas@rappen.net";

        //private Settings mySettings;

        public RRA()
        {
            InitializeComponent();
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        private void RRA_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            //if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            //{
            //    mySettings = new Settings();

            //    LogWarning("Settings not found => a new settings file has been created!");
            //}
            //else
            //{
            //    LogInfo("Settings found and loaded");
            //}
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            ExecuteMethod(GetAccounts);
        }

        private void GetAccounts()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting accounts",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("account")
                    {
                        TopCount = 50
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} accounts");
                    }
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            //SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            gvRecords.OrganizationService = newService;
            LoadEntities(PopulateEntities);

            //mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
            LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
        }

        private void LoadEntities(Action<EntityMetadataProxy[]> callback)
        {
            WorkAsync(new WorkAsyncInfo("Loading entities...",
                (eventargs) =>
                {
                    eventargs.Result = MetadataHelper.LoadEntities(Service);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is RetrieveMetadataChangesResponse)
                        {
                            var metaresponse = ((RetrieveMetadataChangesResponse)completedargs.Result).EntityMetadata;
                            var entities = metaresponse
                                .Where(e => e.IsCustomizable.Value == true && e.IsIntersect.Value != true)
                                .Select(m => new EntityMetadataProxy(m))
                                .OrderBy(e => e.ToString()).ToArray();
                            callback?.Invoke(entities);
                        }
                    }
                }
            });
        }

        private void PopulateEntities(EntityMetadataProxy[] entities)
        {
            cmbEntities.Items.Clear();
            cmbEntities.Items.AddRange(entities);
        }

        private (EntityMetadataProxy, Guid) ParseRecordUrl(string url)
        {
            EntityMetadataProxy entity = null;
            var id = Guid.Empty;
            try
            {
                var uri = new Uri(url);
                var uriparams = HttpUtility.ParseQueryString(uri.Query);
                var idstr = uriparams["id"];
                if (string.IsNullOrEmpty(idstr) && !string.IsNullOrEmpty(uriparams["extraqs"]))
                {
                    var extraqs = HttpUtility.UrlDecode(uriparams["extraqs"]);
                    var decodedparams = HttpUtility.ParseQueryString(extraqs);
                    idstr = decodedparams["id"];
                }
                Guid.TryParse(idstr, out id);
                var etn = uriparams["etn"];
                if (!string.IsNullOrEmpty(etn))
                {
                    entity = cmbEntities.Items.Cast<EntityMetadataProxy>().FirstOrDefault(e => e.Metadata.LogicalName == etn);
                }
                else
                {
                    var etcstr = uriparams["etc"];
                    if (int.TryParse(etcstr, out int etc))
                    {
                        entity = cmbEntities.Items.Cast<EntityMetadataProxy>().FirstOrDefault(e => e.Metadata.ObjectTypeCode == etc);
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
            }
            return (entity, id);
        }

        private void SetStatus(string status)
        {
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs(status));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            typeTimer.Stop();
            typeTimer.Start();
        }

        private void typeTimer_Tick(object sender, EventArgs e)
        {
            typeTimer.Stop();
            FindRecords(textBox1.Text);
        }

        private void FindRecords(string find)
        {
            QueryBase qry = GetUrlQuery(find);
            if (qry == null && cmbEntities.SelectedItem is EntityMetadataProxy entity)
            {
                qry = GetGuidQuery(find, entity);
                if (qry == null)
                {
                    if (string.IsNullOrEmpty(entity.quickfindfetch))
                    {
                        LoadQuickFind(entity, FindRecords, find);
                        return;
                    }
                    if (entity.Metadata.Attributes == null)
                    {
                        LoadAttributes(entity, FindRecords, find);
                        return;
                    }
                    qry = ReplaceQuickFindPlaceholders(entity, find);
                }
            }
            LoadRecords(qry);
        }

        private static QueryBase ReplaceQuickFindPlaceholders(EntityMetadataProxy entity, string find)
        {
            var fx = new XmlDocument();
            fx.LoadXml(entity.quickfindfetch);
            if (fx.SelectSingleNode("fetch/entity") is XmlNode entitynode)
            {
                FixEntity(entity, entitynode, find);
            }
            return new FetchExpression(fx.OuterXml);
        }

        private static void FixEntity(EntityMetadataProxy entity, XmlNode entitynode, string find)
        {
            var filters = entitynode.SelectNodes("filter");
            foreach (XmlNode filter in filters)
            {
                FixFilter(entity, filter, find);
            }
            var linkentities = entitynode.SelectNodes("link-entity");
            foreach (XmlNode linkentity in linkentities)
            {
                FixEntity(entity, linkentity, find);
            }
        }

        private static void FixFilter(EntityMetadataProxy entity, XmlNode filter, string find)
        {
            if (filter.Attributes["type"]?.Value == "or" && filter.Attributes["isquickfindfields"]?.Value == "1")
            {
                var findstr = find + "%";
                var specint = int.TryParse(find, out int findint);
                var specfloat = float.TryParse(find, out float findfloat);
                var specdate = DateTime.TryParse(find, out DateTime finddate);
                var specguid = Guid.TryParse(find, out Guid findguid);
                var specbool = bool.TryParse(find, out bool findbool);
                var conditions = filter.SelectNodes("condition");
                foreach (XmlNode cond in conditions)
                {
                    var attrname = cond.Attributes["attribute"]?.Value;
                    var value = cond.Attributes["value"]?.Value;
                    switch (value.Trim('{').Trim('}'))
                    {
                        case "0":
                            if (entity.Metadata.Attributes.FirstOrDefault(a => a.LogicalName == attrname) is AttributeMetadata attrmeta)
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
                FixFilter(entity, subfilter, find);
            }
        }

        private QueryExpression GetUrlQuery(string url)
        {
            var entity = ParseRecordUrl(url);
            if (entity.Item1 != null && !entity.Item2.Equals(Guid.Empty))
            {
                var meta = entity.Item1.Metadata;
                var qry = new QueryExpression(meta.LogicalName);
                qry.Criteria.AddCondition(meta.PrimaryIdAttribute, ConditionOperator.Equal, entity.Item2);
                qry.ColumnSet.AddColumns(GetQuickFindQueryColumns(entity.Item1));
                return qry;
            }
            return null;
        }

        private string[] GetQuickFindQueryColumns(EntityMetadataProxy entity)
        {
            if (string.IsNullOrEmpty(entity.quickfindfetch))
            {
                LoadQuickFind(entity, null, null);
            }
            var fxdoc = new XmlDocument();
            fxdoc.LoadXml(entity.quickfindfetch);
            var attributes = fxdoc.SelectNodes("fetch/entity/attribute");
            var result = attributes
                .Cast<XmlNode>()
                .Select(a => a.Attributes["name"].Value)
                .ToArray();
            return result;
        }

        private QueryExpression GetGuidQuery(string id, EntityMetadataProxy entity)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                var qry = new QueryExpression(entity.Metadata.LogicalName);
                qry.Criteria.AddCondition(entity.Metadata.PrimaryIdAttribute, ConditionOperator.Equal, guid);
                qry.ColumnSet.AddColumns(entity.Metadata.PrimaryIdAttribute, entity.Metadata.PrimaryNameAttribute);
                return qry;
            }
            return null;
        }

        private void LoadAttributes(EntityMetadataProxy entity, Action<string> callback, string find)
        {
            WorkAsync(new WorkAsyncInfo($"Loading Attributes for {entity}...",
                (eventargs) =>
                {
                    eventargs.Result = MetadataHelper.LoadEntityDetails(Service, entity.Metadata.LogicalName);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else if (completedargs.Result is RetrieveMetadataChangesResponse metadata && metadata.EntityMetadata.Count > 0)
                    {
                        entity.Metadata = metadata.EntityMetadata.FirstOrDefault();
                        callback?.Invoke(find);
                    }
                }
            });
        }

        private void LoadQuickFind(EntityMetadataProxy entity, Action<string> callback, string find)
        {
            if (callback == null)
            {
                GetQuickFind(entity, Service);
                return;
            }
            WorkAsync(new WorkAsyncInfo($"Loading Quick Find query for {entity}...",
                (eventargs) =>
                {
                    GetQuickFind(entity, Service);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        callback?.Invoke(find);
                    }
                }
            });
        }

        private static void GetQuickFind(EntityMetadataProxy entity, IOrganizationService service)
        {
            var qry = new QueryExpression("savedquery");
            qry.ColumnSet.AddColumns("fetchxml", "layoutxml");
            qry.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, entity.Metadata.ObjectTypeCode);
            qry.Criteria.AddCondition("querytype", ConditionOperator.Equal, 4);
            var view = service.RetrieveMultiple(qry).Entities.FirstOrDefault();
            if (view != null && view.Contains("fetchxml"))
            {
                entity.quickfindfetch = view["fetchxml"] as string;
                var layout = new XmlDocument();
                layout.LoadXml(view["layoutxml"] as string);
                entity.layoutcolumns = layout.SelectNodes("grid/row/cell")
                    .Cast<XmlNode>()
                    .Select(c => c.Attributes["name"].Value)
                    .ToList();
            }
        }

        private void LoadRecords(QueryBase qry)
        {
            gvRecords.DataSource = null;
            if (qry == null)
            {
                return;
            }
            WorkAsync(new WorkAsyncInfo("Loading records...",
                (eventargs) =>
                {
                    eventargs.Result = Service.RetrieveMultiple(qry);
                })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is EntityCollection entities)
                        {
                            var entity = cmbEntities.Items.Cast<EntityMetadataProxy>().FirstOrDefault(e => e.Metadata.LogicalName == entities.EntityName);
                            gvRecords.DataSource = entities;
                            SortColumns(gvRecords, entity, Service);
                        }
                    }
                }
            });
        }

        internal static void SortColumns(CRMGridView crmgrid, EntityMetadataProxy entity, IOrganizationService service)
        {
            if (string.IsNullOrEmpty(entity.quickfindfetch))
            {
                GetQuickFind(entity, service);
            }
            var pos = 2;
            foreach (var attribute in entity.layoutcolumns.Select(a => a.Trim()).Where(a => !string.IsNullOrWhiteSpace(a)))
            {
                if (crmgrid.Columns.Contains(attribute))
                {
                    var column = crmgrid.Columns[attribute];
                    foreach (var movecolumn in crmgrid.Columns.Cast<DataGridViewColumn>().Where(c => c.DisplayIndex >= pos && c.DisplayIndex < column.DisplayIndex))
                    {
                        movecolumn.DisplayIndex++;
                    }
                    crmgrid.Columns[attribute].DisplayIndex = pos;
                    pos++;
                }
            }
            crmgrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindRecords(textBox1.Text);
        }

        private void crmGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var record = gvRecords.SelectedRowRecords.Entities.Count == 1 ? gvRecords.SelectedRowRecords.Entities[0] : null;
            btnFindRelations.Enabled = record != null;
            txtRecordName.Text = string.Empty;
            txtRecordId.Text = string.Empty;
            if (record != null)
            {
                if (cmbEntities.SelectedItem is EntityMetadataProxy entity)
                {
                    txtRecordName.Text = record.Contains(entity.Metadata.PrimaryNameAttribute) ? record[entity.Metadata.PrimaryNameAttribute] as string : "?";
                }
                txtRecordId.Text = record.Id.ToString();
            }
        }

        private void btnFindRelations_Click(object sender, EventArgs e)
        {
            var record = gvRecords.SelectedRowRecords.Entities[0];
            var entity = cmbEntities.Items.Cast<EntityMetadataProxy>().FirstOrDefault(ent => ent.Metadata.LogicalName == record.LogicalName);
            AddRelatedChildren(record, entity);
        }

        private void AddRelatedChildren(Entity parentrecord, EntityMetadataProxy entity)
        {
            tabControl1.TabPages.Clear();
            var relations = entity.Metadata.OneToManyRelationships.Count();
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading children",
                AsyncArgument = (entity, chkShowHidden.Checked),
                Work = (worker, args) =>
                {
                    if (!(args.Argument is ValueTuple<EntityMetadataProxy, bool> asyncarg))
                    {
                        return;
                    }
                    var asyncentity = asyncarg.Item1;
                    var includehidden = asyncarg.Item2;
                    var allchildren = new List<QueryInfo>();
                    var current = 0;
                    var rels = asyncentity.Metadata.OneToManyRelationships
                        .Where(r => includehidden || r.AssociatedMenuConfiguration.Behavior != AssociatedMenuBehavior.DoNotDisplay)
                        .OrderBy(r => r.AssociatedMenuConfiguration?.Order);
                    var total = rels.Count();
                    foreach (var rel in rels)
                    {
                        current++;
                        if (cmbEntities.Items
                            .Cast<EntityMetadataProxy>()
                            .FirstOrDefault(ent => ent.Metadata.LogicalName == rel.ReferencingEntity) is EntityMetadataProxy childentity)
                        {
                            worker.ReportProgress(0, $"Loading {current}/{total}\r\n{rel.SchemaName}");
                            allchildren.Add(GetRelatedChildren(parentrecord.Id, childentity, entity, rel));
                        }
                    }
                    args.Result = allchildren;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Result is List<QueryInfo> allchildren)
                    {
                        var selectedchildren = allchildren
                            .Where(c => c != null && c.EntityInfo != null && c.Relationship != null && c.Results != null)
                            .Where(c => !chkShowOnlyData.Checked || c.Results.Entities.Count > 0);
                        foreach (var children in selectedchildren)
                        {
                            new RelatedRecordsControl(tabControl1, Service, children);
                        }
                    }
                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                }
            });
        }

        private QueryInfo GetRelatedChildren(Guid parentid, EntityMetadataProxy childmeta, EntityMetadataProxy parententity, OneToManyRelationshipMetadata rel)
        {
            var lookup = rel.ReferencingAttribute;
            try
            {
                var qry = new QueryByAttribute(childmeta.Metadata.LogicalName);
                qry.ColumnSet = new ColumnSet(GetQuickFindQueryColumns(childmeta));
                qry.AddAttributeValue(lookup, parentid);
                qry.AddOrder(childmeta.Metadata.PrimaryNameAttribute, OrderType.Ascending);
                var children = Service.RetrieveMultiple(qry);
                var result = new QueryInfo
                {
                    AttributesSignature = string.Join("\n", qry.ColumnSet.Columns),
                    EntityInfo = childmeta,
                    Query = qry,
                    Results = children,
                    Relationship = rel
                };
                return result;
            }
            catch (Exception ex)
            {
                //ShowErrorNotification($"{childmeta} error: {ex}", null);
                return null;
            }
        }
    }
}