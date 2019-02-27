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
      this.pnOverlay = new System.Windows.Forms.Panel();
      this.pgProgress = new System.Windows.Forms.ProgressBar();
      this.lbStatus = new System.Windows.Forms.Label();
      this.ActionBar = new System.Windows.Forms.ToolStrip();
      this.btExpand = new System.Windows.Forms.ToolStripButton();
      this.btReduce = new System.Windows.Forms.ToolStripButton();
      this.btViewSource = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnRefresh = new System.Windows.Forms.ToolStripButton();
      this.ToolBar = new System.Windows.Forms.ToolStrip();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.SelectionLabel = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.StatusBar = new System.Windows.Forms.StatusStrip();
      this.pnOverlay.SuspendLayout();
      this.ToolBar.SuspendLayout();
      this.StatusBar.SuspendLayout();
      this.SuspendLayout();
      // 
      // PageContainer
      // 
      this.PageContainer.AutoSize = true;
      this.PageContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.PageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PageContainer.Location = new System.Drawing.Point(0, 25);
      this.PageContainer.Name = "PageContainer";
      this.PageContainer.Size = new System.Drawing.Size(470, 238);
      this.PageContainer.TabIndex = 1;
      // 
      // pnOverlay
      // 
      this.pnOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnOverlay.Controls.Add(this.pgProgress);
      this.pnOverlay.Controls.Add(this.lbStatus);
      this.pnOverlay.Location = new System.Drawing.Point(23, 176);
      this.pnOverlay.Name = "pnOverlay";
      this.pnOverlay.Size = new System.Drawing.Size(291, 62);
      this.pnOverlay.TabIndex = 3;
      this.pnOverlay.Visible = false;
      // 
      // pgProgress
      // 
      this.pgProgress.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.pgProgress.Location = new System.Drawing.Point(32, 29);
      this.pgProgress.Name = "pgProgress";
      this.pgProgress.Size = new System.Drawing.Size(227, 10);
      this.pgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.pgProgress.TabIndex = 2;
      // 
      // lbStatus
      // 
      this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.lbStatus.Location = new System.Drawing.Point(29, 13);
      this.lbStatus.Name = "lbStatus";
      this.lbStatus.Size = new System.Drawing.Size(230, 13);
      this.lbStatus.TabIndex = 1;
      this.lbStatus.Text = ". . .";
      // 
      // ActionBar
      // 
      this.ActionBar.Dock = System.Windows.Forms.DockStyle.Right;
      this.ActionBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ActionBar.Location = new System.Drawing.Point(470, 25);
      this.ActionBar.Name = "ActionBar";
      this.ActionBar.Size = new System.Drawing.Size(26, 238);
      this.ActionBar.TabIndex = 5;
      this.ActionBar.Text = "toolStrip1";
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
      this.btViewSource.Size = new System.Drawing.Size(127, 22);
      this.btViewSource.Text = "Exibir Fonte da Página";
      this.btViewSource.Click += new System.EventHandler(this.btViewSource_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
      // ToolBar
      // 
      this.ToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btExpand,
            this.btReduce,
            this.toolStripSeparator2,
            this.toolStripSeparator4,
            this.btViewSource,
            this.toolStripSeparator3,
            this.mnRefresh,
            this.toolStripSeparator1,
            this.SelectionLabel,
            this.toolStripSeparator8});
      this.ToolBar.Location = new System.Drawing.Point(0, 0);
      this.ToolBar.Name = "ToolBar";
      this.ToolBar.Size = new System.Drawing.Size(496, 25);
      this.ToolBar.Stretch = true;
      this.ToolBar.TabIndex = 2;
      this.ToolBar.Text = "toolStrip1";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // SelectionLabel
      // 
      this.SelectionLabel.Name = "SelectionLabel";
      this.SelectionLabel.Size = new System.Drawing.Size(0, 22);
      this.SelectionLabel.Visible = false;
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
      this.toolStripSeparator8.Visible = false;
      // 
      // StatusLabel
      // 
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(379, 17);
      this.StatusLabel.Spring = true;
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ProgressBar
      // 
      this.ProgressBar.Name = "ProgressBar";
      this.ProgressBar.Size = new System.Drawing.Size(100, 16);
      // 
      // StatusBar
      // 
      this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
      this.StatusBar.Location = new System.Drawing.Point(0, 263);
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(496, 22);
      this.StatusBar.TabIndex = 1;
      this.StatusBar.Text = "statusStrip1";
      // 
      // WindowForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(496, 285);
      this.ControlBox = false;
      this.Controls.Add(this.pnOverlay);
      this.Controls.Add(this.PageContainer);
      this.Controls.Add(this.ActionBar);
      this.Controls.Add(this.StatusBar);
      this.Controls.Add(this.ToolBar);
      this.Name = "WindowForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Janela";
      this.MinimumSizeChanged += new System.EventHandler(this.WindowForm_MinimumSizeChanged);
      this.pnOverlay.ResumeLayout(false);
      this.ToolBar.ResumeLayout(false);
      this.ToolBar.PerformLayout();
      this.StatusBar.ResumeLayout(false);
      this.StatusBar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    internal System.Windows.Forms.Timer Timer;
    public System.Windows.Forms.Panel PageContainer;
    private System.Windows.Forms.Panel pnOverlay;
    private System.Windows.Forms.ProgressBar pgProgress;
    private System.Windows.Forms.Label lbStatus;
    private System.Windows.Forms.ToolStripButton btExpand;
    private System.Windows.Forms.ToolStripButton btReduce;
    private System.Windows.Forms.ToolStripButton btViewSource;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton mnRefresh;
    public System.Windows.Forms.ToolStrip ToolBar;
    public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    internal System.Windows.Forms.ToolStripProgressBar ProgressBar;
    public System.Windows.Forms.StatusStrip StatusBar;
    public System.Windows.Forms.ToolStrip ActionBar;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    public System.Windows.Forms.ToolStripLabel SelectionLabel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
  }
}