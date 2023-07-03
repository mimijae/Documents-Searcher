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
using System.Text.RegularExpressions;


namespace 고문서검색기_doc_hwp_pdf
{
    public partial class Form2 : Form
    {
        OleDbDataReader reader;
        
        string _data;

        public Form2()
        {
            InitializeComponent();
        }

        public void SetText(string data)
        {
            _data = data;
        }

        void GetData()
        {
            using (var conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=paper.accdb"))
            {               
                try
                {
                    conection.Open();
                    var query = $@"SELECT * FROM paper_contents WHERE contents_value LIKE '%{_data}%'";
                    var command = new OleDbCommand(query, conection);
                    int cnt = 0;
                    string UpperFirstdata = UpperFirstChar(_data);
                    string sumText = string.Empty;
                    string paper_name = string.Empty;
                    List<string> arrayName = new List<string>();
                    
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                        
                            paper_name= reader["paper_name"].ToString();
                            arrayName.Add(paper_name);
                            sumText += paper_name + "\r\n"+ reader["contents_line"].ToString() +
                                "\r\n"+ reader["contents_value"].ToString().TrimEnd()+ "\r\n\r\n" + "--------------------------------------------------------" + "\r\n\r\n";                        
                            cnt++;                        
                        }
                       
                    }

                    string[] distArray = arrayName.ToArray();

                    distArray = distArray.Distinct().ToArray();
                    for (int i = 0; i < distArray.Length; i++)
                    {
                        MatchCollection matches = Regex.Matches(sumText, distArray[i]);
                        int cntName = matches.Count;
                        richTextBox.Text += string.Format("Search Words detected {0} : {1} " ,distArray[i] ,cntName)+"\r\n" + "--------------------------------------------------------" + "\r\n\r\n";
                    }
                    richTextBox.Text += sumText;

                    label2.Text = cnt.ToString();
                    ColorText(_data);
                    ColorText(_data.ToUpper());                  
                    ColorText(UpperFirstdata);

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
        private void Form2_Load(object sender, EventArgs e)
        {
             GetData();
        }


    }
}
