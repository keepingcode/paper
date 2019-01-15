namespace Sandbox.Widgets
{
  partial class InfoWidget
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
      this.lbCaption = new System.Windows.Forms.Label();
      this.lbContent = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lbCaption
      // 
      this.lbCaption.AutoSize = true;
      this.lbCaption.Location = new System.Drawing.Point(-3, 0);
      this.lbCaption.Name = "lbCaption";
      this.lbCaption.Size = new System.Drawing.Size(58, 13);
      this.lbCaption.TabIndex = 0;
      this.lbCaption.Text = "Cabeçalho";
      // 
      // lbContent
      // 
      this.lbContent.AutoSize = true;
      this.lbContent.Location = new System.Drawing.Point(-3, 19);
      this.lbContent.Name = "lbContent";
      this.lbContent.Size = new System.Drawing.Size(53, 13);
      this.lbContent.TabIndex = 1;
      this.lbContent.Text = "Conteúdo";
      // 
      // InfoWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.Controls.Add(this.lbContent);
      this.Controls.Add(this.lbCaption);
      this.Name = "InfoWidget";
      this.Resize += new System.EventHandler(this.InfoWidget_Resize);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbCaption;
    private System.Windows.Forms.Label lbContent;
  }
}
