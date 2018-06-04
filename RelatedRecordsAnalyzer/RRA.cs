using Cinteros.Xrm.CRMWinForm;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Rappen.XTB.RRA
{
    public partial class RRA : PluginControlBase, IStatusBarMessenger, IGitHubPlugin, IPayPalPlugin, IAboutPlugin
    {
        #region Private Fields

        private const string aiEndpoint = "https://dc.services.visualstudio.com/v2/track";

        //private const string aiKey = "cc7cb081-b489-421d-bb61-2ee53495c336";    // jonas@rappen.net tenant, TestAI
        private const string aiKey = "eed73022-2444-45fd-928b-5eebd8fa46a6";    // jonas@rappen.net tenant, XrmToolBox

        //private const string aiKey = "b6a4ec7c-ab43-4780-97cd-021e99506337";   // jonas@jonasrapp.net, XrmToolBoxInsights

        private AppInsights ai;

        #endregion Private Fields

        #region Public Constructors

        public RRA()
        {
            InitializeComponent();
            ai = new AppInsights(new AiConfig(aiEndpoint, aiKey)
            {
                PluginName = "Related Records Analyzer"
            });
        }

        #endregion Public Constructors

        #region Public Events

        //private Settings mySettings;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        #endregion Public Events

        #region Public Properties

        public string DonationDescription => "Related Records Analyzer Fan Club";
        public string EmailAccount => "jonas@rappen.net";
        public string RepositoryName => "RelatedRecordsAnalyzer";
        public string UserName => "rappen";

        #endregion Public Properties

        #region Public Methods

        public void ShowAboutDialog()
        {
            MessageBox.Show("This is RRA.");
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

        #endregion Public Methods

        #region Internal Methods

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

        #endregion Internal Methods

        #region Private Form Event Handlers

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindRecords(txtSearch.Text);
        }

        private void crmGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var record = gvRecords.SelectedRowRecords.Entities.Count == 1 ? gvRecords.SelectedRowRecords.Entities[0] : null;
            tsbAnalyze.Enabled = record != null;
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

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            ai.WriteEvent("Close");
            // Before leaving, save the settings
            //SettingsManager.Instance.Save(GetType(), mySettings);
        }

        private void RRA_Load(object sender, EventArgs e)
        {
            ai.WriteEvent("Load");
            ShowInfoNotification("Select entity and search for parent record, or paste record url. Double-click record to open in CRM.\r\nVerify options and Analyze relations of selected record. Right-click child record to make it parent for further analysis.", null);
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

        private void tsbAnalyze_Click(object sender, EventArgs e)
        {
            tvChildren.Nodes.Clear();
            foreach (var page in tabControl1.TabPages.Cast<TabPage>().Where(t => t != tabTree))
            {
                tabControl1.TabPages.Remove(page);
            }
            var record = gvRecords.SelectedRowRecords.Entities[0];
            var entity = cmbEntities.Items.Cast<EntityMetadataProxy>().FirstOrDefault(ent => ent.Metadata.LogicalName == record.LogicalName);
            GetRelatedChildren(record, entity);
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            CancelWorker();
            tsbCancel.Enabled = false;
            MessageBox.Show("Cancelled");
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tslByJonas_Click(object sender, EventArgs e)
        {
            Process.Start("http://jonasrapp.net/?src=RRA");
        }

        private void tvChildren_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Name == "dummy" && e.Node.Tag is ValueTuple<QueryInfo, Entity> nodeinfo)
            {
                GetRelatedChildren(nodeinfo.Item2, nodeinfo.Item1.EntityInfo);
            }
        }

        private void tvChildren_DoubleClick(object sender, EventArgs e)
        {
            if (!(tvChildren.SelectedNode is TreeNode node))
            {
                return;
            }
            SelectChildRecordFromTreeNode(node);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            typeTimer.Stop();
            typeTimer.Start();
        }

        private void typeTimer_Tick(object sender, EventArgs e)
        {
            typeTimer.Stop();
            FindRecords(txtSearch.Text);
        }

        #endregion Private Form Event Handlers

        #region Private Methods

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

        private static TreeNode GetEntityNode(QueryInfo child, TreeView tv)
        {
            var node = new TreeNode($"{child.CollectionDisplayName} ({child.EntityInfo.Metadata.LogicalName}): {child.Results.Entities.Count} records")
            {
                Tag = child
            };
            var parentnode = GetParentNode(child, tv.Nodes);
            if (parentnode == null)
            {
                tv.Nodes.Add(node);
            }
            else
            {
                if (parentnode.Nodes.Count == 1 && parentnode.Nodes[0].Name == "dummy")
                {
                    parentnode.Nodes.RemoveAt(0);
                }
                parentnode.Nodes.Add(node);
            }
            return node;
        }

        private static TreeNode GetParentNode(QueryInfo child, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is ValueTuple<QueryInfo, Entity> nodeinfo &&
                    nodeinfo.Item2.LogicalName == child.ParentEntity.LogicalName &&
                    nodeinfo.Item2.Id.Equals(child.ParentEntity.Id))
                {
                    return node;
                }
                else if (GetParentNode(child, node.Nodes) is TreeNode parent)
                {
                    return parent;
                }
            }
            return null;
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

        private static RelatedRecordsControl GetRelatedRecordsControl(TabControl tc, QueryInfo child)
        {
            return tc.TabPages
                .Cast<TabPage>()
                .Where(p => p.Controls.Count == 1
                    && p.Controls[0] is RelatedRecordsControl
                    && p.Tag is QueryInfo qi
                    && qi.Relationship == child.Relationship)
                .Select(p => p.Controls[0] as RelatedRecordsControl)
                .FirstOrDefault();
        }

        private static TabPage GetTabPage(TabControl tc, QueryInfo child)
        {
            return tc.TabPages
                .Cast<TabPage>()
                .Where(p => p.Controls.Count == 1 && p.Controls[0] is RelatedRecordsControl)
                .FirstOrDefault(p => p.Controls[0] is RelatedRecordsControl rrc &&
                    p.Tag is QueryInfo qi && qi.Relationship == child.Relationship);
        }

        private void AddChildEntitiesDetails(QueryInfo child)
        {
            if (GetRelatedRecordsControl(tabControl1, child) is RelatedRecordsControl details)
            {
                details.AddRecords(child);
            }
            else
            {
                new RelatedRecordsControl(tabControl1, Service, child, RecordDoubleClick, RecordSetAsParent);
            }
        }

        private void AddChildEntitiesToTree(QueryInfo child)
        {
            var entitynode = GetEntityNode(child, tvChildren);
            foreach (var record in child.Results.Entities.OrderBy(r => r.Name(child.EntityInfo)))
            {
                var recordnode = new TreeNode(record.Name(child.EntityInfo))
                {
                    Tag = (child, record)
                };
                recordnode.Nodes.Add("dummy", "Loading...");
                entitynode.Nodes.Add(recordnode);
            }
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

        private string GetEntityReferenceUrl(EntityReference entref)
        {
            if (!string.IsNullOrEmpty(entref.LogicalName) && !entref.Id.Equals(Guid.Empty))
            {
                var url = ConnectionDetail.WebApplicationUrl;
                if (string.IsNullOrEmpty(url))
                {
                    url = string.Concat(ConnectionDetail.ServerName, "/", ConnectionDetail.Organization);
                    if (!url.ToLower().StartsWith("http"))
                    {
                        url = string.Concat("http://", url);
                    }
                }
                url = string.Concat(url,
                    url.EndsWith("/") ? "" : "/",
                    "main.aspx?etn=",
                    entref.LogicalName,
                    "&pagetype=entityrecord&id=",
                    entref.Id.ToString());
                return url;
            }
            return string.Empty;
        }

        private string GetEntityUrl(Entity entity)
        {
            var entref = entity.ToEntityReference();
            switch (entref.LogicalName)
            {
                case "activitypointer":
                    if (!entity.Contains("activitytypecode"))
                    {
                        MessageBox.Show("To open records of type activitypointer, attribute 'activitytypecode' must be included in the query.", "Open Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        entref.LogicalName = string.Empty;
                    }
                    else
                    {
                        entref.LogicalName = entity["activitytypecode"].ToString();
                    }
                    break;

                case "activityparty":
                    if (!entity.Contains("partyid"))
                    {
                        MessageBox.Show("To open records of type activityparty, attribute 'partyid' must be included in the query.", "Open Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        entref.LogicalName = string.Empty;
                    }
                    else
                    {
                        var party = (EntityReference)entity["partyid"];
                        entref.LogicalName = party.LogicalName;
                        entref.Id = party.Id;
                    }
                    break;
            }
            return GetEntityReferenceUrl(entref);
        }

        private QueryExpression GetGuidQuery(string id, EntityMetadataProxy entity)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                var qry = new QueryExpression(entity.Metadata.LogicalName);
                qry.Criteria.AddCondition(entity.Metadata.PrimaryIdAttribute, ConditionOperator.Equal, guid);
                qry.ColumnSet.AddColumns(GetQuickFindQueryColumns(entity));
                SetStatus($"Valid guid for {entity}");
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
                .ToList();
            if (!result.Contains(entity.Metadata.PrimaryNameAttribute))
            {
                result.Add(entity.Metadata.PrimaryNameAttribute);
            }
            return result.ToArray();
        }

        private void GetRelatedChildren(Entity parentrecord, EntityMetadataProxy entity)
        {
            ai.WriteEvent("Analyze");
            tsbAnalyze.Enabled = false;
            tsbCancel.Enabled = true;
            gvRecords.Enabled = false;
            var relations = entity.Metadata.OneToManyRelationships.Count();
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading children",
                IsCancelable = true,
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
                    var rels = asyncentity.Metadata.OneToManyRelationships
                        .Where(r => includehidden || r.AssociatedMenuConfiguration.Behavior != AssociatedMenuBehavior.DoNotDisplay)
                        .OrderBy(r => r.AssociatedMenuConfiguration?.Order);
                    var current = 0;
                    var total = rels.Count();
                    foreach (var rel in rels)
                    {
                        if (worker.CancellationPending)
                        {
                            args.Cancel = true;
                            break;
                        }
                        current++;
                        if (cmbEntities.Items
                            .Cast<EntityMetadataProxy>()
                            .FirstOrDefault(ent => ent.Metadata.LogicalName == rel.ReferencingEntity) is EntityMetadataProxy childentity)
                        {
                            worker.ReportProgress(current * 100 / total, $"Loading {current}/{total}\r\n{rel.SchemaName}");
                            allchildren.Add(LoadRelatedChildren(parentrecord, childentity, entity, rel));
                        }
                    }
                    args.Result = allchildren;
                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(args.ProgressPercentage, args.UserState.ToString().Replace("\r\n", " ")));
                },
                PostWorkCallBack = (args) =>
                {
                    tsbCancel.Enabled = false;
                    if (!args.Cancelled && args.Result is List<QueryInfo> allchildren)
                    {
                        RenderChildren(allchildren);
                    }
                    gvRecords.Enabled = true;
                    tsbAnalyze.Enabled = true;
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(null, string.Empty));
                }
            });
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
                SetStatus($"Valid url for {entity.Item1}");
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

        private QueryInfo LoadRelatedChildren(Entity parent, EntityMetadataProxy childmeta, EntityMetadataProxy parententity, OneToManyRelationshipMetadata rel)
        {
            var lookup = rel.ReferencingAttribute;
            try
            {
                var qry = new QueryByAttribute(childmeta.Metadata.LogicalName);
                qry.ColumnSet = new ColumnSet(GetQuickFindQueryColumns(childmeta));
                qry.AddAttributeValue(lookup, parent.Id);
                qry.AddOrder(childmeta.Metadata.PrimaryNameAttribute, OrderType.Ascending);
                var children = Service.RetrieveMultiple(qry);
                var result = new QueryInfo
                {
                    ParentEntity = parent,
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

        private void PopulateEntities(EntityMetadataProxy[] entities)
        {
            cmbEntities.Items.Clear();
            cmbEntities.Items.AddRange(entities);
        }

        private void RecordDoubleClick(object sender, CRMRecordEventArgs eventargs)
        {
            if (eventargs.Entity != null)
            {
                string url = GetEntityUrl(eventargs.Entity);
                if (!string.IsNullOrEmpty(url))
                {
                    ai.WriteEvent("OpenRecord");
                    Process.Start(url);
                }
            }
        }

        private void RecordSetAsParent(object sender, CRMRecordEventArgs eventargs)
        {
            if (!(eventargs.Entity is Entity selectedentity) ||
                !(cmbEntities.Items
                    .Cast<EntityMetadataProxy>()
                    .FirstOrDefault(e => e.Metadata.LogicalName == selectedentity.LogicalName) is EntityMetadataProxy entity))
            {
                return;
            }
            txtSearch.Text = selectedentity.Id.ToString();
            typeTimer.Enabled = false;
            if (cmbEntities.SelectedItem == entity)
            {
                FindRecords(txtSearch.Text);
            }
            else
            {
                cmbEntities.SelectedItem = entity;
            }
        }

        private void RenderChildren(List<QueryInfo> allchildren)
        {
            tsbAnalyze.Enabled = false;
            tsbCancel.Enabled = true;
            gvRecords.Enabled = false;
            if (tvChildren.SelectedNode?.Nodes?.Count == 1 && tvChildren.SelectedNode.Nodes[0].Name == "dummy")
            {
                tvChildren.SelectedNode.Nodes.RemoveAt(0);
            }
            //tvChildren.Nodes.Clear();
            var selectedchildren = allchildren
                .Where(c => c != null && c.EntityInfo != null && c.Relationship != null && c.Results != null)
                .Where(c => !chkShowOnlyData.Checked || c.Results.Entities.Count > 0);
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Rendering children",
                IsCancelable = true,
                AsyncArgument = (selectedchildren, tabControl1),
                Work = (worker, args) =>
                {
                    if (!(args.Argument is ValueTuple<IEnumerable<QueryInfo>, TabControl> asyncarg))
                    {
                        return;
                    }
                    var children = asyncarg.Item1;
                    var tc = asyncarg.Item2;
                    var current = 0;
                    var total = selectedchildren.Count();
                    foreach (var child in children)
                    {
                        if (worker.CancellationPending)
                        {
                            args.Cancel = true;
                            break;
                        }
                        worker.ReportProgress(current * 100 / total, $"Rendering {child.EntityInfo}");
                        MethodInvoker mi = delegate
                        {
                            AddChildEntitiesToTree(child);
                            AddChildEntitiesDetails(child);
                        };
                        Invoke(mi);
                    }
                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState?.ToString());
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(args.ProgressPercentage, args.UserState.ToString().Replace("\r\n", " ")));
                },
                PostWorkCallBack = (args) =>
                {
                    tsbCancel.Enabled = false;
                    gvRecords.Enabled = true;
                    tsbAnalyze.Enabled = true;
                    SendMessageToStatusBar(this, new StatusBarMessageEventArgs(null, string.Empty));
                }
            });
        }

        private QueryBase ReplaceQuickFindPlaceholders(EntityMetadataProxy entity, string find)
        {
            var fx = new XmlDocument();
            fx.LoadXml(entity.quickfindfetch);
            if (fx.SelectSingleNode("fetch/entity") is XmlNode entitynode)
            {
                FixEntity(entity, entitynode, find);
            }
            SetStatus($"Composed quick find query for {entity}");
            return new FetchExpression(fx.OuterXml);
        }

        private void SelectChildRecordFromTreeNode(TreeNode node)
        {
            var qi = node.Tag is QueryInfo tag1 ? tag1 : node.Tag is ValueTuple<QueryInfo, Entity> tuple1 ? tuple1.Item1 : null;
            if (qi == null)
            {
                return;
            }
            var page = GetTabPage(tabControl1, qi);
            if (page == null)
            {
                return;
            }
            tabControl1.SelectedTab = page;

            var entity = node.Tag is ValueTuple<QueryInfo, Entity> tuple2 ? tuple2.Item2 : null;
            if (entity == null)
            {
                return;
            }
            if (page.Controls.Cast<Control>().FirstOrDefault(c => c is RelatedRecordsControl) is RelatedRecordsControl rrc &&
                rrc.Controls.Cast<Control>().FirstOrDefault(c => c is CRMGridView) is CRMGridView grid &&
                grid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells["#entity"].Value == entity) is DataGridViewRow selectrow)
            {
                foreach (var row in grid.Rows.Cast<DataGridViewRow>())
                {
                    row.Selected = row == selectrow;
                }
            }
        }

        private void SetStatus(string status)
        {
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs(status));
        }

        #endregion Private Methods
    }
}