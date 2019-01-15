namespace Sandbox.Bot.Forms
{
  partial class AboutDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
      this.okButton = new System.Windows.Forms.Button();
      this.txDescription = new System.Windows.Forms.TextBox();
      this.lbCopyright = new System.Windows.Forms.Label();
      this.lbManufacturer = new System.Windows.Forms.Label();
      this.lbVersion = new System.Windows.Forms.Label();
      this.lbProductName = new System.Windows.Forms.Label();
      this.lbGuid = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.okButton.Location = new System.Drawing.Point(259, 216);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "&OK";
      // 
      // txDescription
      // 
      this.txDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txDescription.Location = new System.Drawing.Point(15, 130);
      this.txDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
      this.txDescription.Multiline = true;
      this.txDescription.Name = "txDescription";
      this.txDescription.ReadOnly = true;
      this.txDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txDescription.Size = new System.Drawing.Size(319, 80);
      this.txDescription.TabIndex = 5;
      this.txDescription.TabStop = false;
      // 
      // lbCopyright
      // 
      this.lbCopyright.AutoSize = true;
      this.lbCopyright.Location = new System.Drawing.Point(12, 98);
      this.lbCopyright.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbCopyright.Name = "lbCopyright";
      this.lbCopyright.Size = new System.Drawing.Size(51, 13);
      this.lbCopyright.TabIndex = 4;
      this.lbCopyright.Text = "Copyright";
      this.lbCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbManufacturer
      // 
      this.lbManufacturer.AutoSize = true;
      this.lbManufacturer.Location = new System.Drawing.Point(12, 41);
      this.lbManufacturer.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbManufacturer.Name = "lbManufacturer";
      this.lbManufacturer.Size = new System.Drawing.Size(70, 13);
      this.lbManufacturer.TabIndex = 1;
      this.lbManufacturer.Text = "Manufacturer";
      this.lbManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbVersion
      // 
      this.lbVersion.AutoSize = true;
      this.lbVersion.Location = new System.Drawing.Point(12, 60);
      this.lbVersion.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbVersion.Name = "lbVersion";
      this.lbVersion.Size = new System.Drawing.Size(42, 13);
      this.lbVersion.TabIndex = 2;
      this.lbVersion.Text = "Version";
      this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbProductName
      // 
      this.lbProductName.AutoSize = true;
      this.lbProductName.Location = new System.Drawing.Point(12, 22);
      this.lbProductName.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbProductName.Name = "lbProductName";
      this.lbProductName.Size = new System.Drawing.Size(72, 13);
      this.lbProductName.TabIndex = 0;
      this.lbProductName.Text = "ProductName";
      this.lbProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbGuid
      // 
      this.lbGuid.AutoSize = true;
      this.lbGuid.Location = new System.Drawing.Point(12, 79);
      this.lbGuid.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbGuid.Name = "lbGuid";
      this.lbGuid.Size = new System.Drawing.Size(29, 13);
      this.lbGuid.TabIndex = 3;
      this.lbGuid.Text = "Guid";
      this.lbGuid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AboutDialog
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(346, 251);
      this.Controls.Add(this.lbGuid);
      this.Controls.Add(this.lbProductName);
      this.Controls.Add(this.lbVersion);
      this.Controls.Add(this.lbManufacturer);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.lbCopyright);
      this.Controls.Add(this.txDescription);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutDialog";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Sobre";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox txDescription;
    private System.Windows.Forms.Label lbCopyright;
    private System.Windows.Forms.Label lbManufacturer;
    private System.Windows.Forms.Label lbVersion;
    private System.Windows.Forms.Label lbProductName;
    private System.Windows.Forms.Label lbGuid;
  }
}
