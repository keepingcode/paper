namespace Paper.Browser.Gui.Papers
{
  partial class TablePaper
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
      this.dgContent = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgContent)).BeginInit();
      this.SuspendLayout();
      // 
      // dgContent
      // 
      this.dgContent.AllowUserToAddRows = false;
      this.dgContent.AllowUserToDeleteRows = false;
      this.dgContent.AllowUserToOrderColumns = true;
      this.dgContent.AllowUserToResizeRows = false;
      this.dgContent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.dgContent.BackgroundColor = System.Drawing.SystemColors.Control;
      this.dgContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgContent.Location = new System.Drawing.Point(0, 0);
      this.dgContent.Name = "dgContent";
      this.dgContent.Size = new System.Drawing.Size(150, 150);
      this.dgContent.TabIndex = 0;
      this.dgContent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgContent_CellContentClick);
      this.dgContent.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgContent_CellDoubleClick);
      // 
      // TablePaper
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.Controls.Add(this.dgContent);
      this.Name = "TablePaper";
      ((System.ComponentModel.ISupportInitialize)(this.dgContent)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgContent;
  }
}
