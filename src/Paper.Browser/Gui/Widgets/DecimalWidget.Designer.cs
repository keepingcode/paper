namespace Paper.Browser.Gui.Widgets
{
  partial class DecimalWidget
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
      this.components = new System.ComponentModel.Container();
      this.txContent = new System.Windows.Forms.TextBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.lbFaultCaption = new System.Windows.Forms.Label();
      this.lbCaption = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txContent
      // 
      this.txContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txContent.BackColor = System.Drawing.SystemColors.Control;
      this.txContent.Location = new System.Drawing.Point(0, 16);
      this.txContent.Name = "txContent";
      this.txContent.Size = new System.Drawing.Size(252, 20);
      this.txContent.TabIndex = 10;
      // 
      // lbFaultCaption
      // 
      this.lbFaultCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbFaultCaption.ForeColor = System.Drawing.Color.Firebrick;
      this.lbFaultCaption.Location = new System.Drawing.Point(-3, 0);
      this.lbFaultCaption.Margin = new System.Windows.Forms.Padding(0);
      this.lbFaultCaption.Name = "lbFaultCaption";
      this.lbFaultCaption.Size = new System.Drawing.Size(255, 13);
      this.lbFaultCaption.TabIndex = 12;
      this.lbFaultCaption.Text = "! Cabeçalho";
      this.toolTip.SetToolTip(this.lbFaultCaption, "O valor informado não é válido para este campo.");
      this.lbFaultCaption.Visible = false;
      // 
      // lbCaption
      // 
      this.lbCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbCaption.Location = new System.Drawing.Point(-3, 0);
      this.lbCaption.Margin = new System.Windows.Forms.Padding(0);
      this.lbCaption.Name = "lbCaption";
      this.lbCaption.Size = new System.Drawing.Size(255, 13);
      this.lbCaption.TabIndex = 11;
      this.lbCaption.Text = "Cabeçalho";
      // 
      // DecimalWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txContent);
      this.Controls.Add(this.lbFaultCaption);
      this.Controls.Add(this.lbCaption);
      this.Name = "DecimalWidget";
      this.Size = new System.Drawing.Size(252, 36);
      this.Resize += new System.EventHandler(this.Field_Resize);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txContent;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Label lbFaultCaption;
    private System.Windows.Forms.Label lbCaption;
  }
}
