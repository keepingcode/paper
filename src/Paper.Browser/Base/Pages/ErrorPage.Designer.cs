namespace Paper.Browser.Base.Pages
{
  partial class ErrorPage
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
      this.txFault = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // txFault
      // 
      this.txFault.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.txFault.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txFault.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txFault.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txFault.Location = new System.Drawing.Point(10, 10);
      this.txFault.Name = "txFault";
      this.txFault.ReadOnly = true;
      this.txFault.Size = new System.Drawing.Size(356, 239);
      this.txFault.TabIndex = 0;
      this.txFault.Text = "";
      this.txFault.WordWrap = false;
      // 
      // ErrorPage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txFault);
      this.Name = "ErrorPage";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.Size = new System.Drawing.Size(376, 259);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox txFault;
  }
}
