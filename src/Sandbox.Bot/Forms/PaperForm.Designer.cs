namespace Sandbox.Bot.Forms
{
  partial class PaperForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaperForm));
      this.tsContainer = new System.Windows.Forms.ToolStripContainer();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.tsContainer.BottomToolStripPanel.SuspendLayout();
      this.tsContainer.TopToolStripPanel.SuspendLayout();
      this.tsContainer.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tsContainer
      // 
      // 
      // tsContainer.BottomToolStripPanel
      // 
      this.tsContainer.BottomToolStripPanel.Controls.Add(this.statusStrip1);
      // 
      // tsContainer.ContentPanel
      // 
      this.tsContainer.ContentPanel.Size = new System.Drawing.Size(626, 342);
      this.tsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tsContainer.Location = new System.Drawing.Point(0, 0);
      this.tsContainer.Name = "tsContainer";
      this.tsContainer.Size = new System.Drawing.Size(626, 413);
      this.tsContainer.TabIndex = 0;
      this.tsContainer.Text = "toolStripContainer1";
      // 
      // tsContainer.TopToolStripPanel
      // 
      this.tsContainer.TopToolStripPanel.Controls.Add(this.menuStrip1);
      this.tsContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.statusStrip1.Location = new System.Drawing.Point(0, 0);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(626, 22);
      this.statusStrip1.TabIndex = 0;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.menuStrip1.Enabled = false;
      this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opçõesToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(626, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // opçõesToolStripMenuItem
      // 
      this.opçõesToolStripMenuItem.Enabled = false;
      this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
      this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
      this.opçõesToolStripMenuItem.Text = "Opções";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(6, 0, 1, 0);
      this.toolStrip1.Size = new System.Drawing.Size(626, 25);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 1;
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Enabled = false;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(54, 22);
      this.toolStripButton1.Text = "Ação";
      // 
      // PaperForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(626, 413);
      this.Controls.Add(this.tsContainer);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "PaperForm";
      this.ShowInTaskbar = false;
      this.Text = "Formulário";
      this.tsContainer.BottomToolStripPanel.ResumeLayout(false);
      this.tsContainer.BottomToolStripPanel.PerformLayout();
      this.tsContainer.TopToolStripPanel.ResumeLayout(false);
      this.tsContainer.TopToolStripPanel.PerformLayout();
      this.tsContainer.ResumeLayout(false);
      this.tsContainer.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer tsContainer;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
  }
}