namespace Paper.Browser.Base.Forms
{
  partial class NavigatorForm
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
      this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
      this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
      this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
      this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
      this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
      this.mnMenu = new System.Windows.Forms.MenuStrip();
      this.arquivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnClose = new System.Windows.Forms.ToolStripMenuItem();
      this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnSettings = new System.Windows.Forms.ToolStripMenuItem();
      this.mnMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // BottomToolStripPanel
      // 
      this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
      this.BottomToolStripPanel.Name = "BottomToolStripPanel";
      this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
      // 
      // TopToolStripPanel
      // 
      this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
      this.TopToolStripPanel.Name = "TopToolStripPanel";
      this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
      // 
      // RightToolStripPanel
      // 
      this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
      this.RightToolStripPanel.Name = "RightToolStripPanel";
      this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
      // 
      // LeftToolStripPanel
      // 
      this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
      this.LeftToolStripPanel.Name = "LeftToolStripPanel";
      this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
      // 
      // ContentPanel
      // 
      this.ContentPanel.Size = new System.Drawing.Size(674, 349);
      // 
      // mnMenu
      // 
      this.mnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivToolStripMenuItem,
            this.ferramentasToolStripMenuItem});
      this.mnMenu.Location = new System.Drawing.Point(0, 0);
      this.mnMenu.Name = "mnMenu";
      this.mnMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
      this.mnMenu.Size = new System.Drawing.Size(674, 24);
      this.mnMenu.TabIndex = 1;
      this.mnMenu.Text = "menuStrip1";
      // 
      // arquivToolStripMenuItem
      // 
      this.arquivToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnClose});
      this.arquivToolStripMenuItem.Name = "arquivToolStripMenuItem";
      this.arquivToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.arquivToolStripMenuItem.Text = "&Arquivo";
      // 
      // mnClose
      // 
      this.mnClose.Name = "mnClose";
      this.mnClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
      this.mnClose.Size = new System.Drawing.Size(135, 22);
      this.mnClose.Text = "&Sair";
      // 
      // ferramentasToolStripMenuItem
      // 
      this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSettings});
      this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
      this.ferramentasToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
      this.ferramentasToolStripMenuItem.Text = "&Ferramentas";
      // 
      // mnSettings
      // 
      this.mnSettings.Name = "mnSettings";
      this.mnSettings.Size = new System.Drawing.Size(151, 22);
      this.mnSettings.Text = "&Configurações";
      // 
      // NavigatorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(674, 373);
      this.Controls.Add(this.mnMenu);
      this.IsMdiContainer = true;
      this.Name = "NavigatorForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Paper Browser";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.mnMenu.ResumeLayout(false);
      this.mnMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
    private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
    private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
    private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
    private System.Windows.Forms.ToolStripContentPanel ContentPanel;
    private System.Windows.Forms.MenuStrip mnMenu;
    private System.Windows.Forms.ToolStripMenuItem arquivToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnClose;
    private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnSettings;
  }
}

