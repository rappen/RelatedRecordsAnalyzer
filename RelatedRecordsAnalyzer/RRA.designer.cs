namespace Rappen.XTB.RRA
{
    partial class RRA
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RRA));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAnalyze = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvRecords = new Cinteros.Xrm.CRMWinForm.CRMGridView();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEntities = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtRecordId = new System.Windows.Forms.TextBox();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkShowMM = new System.Windows.Forms.CheckBox();
            this.chkShowOnlyData = new System.Windows.Forms.CheckBox();
            this.chkShowHidden = new System.Windows.Forms.CheckBox();
            this.typeTimer = new System.Windows.Forms.Timer(this.components);
            this.tabTree = new System.Windows.Forms.TabPage();
            this.tvChildren = new System.Windows.Forms.TreeView();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecords)).BeginInit();
            this.gbSearch.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbAnalyze,
            this.tsbCancel});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(886, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(64, 28);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbAnalyze
            // 
            this.tsbAnalyze.Enabled = false;
            this.tsbAnalyze.Image = ((System.Drawing.Image)(resources.GetObject("tsbAnalyze.Image")));
            this.tsbAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnalyze.Name = "tsbAnalyze";
            this.tsbAnalyze.Size = new System.Drawing.Size(127, 28);
            this.tsbAnalyze.Text = "Analyze Relations";
            this.tsbAnalyze.Click += new System.EventHandler(this.tsbAnalyze_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(71, 28);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.gbSearch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(886, 517);
            this.splitContainer1.SplitterDistance = 311;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvRecords);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 438);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Records";
            // 
            // gvRecords
            // 
            this.gvRecords.AllowUserToAddRows = false;
            this.gvRecords.AllowUserToDeleteRows = false;
            this.gvRecords.AllowUserToOrderColumns = true;
            this.gvRecords.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvRecords.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvRecords.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvRecords.EnableHeadersVisualStyles = false;
            this.gvRecords.Location = new System.Drawing.Point(3, 16);
            this.gvRecords.Name = "gvRecords";
            this.gvRecords.ReadOnly = true;
            this.gvRecords.RowHeadersVisible = false;
            this.gvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRecords.ShowFriendlyNames = true;
            this.gvRecords.ShowIdColumn = false;
            this.gvRecords.ShowIndexColumn = false;
            this.gvRecords.ShowLocalTimes = true;
            this.gvRecords.Size = new System.Drawing.Size(305, 419);
            this.gvRecords.TabIndex = 0;
            this.gvRecords.RecordDoubleClick += new Cinteros.Xrm.CRMWinForm.CRMRecordEventHandler(this.RecordDoubleClick);
            this.gvRecords.SelectionChanged += new System.EventHandler(this.crmGridView1_SelectionChanged);
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.txtSearch);
            this.gbSearch.Controls.Add(this.label2);
            this.gbSearch.Controls.Add(this.label1);
            this.gbSearch.Controls.Add(this.cmbEntities);
            this.gbSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSearch.Location = new System.Drawing.Point(0, 0);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(311, 79);
            this.gbSearch.TabIndex = 0;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Find record";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(78, 44);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(227, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Url / Search";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entity";
            // 
            // cmbEntities
            // 
            this.cmbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntities.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEntities.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(78, 17);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(227, 21);
            this.cmbEntities.Sorted = true;
            this.cmbEntities.TabIndex = 0;
            this.cmbEntities.SelectedIndexChanged += new System.EventHandler(this.cmbEntities_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTree);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(567, 434);
            this.tabControl1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 4);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 79);
            this.panel2.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtRecordId);
            this.groupBox4.Controls.Add(this.txtRecordName);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 79);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selected record";
            // 
            // txtRecordId
            // 
            this.txtRecordId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordId.BackColor = System.Drawing.SystemColors.Window;
            this.txtRecordId.Location = new System.Drawing.Point(78, 44);
            this.txtRecordId.Name = "txtRecordId";
            this.txtRecordId.ReadOnly = true;
            this.txtRecordId.Size = new System.Drawing.Size(183, 20);
            this.txtRecordId.TabIndex = 3;
            // 
            // txtRecordName
            // 
            this.txtRecordName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordName.BackColor = System.Drawing.SystemColors.Window;
            this.txtRecordName.Location = new System.Drawing.Point(78, 17);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.ReadOnly = true;
            this.txtRecordName.Size = new System.Drawing.Size(183, 20);
            this.txtRecordName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(267, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(6, 79);
            this.panel3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkShowMM);
            this.groupBox3.Controls.Add(this.chkShowOnlyData);
            this.groupBox3.Controls.Add(this.chkShowHidden);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(273, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 79);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkShowMM
            // 
            this.chkShowMM.AutoSize = true;
            this.chkShowMM.Enabled = false;
            this.chkShowMM.Location = new System.Drawing.Point(191, 19);
            this.chkShowMM.Name = "chkShowMM";
            this.chkShowMM.Size = new System.Drawing.Size(89, 17);
            this.chkShowMM.TabIndex = 3;
            this.chkShowMM.Text = "M:M relations";
            this.chkShowMM.UseVisualStyleBackColor = true;
            // 
            // chkShowOnlyData
            // 
            this.chkShowOnlyData.AutoSize = true;
            this.chkShowOnlyData.Checked = true;
            this.chkShowOnlyData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowOnlyData.Location = new System.Drawing.Point(16, 46);
            this.chkShowOnlyData.Name = "chkShowOnlyData";
            this.chkShowOnlyData.Size = new System.Drawing.Size(163, 17);
            this.chkShowOnlyData.TabIndex = 2;
            this.chkShowOnlyData.Text = "Only show relations with data";
            this.chkShowOnlyData.UseVisualStyleBackColor = true;
            // 
            // chkShowHidden
            // 
            this.chkShowHidden.AutoSize = true;
            this.chkShowHidden.Checked = true;
            this.chkShowHidden.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowHidden.Location = new System.Drawing.Point(16, 19);
            this.chkShowHidden.Name = "chkShowHidden";
            this.chkShowHidden.Size = new System.Drawing.Size(130, 17);
            this.chkShowHidden.TabIndex = 1;
            this.chkShowHidden.Text = "Show hidden relations";
            this.chkShowHidden.UseVisualStyleBackColor = true;
            // 
            // typeTimer
            // 
            this.typeTimer.Interval = 500;
            this.typeTimer.Tick += new System.EventHandler(this.typeTimer_Tick);
            // 
            // tabTree
            // 
            this.tabTree.Controls.Add(this.tvChildren);
            this.tabTree.Location = new System.Drawing.Point(4, 22);
            this.tabTree.Name = "tabTree";
            this.tabTree.Size = new System.Drawing.Size(559, 408);
            this.tabTree.TabIndex = 0;
            this.tabTree.Text = "Hierarchy";
            this.tabTree.UseVisualStyleBackColor = true;
            // 
            // tvChildren
            // 
            this.tvChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvChildren.Location = new System.Drawing.Point(0, 0);
            this.tvChildren.Name = "tvChildren";
            this.tvChildren.Size = new System.Drawing.Size(559, 408);
            this.tvChildren.TabIndex = 0;
            this.tvChildren.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvChildren_BeforeExpand);
            this.tvChildren.DoubleClick += new System.EventHandler(this.tvChildren_DoubleClick);
            // 
            // RRA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "RRA";
            this.Size = new System.Drawing.Size(886, 548);
            this.Load += new System.EventHandler(this.RRA_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvRecords)).EndInit();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEntities;
        private Cinteros.Xrm.CRMWinForm.CRMGridView gvRecords;
        private System.Windows.Forms.Timer typeTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox chkShowOnlyData;
        private System.Windows.Forms.CheckBox chkShowHidden;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtRecordId;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripButton tsbAnalyze;
        private System.Windows.Forms.CheckBox chkShowMM;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.TabPage tabTree;
        private System.Windows.Forms.TreeView tvChildren;
    }
}
