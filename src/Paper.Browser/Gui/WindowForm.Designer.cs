namespace Paper.Browser.Gui
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowForm));
      this.Timer = new System.Windows.Forms.Timer(this.components);
      this.PageContainer = new System.Windows.Forms.Panel();
      this.StatusBar = new System.Windows.Forms.StatusStrip();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.ToolBar = new System.Windows.Forms.ToolStrip();
      this.btExpand = new System.Windows.Forms.ToolStripButton();
      this.btReduce = new System.Windows.Forms.ToolStripButton();
      this.btViewSource = new System.Windows.Forms.ToolStripButton();
      this.pnOverlay = new System.Windows.Forms.Panel();
      this.pgProgress = new System.Windows.Forms.ProgressBar();
      this.lbStatus = new System.Windows.Forms.Label();
      this.mnRefresh = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.StatusBar.SuspendLayout();
      this.ToolBar.SuspendLayout();
      this.pnOverlay.SuspendLayout();
      this.SuspendLayout();
      // 
      // PageContainer
      // 
      this.PageContainer.AutoSize = true;
      this.PageContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.PageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PageContainer.Location = new System.Drawing.Point(0, 25);
      this.PageContainer.Name = "PageContainer";
      this.PageContainer.Size = new System.Drawing.Size(322, 71);
      this.PageContainer.TabIndex = 1;
      // 
      // StatusBar
      // 
      this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
      this.StatusBar.Location = new System.Drawing.Point(0, 96);
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(322, 22);
      this.StatusBar.TabIndex = 1;
      this.StatusBar.Text = "statusStrip1";
      // 
      // StatusLabel
      // 
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(205, 17);
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
      this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btExpand,
            this.btReduce,
            this.btViewSource,
            this.toolStripSeparator1,
            this.mnRefresh});
      this.ToolBar.Location = new System.Drawing.Point(0, 0);
      this.ToolBar.Name = "ToolBar";
      this.ToolBar.Size = new System.Drawing.Size(322, 25);
      this.ToolBar.Stretch = true;
      this.ToolBar.TabIndex = 2;
      this.ToolBar.Text = "toolStrip1";
      // 
      // btExpand
      // 
      this.btExpand.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btExpand.Image = ((System.Drawing.Image)(resources.GetObject("btExpand.Image")));
      this.btExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btExpand.Name = "btExpand";
      this.btExpand.Size = new System.Drawing.Size(56, 22);
      this.btExpand.Text = "Expandir";
      this.btExpand.Click += new System.EventHandler(this.btExpand_Click);
      // 
      // btReduce
      // 
      this.btReduce.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btReduce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btReduce.Image = ((System.Drawing.Image)(resources.GetObject("btReduce.Image")));
      this.btReduce.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btReduce.Name = "btReduce";
      this.btReduce.Size = new System.Drawing.Size(50, 22);
      this.btReduce.Text = "Reduzir";
      this.btReduce.Click += new System.EventHandler(this.btReduce_Click);
      // 
      // btViewSource
      // 
      this.btViewSource.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btViewSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btViewSource.Image = ((System.Drawing.Image)(resources.GetObject("btViewSource.Image")));
      this.btViewSource.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btViewSource.Name = "btViewSource";
      this.btViewSource.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
      this.btViewSource.Size = new System.Drawing.Size(127, 19);
      this.btViewSource.Text = "Exibir Fonte da Página";
      this.btViewSource.Click += new System.EventHandler(this.btViewSource_Click);
      // 
      // pnOverlay
      // 
      this.pnOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnOverlay.Controls.Add(this.pgProgress);
      this.pnOverlay.Controls.Add(this.lbStatus);
      this.pnOverlay.Location = new System.Drawing.Point(12, 31);
      this.pnOverlay.Name = "pnOverlay";
      this.pnOverlay.Size = new System.Drawing.Size(298, 59);
      this.pnOverlay.TabIndex = 3;
      this.pnOverlay.Visible = false;
      // 
      // pgProgress
      // 
      this.pgProgress.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.pgProgress.Location = new System.Drawing.Point(0, 32);
      this.pgProgress.Name = "pgProgress";
      this.pgProgress.Size = new System.Drawing.Size(298, 10);
      this.pgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.pgProgress.TabIndex = 2;
      // 
      // lbStatus
      // 
      this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.lbStatus.Location = new System.Drawing.Point(-3, 16);
      this.lbStatus.Name = "lbStatus";
      this.lbStatus.Size = new System.Drawing.Size(301, 13);
      this.lbStatus.TabIndex = 1;
      this.lbStatus.Text = ". . .";
      // 
      // mnRefresh
      // 
      this.mnRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.mnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnRefresh.Image")));
      this.mnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnRefresh.Name = "mnRefresh";
      this.mnRefresh.Size = new System.Drawing.Size(57, 22);
      this.mnRefresh.Text = "&Atualizar";
      this.mnRefresh.Click += new System.EventHandler(this.mnRefresh_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // WindowForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(322, 118);
      this.ControlBox = false;
      this.Controls.Add(this.pnOverlay);
      this.Controls.Add(this.PageContainer);
      this.Controls.Add(this.ToolBar);
      this.Controls.Add(this.StatusBar);
      this.Name = "WindowForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Janela";
      this.MinimumSizeChanged += new System.EventHandler(this.WindowForm_MinimumSizeChanged);
      this.StatusBar.ResumeLayout(false);
      this.StatusBar.PerformLayout();
      this.ToolBar.ResumeLayout(false);
      this.ToolBar.PerformLayout();
      this.pnOverlay.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    internal System.Windows.Forms.Timer Timer;
    internal System.Windows.Forms.ToolStripProgressBar ProgressBar;
    private System.Windows.Forms.ToolStripButton btExpand;
    private System.Windows.Forms.ToolStripButton btReduce;
    public System.Windows.Forms.Panel PageContainer;
    public System.Windows.Forms.StatusStrip StatusBar;
    public System.Windows.Forms.ToolStrip ToolBar;
    public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    private System.Windows.Forms.Panel pnOverlay;
    private System.Windows.Forms.ProgressBar pgProgress;
    private System.Windows.Forms.Label lbStatus;
    private System.Windows.Forms.ToolStripButton btViewSource;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton mnRefresh;
  }
}