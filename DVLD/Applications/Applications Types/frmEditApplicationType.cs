using DVLD.GlobalClass;
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

namespace DVLD.Applications.Applications_Types
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;

        public frmEditApplicationType(int applicationTypeID)
        {
            InitializeComponent();
            this._ApplicationTypeID = applicationTypeID;
        }

        private void _ResetDefaultValues()
        {
            lblApplicationTypeID.Text = "???";
            txtTitle.Text = string.Empty;
            txtFees.Text = string.Empty;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show("Could not find application type with ID: " + _ApplicationTypeID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationFees.ToString();

            txtTitle.Focus();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            string title = txtTitle.Text.Trim();

            if(string.IsNullOrEmpty(title))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty.");
            }
            else
            {
                errorProvider1.SetError(txtTitle, string.Empty);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            string feesText = txtFees.Text.Trim();

            if(string.IsNullOrEmpty(feesText))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, string.Empty);
            }

            if(!clsValidation.IsNumber(feesText))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees must be a valid number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, string.Empty);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();

            if(decimal.TryParse(txtFees.Text.Trim(), out decimal fees))
            {
                _ApplicationType.ApplicationFees = fees;
            }
            else
            {
                MessageBox.Show("Invalid fees value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(_ApplicationType.UpdateApplicationType())
            {
                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error Saving Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}