namespace Paper.Browser.Gui.Panels
{
  partial class TablePanel
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
      this.DataGrid = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // DataGrid
      // 
      this.DataGrid.AllowUserToAddRows = false;
      this.DataGrid.AllowUserToDeleteRows = false;
      this.DataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.DataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
      this.DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DataGrid.Location = new System.Drawing.Point(0, 0);
      this.DataGrid.Name = "DataGrid";
      this.DataGrid.Size = new System.Drawing.Size(300, 200);
      this.DataGrid.TabIndex = 0;
      // 
      // TablePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.DataGrid);
      this.MinimumSize = new System.Drawing.Size(300, 200);
      this.Name = "TablePanel";
      this.Size = new System.Drawing.Size(300, 200);
      ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    public System.Windows.Forms.DataGridView DataGrid;
  }
}
