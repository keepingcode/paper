namespace Paper.Browser.Gui.Layouts
{
  partial class FlexWindowLayout
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
      this._actionBar = new System.Windows.Forms.ToolStrip();
      this._toolBar = new System.Windows.Forms.ToolStrip();
      this._contentPane = new System.Windows.Forms.Panel();
      this._statusBar = new System.Windows.Forms.StatusStrip();
      this._statusText = new System.Windows.Forms.ToolStripStatusLabel();
      this._progressBar = new System.Windows.Forms.ToolStripProgressBar();
      this._statusBar.SuspendLayout();
      this.SuspendLayout();
      // 
      // _actionBar
      // 
      this._actionBar.Dock = System.Windows.Forms.DockStyle.Right;
      this._actionBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._actionBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
      this._actionBar.Location = new System.Drawing.Point(439, 25);
      this._actionBar.Name = "_actionBar";
      this._actionBar.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this._actionBar.Size = new System.Drawing.Size(31, 343);
      this._actionBar.TabIndex = 9;
      this._actionBar.Text = "toolStrip1";
      // 
      // _toolBar
      // 
      this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._toolBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this._toolBar.Location = new System.Drawing.Point(0, 0);
      this._toolBar.Name = "_toolBar";
      this._toolBar.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this._toolBar.Size = new System.Drawing.Size(470, 25);
      this._toolBar.TabIndex = 10;
      this._toolBar.Text = "toolStrip1";
      // 
      // _contentPane
      // 
      this._contentPane.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this._contentPane.BackColor = System.Drawing.SystemColors.Control;
      this._contentPane.Dock = System.Windows.Forms.DockStyle.Fill;
      this._contentPane.Location = new System.Drawing.Point(0, 25);
      this._contentPane.Margin = new System.Windows.Forms.Padding(10);
      this._contentPane.Name = "_contentPane";
      this._contentPane.Size = new System.Drawing.Size(439, 343);
      this._contentPane.TabIndex = 11;
      // 
      // _statusBar
      // 
      this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusText,
            this._progressBar});
      this._statusBar.Location = new System.Drawing.Point(0, 368);
      this._statusBar.Name = "_statusBar";
      this._statusBar.Size = new System.Drawing.Size(470, 22);
      this._statusBar.TabIndex = 12;
      this._statusBar.Text = "statusStrip1";
      // 
      // _statusText
      // 
      this._statusText.Name = "_statusText";
      this._statusText.Size = new System.Drawing.Size(2, 17);
      this._statusText.Spring = true;
      this._statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _progressBar
      // 
      this._progressBar.Name = "_progressBar";
      this._progressBar.Size = new System.Drawing.Size(100, 16);
      // 
      // FlexWindowLayout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._contentPane);
      this.Controls.Add(this._actionBar);
      this.Controls.Add(this._statusBar);
      this.Controls.Add(this._toolBar);
      this.Name = "FlexWindowLayout";
      this.Size = new System.Drawing.Size(470, 390);
      this._statusBar.ResumeLayout(false);
      this._statusBar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip _actionBar;
    private System.Windows.Forms.ToolStrip _toolBar;
    private System.Windows.Forms.Panel _contentPane;
    private System.Windows.Forms.StatusStrip _statusBar;
    private System.Windows.Forms.ToolStripStatusLabel _statusText;
    private System.Windows.Forms.ToolStripProgressBar _progressBar;
  }
}
