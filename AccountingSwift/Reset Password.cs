using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace AccountingSwift
{
    public partial class Reset_Password : Form
    {
        MySqlConnection connect = new MySqlConnection("datasource=18.224.193.225;port=3306;username=databaseuser;password=1234; database = mainswiftdatabase;");
        public Reset_Password()
        {
            InitializeComponent();
        }

        private void confPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void newPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void userID_TextChanged(object sender, EventArgs e)
        {

        }

        private void newPassBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userID.Text) || string.IsNullOrEmpty(newPass.Text) || string.IsNullOrEmpty(confPass.Text))
            {
                MessageBox.Show("Please enter correct information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newPass.Text != confPass.Text)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connect.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE employeelogin SET pwd = '" + newPass.Text + "' WHERE username='" + userID.Text + "'", connect);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Password has been changed successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        login log = new login();
                        log.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("UserID doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connect.Close();
            newPass.ResetText();
            confPass.ResetText();
            userID.ResetText();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
