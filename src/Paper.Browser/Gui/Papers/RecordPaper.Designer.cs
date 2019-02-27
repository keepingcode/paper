namespace Paper.Browser.Gui.Papers
{
  partial class RecordPaper
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
      this.pnContent = new System.Windows.Forms.FlowLayoutPanel();
      this.SuspendLayout();
      // 
      // pnContent
      // 
      this.pnContent.BackColor = System.Drawing.SystemColors.Control;
      this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnContent.Location = new System.Drawing.Point(10, 10);
      this.pnContent.Name = "pnContent";
      this.pnContent.Size = new System.Drawing.Size(264, 80);
      this.pnContent.TabIndex = 0;
      // 
      // RecordPaper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnContent);
      this.MinimumSize = new System.Drawing.Size(284, 100);
      this.Name = "RecordPaper";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.Size = new System.Drawing.Size(284, 100);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel pnContent;
  }
}
