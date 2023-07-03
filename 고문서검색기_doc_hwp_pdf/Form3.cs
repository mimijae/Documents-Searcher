using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 고문서검색기_doc_hwp_pdf
{
    public partial class Form3 : Form
    {
        OleDbDataReader reader;

        //string[] _nameData= new string[] { };
        string _data;
        int cnt = 0;
        StringBuilder sb = new StringBuilder();
        //public void singleSetText(string[] data)
        //{
        //    _nameData = data;
        //}
        public void SetText(string data)
        {
            _data = data;
        }
        public Form3()
        {
            InitializeComponent();
        }
        public void singleGetData(string _nameData)
        {
                using (var conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=paper.accdb"))
                {
                    try
                    {
                        conection.Open();
                        var query = $@"SELECT * FROM paper_contents WHERE paper_name LIKE '%{_nameData}%' and contents_value LIKE '%{_data}%'";
                        //SELECT * FROM paper WHERE paper_name LIKE '%.docx%' or paper_name LIKE '%.hwp%
                        var command = new OleDbCommand(query, conection);
                      
                      
                      
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //richTextBox.Text += reader["paper_name"].ToString() + "\r\n" + reader["contents_line"].ToString() +
                                //    "번째" + "\r\n" + reader["contents_value"].ToString().TrimEnd() + "\r\n\r\n" + "--------------------------------------------------------" + "\r\n\r\n";                              
                               
                                sb.AppendLine(reader["paper_name"].ToString());
                                sb.AppendLine(reader["contents_line"].ToString());
                                sb.AppendLine(reader["contents_value"].ToString().TrimEnd());
                                sb.AppendLine("\r\n" + "--------------------------------------------------------" + "\r\n");
                                cnt++;
                            }
                        }
                        reader.Close();
                        conection.Close();
                        // 첫번째를 선택하고
                        richTextBox.SelectionStart = richTextBox.SelectionLength = (0);
                        // 선택된 위치에 스크롤을 움직인다.
                        richTextBox.ScrollToCaret();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            
           

        }
        void ColorText(string str)
        {
            string strTarget = str;
            Regex regex = new Regex(strTarget); //Regex 사용하여 특정문자 찾기
            MatchCollection mc = regex.Matches(richTextBox.Text);
            int iCursorPosition = richTextBox.SelectionStart;
            foreach (Match m in mc)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                richTextBox.Select(iStartIdx, iStopIdx);
                richTextBox.SelectionColor = Color.Red;
                richTextBox.SelectionStart = iCursorPosition;
                richTextBox.SelectionColor = Color.Black;
            }
        }
        string UpperFirstChar(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = cnt.ToString();         
            richTextBox.Text = sb.ToString();
            string UpperFirstdata = UpperFirstChar(_data);
            ColorText(_data);
            ColorText(_data.ToUpper());
            ColorText(UpperFirstdata);
        }
    }
}
