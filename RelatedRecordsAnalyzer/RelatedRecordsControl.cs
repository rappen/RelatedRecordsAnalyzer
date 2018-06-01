using Cinteros.Xrm.CRMWinForm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Windows.Forms;

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
            var related = child.EntityInfo.Metadata.DisplayCollectionName.UserLocalizedLabel.Label;
            if (child.Relationship is OneToManyRelationshipMetadata rel1m)
            {
                switch (rel1m.AssociatedMenuConfiguration.Behavior)
                {
                    case AssociatedMenuBehavior.DoNotDisplay:
                        related += " (hidden)";
                        break;
                    case AssociatedMenuBehavior.UseLabel:
                        related = rel1m.AssociatedMenuConfiguration.Label.UserLocalizedLabel.Label;
                        break;
                }
                txtCascadeAssign.Text = rel1m.CascadeConfiguration.Assign?.ToString();
                txtCascadeShare.Text = rel1m.CascadeConfiguration.Share?.ToString();
                txtCascadeUnshare.Text = rel1m.CascadeConfiguration.Unshare?.ToString();
                txtCascadeReparent.Text = rel1m.CascadeConfiguration.Reparent?.ToString();
                txtCascadeDelete.Text = rel1m.CascadeConfiguration.Delete?.ToString();
                txtCascadeMerge.Text = rel1m.CascadeConfiguration.Merge?.ToString();
                txtCascadeRollup.Text = rel1m.CascadeConfiguration.RollupView?.ToString();
            }
            txtEntity.Text = related;
            txtRelation.Text = child.Relationship.SchemaName;
            txtCount.Text = child.Results.Entities.Count.ToString();
            var tp = new TabPage(related);
            tp.Tag = child;
            tp.Name = child.Relationship.SchemaName;
            tp.Controls.Add(this);
            Dock = DockStyle.Fill;
            parent.TabPages.Add(tp);
            gvChildren.OrganizationService = service;
            gvChildren.DataSource = child.Results;
            RRA.SortColumns(gvChildren, child.EntityInfo, service);
        }

        private void ctxMenuSelectAsParent_Click(object sender, System.EventArgs e) 
            => SelectAsParent(sender, new CRMRecordEventArgs(-1, -1, gvChildren.SelectedCellRecords?.Entities?.FirstOrDefault(), string.Empty));
    }
}
