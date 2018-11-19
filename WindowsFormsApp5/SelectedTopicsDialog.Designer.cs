using WindowsFormsControlLibrary1;

namespace WindowsFormsApp5
{
    partial class SelectedTopicsDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.changeDetector1 = new WindowsFormsControlLibrary1.ChangeDetector(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.topicsListView = new WindowsFormsControlLibrary1.SmartListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toggleButton = new System.Windows.Forms.Button();
            this.uncheckAllButton = new System.Windows.Forms.Button();
            this.checkAllButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.uncheckButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(455, 329);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(536, 329);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Location = new System.Drawing.Point(374, 329);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 8;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            // 
            // changeDetector1
            // 
            this.changeDetector1.DialogResult = System.Windows.Forms.DialogResult.None;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.topicsListView);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 282);
            this.panel1.TabIndex = 1;
            // 
            // topicsListView
            // 
            this.topicsListView.AllowColumnReorder = true;
            this.topicsListView.CheckBoxes = true;
            this.topicsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.topicsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topicsListView.FullRowSelect = true;
            this.topicsListView.Location = new System.Drawing.Point(0, 0);
            this.topicsListView.Name = "topicsListView";
            this.topicsListView.Size = new System.Drawing.Size(599, 282);
            this.topicsListView.TabIndex = 0;
            this.topicsListView.UseCompatibleStateImageBehavior = false;
            this.topicsListView.View = System.Windows.Forms.View.Details;
            this.topicsListView.SelectedIndexChanged += new System.EventHandler(this.topicsListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Filename";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Folder";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Path";
            this.columnHeader4.Width = 415;
            // 
            // toggleButton
            // 
            this.toggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleButton.Location = new System.Drawing.Point(535, 300);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(75, 23);
            this.toggleButton.TabIndex = 7;
            this.toggleButton.Text = "Toggle";
            this.toggleButton.UseVisualStyleBackColor = true;
            // 
            // uncheckAllButton
            // 
            this.uncheckAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uncheckAllButton.Location = new System.Drawing.Point(292, 300);
            this.uncheckAllButton.Name = "uncheckAllButton";
            this.uncheckAllButton.Size = new System.Drawing.Size(78, 23);
            this.uncheckAllButton.TabIndex = 4;
            this.uncheckAllButton.Text = "Uncheck all";
            this.uncheckAllButton.UseVisualStyleBackColor = true;
            // 
            // checkAllButton
            // 
            this.checkAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkAllButton.Location = new System.Drawing.Point(211, 300);
            this.checkAllButton.Name = "checkAllButton";
            this.checkAllButton.Size = new System.Drawing.Size(78, 23);
            this.checkAllButton.TabIndex = 3;
            this.checkAllButton.Text = "Check all";
            this.checkAllButton.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(12, 300);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Topics...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkButton
            // 
            this.checkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkButton.Location = new System.Drawing.Point(373, 300);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(78, 23);
            this.checkButton.TabIndex = 5;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            // 
            // uncheckButton
            // 
            this.uncheckButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uncheckButton.Location = new System.Drawing.Point(454, 300);
            this.uncheckButton.Name = "uncheckButton";
            this.uncheckButton.Size = new System.Drawing.Size(78, 23);
            this.uncheckButton.TabIndex = 6;
            this.uncheckButton.Text = "Uncheck";
            this.uncheckButton.UseVisualStyleBackColor = true;
            // 
            // SelectedTopicsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(623, 364);
            this.Controls.Add(this.uncheckButton);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.uncheckAllButton);
            this.Controls.Add(this.checkAllButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectedTopicsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select topics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectedTopicsDialog_FormClosing);
            this.Load += new System.EventHandler(this.SelectedTopicsDialog_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button helpButton;
        private WindowsFormsControlLibrary1.ChangeDetector changeDetector1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.Button uncheckAllButton;
        private System.Windows.Forms.Button checkAllButton;
        private SmartListView topicsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Button uncheckButton;
    }
}

