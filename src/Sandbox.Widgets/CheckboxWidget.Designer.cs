namespace Sandbox.Widgets
{
  partial class CheckboxWidget
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
      this.ckValue = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // ckValue
      // 
      this.ckValue.AutoSize = true;
      this.ckValue.Location = new System.Drawing.Point(0, 3);
      this.ckValue.Name = "ckValue";
      this.ckValue.Size = new System.Drawing.Size(72, 17);
      this.ckValue.TabIndex = 2;
      this.ckValue.Text = "Conteudo";
      this.ckValue.UseVisualStyleBackColor = true;
      // 
      // BitWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.ckValue);
      this.Name = "BitWidget";
      this.Size = new System.Drawing.Size(75, 23);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox ckValue;
  }
}
