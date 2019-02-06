namespace Paper.Browser.Base.Forms
{
  partial class WindowForm
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
      this.Timer = new System.Windows.Forms.Timer(this.components);
      this.PageContainer = new System.Windows.Forms.Panel();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.ToolBar = new System.Windows.Forms.ToolStrip();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // PageContainer
      // 
      this.PageContainer.AutoSize = true;
      this.PageContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.PageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PageContainer.Location = new System.Drawing.Point(0, 25);
      this.PageContainer.Name = "PageContainer";
      this.PageContainer.Size = new System.Drawing.Size(322, 148);
      this.PageContainer.TabIndex = 1;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
      this.statusStrip1.Location = new System.Drawing.Point(0, 173);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(322, 22);
      this.statusStrip1.TabIndex = 1;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // StatusLabel
      // 
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(219, 17);
      this.StatusLabel.Spring = true;
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ProgressBar
      // 
      this.ProgressBar.Name = "ProgressBar";
      this.ProgressBar.Size = new System.Drawing.Size(100, 16);
      // 
      // ToolBar
      // 
      this.ToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ToolBar.Location = new System.Drawing.Point(0, 0);
      this.ToolBar.Name = "ToolBar";
      this.ToolBar.Size = new System.Drawing.Size(322, 25);
      this.ToolBar.Stretch = true;
      this.ToolBar.TabIndex = 2;
      this.ToolBar.Text = "toolStrip1";
      // 
      // WindowForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(322, 195);
      this.Controls.Add(this.PageContainer);
      this.Controls.Add(this.ToolBar);
      this.Controls.Add(this.statusStrip1);
      this.MaximizeBox = false;
      this.MinimumSize = new System.Drawing.Size(230, 100);
      this.Name = "WindowForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Janela";
      this.AutoSizeChanged += new System.EventHandler(this.WindowForm_AutoSizeChanged);
      this.Resize += new System.EventHandler(this.WindowForm_Resize);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    internal System.Windows.Forms.Timer Timer;
    internal System.Windows.Forms.Panel PageContainer;
    private System.Windows.Forms.StatusStrip statusStrip1;
    internal System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    internal System.Windows.Forms.ToolStripProgressBar ProgressBar;
    internal System.Windows.Forms.ToolStrip ToolBar;
  }
}