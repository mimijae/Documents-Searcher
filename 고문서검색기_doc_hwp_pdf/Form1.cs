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
using System.Xml;
using DocumentFormat.OpenXml.Packaging;


namespace 고문서검색기_doc_hwp_pdf
{
    public partial class Form1 : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        OleDbDataReader reader;
        DataTable dt;
        Form2 form2;
        Pdf_Form pdf_Form;
        Single_Form single_Form;
       
        string _connstr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=paper.accdb";
        WaitFormFunc waitFormFunc = new WaitFormFunc();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectData();
            DataGridViewRow row = dgData.SelectedRows[0];
            string data = row.Cells[0].Value.ToString();
            nameTbox.Text = data;
            TextData(data);
            this.dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        #region 연결함수 및 text 전달함수
        public void ConnectData()
        {

            //  string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={strDBPath}";   .accdb         
            conn = new OleDbConnection(_connstr);
            dt = new DataTable();
            adapter = new OleDbDataAdapter("SELECT * FROM paper WHERE paper_name LIKE '%.docx%' or paper_name LIKE '%.hwp%'", conn);
            conn.Open();
            adapter.Fill(dt);
            dgData.DataSource = dt;
            conn.Close();
        }
        void TextData(string str)
        {

            string sql = null;
            string sumText = string.Empty;
            sql = "SELECT * FROM paper_contents WHERE paper_name='" + str + "'";

            conn = new OleDbConnection(_connstr);
            conn.Open();
            cmd = new OleDbCommand(sql, conn);

            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                sumText += reader["contents_value"].ToString()+ "\r\n";
                
                //contentsTbox.Text += "\r\n";
            }
            contentsTbox.Text = sumText;
            reader.Close();
            conn.Close();
            conn = null;
            contentsTbox.SelectionStart = contentsTbox.SelectionLength = (0); ;
            // 선택된 위치에 스크롤을 움직인다.
            contentsTbox.ScrollToCaret();
        }

        private string SearchWordIsMatched(string filepath)
        {
            try
            {
                //string path = filepath;
                //using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
                //{
                //    var text = wordDoc.MainDocumentPart.Document.InnerText;
                //    textBox3.Text = text.ToString();
                //}
                const string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

                StringBuilder textBuilder = new StringBuilder();
                using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(filepath, false))
                {
                    // Manage namespaces to perform XPath queries.  
                    NameTable nt = new NameTable();
                    XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
                    nsManager.AddNamespace("w", wordmlNamespace);

                    // Get the document part from the package.  
                    // Load the XML in the document part into an XmlDocument instance.  
                    XmlDocument xdoc = new XmlDocument(nt);
                    xdoc.Load(wdDoc.MainDocumentPart.GetStream());

                    XmlNodeList paragraphNodes = xdoc.SelectNodes("//w:p", nsManager);
                    foreach (XmlNode paragraphNode in paragraphNodes)
                    {
                        XmlNodeList textNodes = paragraphNode.SelectNodes(".//w:t", nsManager);
                        foreach (System.Xml.XmlNode textNode in textNodes)
                        {
                            textBuilder.Append(textNode.InnerText);
                        }
                        textBuilder.Append(Environment.NewLine);
                    }

                }
                return textBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 


        private void 추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strFilePath = string.Empty;

            openFileDialog1.InitialDirectory = Application.StartupPath;   //프로그램 실행 파일 위치
            openFileDialog1.FileName = "*.docx;*.hwp";
            openFileDialog1.Filter = "db files (*.docx;*.hwp)|*.docx;*.hwp|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFilePath = openFileDialog1.FileName;
                string[] splitStr = { "\r\n" };
                if ((openFileDialog1.FileName).Contains(".doc") || (openFileDialog1.FileName).Contains(".docx"))
                {
                   // conn = new OleDbConnection(_connstr);
                    try
                    {
                        string docStr = SearchWordIsMatched(strFilePath);
                        string[] docStrArray = docStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                        string paper_name = Path.GetFileName(openFileDialog1.FileName);
                        //Path.GetFileName(openFileDialog1.FileName);
                        //Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                        using (conn = new OleDbConnection(_connstr))
                        {
                            conn.Open();
                            for (int i = 0; i < docStrArray.Length; i++)
                            {
                                string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                cmd = new OleDbCommand(querry, conn);
                                cmd.Parameters.AddWithValue("paper_name", paper_name);
                                cmd.Parameters.AddWithValue("contents_line", (i + 1).ToString());
                                cmd.Parameters.AddWithValue("contents_value", docStrArray[i]);
                                cmd.ExecuteNonQuery();
                            }
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR");
                    }

                    ConnectData();

                    //dgData_MouseClick(sender,e);
                    DataGridViewRow row = dgData.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    contentsTbox.Text = string.Empty;
                    TextData(data);
                }
                else if ((openFileDialog1.FileName).Contains(".hwp"))
                {
                    try
                    {
                        string filePath = openFileDialog1.FileName;
                        axHwpCtrl1.Open(filePath);
                        string hwpStr = axHwpCtrl1.GetTextFile("TEXT", "").ToString();
                        string[] hwpStrArray = hwpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                        string paper_name = Path.GetFileName(openFileDialog1.FileName);
                        using (conn = new OleDbConnection(_connstr))
                        {
                            conn.Open();
                            for (int i = 0; i < hwpStrArray.Length; i++)
                            {
                                string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                cmd = new OleDbCommand(querry, conn);
                                cmd.Parameters.AddWithValue("paper_name", paper_name);
                                cmd.Parameters.AddWithValue("contents_line", (i + 1).ToString());
                                cmd.Parameters.AddWithValue("contents_value", hwpStrArray[i]);
                                cmd.ExecuteNonQuery();
                            }
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR");
                    }
                    ConnectData();

                    //dgData_MouseClick(sender,e);
                    DataGridViewRow row = dgData.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    contentsTbox.Text = string.Empty;
                    TextData(data);
                }
               
                else
                {
                    MessageBox.Show("You can only add .docx .hwp files");
                }
            }
        }
      


        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
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

                   // if (x == 1)
                        MessageBox.Show("Delete completed", "Delete Success!!");
                    conn.Close();
                    contentsTbox.Text = string.Empty;
                    ConnectData();
                    //dgData_MouseClick(sender, e);
                    DataGridViewRow row = dgData.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    contentsTbox.Text = string.Empty;
                    TextData(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Failed");
                }
            }
            else
            {
                MessageBox.Show("Canceled to delete");
            }
        }

        private void UpDateCategory_ChildFormEvent(string message)
        {
            DataGridViewRow row = dgData.SelectedRows[0];
            row.Cells[1].Value = message;
        }

        private void searchBtn_Click_1(object sender, EventArgs e)
        {
            //if(form2 != null)+
            //{
            //    form2.Dispose();
            //    form2 = null;
            //}
            if (searchTbox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }
            else if (searchTbox.Text.Length == 1)
            {
                MessageBox.Show("Please enter more than two characters.");
                return;
            }
            else
            {
                try
                {
                    waitFormFunc.Show(this);
                    Thread.Sleep(1000);
                    form2 = new Form2();
                    form2.SetText(searchTbox.Text);
                    form2.Show();
                    waitFormFunc.Close();
                    searchTbox.Text = "";
                }
                catch (Exception)
                {

                }
            }
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
                        dtChanges = null;
                        if (x == 1)
                            {
                                conn.Close();
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


        private void searchTbox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (searchTbox.Text == string.Empty)
                {
                    MessageBox.Show("Please enter a search term.");
                    return;
                }
                else if (searchTbox.Text.Length == 1)
                {
                    MessageBox.Show("Please enter more than two characters.");
                    return;
                }
                else
                {
                    try
                    {
                        waitFormFunc.Show(this);
                        Thread.Sleep(1000);
                        form2 = new Form2();
                        form2.SetText(searchTbox.Text);
                        form2.Show();
                        waitFormFunc.Close();
                        searchTbox.Text = "";
                    }
                    catch (Exception)
                    {

                    }
                }
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
                    DataGridViewRow row = dgData.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    contentsTbox.Text = string.Empty;
                    waitFormFunc.Show(this);
                    Thread.Sleep(200);
                    nameTbox.Text = data;
                    TextData(data);
                    waitFormFunc.Close();
                }
                else
                {
                    MessageBox.Show("Please click on the item.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgData_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                string[] splitStr = { "\r\n" };
                foreach (string strFilePath in file)
                {
                    if (strFilePath.Contains(".doc") || strFilePath.Contains(".docx"))
                    {
                        conn = new OleDbConnection(_connstr);
                        try
                        {
                            string docStr = SearchWordIsMatched(strFilePath);
                            string[] docStrArray = docStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                            string paper_name = Path.GetFileName(strFilePath);
                            //Path.GetFileName(openFileDialog1.FileName);
                            //Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                            using (conn = new OleDbConnection(_connstr))
                            {
                                conn.Open();
                                for (int i = 0; i < docStrArray.Length; i++)
                                {
                                    string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                    cmd = new OleDbCommand(querry, conn);
                                    cmd.Parameters.AddWithValue("paper_name", paper_name);
                                    cmd.Parameters.AddWithValue("contents_line", (i + 1).ToString());
                                    cmd.Parameters.AddWithValue("contents_value", docStrArray[i]);
                                    cmd.ExecuteNonQuery();
                                }
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ERROR");
                        }

                        ConnectData();
                        MessageBox.Show("Added to database successfully!");
                        //dgData_MouseClick(sender,e);
                        DataGridViewRow row = dgData.SelectedRows[0];
                        string data = row.Cells[0].Value.ToString();
                        contentsTbox.Text = string.Empty;
                        TextData(data);
                    }
                    else if (strFilePath.Contains(".hwp"))
                    {
                        try
                        {
                            string filePath = strFilePath;
                            axHwpCtrl1.Open(filePath);
                            string hwpStr = axHwpCtrl1.GetTextFile("TEXT", "").ToString();
                            string[] hwpStrArray = hwpStr.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
                            string paper_name = Path.GetFileName(strFilePath);
                            using (conn = new OleDbConnection(_connstr))
                            {
                                conn.Open();
                                for (int i = 0; i < hwpStrArray.Length; i++)
                                {
                                    string querry = "insert into paper_contents values(@paper_name,@contents_line,@contents_value)";
                                    cmd = new OleDbCommand(querry, conn);
                                    cmd.Parameters.AddWithValue("paper_name", paper_name);
                                    cmd.Parameters.AddWithValue("contents_line", (i + 1).ToString());
                                    cmd.Parameters.AddWithValue("contents_value", hwpStrArray[i]);
                                    cmd.ExecuteNonQuery();
                                }
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ERROR");
                        }
                        ConnectData();
                        MessageBox.Show("Added to database successfully!");
                        //dgData_MouseClick(sender,e);
                        DataGridViewRow row = dgData.SelectedRows[0];
                        string data = row.Cells[0].Value.ToString();
                        contentsTbox.Text = string.Empty;
                        TextData(data);
                    }
                    else
                    {
                        MessageBox.Show("You can only add .docx .hwp files");
                    }

                }
              
            }
        }

        private void dgData_DragEnter(object sender, DragEventArgs e)
        {
            // 마우스 아이콘 효과
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
            }
        }

        private void searchByTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            single_Form = new Single_Form();
            single_Form.Show();
        }

        private void 보기모드ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pdf_Form = new Pdf_Form();
            pdf_Form.Show();
        }
    }
}
