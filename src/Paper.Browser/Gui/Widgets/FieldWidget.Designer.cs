﻿namespace Paper.Browser.Gui.Widgets
{
  partial class FieldWidget
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
      this.txContent = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lbCaption
      // 
      this.lbCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbCaption.Location = new System.Drawing.Point(-3, 0);
      this.lbCaption.Name = "lbCaption";
      this.lbCaption.Size = new System.Drawing.Size(255, 16);
      this.lbCaption.TabIndex = 0;
      this.lbCaption.Text = ". . .";
      // 
      // txContent
      // 
      this.txContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txContent.BackColor = System.Drawing.SystemColors.Control;
      this.txContent.Location = new System.Drawing.Point(0, 16);
      this.txContent.Name = "txContent";
      this.txContent.ReadOnly = true;
      this.txContent.Size = new System.Drawing.Size(252, 20);
      this.txContent.TabIndex = 1;
      // 
      // Field
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txContent);
      this.Controls.Add(this.lbCaption);
      this.Name = "Field";
      this.Size = new System.Drawing.Size(252, 36);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbCaption;
    private System.Windows.Forms.TextBox txContent;
  }
}
