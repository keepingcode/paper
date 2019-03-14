namespace Paper.Browser.Gui.Layouts
{
  partial class StartWindowLayout
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
      this._progressBar = new System.Windows.Forms.ProgressBar();
      this._statusText = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // _progressBar
      // 
      this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
      this._progressBar.Location = new System.Drawing.Point(26, 40);
      this._progressBar.Name = "_progressBar";
      this._progressBar.Size = new System.Drawing.Size(257, 23);
      this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this._progressBar.TabIndex = 0;
      // 
      // _statusText
      // 
      this._statusText.AutoSize = true;
      this._statusText.Location = new System.Drawing.Point(23, 24);
      this._statusText.Name = "_statusText";
      this._statusText.Size = new System.Drawing.Size(71, 13);
      this._statusText.TabIndex = 1;
      this._statusText.Text = "Carregando...";
      // 
      // StartWindowLayout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._statusText);
      this.Controls.Add(this._progressBar);
      this.MinimumSize = new System.Drawing.Size(310, 94);
      this.Name = "StartWindowLayout";
      this.Size = new System.Drawing.Size(310, 94);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar _progressBar;
    private System.Windows.Forms.Label _statusText;
  }
}
