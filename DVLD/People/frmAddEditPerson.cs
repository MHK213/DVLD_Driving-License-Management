using DVLD_Business;
using System;
using DVLD.GlobalClass;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew, Update }
        private enMode mode;

        private clsPerson _person;
        private int _personID;

        public frmAddEditPerson()
        {
            InitializeComponent();

            mode = enMode.AddNew;
        }

        public frmAddEditPerson(int personID)
        {
            InitializeComponent();

            _personID = personID;

            mode = enMode.Update;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox temp = ((TextBox)sender);

            if (string.IsNullOrEmpty(temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                errorProvider1.SetError(txtEmail, "");
                return;
            }

            if (!clsValidation.ValidateEmail(email))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail,
                    "Invalid Email Format!");
                return;
            }

            errorProvider1.SetError(txtEmail, "");
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string nationalNo = txtNationalNo.Text.Trim();

            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo,
                    "This field is required!");
                return;
            }

            if (clsPerson.IsPersonExist(nationalNo) &&
                nationalNo != _person.NationalNo)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo,
                    "National Number is used for another person!");
                return;
            }

            errorProvider1.SetError(txtNationalNo, "");
        }

        private void _rbChecked()
        {
            if (pbPersonImage.ImageLocation != null)
                return;

            pbPersonImage.Image = rbMale.Checked
                ? global::DVLD.Properties.Resources.Male_512
                : global::DVLD.Properties.Resources.Female_512;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _rbChecked();
        }

        private void _FillCountriesInComboBox()
        {
            cbCountry.Items.Clear();

            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            _rbChecked();

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindStringExact("Algeria");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

        }

        private void _loadData()
        {

            _person = clsPerson.Find(_personID);

            if (_person == null)
            {
                MessageBox.Show("This Form will be closed because no contact with this ID");
                this.Close();

                return;
            }

            lblPersonID.Text = _person.PersonID.ToString();
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;
            txtNationalNo.Text = _person.NationalNo;
            dtpDateOfBirth.Value = _person.DateOfBirth;

            if (_person.Gender == 1)
            {
                rbFemale.Checked = true;
                pbPersonImage.Image = global::DVLD.Properties.Resources.Female_512;
            }
            else
            {
                rbMale.Checked = true;
                pbPersonImage.Image = global::DVLD.Properties.Resources.Male_512;
            }

            txtPhone.Text = _person.Phone;
            txtEmail.Text = _person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_person.CountryInfo.CountryName);
            txtAddress.Text = _person.Address;

            if (_person.ImagePath != null) pbPersonImage.ImageLocation = _person.ImagePath;

            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

        }

        private bool _HandlePersonImage()
        {

            if (_person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_person.ImagePath != "")
                {

                    try
                    {
                        File.Delete(_person.ImagePath);
                    }
                    catch (IOException){}
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (mode == enMode.Update) _loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valide!, put the mouse on the red icon");
                return;
            }

            if (!_HandlePersonImage()) return;

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _person.FirstName = txtFirstName.Text.Trim();
            _person.SecondName = txtSecondName.Text.Trim();
            _person.ThirdName = txtThirdName.Text.Trim();
            _person.LastName = txtLastName.Text.Trim();
            _person.NationalNo = txtNationalNo.Text.Trim();
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Gender = (rbMale.Checked) ? Convert.ToByte(rbMale.Tag) : Convert.ToByte(rbFemale.Tag);
            _person.Phone = txtPhone.Text.Trim();
            _person.Email = txtEmail.Text.Trim();
            _person.NationalityCountryID = NationalityCountryID;
            _person.Address = txtAddress.Text.Trim();

            _person.ImagePath = (pbPersonImage.ImageLocation != null) ? pbPersonImage.ImageLocation : "";


            if (_person.Save())
            {
                lblPersonID.Text = _person.PersonID.ToString();
                lblTitle.Text = "Update Person";
                mode = enMode.Update;

                MessageBox.Show("Data Saved Succefully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);


                DataBack?.Invoke(this, _person.PersonID);
            }
            else MessageBox.Show("Error! Data is not Saved Successfuly");

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            _rbChecked();

            llRemoveImage.Visible = false;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }
    }
}