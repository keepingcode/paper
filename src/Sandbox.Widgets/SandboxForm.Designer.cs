namespace Sandbox.Widgets
{
  partial class SandboxForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.bitWidget1 = new Sandbox.Widgets.BitWidget();
      this.textWidget1 = new Sandbox.Widgets.TextWidget();
      this.SuspendLayout();
      // 
      // bitWidget1
      // 
      this.bitWidget1.AutoSize = true;
      this.bitWidget1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.bitWidget1.Location = new System.Drawing.Point(29, 32);
      this.bitWidget1.Name = "bitWidget1";
      this.bitWidget1.Size = new System.Drawing.Size(225, 26);
      this.bitWidget1.TabIndex = 0;
      this.bitWidget1.Text = "Cabeçalho";
      this.bitWidget1.Value = false;
      // 
      // textWidget1
      // 
      this.textWidget1.AutoSize = true;
      this.textWidget1.Location = new System.Drawing.Point(29, 64);
      this.textWidget1.Name = "textWidget1";
      this.textWidget1.Placeholder = "Digite algum texto...";
      this.textWidget1.Size = new System.Drawing.Size(225, 39);
      this.textWidget1.TabIndex = 1;
      this.textWidget1.Text = "textWidget1";
      this.textWidget1.Value = "";
      // 
      // SandboxForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(396, 450);
      this.Controls.Add(this.textWidget1);
      this.Controls.Add(this.bitWidget1);
      this.Name = "SandboxForm";
      this.Text = "SandboxForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private BitWidget bitWidget1;
    private TextWidget textWidget1;
  }
}