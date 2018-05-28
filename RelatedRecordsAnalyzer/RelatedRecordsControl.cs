using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Windows.Forms;

namespace Rappen.XTB.RRA
{
    public partial class RelatedRecordsControl : UserControl
    {
        public RelatedRecordsControl(TabControl parent, IOrganizationService service, EntityMetadataProxy entity, OneToManyRelationshipMetadata relationship, EntityCollection entities)
        {
            InitializeComponent();
            var related = entity.Metadata.DisplayCollectionName.UserLocalizedLabel.Label;
            switch (relationship.AssociatedMenuConfiguration.Behavior)
            {
                case AssociatedMenuBehavior.DoNotDisplay:
                    related += " (hidden)";
                    break;
                case AssociatedMenuBehavior.UseLabel:
                    related = relationship.AssociatedMenuConfiguration.Label.UserLocalizedLabel.Label;
                    break;
            }
            txtEntity.Text = related;
            txtRelation.Text = relationship.SchemaName;
            txtCount.Text = entities.Entities.Count.ToString();
            txtCascadeAssign.Text = relationship.CascadeConfiguration.Assign?.ToString();
            txtCascadeShare.Text = relationship.CascadeConfiguration.Share?.ToString();
            txtCascadeUnshare.Text = relationship.CascadeConfiguration.Unshare?.ToString();
            txtCascadeReparent.Text = relationship.CascadeConfiguration.Reparent?.ToString();
            txtCascadeDelete.Text = relationship.CascadeConfiguration.Delete?.ToString();
            txtCascadeMerge.Text = relationship.CascadeConfiguration.Merge?.ToString();
            txtCascadeRollup.Text = relationship.CascadeConfiguration.RollupView?.ToString();
            crmGridView1.OrganizationService = service;
            crmGridView1.DataSource = entities;
            var tp = new TabPage(related);
            tp.Name = relationship?.SchemaName;
            tp.Controls.Add(this);
            Dock = DockStyle.Fill;
            parent.TabPages.Add(tp);
        }
    }
}
