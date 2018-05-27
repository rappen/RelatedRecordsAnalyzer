using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Drawing;
using System.Windows.Forms;

namespace Rappen.XTB.RRA
{
    public partial class RelatedRecordsControl : UserControl
    {
        public RelatedRecordsControl(Panel parent, IOrganizationService service, EntityMetadataProxy entity, OneToManyRelationshipMetadata relationship, EntityCollection entities)
        {
            InitializeComponent();
            gb.Text = $"{entity} {relationship.SchemaName} ({parent.Controls.Count})";
            crmGridView1.OrganizationService = service;
            crmGridView1.DataSource = entities;
            foreach (Control control in parent.Controls)
            {
                //if (control.Dock == DockStyle.Fill)
                {
                    control.Dock = DockStyle.Top;
                    control.SendToBack();
                }
            }
            if (parent.Controls.Count > 0)
            {
                var splitter = new Splitter
                {
                    Dock = DockStyle.Top,
                    BackColor = Color.Red
                };
                parent.Controls.Add(splitter);
                splitter.BringToFront();
            }
            parent.Controls.Add(this);
            Dock = DockStyle.Fill;
            BringToFront();
        }
    }
}
