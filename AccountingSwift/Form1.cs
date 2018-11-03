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
using ExcelDataReader;
//Accounting form written by Colton Adkins.

namespace AccountingSwift
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=18.224.193.225;port=3306;username=databaseuser;password=1234; database = mainswiftdatabase");
        MySqlCommandBuilder scb;
        MySqlDataAdapter sda;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();

        }

        public static String[] states = new String[]{
        "Alabama", "Alaska", "Arizona", "Arkansas", "California",
        "Colorado", "Connecticut", "Delaware", "Florida", "Georgia",
        "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas",
        "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts",
        "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana",
        "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
        "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma",
        "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
        "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia",
        "Wisconsin", "Wyoming"
        };
        public static String[] branch = new String[]
        {
            "Columbus, OH", "Topeka, KS", "New York, NY", "Plano, TX", "El Segundo, CA"
        };
        public static String[] items = new String[]
        {
            "10G Needles[100]", "12G Needles[100]", "14G Needles[100]", "16G Needles[100]", "18G Needles[100]", "20G Needles[100]", "22G Needles[100] ","Drug Testkits[1]", "Gloves[100]", "IV Starters[1]", "Paternity Tests[1]", "Scrubs[1]", "Syringes[100]", "Urine Receptacles[100]", "Vials[100]"
        };
        public static String[] itemArray = new String[]
        {
            "15.00", "16.00", "17.00","18.00","18.50","20.00","11.75","12.00","15.00","1.00","9.50","5.50","20.00", "25.00", "25.00",  "75.00"
        };
        private void Form1_Load(object sender, EventArgs e)
        {
            //Adds in strings to the combo boxes when the form loads.
            for (int i = 0; i < 50; i++)
            {
                stateComboBox.Items.Add(states[i]);
                billingStateBox.Items.Add(states[i]);
            }
            for (int j = 0; j < 5; j++)
            {
                locationBox.Items.Add(branch[j]);
            }
            for (int h = 0; h < 15; h++)
            {
                inventoryList.Items.Add(items[h]);
                expenseEntry.Items.Add(items[h]);
            }
            connection.Open();
        }

        private void exitButton_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void subButton_Click_1(object sender, EventArgs e)
        {
            //Validates that there's no empty strings.
            if (string.IsNullOrEmpty(companyBoxEntry.Text) || string.IsNullOrEmpty(emailContact.Text) || string.IsNullOrEmpty(addressBox.Text) || string.IsNullOrEmpty(addressLine2.Text) || string.IsNullOrEmpty(cityBox.Text) || string.IsNullOrEmpty(stateComboBox.Text))
            {
                //If entry contains a empty string, sends a message to enter valid account info.
                MessageBox.Show("Please enter valid account information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Attempts to parse the data in these fields to verify it's numbers.
                    long companyZip = Int64.Parse(zipCodeBox.Text);
                    //If parsed, display message and submit to database.
                    //Ensures the ZIP and contact information were written correctly before sending to the database.
                    if (zipCodeBox.TextLength == 5 && contactNum.TextLength == 14 && IsValidEmail(emailContact.Text))
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO vendor(companyName, vendorNumber, vendorEmail, vendorAddress, vendorAddress2, vendorState, vendorCity, vendorZip) VALUES(@company, @vendor, @email, @address, @address2, @state, @city, @zip)", connection);
                        cmd.Parameters.AddWithValue("@company", companyBoxEntry.Text);
                        cmd.Parameters.AddWithValue("@vendor", contactNum.Text);
                        cmd.Parameters.AddWithValue("@email", emailContact.Text);
                        cmd.Parameters.AddWithValue("@address", addressBox.Text);
                        cmd.Parameters.AddWithValue("@address2", addressLine2.Text);
                        cmd.Parameters.AddWithValue("@state", stateComboBox.Text);
                        cmd.Parameters.AddWithValue("@city", cityBox.Text);
                        cmd.Parameters.AddWithValue("@zip", companyZip);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Data has been submitted successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MySqlCommand cmd1 = new MySqlCommand("Select * FROM vendor ORDER BY idvendor DESC LIMIT 1", connection);
                            MySqlDataReader dr1 = cmd1.ExecuteReader();
                            if (dr1.Read())
                            {
                                vendorID.Text = dr1["idvendor"].ToString();
                                dr1.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please ensure you've typed everything correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //If value is unable to be parsed, sends a message to enter numeric values for the fields.
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Resets the form.
            stateComboBox.SelectedIndex = -1;
            zipCodeBox.ResetText();
            cityBox.ResetText();
            addressLine2.ResetText();
            emailContact.ResetText();
            contactNum.ResetText();
            addressBox.ResetText();
            companyBoxEntry.ResetText();
            companyBoxEntry.Focus();
        }

        private void companyBoxEntry_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void emailContact_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void contactNum_TextChanged_1(object sender, EventArgs e)
        {
            ulong number;
            if (contactNum.Text.Length == 10 && ulong.TryParse(contactNum.Text, out number))
            {
                string num = contactNum.Text;
                contactNum.Text = String.Format("({0}) {1}-{2}", num.Substring(0, 3), num.Substring(3, 3), num.Substring(6));
            }
        }

        private void addressBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void addressLine2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cityBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void stateComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void zipCodeBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cvvInformation_TextChanged(object sender, EventArgs e)
        {

        }

        private void yearUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void monthUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void creditCardInformation_TextChanged(object sender, EventArgs e)
        {

        }

        private void billingStateBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void billingCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void billingZipCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void paymentButton_Click(object sender, EventArgs e)
        {
            //Validates that there's no empty strings.
            if (string.IsNullOrEmpty(billingAddress.Text) || string.IsNullOrEmpty(billingCity.Text) || string.IsNullOrEmpty(paymentType.Text) || string.IsNullOrEmpty(billingStateBox.Text) || string.IsNullOrEmpty(paymentType.Text))
            {
                //If entry contains a empty string, sends a message to enter valid account info.
                MessageBox.Show("Please enter valid account information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Attempts to parse the data in these fields to verify it's numbers.
                    long zip = Int64.Parse(billingZipCode.Text);
                    double amt = Double.Parse(amountPaid.Text);
                    long cc = Int64.Parse(creditCardInformation.Text);
                    long cvv = Int64.Parse(cvvInformation.Text);
                    double amtDue = Double.Parse(amountRemaining.Text);
                    double bal = amtDue - amt;
                    //If parsed, and data has been validated, display message and submit to database.
                    //Ensures that the ZIP is at least 5 characters, CVV is 3 or 4, card # is 16 digits and the amount paid isn't more than the amount due.
                    if(paymentType.SelectedIndex == 3 && cvvInformation.TextLength < 4)
                    {
                        MessageBox.Show("For American Express Cards, Please enter 4 Digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else if (billingZipCode.TextLength == 5 && cvvInformation.TextLength >= 3 && creditCardInformation.TextLength == 16 && amt <= amtDue)
                    {
                        calcTotal();
                        balBox.Text = bal.ToString("C2");
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO payment(idTesting, amountDue, creditCard, expMonth, expYear, CVV, billingAddress, billingState, billingCity, billingZip, amountPaid, balance, cardType) VALUES(@idtest, @amountDue, @cc, @month, @year, @cvv, @address, @state, @city, @zip, @amountPaid, @bal, @cardType)", connection);
                        cmd.Parameters.AddWithValue("@idtest", paymentAppliedTo.Text);
                        cmd.Parameters.AddWithValue("@amountDue", amtDue);
                        cmd.Parameters.AddWithValue("@cc", cc);
                        cmd.Parameters.AddWithValue("@month", monthUpDown.Value);
                        cmd.Parameters.AddWithValue("@year", yearUpDown.Value);
                        cmd.Parameters.AddWithValue("@cvv", cvv);
                        cmd.Parameters.AddWithValue("@address", billingAddress.Text);
                        cmd.Parameters.AddWithValue("@state", billingStateBox.Text);
                        cmd.Parameters.AddWithValue("@city", billingCity.Text);
                        cmd.Parameters.AddWithValue("@zip", zip);
                        cmd.Parameters.AddWithValue("@amountPaid", amt);
                        cmd.Parameters.AddWithValue("@bal", bal);
                        cmd.Parameters.AddWithValue("@cardType", paymentType.Text);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Data has been submitted successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please ensure you've typed everything correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //If value is unable to be parsed, sends a message to enter numeric values for the fields.
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Resets the form.
            monthUpDown.Value = 1;
            yearUpDown.Value = 2018;
            paymentType.SelectedIndex = -1;
            billingStateBox.SelectedIndex = -1;
            billingAddress.ResetText();
            billingCity.ResetText();
            billingZipCode.ResetText();
            creditCardInformation.ResetText();
            cvvInformation.ResetText();
            amountRemaining.ResetText();
            amountPaid.ResetText();
            balBox.ResetText();
            paymentAppliedTo.ResetText();
            custAccount.ResetText();
        }

        private void paymentExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void billingAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void locationBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void expenseButton_Click(object sender, EventArgs e)
        {

            //Validates that there's no empty strings.
            if (string.IsNullOrEmpty(expenseAmount.Text) || string.IsNullOrEmpty(expenseEntry.Text) || string.IsNullOrEmpty(locationBox.Text) || string.IsNullOrEmpty(accBox.Text) || string.IsNullOrEmpty(glCodes.Text))
            {
                //If entry contains a empty string, sends a message to enter valid account info.
                MessageBox.Show("Please enter valid account information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    //Attempts to parse the data in these fields to verify it's numbers.
                    double expenseAmt = Double.Parse(expenseAmount.Text);
                    long idVendor = Int64.Parse(vendorID.Text);
                    //If parsed, display message and submit to database.
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO expense(dateCreated, idvendor, expenseItem, expenseAmt, expenseQuantity, locationBox, glCode, paymentType, totalCost, shippingType) VALUES(@date, @vendorid, @item, @amount, @quantity, @location, @glcode, @acctype, @total, @shipping)", connection);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@vendorid", idVendor);
                    cmd.Parameters.AddWithValue("@item", expenseEntry.Text);
                    cmd.Parameters.AddWithValue("@amount", expenseAmt);
                    cmd.Parameters.AddWithValue("@quantity", quantityUpDown.Value);
                    cmd.Parameters.AddWithValue("@location", locationBox.Text);
                    cmd.Parameters.AddWithValue("@glcode", glCodes.Text);
                    cmd.Parameters.AddWithValue("@acctype", accBox.Text);
                    cmd.Parameters.AddWithValue("@total", totalBox.Text);
                    cmd.Parameters.AddWithValue("@shipping", shippingBox.Text);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Data has been submitted successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //If value is unable to be parsed, sends a message to enter numeric values for the fields.
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (MySqlException sql)
                {
                    MessageBox.Show(sql.Message, "Vendor ID does not Exist.");
                }
            }
            //Resets the form.
            locationBox.SelectedIndex = -1;
            glCodes.SelectedIndex = -1;
            accBox.SelectedIndex = -1;
            quantityUpDown.Value = 1;
            expenseAmount.ResetText();
            vendorID.ResetText();
            expenseAmount.ResetText();
            expenseEntry.SelectedIndex = -1;
            shippingBox.SelectedIndex = -1;
            totalBox.ResetText();
        }

        private void expenseExit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void expenseAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void vendorID_TextChanged(object sender, EventArgs e)
        {

        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT * From payment", connection);
            dt = new DataTable();
            sda.Fill(dt);
            dataGrid.DataSource = dt;
        }

        private void financialBox_Enter(object sender, EventArgs e)
        {

        }

        private void reportLabel_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void balBtn_Click(object sender, EventArgs e)
        {

        }

        private void cashflowBtn_Click(object sender, EventArgs e)
        {

        }

        private void genJournal_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT * FROM expense", connection);
            dt = new DataTable();
            sda.Fill(dt);
            generalJournal.DataSource = dt;
        }

        private void Inventory_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT * FROM Inventory", connection);
            dt = new DataTable();
            sda.Fill(dt);
            dataGrid.DataSource = dt;
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void labelq_Click(object sender, EventArgs e)
        {

        }

        private void inventoryList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void invSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inventoryList.Text))
            {
                MessageBox.Show("Please enter valid data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO inventory(itemType, amount) VALUES(@item, @amount)", connection);
                    cmd.Parameters.AddWithValue("@item", inventoryList.Text);
                    cmd.Parameters.AddWithValue("@amount", invQuantityUpDown.Value);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Data has been submitted successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Resets the information stored within quantity
            inventoryList.SelectedIndex = -1;
            invQuantityUpDown.Value = 1;
        }

        private void glCodes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void accBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void amountPaid_TextChanged(object sender, EventArgs e)
        {

        }
        //Created by Microsoft to ensure email validity.
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void vendorInfo_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT * FROM vendor", connection);
            dt = new DataTable();
            sda.Fill(dt);
            dataGrid.DataSource = dt;
        }

        private void vendorResetBtn_Click(object sender, EventArgs e)
        {
            stateComboBox.SelectedIndex = -1;
            zipCodeBox.ResetText();
            cityBox.ResetText();
            addressLine2.ResetText();
            emailContact.ResetText();
            contactNum.ResetText();
            addressBox.ResetText();
            companyBoxEntry.ResetText();
            companyBoxEntry.Focus();
        }

        private void expenseResetBtn_Click(object sender, EventArgs e)
        {
            locationBox.SelectedIndex = -1;
            glCodes.SelectedIndex = -1;
            accBox.SelectedIndex = -1;
            quantityUpDown.Value = 1;
            expenseAmount.ResetText();
            vendorID.ResetText();
            expenseAmount.ResetText();
            expenseEntry.ResetText();
            expenseEntry.SelectedIndex = -1;
            shippingBox.SelectedIndex = -1;
        }

        private void paymentResetBtn_Click(object sender, EventArgs e)
        {
            yearUpDown.Value = 2018;
            monthUpDown.Value = 1;
            paymentType.SelectedIndex = -1;
            billingStateBox.SelectedIndex = -1;
            billingAddress.ResetText();
            billingCity.ResetText();
            billingZipCode.ResetText();
            creditCardInformation.ResetText();
            cvvInformation.ResetText();
            amountRemaining.ResetText();
            amountPaid.ResetText();
            balBox.ResetText();
            totalBox.ResetText();
            paymentAppliedTo.ResetText();
            custAccount.ResetText();
            totalBox.ResetText();
        }

        private void invResetBtn_Click(object sender, EventArgs e)
        {
            inventoryList.SelectedIndex = -1;
            invQuantityUpDown.Value = 1;
        }

        private void balBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void expenseEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = expenseEntry.SelectedIndex;
                expenseAmount.Text = itemArray[i];
            }
            catch (IndexOutOfRangeException)
            {

            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                scb = new MySqlCommandBuilder(sda);
                sda.Update(dt);
                MessageBox.Show("Information updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void custAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query2 = "SELECT idPatient, testing.idTesting, orderedbyname, testname, testprice FROM patient, test, testing " +
                   "WHERE patient.idPatient = testing.patientid AND testing.testid = test.testid " +
                   "AND testing.testid = test.testid AND patient.SSN = " + custAccount.Text;
                MySqlCommand cmd1 = new MySqlCommand(query2, connection);
                cmd1.ExecuteNonQuery();
                MySqlDataReader read = cmd1.ExecuteReader();
                if (read.HasRows)
                {
                    MessageBox.Show("Customer Found.");
                    while (read.Read())
                    {
                        string amt = read["testprice"].ToString();
                        amountRemaining.Text = amt;
                        string testingid = read["idTesting"].ToString();
                        paymentAppliedTo.Text = testingid;
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found:");
                }
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Neither record was written to database.");
            }
        }
            

        private void dateTestTaken_ValueChanged(object sender, EventArgs e)
        {

        }

        private void paymentAppliedTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void amountRemaining_TextChanged(object sender, EventArgs e)
        {

        }

        private void paymentType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void shippingBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcTotal();
        }

        private void invQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void quantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            calcTotal();
        }
        public void calcTotal()
        {
            if (!String.IsNullOrEmpty(expenseAmount.Text))
            {
                double expense = Double.Parse(expenseAmount.Text);

                try
                {
                    int count = Convert.ToInt32(Math.Round(quantityUpDown.Value, 0));
                    double shipping = 0;
                    if (shippingBox.SelectedIndex == 2)
                    {
                        shipping = 7.95;
                    }
                    else if (shippingBox.SelectedIndex == 1)
                    {
                        shipping = 14.95;
                    }
                    else if (shippingBox.SelectedIndex == 0)
                    {
                        shipping = 21.95;
                    }
                    if (shippingBox.SelectedIndex > -1)
                    {
                        double tax = 1.06;
                        double total = ((count * expense) + shipping) * tax;
                        totalBox.Text = "$" + Math.Round(total, 2);
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void totalBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void glGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void genJournalReport_Click(object sender, EventArgs e)
        {

        }

        private void generalJournal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                scb = new MySqlCommandBuilder(sda);
                sda.Update(dt);
                MessageBox.Show("Information updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateReport_Click(object sender, EventArgs e)
        {
            try
            {
                scb = new MySqlCommandBuilder(sda);
                sda.Update(dt);
                MessageBox.Show("Information updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void glView_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT expenseItem, expenseAmt, vendor.idvendor FROM expense, vendor " +
                "WHERE expense.idvendor = vendor.idvendor", connection);
            dt = new DataTable();
            sda.Fill(dt);
            glGrid.DataSource = dt;
        }
        private void glView2_Click(object sender, EventArgs e)
        {
            sda = new MySqlDataAdapter("SELECT testprice, idPatient, idTesting FROM test, patient, testing " +
                "WHERE patient.idPatient = testing.patientid", connection);
            dt = new DataTable();
            sda.Fill(dt);
            glGrid.DataSource = dt;
        }
    }
}
