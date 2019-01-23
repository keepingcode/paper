namespace Sandbox.Bot.Forms
{
  partial class ActionDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionDialog));
      this.tsContainer = new System.Windows.Forms.ToolStripContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pnContent = new System.Windows.Forms.FlowLayoutPanel();
      this.tsContainer.ContentPanel.SuspendLayout();
      this.tsContainer.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tsContainer
      // 
      // 
      // tsContainer.ContentPanel
      // 
      this.tsContainer.ContentPanel.Controls.Add(this.panel1);
      this.tsContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(20);
      this.tsContainer.ContentPanel.Size = new System.Drawing.Size(626, 388);
      this.tsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tsContainer.Location = new System.Drawing.Point(0, 0);
      this.tsContainer.Name = "tsContainer";
      this.tsContainer.Size = new System.Drawing.Size(626, 413);
      this.tsContainer.TabIndex = 1;
      this.tsContainer.Text = "toolStripContainer1";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.pnContent);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new System.Windows.Forms.Padding(10);
      this.panel1.Size = new System.Drawing.Size(626, 388);
      this.panel1.TabIndex = 0;
      // 
      // pnContent
      // 
      this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.pnContent.Location = new System.Drawing.Point(10, 10);
      this.pnContent.Name = "pnContent";
      this.pnContent.Size = new System.Drawing.Size(606, 368);
      this.pnContent.TabIndex = 3;
      // 
      // ActionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(626, 413);
      this.Controls.Add(this.tsContainer);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ActionDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Formulário";
      this.tsContainer.ContentPanel.ResumeLayout(false);
      this.tsContainer.ResumeLayout(false);
      this.tsContainer.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer tsContainer;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.FlowLayoutPanel pnContent;
  }
}