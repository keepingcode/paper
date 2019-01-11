namespace Sandbox.Bot.Forms
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.tbMenu = new System.Windows.Forms.MenuStrip();
      this.mnMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.mnHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.tbMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // tbMenu
      // 
      this.tbMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnMenu,
            this.mnHelp});
      this.tbMenu.Location = new System.Drawing.Point(0, 0);
      this.tbMenu.Name = "tbMenu";
      this.tbMenu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
      this.tbMenu.Size = new System.Drawing.Size(722, 24);
      this.tbMenu.TabIndex = 2;
      this.tbMenu.Text = "menuStrip2";
      // 
      // mnMenu
      // 
      this.mnMenu.Enabled = false;
      this.mnMenu.Name = "mnMenu";
      this.mnMenu.Size = new System.Drawing.Size(50, 20);
      this.mnMenu.Text = "&Menu";
      // 
      // mnHelp
      // 
      this.mnHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnAbout});
      this.mnHelp.Name = "mnHelp";
      this.mnHelp.Size = new System.Drawing.Size(50, 20);
      this.mnHelp.Text = "&Ajuda";
      // 
      // mnAbout
      // 
      this.mnAbout.Name = "mnAbout";
      this.mnAbout.Size = new System.Drawing.Size(104, 22);
      this.mnAbout.Text = "&Sobre";
      this.mnAbout.Click += new System.EventHandler(this.mnAbout_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 491);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(722, 22);
      this.statusStrip1.TabIndex = 3;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(722, 513);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.tbMenu);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PaperBot";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.tbMenu.ResumeLayout(false);
      this.tbMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip tbMenu;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnMenu;
    private System.Windows.Forms.ToolStripMenuItem mnHelp;
    private System.Windows.Forms.ToolStripMenuItem mnAbout;
  }
}

