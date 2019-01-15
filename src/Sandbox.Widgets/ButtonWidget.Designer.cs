namespace Sandbox.Widgets
{
  partial class ButtonWidget
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
      this.btAction = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btAction
      // 
      this.btAction.AutoSize = true;
      this.btAction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btAction.Location = new System.Drawing.Point(0, 0);
      this.btAction.Name = "btAction";
      this.btAction.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.btAction.Size = new System.Drawing.Size(46, 23);
      this.btAction.TabIndex = 0;
      this.btAction.Text = "Text";
      this.btAction.UseVisualStyleBackColor = true;
      // 
      // ButtonWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.btAction);
      this.Name = "ButtonWidget";
      this.Size = new System.Drawing.Size(49, 26);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btAction;
  }
}
