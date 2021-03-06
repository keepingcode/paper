﻿namespace Sandbox.Bot.Forms
{
  partial class PaperForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaperForm));
      this.tsContainer = new System.Windows.Forms.ToolStripContainer();
      this.tbLinks = new System.Windows.Forms.ToolStrip();
      this.tbActions = new System.Windows.Forms.ToolStrip();
      this.tsContainer.RightToolStripPanel.SuspendLayout();
      this.tsContainer.SuspendLayout();
      this.SuspendLayout();
      // 
      // tsContainer
      // 
      // 
      // tsContainer.ContentPanel
      // 
      this.tsContainer.ContentPanel.Size = new System.Drawing.Size(600, 388);
      this.tsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tsContainer.Location = new System.Drawing.Point(0, 0);
      this.tsContainer.Name = "tsContainer";
      // 
      // tsContainer.RightToolStripPanel
      // 
      this.tsContainer.RightToolStripPanel.Controls.Add(this.tbActions);
      this.tsContainer.RightToolStripPanel.Controls.Add(this.tbLinks);
      this.tsContainer.Size = new System.Drawing.Size(626, 413);
      this.tsContainer.TabIndex = 0;
      this.tsContainer.Text = "toolStripContainer1";
      // 
      // tbLinks
      // 
      this.tbLinks.Dock = System.Windows.Forms.DockStyle.None;
      this.tbLinks.Location = new System.Drawing.Point(0, 114);
      this.tbLinks.Name = "tbLinks";
      this.tbLinks.Size = new System.Drawing.Size(26, 111);
      this.tbLinks.TabIndex = 1;
      // 
      // tbActions
      // 
      this.tbActions.Dock = System.Windows.Forms.DockStyle.None;
      this.tbActions.Location = new System.Drawing.Point(0, 3);
      this.tbActions.Name = "tbActions";
      this.tbActions.Size = new System.Drawing.Size(26, 111);
      this.tbActions.TabIndex = 0;
      // 
      // PaperForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(626, 413);
      this.Controls.Add(this.tsContainer);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PaperForm";
      this.ShowInTaskbar = false;
      this.Text = "Formulário";
      this.Activated += new System.EventHandler(this.PaperForm_Activated);
      this.tsContainer.RightToolStripPanel.ResumeLayout(false);
      this.tsContainer.RightToolStripPanel.PerformLayout();
      this.tsContainer.ResumeLayout(false);
      this.tsContainer.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer tsContainer;
    private System.Windows.Forms.ToolStrip tbActions;
    private System.Windows.Forms.ToolStrip tbLinks;
  }
}