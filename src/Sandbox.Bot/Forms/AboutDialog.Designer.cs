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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
      this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.logoPictureBox = new System.Windows.Forms.PictureBox();
      this.lbProductName = new System.Windows.Forms.Label();
      this.lbVersion = new System.Windows.Forms.Label();
      this.lbManufacturer = new System.Windows.Forms.Label();
      this.lbCopyright = new System.Windows.Forms.Label();
      this.txDescription = new System.Windows.Forms.TextBox();
      this.okButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.ColumnCount = 2;
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
      this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
      this.tableLayoutPanel.Controls.Add(this.lbProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add(this.lbVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add(this.lbManufacturer, 1, 2);
      this.tableLayoutPanel.Controls.Add(this.lbCopyright, 1, 3);
      this.tableLayoutPanel.Controls.Add(this.txDescription, 1, 4);
      this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
      this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 6;
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
      this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
      this.tableLayoutPanel.TabIndex = 0;
      // 
      // logoPictureBox
      // 
      this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
      this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
      this.logoPictureBox.Name = "logoPictureBox";
      this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
      this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
      this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.logoPictureBox.TabIndex = 12;
      this.logoPictureBox.TabStop = false;
      // 
      // lbProductName
      // 
      this.lbProductName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbProductName.Location = new System.Drawing.Point(143, 0);
      this.lbProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.lbProductName.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbProductName.Name = "lbProductName";
      this.lbProductName.Size = new System.Drawing.Size(271, 17);
      this.lbProductName.TabIndex = 19;
      this.lbProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbVersion
      // 
      this.lbVersion.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbVersion.Location = new System.Drawing.Point(143, 26);
      this.lbVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.lbVersion.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbVersion.Name = "lbVersion";
      this.lbVersion.Size = new System.Drawing.Size(271, 17);
      this.lbVersion.TabIndex = 0;
      this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbManufacturer
      // 
      this.lbManufacturer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbManufacturer.Location = new System.Drawing.Point(143, 52);
      this.lbManufacturer.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.lbManufacturer.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbManufacturer.Name = "lbManufacturer";
      this.lbManufacturer.Size = new System.Drawing.Size(271, 17);
      this.lbManufacturer.TabIndex = 21;
      this.lbManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbCopyright
      // 
      this.lbCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbCopyright.Location = new System.Drawing.Point(143, 78);
      this.lbCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
      this.lbCopyright.MaximumSize = new System.Drawing.Size(0, 17);
      this.lbCopyright.Name = "lbCopyright";
      this.lbCopyright.Size = new System.Drawing.Size(271, 17);
      this.lbCopyright.TabIndex = 22;
      this.lbCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txDescription
      // 
      this.txDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txDescription.Location = new System.Drawing.Point(143, 107);
      this.txDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
      this.txDescription.Multiline = true;
      this.txDescription.Name = "txDescription";
      this.txDescription.ReadOnly = true;
      this.txDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txDescription.Size = new System.Drawing.Size(271, 126);
      this.txDescription.TabIndex = 23;
      this.txDescription.TabStop = false;
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.okButton.Location = new System.Drawing.Point(339, 239);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 24;
      this.okButton.Text = "&OK";
      // 
      // AboutDialog
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(435, 283);
      this.Controls.Add(this.tableLayoutPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutDialog";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Sobre";
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.PictureBox logoPictureBox;
    private System.Windows.Forms.Label lbProductName;
    private System.Windows.Forms.Label lbVersion;
    private System.Windows.Forms.Label lbManufacturer;
    private System.Windows.Forms.Label lbCopyright;
    private System.Windows.Forms.TextBox txDescription;
    private System.Windows.Forms.Button okButton;
  }
}
