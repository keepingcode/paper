namespace Paper.Browser.Base.Pages
{
  partial class RowsPage
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
      this.dgRows = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgRows)).BeginInit();
      this.SuspendLayout();
      // 
      // dgRows
      // 
      this.dgRows.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
      this.dgRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgRows.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgRows.Location = new System.Drawing.Point(0, 0);
      this.dgRows.Name = "dgRows";
      this.dgRows.Size = new System.Drawing.Size(150, 150);
      this.dgRows.TabIndex = 0;
      // 
      // RowsPage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.Controls.Add(this.dgRows);
      this.Name = "RowsPage";
      ((System.ComponentModel.ISupportInitialize)(this.dgRows)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgRows;
  }
}
