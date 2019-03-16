namespace Paper.Browser.Gui.Widgets
{
  partial class CheckboxFieldWidget
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
      this.lbText = new System.Windows.Forms.Label();
      this.ckValue = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // lbText
      // 
      this.lbText.AutoSize = true;
      this.lbText.Location = new System.Drawing.Point(-3, 0);
      this.lbText.Name = "lbText";
      this.lbText.Size = new System.Drawing.Size(28, 13);
      this.lbText.TabIndex = 0;
      this.lbText.Text = "Text";
      // 
      // dpValue
      // 
      this.ckValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ckValue.Location = new System.Drawing.Point(0, 17);
      this.ckValue.Name = "dpValue";
      this.ckValue.Size = new System.Drawing.Size(161, 20);
      this.ckValue.TabIndex = 1;
      this.ckValue.Text = "Desativado";
      // 
      // CheckboxFieldWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.ckValue);
      this.Controls.Add(this.lbText);
      this.Name = "CheckboxFieldWidget";
      this.Size = new System.Drawing.Size(161, 37);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbText;
    private System.Windows.Forms.CheckBox ckValue;
  }
}
