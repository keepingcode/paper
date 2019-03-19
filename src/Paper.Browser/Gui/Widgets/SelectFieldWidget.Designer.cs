namespace Paper.Browser.Gui.Widgets
{
  partial class SelectFieldWidget
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
      this.cbValue = new System.Windows.Forms.ComboBox();
      this.lbText = new System.Windows.Forms.Label();
      this.lbValue = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // cbValue
      // 
      this.cbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbValue.Location = new System.Drawing.Point(0, 16);
      this.cbValue.Name = "cbValue";
      this.cbValue.Size = new System.Drawing.Size(161, 21);
      this.cbValue.TabIndex = 3;
      // 
      // lbText
      // 
      this.lbText.AutoSize = true;
      this.lbText.Location = new System.Drawing.Point(-3, 0);
      this.lbText.Name = "lbText";
      this.lbText.Size = new System.Drawing.Size(28, 13);
      this.lbText.TabIndex = 2;
      this.lbText.Text = "Text";
      // 
      // lbValue
      // 
      this.lbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbValue.FormattingEnabled = true;
      this.lbValue.Location = new System.Drawing.Point(0, 17);
      this.lbValue.Name = "lbValue";
      this.lbValue.Size = new System.Drawing.Size(161, 17);
      this.lbValue.TabIndex = 4;
      // 
      // SelectFieldWidget
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lbText);
      this.Controls.Add(this.lbValue);
      this.Controls.Add(this.cbValue);
      this.Name = "SelectFieldWidget";
      this.Size = new System.Drawing.Size(161, 37);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cbValue;
    private System.Windows.Forms.Label lbText;
    private System.Windows.Forms.ListBox lbValue;
  }
}
