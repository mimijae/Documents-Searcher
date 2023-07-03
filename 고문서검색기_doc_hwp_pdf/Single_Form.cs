using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 고문서검색기_doc_hwp_pdf
{
    public partial class Single_Form : Form
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        OleDbDataAdapter adapter;
        OleDbDataReader reader;
        DataTable dt;
        string _connstr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=paper.accdb";
        WaitFormFunc waitFormFunc = new WaitFormFunc();
        DataTable table = new DataTable();
        Form3 form3;

        public Single_Form()
        {
            InitializeComponent();
        }

        private void Single_Form_Load(object sender, EventArgs e)
        {
            ConnectData();

            this.dgData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.Columns.Add("paper_name", typeof(string));
            table.Columns.Add("category", typeof(string));
           

        }
        public void ConnectData()
        {
            //  string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={strDBPath}";   .accdb         
            conn = new OleDbConnection(_connstr);
            dt = new DataTable();
            adapter = new OleDbDataAdapter("SELECT * FROM paper WHERE paper_name LIKE '%.docx%' or paper_name LIKE '%.hwp%'", conn);
            conn.Open();
            adapter.Fill(dt);
            dgData1.DataSource = dt;
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
                sumText += reader["contents_value"].ToString() + "\r\n";

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

        private void dgData2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left )
                {
                    DataGridViewRow row = dgData2.SelectedRows[0];
                    string data = row.Cells[0].Value.ToString();
                    contentsTbox.Text = string.Empty;
                    waitFormFunc.Show(this);
                    Thread.Sleep(150);
                    nameTbox.Text = data;
                    TextData(data);
                    waitFormFunc.Close();
                }
                else if(e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridView.HitTestInfo info = dgData2.HitTest(e.X, e.Y);
                    dgData2.CurrentCell = dgData2.Rows[info.RowIndex].Cells[info.ColumnIndex];
                    if (MessageBox.Show("Are you sure you want to delete it?", "delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        dgData2.Rows.Remove(dgData2.SelectedRows[0]);
                        MessageBox.Show("Delete completed", "Delete Success!!");
                    }
                    else
                    {
                        MessageBox.Show("Canceled to delete");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a search term.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Click on the correct cell");
            }
        }

        private void extractBtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgData1.SelectedRows[0];
            string singleResult = string.Empty;           
            if (dgData2.RowCount==0)
            {
                table.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                dgData2.DataSource = table;
            }
           else if (dgData2.RowCount >= 1)
            {
                for (int j = 0; j < dgData2.RowCount; j++)
                {
                    if (row.Cells[0].Value.ToString() == dgData2[0, j].Value.ToString())
                    {
                        MessageBox.Show("There are duplicate values.");
                        dgData2.Rows.Remove(dgData2.Rows[j]);
                        break;
                    }
                }
                table.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                dgData2.DataSource = table;

            }
                //table.Clear();
                //DataGridViewRow row = dgData1.SelectedRows[0];
                //if (true == table.DefaultView.Sort.Contains(row.Cells[0].Value.ToString()))
                //{
                //    MessageBox.Show("중복된 값입니다");
                //    return;
                //}
                //else
                //{
                //    table.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                //    dgData2.DataSource = table;
                //}
                //dgData2.DataSource = table;
            }

        private void searchBtn_Click(object sender, EventArgs e)
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
                    DataGridViewRow row1 = dgData2.SelectedRows[0];
                   // DataGridViewRow row2 = ;
                    waitFormFunc.Show(this);
                    Thread.Sleep(1000);
                    form3 = new Form3();
                    form3.SetText(searchTbox.Text);
                    string singleResult=string.Empty;

                   
                    for (int i = 0; i < dgData2.RowCount; i++)
                    {
                        singleResult=dgData2[0,i].Value.ToString();
                        form3.singleGetData(singleResult);
                    }
                
                    form3.Show();
                    waitFormFunc.Close();
                    searchTbox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void searchTbox_KeyDown(object sender, KeyEventArgs e)
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
                        DataGridViewRow row1 = dgData2.SelectedRows[0];
                        // DataGridViewRow row2 = ;
                        waitFormFunc.Show(this);
                        Thread.Sleep(1000);
                        form3 = new Form3();
                        form3.SetText(searchTbox.Text);
                        string singleResult = string.Empty;


                        for (int i = 0; i < dgData2.RowCount; i++)
                        {
                            singleResult = dgData2[0, i].Value.ToString();
                            form3.singleGetData(singleResult);
                        }

                        form3.Show();
                        waitFormFunc.Close();
                        searchTbox.Text = "";
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

    }
}
