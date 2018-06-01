namespace Rappen.XTB.RRA
{
    partial class RelatedRecordsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvChildren = new Cinteros.Xrm.CRMWinForm.CRMGridView();
            this.ctxMenuChildren = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuSelectAsParent = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCascadeRollup = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCascadeMerge = new System.Windows.Forms.TextBox();
            this.txtCascadeDelete = new System.Windows.Forms.TextBox();
            this.txtCascadeReparent = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCascadeUnshare = new System.Windows.Forms.TextBox();
            this.txtCascadeShare = new System.Windows.Forms.TextBox();
            this.txtCascadeAssign = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRelation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvChildren)).BeginInit();
            this.ctxMenuChildren.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvChildren
            // 
            this.gvChildren.AllowUserToAddRows = false;
            this.gvChildren.AllowUserToDeleteRows = false;
            this.gvChildren.AllowUserToOrderColumns = true;
            this.gvChildren.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gvChildren.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvChildren.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvChildren.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvChildren.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvChildren.ContextMenuStrip = this.ctxMenuChildren;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvChildren.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvChildren.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvChildren.EnableHeadersVisualStyles = false;
            this.gvChildren.Location = new System.Drawing.Point(0, 124);
            this.gvChildren.Name = "gvChildren";
            this.gvChildren.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvChildren.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvChildren.RowHeadersVisible = false;
            this.gvChildren.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvChildren.ShowFriendlyNames = true;
            this.gvChildren.ShowIdColumn = false;
            this.gvChildren.ShowLocalTimes = true;
            this.gvChildren.Size = new System.Drawing.Size(646, 263);
            this.gvChildren.TabIndex = 0;
            // 
            // ctxMenuChildren
            // 
            this.ctxMenuChildren.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuSelectAsParent});
            this.ctxMenuChildren.Name = "ctxMenuChildren";
            this.ctxMenuChildren.Size = new System.Drawing.Size(194, 26);
            // 
            // ctxMenuSelectAsParent
            // 
            this.ctxMenuSelectAsParent.Name = "ctxMenuSelectAsParent";
            this.ctxMenuSelectAsParent.Size = new System.Drawing.Size(193, 22);
            this.ctxMenuSelectAsParent.Text = "Select as parent record";
            this.ctxMenuSelectAsParent.Click += new System.EventHandler(this.ctxMenuSelectAsParent_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtCascadeRollup);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtCascadeMerge);
            this.panel1.Controls.Add(this.txtCascadeDelete);
            this.panel1.Controls.Add(this.txtCascadeReparent);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCascadeUnshare);
            this.panel1.Controls.Add(this.txtCascadeShare);
            this.panel1.Controls.Add(this.txtCascadeAssign);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtRelation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtEntity);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 124);
            this.panel1.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(78, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Entity Info";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(464, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Rollup Vw";
            // 
            // txtCascadeRollup
            // 
            this.txtCascadeRollup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeRollup.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeRollup.Location = new System.Drawing.Point(520, 88);
            this.txtCascadeRollup.Name = "txtCascadeRollup";
            this.txtCascadeRollup.ReadOnly = true;
            this.txtCascadeRollup.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeRollup.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(464, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Merge";
            // 
            // txtCascadeMerge
            // 
            this.txtCascadeMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeMerge.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeMerge.Location = new System.Drawing.Point(520, 62);
            this.txtCascadeMerge.Name = "txtCascadeMerge";
            this.txtCascadeMerge.ReadOnly = true;
            this.txtCascadeMerge.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeMerge.TabIndex = 19;
            // 
            // txtCascadeDelete
            // 
            this.txtCascadeDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeDelete.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeDelete.Location = new System.Drawing.Point(520, 36);
            this.txtCascadeDelete.Name = "txtCascadeDelete";
            this.txtCascadeDelete.ReadOnly = true;
            this.txtCascadeDelete.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeDelete.TabIndex = 18;
            // 
            // txtCascadeReparent
            // 
            this.txtCascadeReparent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeReparent.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeReparent.Location = new System.Drawing.Point(520, 10);
            this.txtCascadeReparent.Name = "txtCascadeReparent";
            this.txtCascadeReparent.ReadOnly = true;
            this.txtCascadeReparent.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeReparent.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(464, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Delete";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(464, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Reparent";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(293, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Unshare";
            // 
            // txtCascadeUnshare
            // 
            this.txtCascadeUnshare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeUnshare.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeUnshare.Location = new System.Drawing.Point(349, 88);
            this.txtCascadeUnshare.Name = "txtCascadeUnshare";
            this.txtCascadeUnshare.ReadOnly = true;
            this.txtCascadeUnshare.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeUnshare.TabIndex = 13;
            // 
            // txtCascadeShare
            // 
            this.txtCascadeShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeShare.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeShare.Location = new System.Drawing.Point(349, 62);
            this.txtCascadeShare.Name = "txtCascadeShare";
            this.txtCascadeShare.ReadOnly = true;
            this.txtCascadeShare.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeShare.TabIndex = 12;
            // 
            // txtCascadeAssign
            // 
            this.txtCascadeAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCascadeAssign.BackColor = System.Drawing.SystemColors.Window;
            this.txtCascadeAssign.Location = new System.Drawing.Point(349, 36);
            this.txtCascadeAssign.Name = "txtCascadeAssign";
            this.txtCascadeAssign.ReadOnly = true;
            this.txtCascadeAssign.Size = new System.Drawing.Size(100, 20);
            this.txtCascadeAssign.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Share";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Assign";
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtCount.Location = new System.Drawing.Point(81, 88);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(195, 20);
            this.txtCount.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Records";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(346, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cascade";
            // 
            // txtRelation
            // 
            this.txtRelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRelation.BackColor = System.Drawing.SystemColors.Window;
            this.txtRelation.Location = new System.Drawing.Point(81, 62);
            this.txtRelation.Name = "txtRelation";
            this.txtRelation.ReadOnly = true;
            this.txtRelation.Size = new System.Drawing.Size(195, 20);
            this.txtRelation.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Relationship";
            // 
            // txtEntity
            // 
            this.txtEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntity.BackColor = System.Drawing.SystemColors.Window;
            this.txtEntity.Location = new System.Drawing.Point(81, 36);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.ReadOnly = true;
            this.txtEntity.Size = new System.Drawing.Size(195, 20);
            this.txtEntity.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity";
            // 
            // RelatedRecordsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.gvChildren);
            this.Controls.Add(this.panel1);
            this.Name = "RelatedRecordsControl";
            this.Size = new System.Drawing.Size(646, 387);
            ((System.ComponentModel.ISupportInitialize)(this.gvChildren)).EndInit();
            this.ctxMenuChildren.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Cinteros.Xrm.CRMWinForm.CRMGridView gvChildren;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRelation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEntity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCascadeRollup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCascadeMerge;
        private System.Windows.Forms.TextBox txtCascadeDelete;
        private System.Windows.Forms.TextBox txtCascadeReparent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCascadeUnshare;
        private System.Windows.Forms.TextBox txtCascadeShare;
        private System.Windows.Forms.TextBox txtCascadeAssign;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip ctxMenuChildren;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuSelectAsParent;
    }
}
