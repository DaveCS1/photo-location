namespace ShowPhotoLocation
{
   partial class FrmShowPhotoLocation
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
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.treeView1 = new System.Windows.Forms.TreeView();
         this.splitContainer2 = new System.Windows.Forms.SplitContainer();
         this.rasterPictureBox1 = new Leadtools.Controls.RasterPictureBox();
         this.webBrowser1 = new System.Windows.Forms.WebBrowser();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
         this.labelStatusMapLink = new System.Windows.Forms.ToolStripStatusLabel();
         this.labelStatusStreeViewLink = new System.Windows.Forms.ToolStripStatusLabel();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
         this.splitContainer2.Panel1.SuspendLayout();
         this.splitContainer2.Panel2.SuspendLayout();
         this.splitContainer2.SuspendLayout();
         this.statusStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.treeView1);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
         this.splitContainer1.Size = new System.Drawing.Size(793, 709);
         this.splitContainer1.SplitterDistance = 288;
         this.splitContainer1.TabIndex = 0;
         // 
         // treeView1
         // 
         this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.treeView1.Location = new System.Drawing.Point(0, 0);
         this.treeView1.Name = "treeView1";
         this.treeView1.Size = new System.Drawing.Size(288, 709);
         this.treeView1.TabIndex = 0;
         this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
         // 
         // splitContainer2
         // 
         this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer2.Location = new System.Drawing.Point(0, 0);
         this.splitContainer2.Name = "splitContainer2";
         this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer2.Panel1
         // 
         this.splitContainer2.Panel1.Controls.Add(this.rasterPictureBox1);
         // 
         // splitContainer2.Panel2
         // 
         this.splitContainer2.Panel2.Controls.Add(this.webBrowser1);
         this.splitContainer2.Size = new System.Drawing.Size(501, 709);
         this.splitContainer2.SplitterDistance = 229;
         this.splitContainer2.TabIndex = 0;
         // 
         // rasterPictureBox1
         // 
         this.rasterPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rasterPictureBox1.Location = new System.Drawing.Point(0, 0);
         this.rasterPictureBox1.Name = "rasterPictureBox1";
         this.rasterPictureBox1.Size = new System.Drawing.Size(501, 229);
         this.rasterPictureBox1.TabIndex = 1;
         this.rasterPictureBox1.TabStop = false;
         // 
         // webBrowser1
         // 
         this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.webBrowser1.Location = new System.Drawing.Point(0, 0);
         this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
         this.webBrowser1.Name = "webBrowser1";
         this.webBrowser1.Size = new System.Drawing.Size(501, 476);
         this.webBrowser1.TabIndex = 0;
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.labelStatusMapLink,
            this.labelStatusStreeViewLink});
         this.statusStrip1.Location = new System.Drawing.Point(0, 687);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(793, 22);
         this.statusStrip1.TabIndex = 1;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // labelStatus
         // 
         this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelStatus.Name = "labelStatus";
         this.labelStatus.Size = new System.Drawing.Size(39, 17);
         this.labelStatus.Text = "Status";
         // 
         // labelStatusMapLink
         // 
         this.labelStatusMapLink.IsLink = true;
         this.labelStatusMapLink.Name = "labelStatusMapLink";
         this.labelStatusMapLink.Size = new System.Drawing.Size(109, 17);
         this.labelStatusMapLink.Text = "Open Google Maps";
         this.labelStatusMapLink.Visible = false;
         this.labelStatusMapLink.Click += new System.EventHandler(this.LabelStatusMapLink_Click);
         // 
         // labelStatusStreeViewLink
         // 
         this.labelStatusStreeViewLink.IsLink = true;
         this.labelStatusStreeViewLink.Name = "labelStatusStreeViewLink";
         this.labelStatusStreeViewLink.Size = new System.Drawing.Size(138, 17);
         this.labelStatusStreeViewLink.Text = "Open Google Street View";
         this.labelStatusStreeViewLink.Visible = false;
         this.labelStatusStreeViewLink.Click += new System.EventHandler(this.LabelStatusStreeViewLink_Click);
         // 
         // frmShowPhotoLocation
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(793, 709);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.splitContainer1);
         this.Name = "FrmShowPhotoLocation";
         this.Text = "Show Photo Location";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.splitContainer2.Panel1.ResumeLayout(false);
         this.splitContainer2.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
         this.splitContainer2.ResumeLayout(false);
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.TreeView treeView1;
      private System.Windows.Forms.SplitContainer splitContainer2;
      private System.Windows.Forms.WebBrowser webBrowser1;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel labelStatus;
      private Leadtools.Controls.RasterPictureBox rasterPictureBox1;
      private System.Windows.Forms.ToolStripStatusLabel labelStatusMapLink;
      private System.Windows.Forms.ToolStripStatusLabel labelStatusStreeViewLink;
   }
}

