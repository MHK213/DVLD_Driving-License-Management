using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.User
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            ctrlPersonCard1.ResetDefaultValues();

            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
            lblIsActive.ForeColor = Color.Black;

            _User = null;
            _UserID = -1;
        }

        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);

            _UserID = _User.UserID;

            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;

            if(_User.IsActive )
            {
                lblIsActive.Text = "Yes";
                lblIsActive.ForeColor = Color.Green;
            }
            else
            {
                lblIsActive.Text = "No";
                lblIsActive.ForeColor = Color.Red;
            }
        }

        public void LoadUserInfo(int userID)
        {
            _User = clsUser.FindByUserID(userID);

            if (_User == null)
            {
                _ResetDefaultValues();
                MessageBox.Show($"No User with UserID : {userID}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillUserInfo();
        }
    }
}