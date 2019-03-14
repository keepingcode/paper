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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.WidgetGrid = new Paper.Browser.Gui.WidgetGrid();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.ExitButton = new System.Windows.Forms.Button();
      this.SubmitButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.AutoSize = true;
      this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.WidgetGrid, 0, 0);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 43);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 120);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // WidgetGrid
      // 
      this.WidgetGrid.AllowDrop = true;
      this.WidgetGrid.Location = new System.Drawing.Point(10, 10);
      this.WidgetGrid.Margin = new System.Windows.Forms.Padding(10);
      this.WidgetGrid.Name = "WidgetGrid";
      this.WidgetGrid.Size = new System.Drawing.Size(200, 100);
      this.WidgetGrid.TabIndex = 0;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.flowLayoutPanel1.Controls.Add(this.ExitButton);
      this.flowLayoutPanel1.Controls.Add(this.SubmitButton);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 288);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4, 7, 4, 7);
      this.flowLayoutPanel1.Size = new System.Drawing.Size(409, 43);
      this.flowLayoutPanel1.TabIndex = 3;
      // 
      // ExitButton
      // 
      this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.ExitButton.Location = new System.Drawing.Point(323, 10);
      this.ExitButton.Name = "ExitButton";
      this.ExitButton.Size = new System.Drawing.Size(75, 23);
      this.ExitButton.TabIndex = 3;
      this.ExitButton.Text = "Cancelar";
      this.ExitButton.UseVisualStyleBackColor = true;
      // 
      // SubmitButton
      // 
      this.SubmitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.SubmitButton.Location = new System.Drawing.Point(242, 10);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(75, 23);
      this.SubmitButton.TabIndex = 2;
      this.SubmitButton.Text = "Ok";
      this.SubmitButton.UseVisualStyleBackColor = true;
      // 
      // ActionForm
      // 
      this.AcceptButton = this.SubmitButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.CancelButton = this.ExitButton;
      this.ClientSize = new System.Drawing.Size(409, 331);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ActionForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Edição";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    public WidgetGrid WidgetGrid;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    public System.Windows.Forms.Button SubmitButton;
    public System.Windows.Forms.Button ExitButton;
  }
}