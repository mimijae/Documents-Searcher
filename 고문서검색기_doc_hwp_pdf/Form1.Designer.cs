
namespace 고문서검색기_doc_hwp_pdf
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.추가ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.보기모드ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.searchBtn = new System.Windows.Forms.Button();
            this.searchTbox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.contentsTbox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.axHwpCtrl1 = new AxHWPCONTROLLib.AxHwpCtrl();
            this.nameTbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchByTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axHwpCtrl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.보기모드ToolStripMenuItem,
            this.searchByTextToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(938, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.추가ToolStripMenuItem,
            this.삭제ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.파일ToolStripMenuItem.Text = "File";
            // 
            // 추가ToolStripMenuItem
            // 
            this.추가ToolStripMenuItem.Name = "추가ToolStripMenuItem";
            this.추가ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.추가ToolStripMenuItem.Text = "Add";
            this.추가ToolStripMenuItem.Click += new System.EventHandler(this.추가ToolStripMenuItem_Click);
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.삭제ToolStripMenuItem.Text = "Delete";
            this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
            // 
            // 보기모드ToolStripMenuItem
            // 
            this.보기모드ToolStripMenuItem.Name = "보기모드ToolStripMenuItem";
            this.보기모드ToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.보기모드ToolStripMenuItem.Text = "PDF Viewer";
            this.보기모드ToolStripMenuItem.Click += new System.EventHandler(this.보기모드ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 598F));
            this.tableLayoutPanel1.Controls.Add(this.dgData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 522F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(938, 656);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // dgData
            // 
            this.dgData.AllowDrop = true;
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgData.Location = new System.Drawing.Point(3, 47);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersWidth = 51;
            this.dgData.RowTemplate.Height = 23;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(256, 606);
            this.dgData.TabIndex = 3;
            this.dgData.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgData_DragDrop);
            this.dgData.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgData_DragEnter);
            this.dgData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgData_MouseClick_1);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.47337F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.52664F));
            this.tableLayoutPanel2.Controls.Add(this.searchBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchTbox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(265, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(670, 40);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // searchBtn
            // 
            this.searchBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBtn.Font = new System.Drawing.Font("굴림", 12F);
            this.searchBtn.Location = new System.Drawing.Point(522, 3);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(145, 34);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "All search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click_1);
            // 
            // searchTbox
            // 
            this.searchTbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTbox.Font = new System.Drawing.Font("굴림", 12F);
            this.searchTbox.Location = new System.Drawing.Point(3, 3);
            this.searchTbox.Multiline = true;
            this.searchTbox.Name = "searchTbox";
            this.searchTbox.Size = new System.Drawing.Size(513, 34);
            this.searchTbox.TabIndex = 3;
            this.searchTbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTbox_KeyDown_1);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 671F));
            this.tableLayoutPanel3.Controls.Add(this.contentsTbox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(265, 46);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 479F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(670, 608);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // contentsTbox
            // 
            this.contentsTbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentsTbox.Font = new System.Drawing.Font("굴림", 13F);
            this.contentsTbox.Location = new System.Drawing.Point(3, 47);
            this.contentsTbox.Multiline = true;
            this.contentsTbox.Name = "contentsTbox";
            this.contentsTbox.ReadOnly = true;
            this.contentsTbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentsTbox.Size = new System.Drawing.Size(665, 558);
            this.contentsTbox.TabIndex = 8;
            this.contentsTbox.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel4.Controls.Add(this.axHwpCtrl1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.nameTbox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(665, 40);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // axHwpCtrl1
            // 
            this.axHwpCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axHwpCtrl1.Enabled = true;
            this.axHwpCtrl1.Location = new System.Drawing.Point(667, 3);
            this.axHwpCtrl1.Name = "axHwpCtrl1";
            this.axHwpCtrl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHwpCtrl1.OcxState")));
            this.axHwpCtrl1.Size = new System.Drawing.Size(1, 34);
            this.axHwpCtrl1.TabIndex = 9;
            this.axHwpCtrl1.Visible = false;
            // 
            // nameTbox
            // 
            this.nameTbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTbox.Font = new System.Drawing.Font("굴림", 14F);
            this.nameTbox.Location = new System.Drawing.Point(69, 3);
            this.nameTbox.Multiline = true;
            this.nameTbox.Name = "nameTbox";
            this.nameTbox.ReadOnly = true;
            this.nameTbox.Size = new System.Drawing.Size(592, 34);
            this.nameTbox.TabIndex = 6;
            this.nameTbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 14F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 40);
            this.label1.TabIndex = 5;
            this.label1.Text = "Title : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // searchByTextToolStripMenuItem
            // 
            this.searchByTextToolStripMenuItem.Name = "searchByTextToolStripMenuItem";
            this.searchByTextToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.searchByTextToolStripMenuItem.Text = "Search by text";
            this.searchByTextToolStripMenuItem.Click += new System.EventHandler(this.searchByTextToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 680);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaperSearch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axHwpCtrl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 보기모드ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 추가ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox searchTbox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox contentsTbox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private AxHWPCONTROLLib.AxHwpCtrl axHwpCtrl1;
        private System.Windows.Forms.TextBox nameTbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem searchByTextToolStripMenuItem;
    }
}

