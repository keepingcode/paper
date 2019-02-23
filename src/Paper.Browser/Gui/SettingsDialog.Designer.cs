namespace Paper.Browser.Gui
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
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Conectividade");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Geral", new System.Windows.Forms.TreeNode[] {
            treeNode5});
      this.tvSections = new System.Windows.Forms.TreeView();
      this.btClose = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.txHost = new System.Windows.Forms.TextBox();
      this.btSave = new System.Windows.Forms.Button();
      this.txPort = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // tvSections
      // 
      this.tvSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tvSections.Location = new System.Drawing.Point(12, 12);
      this.tvSections.Name = "tvSections";
      treeNode5.Name = "ndConectividade";
      treeNode5.Text = "Conectividade";
      treeNode6.Name = "ndGeneral";
      treeNode6.Text = "Geral";
      this.tvSections.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
      this.tvSections.Size = new System.Drawing.Size(152, 269);
      this.tvSections.TabIndex = 0;
      this.tvSections.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSections_BeforeCollapse);
      // 
      // btClose
      // 
      this.btClose.Location = new System.Drawing.Point(461, 287);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(75, 23);
      this.btClose.TabIndex = 6;
      this.btClose.Text = "&Sair";
      this.btClose.UseVisualStyleBackColor = true;
      this.btClose.Click += new System.EventHandler(this.btClose_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(170, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(120, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Nome ou IP do Servidor";
      // 
      // txHost
      // 
      this.txHost.Location = new System.Drawing.Point(173, 28);
      this.txHost.Name = "txHost";
      this.txHost.Size = new System.Drawing.Size(363, 20);
      this.txHost.TabIndex = 2;
      this.txHost.TextChanged += new System.EventHandler(this.txEndpoint_TextChanged);
      // 
      // btSave
      // 
      this.btSave.Enabled = false;
      this.btSave.Location = new System.Drawing.Point(380, 287);
      this.btSave.Name = "btSave";
      this.btSave.Size = new System.Drawing.Size(75, 23);
      this.btSave.TabIndex = 5;
      this.btSave.Text = "&Aplicar";
      this.btSave.UseVisualStyleBackColor = true;
      this.btSave.Click += new System.EventHandler(this.btSave_Click);
      // 
      // txPort
      // 
      this.txPort.Location = new System.Drawing.Point(173, 68);
      this.txPort.Name = "txPort";
      this.txPort.Size = new System.Drawing.Size(86, 20);
      this.txPort.TabIndex = 4;
      this.txPort.TextChanged += new System.EventHandler(this.txPort_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(170, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Porta do Servidor";
      // 
      // SettingsDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(548, 322);
      this.Controls.Add(this.txPort);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btSave);
      this.Controls.Add(this.txHost);
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
    private System.Windows.Forms.TextBox txHost;
    private System.Windows.Forms.Button btSave;
    private System.Windows.Forms.TextBox txPort;
    private System.Windows.Forms.Label label2;
  }
}