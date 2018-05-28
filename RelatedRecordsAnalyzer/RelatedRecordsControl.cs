using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Windows.Forms;

namespace Rappen.XTB.RRA
{
    public partial class RelatedRecordsControl : UserControl
    {
        public RelatedRecordsControl(TabControl parent, IOrganizationService service, QueryInfo qi)
        {
            InitializeComponent();
            var related = qi.EntityInfo.Metadata.DisplayCollectionName.UserLocalizedLabel.Label;
            if (qi.Relationship is OneToManyRelationshipMetadata rel1m)
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
            txtRelation.Text = qi.Relationship.SchemaName;
            txtCount.Text = qi.Results.Entities.Count.ToString();
            var tp = new TabPage(related);
            tp.Name = qi.Relationship.SchemaName;
            tp.Controls.Add(this);
            Dock = DockStyle.Fill;
            parent.TabPages.Add(tp);
            gvChildren.OrganizationService = service;
            gvChildren.DataSource = qi.Results;
            RRA.SortColumns(gvChildren, qi.EntityInfo, service);
            //gvChildren.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}
