namespace Sandbox.Bot.Forms
{
  partial class SettingsDialog
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
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Conectividade");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Geral", new System.Windows.Forms.TreeNode[] {
            treeNode3});
      this.tvSections = new System.Windows.Forms.TreeView();
      this.btClose = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.txEndpoint = new System.Windows.Forms.TextBox();
      this.btSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tvSections
      // 
      this.tvSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tvSections.Location = new System.Drawing.Point(12, 12);
      this.tvSections.Name = "tvSections";
      treeNode3.Name = "ndConectividade";
      treeNode3.Text = "Conectividade";
      treeNode4.Name = "ndGeneral";
      treeNode4.Text = "Geral";
      this.tvSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
      this.tvSections.Size = new System.Drawing.Size(152, 269);
      this.tvSections.TabIndex = 0;
      this.tvSections.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSections_BeforeCollapse);
      // 
      // btClose
      // 
      this.btClose.Location = new System.Drawing.Point(461, 287);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(75, 23);
      this.btClose.TabIndex = 1;
      this.btClose.Text = "&Sair";
      this.btClose.UseVisualStyleBackColor = true;
      this.btClose.Click += new System.EventHandler(this.btClose_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(170, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(131, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "URL do servidor de dados";
      // 
      // txEndpoint
      // 
      this.txEndpoint.Location = new System.Drawing.Point(173, 28);
      this.txEndpoint.Name = "txEndpoint";
      this.txEndpoint.Size = new System.Drawing.Size(363, 20);
      this.txEndpoint.TabIndex = 3;
      this.txEndpoint.TextChanged += new System.EventHandler(this.txEndpoint_TextChanged);
      // 
      // btSave
      // 
      this.btSave.Enabled = false;
      this.btSave.Location = new System.Drawing.Point(380, 287);
      this.btSave.Name = "btSave";
      this.btSave.Size = new System.Drawing.Size(75, 23);
      this.btSave.TabIndex = 4;
      this.btSave.Text = "&Aplicar";
      this.btSave.UseVisualStyleBackColor = true;
      this.btSave.Click += new System.EventHandler(this.btSave_Click);
      // 
      // SettingsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(548, 322);
      this.Controls.Add(this.btSave);
      this.Controls.Add(this.txEndpoint);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btClose);
      this.Controls.Add(this.tvSections);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SettingsDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Configurações";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsDialog_FormClosing);
      this.Load += new System.EventHandler(this.SettingsDialog_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView tvSections;
    private System.Windows.Forms.Button btClose;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txEndpoint;
    private System.Windows.Forms.Button btSave;
  }
}