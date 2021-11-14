using RestSharp;
using System;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = comboBox1.Text.Substring(0, 3);
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var url = "https://api.frankfurter.app/currencies";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string a = response.Content;
            char[] charsToTrim = { '}', '{' };
            string result = a.Trim(charsToTrim);
            string result2 = result.Replace('"', ' ');
            int b = 0;
            for (int x = 0; x < result2.Length; x++)
            {
                if (result2[x] == ',')
                {
                    b++;
                }
            }
            string[] cur1 = new string[b + 1];
            string[] cur2 = new string[b + 1];
            string d = "";
            for (int x = 0, y = 0; x < result2.Length; x++)
            {
                if (result2[x] == ',')
                {
                    string v = d.Trim();
                    cur1[y] = v;
                    cur2[y] = v;
                    y++;             
                    d = "";
                }
                else
                {
                    
                    d += result2[x];
                }
            }
            comboBox1.DataSource = cur1;
            comboBox1.SelectedIndex = 31;
            comboBox3.DataSource = cur2;
            comboBox3.SelectedIndex = 30;
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Text = comboBox3.Text.Substring(0, 3);
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string amount = textBox1.Text;
            string currency1 = comboBox1.Text.Substring(0, 3);
            string currency2 = comboBox3.Text.Substring(0, 3);
            if (currency1==currency2)
            {
                textBox2.Text = amount;
            }
            else
            {
                string host = $"https://api.frankfurter.app/latest?amount={amount}&from={currency1}&to={currency2}";
                var url = host;
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                int produce = response.Content.IndexOf(currency2, 0);
                string result = "";
                for (int x = produce + 5; x < response.Content.Length - 2; x++)
                {
                    result += response.Content[x];
                }
                textBox2.Text = result;
            }           
        }
    }
}
