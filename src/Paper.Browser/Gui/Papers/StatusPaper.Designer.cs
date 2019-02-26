namespace Paper.Browser.Gui.Papers
{
  partial class StatusPaper
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
      this.lbStatus = new System.Windows.Forms.Label();
      this.lnDetail = new System.Windows.Forms.LinkLabel();
      this.txDetail = new System.Windows.Forms.RichTextBox();
      this.lbMessage = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lbStatus
      // 
      this.lbStatus.AutoSize = true;
      this.lbStatus.Dock = System.Windows.Forms.DockStyle.Top;
      this.lbStatus.ForeColor = System.Drawing.SystemColors.GrayText;
      this.lbStatus.Location = new System.Drawing.Point(10, 10);
      this.lbStatus.Name = "lbStatus";
      this.lbStatus.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.lbStatus.Size = new System.Drawing.Size(128, 23);
      this.lbStatus.TabIndex = 0;
      this.lbStatus.Text = "500 - Internal Server Error";
      // 
      // lnDetail
      // 
      this.lnDetail.AutoSize = true;
      this.lnDetail.Dock = System.Windows.Forms.DockStyle.Top;
      this.lnDetail.Location = new System.Drawing.Point(10, 61);
      this.lnDetail.Name = "lnDetail";
      this.lnDetail.Size = new System.Drawing.Size(49, 13);
      this.lnDetail.TabIndex = 1;
      this.lnDetail.TabStop = true;
      this.lnDetail.Text = "Detalhes";
      this.lnDetail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnDetail_LinkClicked);
      // 
      // txDetail
      // 
      this.txDetail.BackColor = System.Drawing.SystemColors.Window;
      this.txDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txDetail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txDetail.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txDetail.Location = new System.Drawing.Point(10, 74);
      this.txDetail.Name = "txDetail";
      this.txDetail.ReadOnly = true;
      this.txDetail.Size = new System.Drawing.Size(380, 116);
      this.txDetail.TabIndex = 2;
      this.txDetail.Text = "";
      this.txDetail.Visible = false;
      this.txDetail.WordWrap = false;
      // 
      // lbMessage
      // 
      this.lbMessage.AutoSize = true;
      this.lbMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.lbMessage.Location = new System.Drawing.Point(10, 33);
      this.lbMessage.Name = "lbMessage";
      this.lbMessage.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
      this.lbMessage.Size = new System.Drawing.Size(180, 28);
      this.lbMessage.TabIndex = 3;
      this.lbMessage.Text = "Não foi possível realizar a operação.";
      // 
      // StatusPaper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txDetail);
      this.Controls.Add(this.lnDetail);
      this.Controls.Add(this.lbMessage);
      this.Controls.Add(this.lbStatus);
      this.MinimumSize = new System.Drawing.Size(400, 200);
      this.Name = "StatusPaper";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.Size = new System.Drawing.Size(400, 200);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbStatus;
    private System.Windows.Forms.LinkLabel lnDetail;
    private System.Windows.Forms.RichTextBox txDetail;
    private System.Windows.Forms.Label lbMessage;
  }
}
