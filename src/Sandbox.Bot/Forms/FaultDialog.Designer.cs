namespace Sandbox.Bot.Forms
{
  partial class FaultDialog
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
      this.lbMessage = new System.Windows.Forms.Label();
      this.btOk = new System.Windows.Forms.Button();
      this.lnDetail = new System.Windows.Forms.LinkLabel();
      this.txDetail = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // lbMessage
      // 
      this.lbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbMessage.Location = new System.Drawing.Point(12, 14);
      this.lbMessage.Name = "lbMessage";
      this.lbMessage.Size = new System.Drawing.Size(409, 79);
      this.lbMessage.TabIndex = 0;
      this.lbMessage.Text = "...";
      // 
      // btOk
      // 
      this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btOk.Location = new System.Drawing.Point(346, 122);
      this.btOk.Name = "btOk";
      this.btOk.Size = new System.Drawing.Size(75, 23);
      this.btOk.TabIndex = 2;
      this.btOk.Text = "&OK";
      this.btOk.UseVisualStyleBackColor = true;
      // 
      // lnDetail
      // 
      this.lnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lnDetail.AutoSize = true;
      this.lnDetail.Location = new System.Drawing.Point(12, 127);
      this.lnDetail.Name = "lnDetail";
      this.lnDetail.Size = new System.Drawing.Size(49, 13);
      this.lnDetail.TabIndex = 3;
      this.lnDetail.TabStop = true;
      this.lnDetail.Text = "Detalhes";
      this.lnDetail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnDetail_LinkClicked);
      // 
      // txDetail
      // 
      this.txDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txDetail.BackColor = System.Drawing.SystemColors.Control;
      this.txDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txDetail.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txDetail.Location = new System.Drawing.Point(15, 96);
      this.txDetail.Name = "txDetail";
      this.txDetail.ReadOnly = true;
      this.txDetail.Size = new System.Drawing.Size(406, 20);
      this.txDetail.TabIndex = 4;
      this.txDetail.Text = "";
      this.txDetail.Visible = false;
      this.txDetail.WordWrap = false;
      // 
      // FaultDialog
      // 
      this.AcceptButton = this.btOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(433, 157);
      this.ControlBox = false;
      this.Controls.Add(this.txDetail);
      this.Controls.Add(this.lnDetail);
      this.Controls.Add(this.btOk);
      this.Controls.Add(this.lbMessage);
      this.Name = "FaultDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Falha";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbMessage;
    private System.Windows.Forms.Button btOk;
    private System.Windows.Forms.LinkLabel lnDetail;
    private System.Windows.Forms.RichTextBox txDetail;
  }
}