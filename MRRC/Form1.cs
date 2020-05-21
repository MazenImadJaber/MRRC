using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MRRCManagement;
using System.IO;

namespace MRRC
{
    public partial class Form1 : Form
    {
        string selectedRego;
        string selectedRegoRental;
        string selectedRegoRent;
        Vehicle selectedVehicle;
        Fleet fleet = new Fleet();
        CRM custmers = new CRM();
        private const int RegoCOL = 0;
        private const int IDCOL = 0;
        private string[] customerColumns = new string[] { "ID", "Title", "First Name", "Last Name","Gender", "DOB" };
    


        public Form1()
        {
            InitializeComponent();
            classComboBox.Items.Add(Vehicle.VehicleClass.Commercial);
            classComboBox.Items.Add(Vehicle.VehicleClass.Economy);
            classComboBox.Items.Add(Vehicle.VehicleClass.Family);
            classComboBox.Items.Add(Vehicle.VehicleClass.Luxury);
            transmissionComboBox.Items.Add(Vehicle.TransmissionType.Manual);
            transmissionComboBox.Items.Add(Vehicle.TransmissionType.Automatic);
            fuelComboBox.Items.Add(Vehicle.FuelType.Diesel);
            fuelComboBox.Items.Add(Vehicle.FuelType.Petrol);
            fuelComboBox.Items.Add(Vehicle.FuelType.Electric);
            fuelComboBox.Items.Add(Vehicle.FuelType.LPG);
            genderComboBox.Items.Add(Customer.Gender.Female);
            genderComboBox.Items.Add(Customer.Gender.Male);
            genderComboBox.Items.Add(Customer.Gender.NonBiary);
            genderComboBox.Items.Add(Customer.Gender.other);
            genderComboBox.Items.Add(Customer.Gender.TransFemale);
            genderComboBox.Items.Add(Customer.Gender.TransMale);
            string[] dataColumnsNames = new string[] {"Rego", "Make", "Model", "Year", "VehicleClass", "NumSeats", "Transmission",
                "Fuel", "GPS", "SunRoof", "Colour", "DailyRate" };

           

            SetUpGrid();
            SetupDataGridViewColumns();
            SetUpGridCustmer();
            SetUpRentalReport();
            SetUpCustomersSelectionBox();


        }
        private void SetUpCustomersSelectionBox()
        {
            listBox1.Items.Clear();
            foreach (Customer cust in custmers.Customers)
            {
                listBox1.Items.Add(cust);
            }
        }
        private void SetupDataGridViewColumns()
        {
            dataGridViewCustomers.ColumnCount = customerColumns.Length;
            for (int i = 0; i < customerColumns.Length; i++)
            {
                dataGridViewCustomers.Columns[i].Name = customerColumns[i];
            }
        }
        private void SetUpGrid()
        {
            FleetDataGrid.Rows.Clear();

            foreach (Vehicle vec in fleet.Vehicles)
            {
                FleetDataGrid.Rows.Add(vec.ToCSVString().Split(','));
            }
        }
        private void SetUpRentalReport()
        {
            int totalRented = 0;
            double totalRent = 0;
            rentalsDataGridView.Rows.Clear();

            foreach (Vehicle vec in fleet.GetFleet(true))
            {
                string[] input = new string[] { vec.VehicleRego, fleet.RentedBy(vec.VehicleRego).ToString(), "$"+vec.DailyRate.ToString() };
                rentalsDataGridView.Rows.Add(input);
                totalRented++;
                totalRent += vec.DailyRate;

            }
            rentedLabel.Text = totalRented.ToString();
            totalDailyRateLabel.Text = "$" + totalRent.ToString();

        }
        private void SetUpGridCustmer()
        {
            dataGridViewCustomers.Rows.Clear();

            foreach (Customer cust in custmers.GetCustomers())
            {

                dataGridViewCustomers.Rows.Add(cust.ToCSVString().Split(','));
            }
        }
    private void FleetDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = FleetDataGrid.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedRego = null;
            }
            else
            {
                selectedRego = (string)(FleetDataGrid.SelectedRows[0].Cells[RegoCOL].Value);
                selectedVehicle = GetVehicle(selectedRego);
                Remove.Enabled = true;
                Modify.Enabled = true;

            }
        }
    private void RemoveVehicle_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show(String.Format("Remove Vehicle {0}?", selectedRego), "Remove Vehicle confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                bool rented = fleet.IsRented(selectedRego);
                if (!rented)
                {
                    fleet.removeVehicle(selectedRego);

                }
                else
                {
                    DialogResult dialogResult1 = MessageBox.Show(String.Format("unsuccessful! vehicle is rented"), "Remove Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            SetUpGrid();

        }
        private void custRemoveButton_Click(object sender, EventArgs e)
        {
            bool exists = false;
          if ( int.TryParse(selectedIDTextBox.Text, out int ID))
            {
                foreach(Customer cust in custmers.Customers)
                {
                    if (cust.ID == ID)
                    {
                        exists = true;
                    }
                }
                if (exists)
                {

                   DialogResult dialogResult1 = MessageBox.Show(String.Format("Remove Customer {0}?", ID), "Remove Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        if (fleet.IsRenting(ID))
                        {
                            DialogResult dialogResult2 = MessageBox.Show(String.Format("Customer {0} is rentig!", ID), "Remove Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            
                            custmers.RemoveCustomer(GetCustomer(ID), fleet);
                            SetUpGridCustmer();
                        }
                    }

                }
                else
                {
                    DialogResult dialogResult3 = MessageBox.Show(String.Format("ID Does not Exists!"), "Remove Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(String.Format("ID must be a number!"), "Remove Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void ModifyVehicle_Click(object sender, EventArgs e)
        {

            groupBox1.Enabled = false;
            FleetDataGrid.Enabled = false;
            groupBox2.Enabled = true;
            groupBox2.Visible = true;
            groupBox2.Text = "Modify Vehicle";
            regoTextBox.Text = selectedRego;
            makeTextBox.Text = selectedVehicle.Make;
            modelTextBox.Text = selectedVehicle.Model;
            classComboBox.Text = selectedVehicle.Class.ToString();
            yearTextBox.Text = selectedVehicle.Year.ToString();
            transmissionComboBox.Text = selectedVehicle.Transmission.ToString();
            seatsNumericUpDown.Value = selectedVehicle.NumSeats;
            fuelComboBox.Text = selectedVehicle.Fuel.ToString();
            GPSCheckbox.Checked = selectedVehicle.GPSstatus;
            sunRoofCheckBox.Checked = selectedVehicle.SunRoof;
            colourTextBox.Text = selectedVehicle.Colour;
            rateNumericUpDown.Maximum = 1000;

            rateNumericUpDown.Value = decimal.Parse(selectedVehicle.DailyRate.ToString());


        }
        private Vehicle GetVehicle(string rego)
        {
            foreach (Vehicle vec in fleet.GetFleet())
            {
                if (vec.VehicleRego == rego)
                {
                    return vec;
                }

            }


            return null;
        }

        private Customer GetCustomer(int ID)
        {
            foreach (Customer cust in custmers.GetCustomers())
            {
                if (cust.ID == ID)
                {
                    return cust;
                }

            }


            return null;
        }

        private void cancelModifyButton_Click(object sender, EventArgs e)
        {

            groupBox1.Enabled = true;
            FleetDataGrid.Enabled = true;
            groupBox2.Enabled = false;
            groupBox2.Visible = false;

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (groupBox2.Text == "Modify Vehicle")
            {
                fleet.Vehicles.Remove(selectedVehicle);


                Vehicle updatedVehicle = new Vehicle(selectedRego, makeTextBox.Text, modelTextBox.Text, int.Parse(yearTextBox.Text),
                    (Vehicle.VehicleClass)classComboBox.SelectedItem, (int)seatsNumericUpDown.Value, (Vehicle.TransmissionType)transmissionComboBox.SelectedItem,
                    (Vehicle.FuelType)fuelComboBox.SelectedItem, GPSCheckbox.Checked, sunRoofCheckBox.Checked, colourTextBox.Text, (double)rateNumericUpDown.Value);
                fleet.Vehicles.Add(updatedVehicle);
                fleet.saveToFile();
                SetUpGrid();
                groupBox1.Enabled = true;
                FleetDataGrid.Enabled = true;
                groupBox2.Enabled = false;
                groupBox2.Visible = false;
            }
            else
            {
                if (int.TryParse(yearTextBox.Text, out int year))
                {
                    Vehicle newVehicle = new Vehicle(regoTextBox.Text.ToUpper(), makeTextBox.Text, modelTextBox.Text, year, (Vehicle.VehicleClass)classComboBox.SelectedItem);
                    newVehicle.NumSeats = (int)seatsNumericUpDown.Value;
                    newVehicle.GPSstatus = GPSCheckbox.Checked;
                    newVehicle.SunRoof = sunRoofCheckBox.Checked;
                    newVehicle.DailyRate = (double)rateNumericUpDown.Value;

                    if (transmissionComboBox.SelectedItem != null)
                    {
                        newVehicle.Transmission = (Vehicle.TransmissionType)transmissionComboBox.SelectedItem;

                    }
                    if (fuelComboBox != null)
                    {
                        newVehicle.Fuel = (Vehicle.FuelType)fuelComboBox.SelectedItem;

                    }
                    if (colourTextBox.Text != null)
                    {
                        newVehicle.Colour = colourTextBox.Text;
                    }

                    if (fleet.addVehicle(newVehicle))
                    {
                        fleet.saveToFile();
                        SetUpGrid();
                        groupBox1.Enabled = true;
                        FleetDataGrid.Enabled = true;
                        groupBox2.Enabled = false;
                        groupBox2.Visible = false;
                        label13.Visible = false;
                        label14.Visible = false;
                        label15.Visible = false;
                        label16.Visible = false;
                        label17.Visible = false;
                    }
                    else
                    {
                        DialogResult dialogResult4 = MessageBox.Show(String.Format("Car rego must by of length 6 and not in fleet"), "ADD Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        label13.Visible = true;
                        label14.Visible = true;
                        label15.Visible = true;
                        label16.Visible = true;
                        label17.Visible = true;
                    }


                }
                else
                {
                    DialogResult dialogResult3 = MessageBox.Show(String.Format("Year must be a number"), "ADD Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    label16.Visible = true;
                    label17.Visible = true;
                }

            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            submitButton.Enabled = false;
            groupBox1.Enabled = false;
            FleetDataGrid.Enabled = false;
            groupBox2.Enabled = true;
            groupBox2.Visible = true;
            groupBox2.Text = "Add Vehicle";
            regoTextBox.Enabled = true;
            regoTextBox.Text = null;
            makeTextBox.Text = null;
            modelTextBox.Text = null;
            classComboBox.Text = null;
            yearTextBox.Text = null;
            transmissionComboBox.Text = null;
            seatsNumericUpDown.Value = 4;
            fuelComboBox.Text = null;
            GPSCheckbox.Checked = false;
            sunRoofCheckBox.Checked = false;
            colourTextBox.Text = null;
            rateNumericUpDown.Value = 0;


        }

        private void regoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (regoTextBox.Text != null && makeTextBox.Text != null && modelTextBox.Text != null && yearTextBox.Text != null && classComboBox.SelectedItem != null)
            {
                submitButton.Enabled = true;
            }
        }
        private void yearTextBox_TextChanged(object sender, EventArgs e)
        {
            if (regoTextBox.Text != null && makeTextBox.Text != null && modelTextBox.Text != null && yearTextBox.Text != null && classComboBox.SelectedItem != null)
            {
                submitButton.Enabled = true;
            }
        }
   
        

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void custModButton_Click(object sender, EventArgs e)
        {
            IDTextBox.Enabled = false;
            bool exists = false;
            if (int.TryParse(selectedIDTextBox.Text, out int ID))
            {
                foreach (Customer cust in custmers.Customers)
                {
                    if (cust.ID == ID)
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    Customer selectedCustomer = GetCustomer(ID);
                    IDTextBox.Text = ID.ToString();
                    addCustmerGroupBox.Text = "Modify";
                    addCustmerGroupBox.Visible = true;
                    ModifyCustGroupBox.Enabled = false;
                    titleTextBox.Text = selectedCustomer.Title;
                    firstNameTextBox.Text = selectedCustomer.FirstName;
                    lastNameTextBox.Text = selectedCustomer.LastName;
                    genderComboBox.Text = selectedCustomer.GENDER.ToString();
                    dateTimePicker1.Text = selectedCustomer.DOB;

                }
                else
                {
                    DialogResult dialogResult3 = MessageBox.Show(String.Format("ID Does not Exists"), "Modify Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(String.Format("ID must be a number"), "Modify Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

           


        }

        private void custSubmitButton_Click(object sender, EventArgs e)
        {
            if (addCustmerGroupBox.Text == "Modify")
            {
                custmers.Customers.Remove(GetCustomer(int.Parse(IDTextBox.Text)));
            custmers.Customers.Add(new Customer(int.Parse(IDTextBox.Text), titleTextBox.Text, firstNameTextBox.Text,
                lastNameTextBox.Text, (Customer.Gender)genderComboBox.SelectedItem, dateTimePicker1.Value.ToShortDateString()));

                custmers.saveToFile();
                SetUpGridCustmer();
                SetUpCustomersSelectionBox();
                addCustmerGroupBox.Visible = false;
                ModifyCustGroupBox.Enabled = true;


            }
            else
            {
                bool exists = false;
                if (int.TryParse(IDTextBox.Text, out int ID))
                {
                    foreach (Customer cust in custmers.Customers)
                    {
                        if (cust.ID == ID)
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        custmers.Customers.Add(new Customer(ID, titleTextBox.Text, firstNameTextBox.Text,
               lastNameTextBox.Text, (Customer.Gender)genderComboBox.SelectedItem, dateTimePicker1.Value.ToShortDateString()));

                        custmers.saveToFile();
                        SetUpGridCustmer();
                        SetUpCustomersSelectionBox();
                        addCustmerGroupBox.Visible = false;
                        ModifyCustGroupBox.Enabled = true;

                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(String.Format("ID must Unique"), "ADD Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else 
                {
                    DialogResult dialogResult = MessageBox.Show(String.Format("ID must be a number"), "ADD Customer", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                    
            }


        }

        private void custAddButton_Click(object sender, EventArgs e)
        {
            addCustmerGroupBox.Text = "Add";
            addCustmerGroupBox.Visible = true;
            ModifyCustGroupBox.Enabled = false;
            IDTextBox.Enabled = true;
            IDTextBox.Text = null;
            titleTextBox.Text = null;
            firstNameTextBox.Text = null;
            lastNameTextBox.Text = null;
            genderComboBox.Text = null;
           
        }

        private void custCancelBotton_Click(object sender, EventArgs e)
        {
            addCustmerGroupBox.Visible = false;
            ModifyCustGroupBox.Enabled = true;

        }

        private void genderComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (IDTextBox.Text != null && titleTextBox.Text != null && firstNameTextBox.Text != null && lastNameTextBox.Text != null)
            {
                custSubmitButton.Enabled = true;
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void rentalsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = rentalsDataGridView.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedRegoRental = null;
            }
            else
            {
                selectedRegoRental = (string)(rentalsDataGridView.SelectedRows[0].Cells[RegoCOL].Value);
               
                Remove.Enabled = true;
                Modify.Enabled = true;

            }
        }

        private void returnVehicalButton_Click(object sender, EventArgs e)
        {
            fleet.RetrunCar(selectedRegoRental);
            SetUpRentalReport();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = null;
            button1.Enabled = true;
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            rentDataGridView.Rows.Clear();
            foreach(Vehicle vec in fleet.Search())
            {
                rentDataGridView.Rows.Add(vec.ToCSVString().Split(','));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Enquiries....")
            {
                rentDataGridView.Rows.Clear();
                foreach (Vehicle vec in fleet.Search((double)numericUpDown1.Value, (double)numericUpDown2.Value))
                {
                    rentDataGridView.Rows.Add(vec.ToCSVString().Split(','));
                }
            }
            else
            {
                rentDataGridView.Rows.Clear();
                foreach (Vehicle vec in fleet.Search((double)numericUpDown1.Value, (double)numericUpDown2.Value,textBox1.Text))
                {
                    rentDataGridView.Rows.Add(vec.ToCSVString().Split(','));
                }
            }
           
        }

        private void rentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int rowsCount = rentDataGridView.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1)
            {
                selectedRegoRent = null;
            }
            else
            {
                selectedRegoRent = (string)(rentDataGridView.SelectedRows[0].Cells[RegoCOL].Value);
                label28.Text ="$"+ GetVehicle(selectedRegoRent).DailyRate.ToString();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            label28.Text = "$" +( GetVehicle(selectedRegoRent).DailyRate*(double)numericUpDown3.Value).ToString();
        }

        private void rentButton_Click(object sender, EventArgs e)
        {
            Customer selectedCust = (Customer)listBox1.SelectedItem;
            DialogResult dialogResult = MessageBox.Show(String.Format(selectedCust.Title + " " + selectedCust.FirstName + " " + selectedCust.LastName + ", Are You sure you want to rent " + selectedRegoRent + " for a total of " +label28.Text),
                "Remove Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
            fleet.rentCar(selectedRegoRent, selectedCust.ID);
            SetUpRentalReport();
            }
               
        }
    }
}
