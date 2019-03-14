namespace Paper.Browser.Gui.Layouts
{
  partial class FixedWindowLayout
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this._toolBar = new System.Windows.Forms.ToolStrip();
      this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this._statusText = new System.Windows.Forms.ToolStripStatusLabel();
      this._progressBar = new System.Windows.Forms.ToolStripProgressBar();
      this._statusBar = new System.Windows.Forms.StatusStrip();
      this._actionBar = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this._contentPane = new System.Windows.Forms.Panel();
      this._toolBar.SuspendLayout();
      this.LayoutPanel.SuspendLayout();
      this._statusBar.SuspendLayout();
      this._actionBar.SuspendLayout();
      this.SuspendLayout();
      // 
      // _toolBar
      // 
      this._toolBar.Dock = System.Windows.Forms.DockStyle.None;
      this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
      this._toolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this._toolBar.Location = new System.Drawing.Point(12, 5);
      this._toolBar.Name = "_toolBar";
      this._toolBar.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this._toolBar.Size = new System.Drawing.Size(125, 25);
      this._toolBar.TabIndex = 3;
      this._toolBar.Text = "toolStrip1";
      // 
      // LayoutPanel
      // 
      this.LayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.LayoutPanel.ColumnCount = 2;
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.LayoutPanel.Controls.Add(this._contentPane, 0, 1);
      this.LayoutPanel.Controls.Add(this._statusBar, 0, 2);
      this.LayoutPanel.Controls.Add(this._actionBar, 1, 1);
      this.LayoutPanel.Controls.Add(this._toolBar, 0, 0);
      this.LayoutPanel.Location = new System.Drawing.Point(75, 40);
      this.LayoutPanel.Name = "LayoutPanel";
      this.LayoutPanel.RowCount = 3;
      this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.LayoutPanel.Size = new System.Drawing.Size(413, 340);
      this.LayoutPanel.TabIndex = 9;
      // 
      // _statusText
      // 
      this._statusText.Name = "_statusText";
      this._statusText.Size = new System.Drawing.Size(203, 15);
      this._statusText.Spring = true;
      this._statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _progressBar
      // 
      this._progressBar.Name = "_progressBar";
      this._progressBar.Size = new System.Drawing.Size(100, 14);
      // 
      // _statusBar
      // 
      this._statusBar.Dock = System.Windows.Forms.DockStyle.None;
      this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusText,
            this._progressBar});
      this._statusBar.Location = new System.Drawing.Point(58, 259);
      this._statusBar.Name = "_statusBar";
      this._statusBar.Size = new System.Drawing.Size(150, 22);
      this._statusBar.TabIndex = 8;
      this._statusBar.Text = "statusStrip1";
      // 
      // _actionBar
      // 
      this._actionBar.Dock = System.Windows.Forms.DockStyle.Fill;
      this._actionBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._actionBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
      this._actionBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
      this._actionBar.Location = new System.Drawing.Point(175, 20);
      this._actionBar.Name = "_actionBar";
      this._actionBar.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this._actionBar.Size = new System.Drawing.Size(176, 224);
      this._actionBar.TabIndex = 9;
      this._actionBar.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
      this.toolStripLabel1.Text = "toolStripLabel1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(169, 15);
      this.toolStripLabel2.Text = "toolStripLabel2";
      // 
      // _contentPane
      // 
      this._contentPane.AutoSize = true;
      this._contentPane.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this._contentPane.BackColor = System.Drawing.SystemColors.Control;
      this._contentPane.Location = new System.Drawing.Point(84, 128);
      this._contentPane.Margin = new System.Windows.Forms.Padding(10);
      this._contentPane.MinimumSize = new System.Drawing.Size(37, 37);
      this._contentPane.Name = "_contentPane";
      this._contentPane.Size = new System.Drawing.Size(37, 37);
      this._contentPane.TabIndex = 10;
      // 
      // FixedWindowLayout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.LayoutPanel);
      this.Name = "FixedWindowLayout";
      this.Size = new System.Drawing.Size(466, 468);
      this._toolBar.ResumeLayout(false);
      this._toolBar.PerformLayout();
      this.LayoutPanel.ResumeLayout(false);
      this.LayoutPanel.PerformLayout();
      this._statusBar.ResumeLayout(false);
      this._statusBar.PerformLayout();
      this._actionBar.ResumeLayout(false);
      this._actionBar.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ToolStrip _toolBar;
    private System.Windows.Forms.TableLayoutPanel LayoutPanel;
    private System.Windows.Forms.StatusStrip _statusBar;
    private System.Windows.Forms.ToolStripStatusLabel _statusText;
    private System.Windows.Forms.ToolStripProgressBar _progressBar;
    private System.Windows.Forms.ToolStrip _actionBar;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.Panel _contentPane;
  }
}
