namespace Paper.Browser.Gui.Papers
{
  partial class TextPlainPaper
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
      this.txContent = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // txContent
      // 
      this.txContent.BackColor = System.Drawing.SystemColors.Window;
      this.txContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txContent.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txContent.Location = new System.Drawing.Point(0, 0);
      this.txContent.Name = "txContent";
      this.txContent.ReadOnly = true;
      this.txContent.Size = new System.Drawing.Size(150, 150);
      this.txContent.TabIndex = 0;
      this.txContent.Text = "";
      this.txContent.WordWrap = false;
      // 
      // TextPlainPaper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txContent);
      this.Name = "TextPlainPaper";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox txContent;
  }
}
