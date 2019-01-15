namespace Sandbox.Widgets
{
  partial class BitWidget
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
      this.pnSlideRail = new System.Windows.Forms.Panel();
      this.btSlider = new System.Windows.Forms.Button();
      this.lbCaption = new System.Windows.Forms.Label();
      this.pnSlideRail.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnSlideRail
      // 
      this.pnSlideRail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pnSlideRail.BackColor = System.Drawing.SystemColors.Window;
      this.pnSlideRail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnSlideRail.Controls.Add(this.btSlider);
      this.pnSlideRail.Location = new System.Drawing.Point(182, 0);
      this.pnSlideRail.Name = "pnSlideRail";
      this.pnSlideRail.Size = new System.Drawing.Size(45, 20);
      this.pnSlideRail.TabIndex = 1;
      this.pnSlideRail.Click += new System.EventHandler(this.pnSlideRail_Click);
      // 
      // btSlider
      // 
      this.btSlider.BackColor = System.Drawing.SystemColors.ControlDark;
      this.btSlider.FlatAppearance.BorderSize = 0;
      this.btSlider.Location = new System.Drawing.Point(0, 0);
      this.btSlider.Name = "btSlider";
      this.btSlider.Size = new System.Drawing.Size(23, 18);
      this.btSlider.TabIndex = 0;
      this.btSlider.UseVisualStyleBackColor = false;
      this.btSlider.Click += new System.EventHandler(this.btSlider_Click);
      // 
      // lbCaption
      // 
      this.lbCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbCaption.Location = new System.Drawing.Point(-3, 4);
      this.lbCaption.Name = "lbCaption";
      this.lbCaption.Size = new System.Drawing.Size(183, 14);
      this.lbCaption.TabIndex = 0;
      this.lbCaption.Text = "Cabeçalho";
      this.lbCaption.Click += new System.EventHandler(this.lbCaption_Click);
      // 
      // BitWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.lbCaption);
      this.Controls.Add(this.pnSlideRail);
      this.Name = "BitWidget";
      this.Size = new System.Drawing.Size(227, 23);
      this.pnSlideRail.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel pnSlideRail;
    private System.Windows.Forms.Label lbCaption;
    private System.Windows.Forms.Button btSlider;
  }
}
