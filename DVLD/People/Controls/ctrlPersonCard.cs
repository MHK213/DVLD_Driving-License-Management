using System.IO;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPerson _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            llEditPersonInfo.Enabled = false;

            _PersonID = -1;

            lblPersonID.Text = "[????]";
            lblFullName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblCountry.Text = "[????]";
            pbPersonImage.Image = global::DVLD.Properties.Resources.Male_512;
            pbGendor.Image = global::DVLD.Properties.Resources.Man_32;

        }

        private void _LoadPersonImage()
        {

            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {

            llEditPersonInfo.Enabled = true;

            _PersonID = _Person.PersonID;

            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();

            if (_Person.Gender == 1)
            {
                lblGendor.Text = "Female";
                pbPersonImage.Image = global::DVLD.Properties.Resources.Female_512;
            }
            else
            {
                lblGendor.Text = "Male";
                pbPersonImage.Image = global::DVLD.Properties.Resources.Male_512;
            }

            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblAddress.Text = _Person.Address;

            _LoadPersonImage();

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }

        public void LoadPersonInfo(int personID)
        {
            _Person = clsPerson.Find(personID);

            if (_Person == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"No Person with Person Id {personID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string nationalNo)
        {
            _Person = clsPerson.Find(nationalNo);

            if (_Person == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"No Person with NationalNo. {nationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }
    }
}