using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 고문서검색기_doc_hwp_pdf
{
    public partial class Pdf_Form : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        OleDbDataReader reader;
        DataTable dt;
        string _connstr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=paper.accdb";
        public Pdf_Form()
        {
            InitializeComponent();
        }

        private void Pdf_Form_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectData();
                conn.Open();
                DataGridViewRow row = dgData.SelectedRows[0];
                string data = row.Cells[0].Value.ToString();
                string strFilePath = string.Empty;
                var query = $"SELECT * FROM paper_contents WHERE contents_value LIKE '%{data}%'";
                var command = new OleDbCommand(query, conn);
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        strFilePath = reader["contents_value"].ToString();
                    }
                    axAcroPDF1.LoadFile(strFilePath);
                }
                label2.Text = data;
                reader.Close();
                conn.Close();

                this.dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Normal;

            }
            catch (Exception)
            {
              
            }
           

        }

        #region 연결함수 및 text 전달함수
        void ConnectData()
        { 
            conn = new OleDbConnection(_connstr);
            dt = new DataTable();
            adapter = new OleDbDataAdapter("SELECT * FROM paper WHERE paper_name LIKE '%.pdf%'", conn);
            conn.Open();
            adapter.Fill(dt);
            dgData.DataSource = dt;
            conn.Close();
        }

        void categorySave()
        {
            try
            {

                DataTable dtChanges = new DataTable();
                //데이터 그리드 뷰에 있는 데이터를 바인딩 해줌
                DataTable dtSTUDENT = (DataTable)dgData.DataSource;
                //수정된 데이터들만 추출하여 dtChanges에 저장
                dtChanges = dtSTUDENT.GetChanges(DataRowState.Modified);
                string update_query = string.Empty;
                if (dtChanges != null)
                {
                    for (int i = 0; i < dtChanges.Rows.Count; i++)
                    {
                        update_query = @"UPDATE paper SET category= '@category' WHERE paper_name= '@paper_name'";
                        update_query = update_query.Replace("@category", dtChanges.Rows[i]["category"].ToString());
                        update_query = update_query.Replace("@paper_name", dtChanges.Rows[i]["paper_name"].ToString());
                        conn = new OleDbConnection(_connstr);
                        cmd = new OleDbCommand(update_query, conn);
                        conn.Open();
                        int x = cmd.ExecuteNonQuery();
                        conn.Close();
                        dtChanges = null;
                        if (x == 1)
                        {                          
                            ConnectData();
                            MessageBox.Show("Saved.");
                            return;
                        }

                    }

                }
             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error",
     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        private void 추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            openFileDialog1.InitialDirectory = Application.StartupPath;   //프로그램 실행 파일 위치
            openFileDialog1.FileName = "*.pdf";
            openFileDialog1.Filter = "db files (*.pdf)|*.pdf|All files (*.*)|*.*";
            string strFilePath= string.Empty;
            string paper_name = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {               
                if ((openFileDialog1.FileName).Contains(".pdf"))
                {
                    try
                    {
                       strFilePath = openFileDialog1.FileName;
                       paper_name = Path.GetFileName(openFileDialog1.FileName);
                        if (strFilePath.Length > 0)
                        {
                            //axAcroPDF1.LoadFile(strFilePath);
                            //textBox1.Text = paper_name;
                            using (conn = new OleDbConnection(_connstr))
                            {
                                conn.Open();

                                string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                cmd = new OleDbCommand(querry, conn);
                                cmd.Parameters.AddWithValue("paper_name", paper_name);
                                cmd.Parameters.AddWithValue("contents_line", (1).ToString());
                                cmd.Parameters.AddWithValue("contents_value", strFilePath);
                                cmd.ExecuteNonQuery();

                                conn.Close();

                                conn.Open();
                                string paper_querry = "insert into paper values(@paper_name,@category)";
                                cmd = new OleDbCommand(paper_querry, conn);
                                cmd.Parameters.AddWithValue("paper_name", paper_name);
                                cmd.Parameters.AddWithValue("category", string.Empty);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                MessageBox.Show("Added to database successfully!");
                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR");
                    }
                    ConnectData();

                }
            }
            else
            {
               // MessageBox.Show(".pdf 파일만 추가할수있습니다");
            }
        }

        private void 삭제ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete it?", "delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string query = "Delete From paper Where paper_name=@paper_name";
                    conn = new OleDbConnection(_connstr);
                    conn.Open();
                    cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@paper_name", dgData.CurrentRow.Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    string papercontents_query = "Delete From paper_contents Where paper_name=@paper_name";
                    conn = new OleDbConnection(_connstr);
                    conn.Open();
                    cmd = new OleDbCommand(papercontents_query, conn);
                    cmd.Parameters.AddWithValue("@paper_name", dgData.CurrentRow.Cells[0].Value);
                    cmd.ExecuteNonQuery();

                 //   if (x == 1)
                        MessageBox.Show("Delete completed", "Delete Success!!");
                    conn.Close();

                    ConnectData();
                    //dgData_MouseClick(sender, e);
                    //DataGridViewRow row = dgData.SelectedRows[0];
                    //string data = row.Cells[0].Value.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Failed");
                }
            }
            else
            {
                MessageBox.Show("Canceled deletion.");
            }
        }



        private void Pdf_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            axAcroPDF1.Dispose();
            ////this.Close();
        }


        private void dgData_DragDrop_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                string[] splitStr = { "\r\n" };
                foreach (string strFilePath in file)
                {
                    if (strFilePath.Contains(".pdf"))
                    {
                        conn = new OleDbConnection(_connstr);
                        try
                        {
                            string paper_name = Path.GetFileName(strFilePath);
                            if (strFilePath.Length > 0)
                            {
                                //axAcroPDF1.LoadFile(strFilePath);
                                //textBox1.Text = paper_name;
                                using (conn = new OleDbConnection(_connstr))
                                {
                                    conn.Open();

                                    string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                    cmd = new OleDbCommand(querry, conn);
                                    cmd.Parameters.AddWithValue("paper_name", paper_name);
                                    cmd.Parameters.AddWithValue("contents_line", (1).ToString());
                                    cmd.Parameters.AddWithValue("contents_value", strFilePath);
                                    cmd.ExecuteNonQuery();

                                    conn.Close();

                                    conn.Open();
                                    string paper_querry = "insert into paper values(@paper_name,@category)";
                                    cmd = new OleDbCommand(paper_querry, conn);
                                    cmd.Parameters.AddWithValue("paper_name", paper_name);
                                    cmd.Parameters.AddWithValue("category", string.Empty);
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                   
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ERROR");
                        }

                        ConnectData();
                        MessageBox.Show("Added to database successfully!");
                    }

                    else
                    {
                        MessageBox.Show("You can only add .pdf files.");
                    }

                }
                
            }
        }

        private void dgData_DragEnter_1(object sender, DragEventArgs e)
        {
            // 마우스 아이콘 효과
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
            }
        }
      
        private void dgData_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridView.HitTestInfo info = dgData.HitTest(e.X, e.Y);
                    dgData.CurrentCell = dgData.Rows[info.RowIndex].Cells[info.ColumnIndex];
                    upDateCategory upDateCategory = new upDateCategory();
                    upDateCategory.ChildFormEvent += UpDateCategory_ChildFormEvent;
                    upDateCategory.ShowDialog();
                    DataGridViewCell gridViewData = this.dgData.CurrentCell = dgData.CurrentCell;
                   this.dgData.CurrentCell = this.dgData[0, 1];
                    this.dgData.CurrentCell = gridViewData;
                    this.dgData.CurrentCell = this.dgData[1, 0];
                    categorySave();
                    info = dgData.HitTest(e.X, e.Y);
                    dgData.CurrentCell = dgData.Rows[info.RowIndex].Cells[info.ColumnIndex];
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    conn.Open();
                    DataGridViewRow row = dgData.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    string strFilePath = string.Empty;
                    var query = $"SELECT * FROM paper_contents WHERE contents_value LIKE '%{data}%'";
                    var command = new OleDbCommand(query, conn);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            strFilePath = reader["contents_value"].ToString();
                        }
                        axAcroPDF1.LoadFile(strFilePath);
                    }
                    label2.Text = data;
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception)
            {
               // MessageBox.Show(ex.ToString());
            }
        }

        private void UpDateCategory_ChildFormEvent(string message)
        {
            DataGridViewRow row = dgData.SelectedRows[0];
            row.Cells[1].Value = message;
        }

    }
}
