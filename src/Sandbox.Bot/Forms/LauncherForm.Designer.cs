using Sandbox.Bot.Properties;

namespace Sandbox.Bot.Forms
{
  partial class LauncherForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
      this.lbMessage = new System.Windows.Forms.Label();
      this.pgProgress = new System.Windows.Forms.ProgressBar();
      this.btCancel = new System.Windows.Forms.Button();
      this.lbDetail = new System.Windows.Forms.Label();
      this.btSettings = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lbMessage
      // 
      this.lbMessage.AutoSize = true;
      this.lbMessage.Location = new System.Drawing.Point(12, 21);
      this.lbMessage.Name = "lbMessage";
      this.lbMessage.Size = new System.Drawing.Size(169, 13);
      this.lbMessage.TabIndex = 0;
      this.lbMessage.Text = "Localizando o servidor de dados...";
      // 
      // pgProgress
      // 
      this.pgProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pgProgress.Location = new System.Drawing.Point(15, 37);
      this.pgProgress.Name = "pgProgress";
      this.pgProgress.Size = new System.Drawing.Size(489, 23);
      this.pgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.pgProgress.TabIndex = 1;
      // 
      // btCancel
      // 
      this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btCancel.AutoSize = true;
      this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btCancel.Location = new System.Drawing.Point(429, 194);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new System.Drawing.Size(75, 23);
      this.btCancel.TabIndex = 4;
      this.btCancel.Text = "&Sair";
      this.btCancel.UseVisualStyleBackColor = true;
      this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
      // 
      // lbDetail
      // 
      this.lbDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbDetail.Location = new System.Drawing.Point(12, 69);
      this.lbDetail.Name = "lbDetail";
      this.lbDetail.Size = new System.Drawing.Size(492, 122);
      this.lbDetail.TabIndex = 2;
      // 
      // btSettings
      // 
      this.btSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btSettings.AutoSize = true;
      this.btSettings.Location = new System.Drawing.Point(15, 194);
      this.btSettings.Name = "btSettings";
      this.btSettings.Size = new System.Drawing.Size(94, 23);
      this.btSettings.TabIndex = 3;
      this.btSettings.Text = "&Configurações";
      this.btSettings.UseVisualStyleBackColor = true;
      this.btSettings.Click += new System.EventHandler(this.btSettings_Click);
      // 
      // LauncherForm
      // 
      this.AcceptButton = this.btCancel;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btCancel;
      this.ClientSize = new System.Drawing.Size(516, 229);
      this.Controls.Add(this.btSettings);
      this.Controls.Add(this.lbDetail);
      this.Controls.Add(this.btCancel);
      this.Controls.Add(this.pgProgress);
      this.Controls.Add(this.lbMessage);
      this.Icon = Resources.launch;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(216, 170);
      this.Name = "LauncherForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Lançador do Sistema";
      this.Shown += new System.EventHandler(this.StartDialog_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lbMessage;
    private System.Windows.Forms.ProgressBar pgProgress;
    private System.Windows.Forms.Button btCancel;
    private System.Windows.Forms.Label lbDetail;
    private System.Windows.Forms.Button btSettings;
  }
}