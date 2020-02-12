using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Net;
//inside your php or pastebin you will need to have already added SHA512 encrypted usernamepassword
namespace SecuredLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
                button1.Text = "Hide";
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                button1.Text = "Show";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string password = textBox2.Text;
            string userpass = user + password;

            using (SHA512 sha512Hash = SHA512.Create())
            {

                byte[] sourceBytes = Encoding.UTF8.GetBytes(userpass);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
               

                string[] linefrompastebin = new string[100];
                
                int i = 0;
                int maxLines = 0;
                bool _isAllowed = false;

                var url = ""; //insert link to php or pastebin
                var client = new WebClient();





                using (var stream = client.OpenRead(url))
                using (var reader = new StreamReader(stream))
                {
                    linefrompastebin[0] = "";

                    //Store lines from HTML into string
                    while ((linefrompastebin[i] = reader.ReadLine()) != null)
                    {
                        i++;
                    }
                    maxLines = i;
                }

                //do some line processing - compare user with whitelist
                for (i = 0; i < maxLines; i++)
                {
                    Console.WriteLine(linefrompastebin[i]);

                    if (linefrompastebin[i] == hash)
                    {
                        MessageBox.Show("Logged In!");
                        //this.Hide;
                        this.Hide();
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                        this.Close();
                    }

                }
            } 
        }

    }
}
