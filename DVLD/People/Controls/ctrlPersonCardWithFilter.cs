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
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSlected(int personID)
        {
            Action<int> handler = OnPersonSelected;

            if(handler != null) handler(personID);
        }

        private bool _ShowAddPereson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPereson; }
            set
            {
                _ShowAddPereson = value;
                btnAddNewPerson.Visible = _ShowAddPereson;
            }
        }

        private bool _ShowFilter = true;
        public bool ShowFilter
        {
            get { return _ShowFilter; }
            set
            {
                _ShowFilter = value;
                gbFilters.Visible = _ShowFilter;
            }
        }

        private int _PersonID;
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int personID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personID.ToString();

            FindNow();
        }

        private void FindNow()
        {
            switch(cbFilterBy.Text)
            {
                case "National No.":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(Convert.ToInt32(txtFilterValue.Text));
                    break;
                default:
                    break;
            }

            if(OnPersonSelected != null && _ShowFilter) OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int personID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon for more details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13) btnFind.PerformClick();

            bool IsNumericFilter = (cbFilterBy.Text == "Person ID");

            e.Handled = (IsNumericFilter && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required.");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, string.Empty);
            }
        }
    }
}