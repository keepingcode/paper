namespace Paper.Browser.Gui
{
  partial class ActionForm
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
      this.ContentPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.pnActions = new System.Windows.Forms.FlowLayoutPanel();
      this.CancelButton = new System.Windows.Forms.Button();
      this.SubmitButton = new System.Windows.Forms.Button();
      this.pnActions.SuspendLayout();
      this.SuspendLayout();
      // 
      // ContentPanel
      // 
      this.ContentPanel.BackColor = System.Drawing.SystemColors.Control;
      this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ContentPanel.Location = new System.Drawing.Point(10, 10);
      this.ContentPanel.Name = "ContentPanel";
      this.ContentPanel.Size = new System.Drawing.Size(264, 42);
      this.ContentPanel.TabIndex = 1;
      // 
      // pnActions
      // 
      this.pnActions.AutoSize = true;
      this.pnActions.Controls.Add(this.CancelButton);
      this.pnActions.Controls.Add(this.SubmitButton);
      this.pnActions.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.pnActions.Location = new System.Drawing.Point(10, 52);
      this.pnActions.Name = "pnActions";
      this.pnActions.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
      this.pnActions.Size = new System.Drawing.Size(264, 35);
      this.pnActions.TabIndex = 3;
      // 
      // CancelButton
      // 
      this.CancelButton.Location = new System.Drawing.Point(186, 9);
      this.CancelButton.Name = "CancelButton";
      this.CancelButton.Size = new System.Drawing.Size(75, 23);
      this.CancelButton.TabIndex = 1;
      this.CancelButton.Text = "&Cancelar";
      this.CancelButton.UseVisualStyleBackColor = true;
      this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
      // 
      // SubmitButton
      // 
      this.SubmitButton.Location = new System.Drawing.Point(105, 9);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(75, 23);
      this.SubmitButton.TabIndex = 2;
      this.SubmitButton.Text = "Executar";
      this.SubmitButton.UseVisualStyleBackColor = true;
      this.SubmitButton.Click += new System.EventHandler(this.btSubmit_Click);
      // 
      // ActionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 97);
      this.ControlBox = false;
      this.Controls.Add(this.ContentPanel);
      this.Controls.Add(this.pnActions);
      this.MinimumSize = new System.Drawing.Size(300, 136);
      this.Name = "ActionForm";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Ação";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActionForm_FormClosing);
      this.Click += new System.EventHandler(this.ActionForm_Click);
      this.pnActions.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.FlowLayoutPanel pnActions;
    public System.Windows.Forms.Button CancelButton;
    public System.Windows.Forms.Button SubmitButton;
    public System.Windows.Forms.FlowLayoutPanel ContentPanel;
  }
}