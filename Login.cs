using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace squitch
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void EnterBT_Click(object sender, EventArgs e)
        {
            Boolean UserValid;
            string connString = "Data Source=DESKTOP-A1RT4HN\\SQLEXPRESS; Initial Catalog=SQUITCHDB_SS;Integrated Security=True";
            try
            {
                using (SqlConnection myConnection = new SqlConnection(connString))
                {
                    string query = "SELECT * from Security WHERE UserID = '" + UserIDTB.Text + "' AND Password = '" + PasswordTB.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, myConnection);
                    myConnection.Open();
                    SqlDataReader readerReturnValue = cmd.ExecuteReader();

                    if (readerReturnValue.HasRows == true)
                    {
                        UserValid = true;
                        MessageBox.Show("You have been validated.");
                        var Mainform = new main();
                        Mainform.FormToShowOnClosing = this;
                        Mainform.Show();
                        this.Hide();
                    }
                    else
                    {
                        UserValid = false;
                        MessageBox.Show("Please try again. Your UserID or Password was not found.");
                        PasswordTB.Text = "";
                        UserIDTB.Text = "";
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void ExitBT_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
