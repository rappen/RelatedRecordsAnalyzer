using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Windows.Forms;
using xrmtb.XrmToolBox.Controls;

namespace Rappen.XTB.RRA
{
    public partial class RelatedRecordsControl : UserControl
    {
        private CRMRecordEventHandler SelectAsParent;

        public RelatedRecordsControl(TabControl parent, IOrganizationService service, QueryInfo child, CRMRecordEventHandler recorddoubleclick, CRMRecordEventHandler recordselectasparent)
        {
            InitializeComponent();
            gvChildren.RecordDoubleClick += recorddoubleclick;
            SelectAsParent = recordselectasparent;
            if (child.Relationship is OneToManyRelationshipMetadata rel1m)
            {
                txtCascadeAssign.Text = rel1m.CascadeConfiguration.Assign?.ToString();
                txtCascadeShare.Text = rel1m.CascadeConfiguration.Share?.ToString();
                txtCascadeUnshare.Text = rel1m.CascadeConfiguration.Unshare?.ToString();
                txtCascadeReparent.Text = rel1m.CascadeConfiguration.Reparent?.ToString();
                txtCascadeDelete.Text = rel1m.CascadeConfiguration.Delete?.ToString();
                pan1Mrel.Visible = true;
                panMMrel.Visible = false;
            }
            if (child.Relationship is ManyToManyRelationshipMetadata relmm)
            {
                txtMMEntity1.Text = relmm.Entity1LogicalName;
                txtMMAttribute1.Text = relmm.Entity1IntersectAttribute;
                txtMMEntity2.Text = relmm.Entity2LogicalName;
                txtMMAttribute2.Text = relmm.Entity2IntersectAttribute;
                pan1Mrel.Visible = false;
                panMMrel.Visible = true;
            }
            txtEntity.Text = child.CollectionDisplayName;
            txtRelation.Text = child.Relationship.SchemaName;
            txtCount.Text = child.Results.Entities.Count.ToString();
            var tp = new TabPage(child.CollectionDisplayName + (child.Relationship is ManyToManyRelationshipMetadata ? " M:M" : ""))
            {
                Tag = child,
                Name = child.Relationship.SchemaName
            };
            tp.Controls.Add(this);
            Dock = DockStyle.Fill;
            parent.TabPages.Add(tp);
            gvChildren.OrganizationService = service;
            gvChildren.DataSource = child.Results;
            RRA.SortColumns(gvChildren, child.EntityInfo, gvChildren.OrganizationService);
        }

        public void AddRecords(QueryInfo child)
        {
            if (!(gvChildren.GetDataSource<EntityCollection>() is EntityCollection entities))
            {
                return;
            }
            entities.Merge(child.Results);
            txtCount.Text = entities.Entities.Count.ToString();
            gvChildren.Refresh();
            RRA.SortColumns(gvChildren, child.EntityInfo, gvChildren.OrganizationService);
        }

        private void ctxMenuSelectAsParent_Click(object sender, System.EventArgs e)
            => SelectAsParent(sender, new CRMRecordEventArgs(-1, -1, gvChildren.SelectedCellRecords?.Entities?.FirstOrDefault(), string.Empty));
    }
}
