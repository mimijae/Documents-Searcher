using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 고문서검색기_doc_hwp_pdf
{
    public partial class upDateCategory : Form
    {
        public delegate void ChildFormSnedDataHandler(string message);
        public event ChildFormSnedDataHandler ChildFormEvent;
        public upDateCategory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save the category?", "Save Category", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string str = string.Empty;
                str = textBox1.Text;
                //델리게이트 이벤트를 통해 부모폼으로 데이터 전송
                this.ChildFormEvent(str);
                this.Close();
            }
            else
            {
                MessageBox.Show("Save canceled.");
            }
        }


    }
}
