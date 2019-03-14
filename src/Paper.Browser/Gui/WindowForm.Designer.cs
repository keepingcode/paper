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
      this.timer = new System.Windows.Forms.Timer(this.components);
      this.LayoutPane = new System.Windows.Forms.TableLayoutPanel();
      this.ToolBar = new System.Windows.Forms.ToolStrip();
      this.ActionBar = new System.Windows.Forms.ToolStrip();
      this.StatusBar = new System.Windows.Forms.StatusStrip();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.ContentPane = new System.Windows.Forms.Panel();
      this.SelectionCountLabel = new System.Windows.Forms.ToolStripLabel();
      this.LayoutPane.SuspendLayout();
      this.ToolBar.SuspendLayout();
      this.StatusBar.SuspendLayout();
      this.SuspendLayout();
      // 
      // LayoutPane
      // 
      this.LayoutPane.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.LayoutPane.ColumnCount = 2;
      this.LayoutPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.LayoutPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.LayoutPane.Controls.Add(this.ToolBar, 0, 0);
      this.LayoutPane.Controls.Add(this.ActionBar, 1, 1);
      this.LayoutPane.Controls.Add(this.StatusBar, 0, 2);
      this.LayoutPane.Controls.Add(this.ContentPane, 0, 1);
      this.LayoutPane.Location = new System.Drawing.Point(0, 0);
      this.LayoutPane.Name = "LayoutPane";
      this.LayoutPane.RowCount = 3;
      this.LayoutPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.LayoutPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.LayoutPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.LayoutPane.Size = new System.Drawing.Size(378, 249);
      this.LayoutPane.TabIndex = 1;
      // 
      // ToolBar
      // 
      this.LayoutPane.SetColumnSpan(this.ToolBar, 2);
      this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectionCountLabel});
      this.ToolBar.Location = new System.Drawing.Point(0, 0);
      this.ToolBar.Name = "ToolBar";
      this.ToolBar.Size = new System.Drawing.Size(378, 20);
      this.ToolBar.TabIndex = 0;
      this.ToolBar.Text = "toolStrip1";
      // 
      // ActionBar
      // 
      this.ActionBar.Dock = System.Windows.Forms.DockStyle.None;
      this.ActionBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
      this.ActionBar.Location = new System.Drawing.Point(352, 20);
      this.ActionBar.Name = "ActionBar";
      this.ActionBar.Size = new System.Drawing.Size(26, 111);
      this.ActionBar.TabIndex = 1;
      this.ActionBar.Text = "toolStrip2";
      // 
      // StatusBar
      // 
      this.LayoutPane.SetColumnSpan(this.StatusBar, 2);
      this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.ProgressBar});
      this.StatusBar.Location = new System.Drawing.Point(0, 229);
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(378, 20);
      this.StatusBar.TabIndex = 2;
      this.StatusBar.Text = "statusStrip1";
      // 
      // StatusLabel
      // 
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(261, 15);
      this.StatusLabel.Spring = true;
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // ProgressBar
      // 
      this.ProgressBar.Name = "ProgressBar";
      this.ProgressBar.Size = new System.Drawing.Size(100, 14);
      // 
      // ContentPane
      // 
      this.ContentPane.BackColor = System.Drawing.SystemColors.Control;
      this.ContentPane.Location = new System.Drawing.Point(10, 30);
      this.ContentPane.Margin = new System.Windows.Forms.Padding(10);
      this.ContentPane.MinimumSize = new System.Drawing.Size(200, 100);
      this.ContentPane.Name = "ContentPane";
      this.ContentPane.Size = new System.Drawing.Size(302, 100);
      this.ContentPane.TabIndex = 3;
      // 
      // SelectionCountLabel
      // 
      this.SelectionCountLabel.Name = "SelectionCountLabel";
      this.SelectionCountLabel.Size = new System.Drawing.Size(0, 17);
      // 
      // WindowForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(555, 376);
      this.Controls.Add(this.LayoutPane);
      this.KeyPreview = true;
      this.Name = "WindowForm";
      this.Text = "Janela";
      this.LayoutPane.ResumeLayout(false);
      this.LayoutPane.PerformLayout();
      this.ToolBar.ResumeLayout(false);
      this.ToolBar.PerformLayout();
      this.StatusBar.ResumeLayout(false);
      this.StatusBar.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.ToolStripProgressBar ProgressBar;
    public System.Windows.Forms.ToolStrip ToolBar;
    public System.Windows.Forms.ToolStrip ActionBar;
    public System.Windows.Forms.StatusStrip StatusBar;
    public System.Windows.Forms.Panel ContentPane;
    public System.Windows.Forms.TableLayoutPanel LayoutPane;
    private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    public System.Windows.Forms.ToolStripLabel SelectionCountLabel;
  }
}