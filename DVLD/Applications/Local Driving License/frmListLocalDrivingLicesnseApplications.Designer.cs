
namespace DVLD.Applications.Local_Driving_License
{
    partial class frmListLocalDrivingLicesnseApplications
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 =
                new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 =
                new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 =
                new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 =
                new System.Windows.Forms.DataGridViewCellStyle();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalDrivingLicenseApplications = new System.Windows.Forms.DataGridView();

            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelApplicaitonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ScheduleTestsMenue = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.issueDrivingLicenseFirstTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).BeginInit();
            this.cmsApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 55, 85);
            this.lblTitle.Location = new System.Drawing.Point(200, 119);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(744, 35);
            this.lblTitle.TabIndex = 123;
            this.lblTitle.Text = "Local Driving License Applications";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD.Properties.Resources.Applications;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(496, 6);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(136, 108);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 128;
            this.pbPersonImage.TabStop = false;

            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Local_32;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(620, 68);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 130;
            this.pictureBox1.TabStop = false;

            // 
            // label1 - Filter By
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(31, 55, 85);
            this.label1.Location = new System.Drawing.Point(16, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 125;
            this.label1.Text = "Filter By:";

            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
                "None",
                "L.D.L.AppID",
                "National No.",
                "Full Name",
                "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(84, 196);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(172, 23);
            this.cbFilterBy.TabIndex = 127;

            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilterValue.Location = new System.Drawing.Point(264, 197);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(232, 23);
            this.txtFilterValue.TabIndex = 126;

            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.BackColor = System.Drawing.Color.White;
            this.btnAddNewApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewApplication.FlatAppearance.BorderSize = 1;
            this.btnAddNewApplication.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            this.btnAddNewApplication.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(235, 240, 247);
            this.btnAddNewApplication.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(220, 230, 242);
            this.btnAddNewApplication.Image =
                global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.ImageAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddNewApplication.Location = new System.Drawing.Point(1040, 166);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(94, 61);
            this.btnAddNewApplication.TabIndex = 129;
            this.btnAddNewApplication.Text = "";
            this.btnAddNewApplication.UseVisualStyleBackColor = false;
            this.btnAddNewApplication.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // dgvLocalDrivingLicenseApplications
            // 
            this.dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToDeleteRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToResizeRows = false;
            this.dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocalDrivingLicenseApplications.BackgroundColor =
                System.Drawing.Color.White;
            this.dgvLocalDrivingLicenseApplications.BorderStyle =
                System.Windows.Forms.BorderStyle.None;
            this.dgvLocalDrivingLicenseApplications.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersHeight = 32;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLocalDrivingLicenseApplications.ContextMenuStrip = this.cmsApplications;
            this.dgvLocalDrivingLicenseApplications.EditMode =
                System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLocalDrivingLicenseApplications.EnableHeadersVisualStyles = false;
            this.dgvLocalDrivingLicenseApplications.GridColor =
                System.Drawing.Color.FromArgb(225, 230, 237);
            this.dgvLocalDrivingLicenseApplications.Location =
                new System.Drawing.Point(10, 230);
            this.dgvLocalDrivingLicenseApplications.Margin =
                new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvLocalDrivingLicenseApplications.MultiSelect = false;
            this.dgvLocalDrivingLicenseApplications.Name =
                "dgvLocalDrivingLicenseApplications";
            this.dgvLocalDrivingLicenseApplications.ReadOnly = true;
            this.dgvLocalDrivingLicenseApplications.RowHeadersVisible = false;
            this.dgvLocalDrivingLicenseApplications.RowTemplate.Height = 27;
            this.dgvLocalDrivingLicenseApplications.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalDrivingLicenseApplications.Size =
                new System.Drawing.Size(1124, 266);
            this.dgvLocalDrivingLicenseApplications.TabIndex = 119;
            this.dgvLocalDrivingLicenseApplications.TabStop = false;

            // Header Style
            dataGridViewCellStyle2.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            dataGridViewCellStyle2.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode =
                System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersDefaultCellStyle =
                dataGridViewCellStyle2;

            // Default Cell Style
            dataGridViewCellStyle3.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle3.ForeColor =
                System.Drawing.Color.FromArgb(55, 65, 81);
            dataGridViewCellStyle3.SelectionBackColor =
                System.Drawing.Color.FromArgb(219, 231, 245);
            dataGridViewCellStyle3.SelectionForeColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            dataGridViewCellStyle3.Padding =
                new System.Windows.Forms.Padding(3, 1, 3, 1);
            dataGridViewCellStyle3.WrapMode =
                System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocalDrivingLicenseApplications.DefaultCellStyle =
                dataGridViewCellStyle3;

            // Alternating Rows
            dataGridViewCellStyle4.BackColor =
                System.Drawing.Color.FromArgb(246, 248, 251);
            dataGridViewCellStyle4.ForeColor =
                System.Drawing.Color.FromArgb(55, 65, 81);
            this.dgvLocalDrivingLicenseApplications.AlternatingRowsDefaultCellStyle =
                dataGridViewCellStyle4;

            // Row Headers
            dataGridViewCellStyle1.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            dataGridViewCellStyle1.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode =
                System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalDrivingLicenseApplications.RowHeadersDefaultCellStyle =
                dataGridViewCellStyle1;

            // 
            // cmsApplications
            // 
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.showDetailsToolStripMenuItem,
                this.toolStripSeparator2,
                this.editToolStripMenuItem,
                this.DeleteApplicationToolStripMenuItem,
                this.toolStripSeparator5,
                this.CancelApplicaitonToolStripMenuItem,
                this.toolStripSeparator1,
                this.ScheduleTestsMenue,
                this.toolStripSeparator3,
                this.issueDrivingLicenseFirstTimeToolStripMenuItem,
                this.toolStripSeparator4,
                this.showLicenseToolStripMenuItem,
                this.toolStripSeparator6,
                this.showPersonLicenseHistoryToolStripMenuItem
            });
            this.cmsApplications.BackColor = System.Drawing.Color.White;
            this.cmsApplications.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmsApplications.ForeColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.ShowImageMargin = true;
            this.cmsApplications.Size = new System.Drawing.Size(262, 344);

            // showDetailsToolStripMenuItem
            this.showDetailsToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.PersonDetails_32;
            this.showDetailsToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.showDetailsToolStripMenuItem.Text = "&Show Application Details";

            // toolStripSeparator2
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(258, 6);

            // editToolStripMenuItem
            this.editToolStripMenuItem.Image = global::DVLD.Properties.Resources.edit_32;
            this.editToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.editToolStripMenuItem.Text = "&Edit Application";

            // DeleteApplicationToolStripMenuItem
            this.DeleteApplicationToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.Delete_32_2;
            this.DeleteApplicationToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteApplicationToolStripMenuItem.Name =
                "DeleteApplicationToolStripMenuItem";
            this.DeleteApplicationToolStripMenuItem.Size =
                new System.Drawing.Size(261, 38);
            this.DeleteApplicationToolStripMenuItem.Text = "&Delete Application";

            // toolStripSeparator5
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(258, 6);

            // CancelApplicaitonToolStripMenuItem
            this.CancelApplicaitonToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.Delete_32;
            this.CancelApplicaitonToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelApplicaitonToolStripMenuItem.Name =
                "CancelApplicaitonToolStripMenuItem";
            this.CancelApplicaitonToolStripMenuItem.Size =
                new System.Drawing.Size(261, 38);
            this.CancelApplicaitonToolStripMenuItem.Text = "&Cancel Application";

            // toolStripSeparator1
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);

            // ScheduleTestsMenue
            this.ScheduleTestsMenue.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[] {
                    this.scheduleVisionTestToolStripMenuItem,
                    this.scheduleWrittenTestToolStripMenuItem,
                    this.scheduleStreetTestToolStripMenuItem
                });
            this.ScheduleTestsMenue.Image =
                global::DVLD.Properties.Resources.Schedule_Test_32;
            this.ScheduleTestsMenue.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleTestsMenue.Name = "ScheduleTestsMenue";
            this.ScheduleTestsMenue.Size = new System.Drawing.Size(261, 38);
            this.ScheduleTestsMenue.Text = "Sechdule &Tests";

            // scheduleVisionTestToolStripMenuItem
            this.scheduleVisionTestToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.Vision_Test_32;
            this.scheduleVisionTestToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scheduleVisionTestToolStripMenuItem.Name =
                "scheduleVisionTestToolStripMenuItem";
            this.scheduleVisionTestToolStripMenuItem.Size =
                new System.Drawing.Size(203, 38);
            this.scheduleVisionTestToolStripMenuItem.Text = "Schedule Vision Test";

            // scheduleWrittenTestToolStripMenuItem
            this.scheduleWrittenTestToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.Written_Test_32;
            this.scheduleWrittenTestToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scheduleWrittenTestToolStripMenuItem.Name =
                "scheduleWrittenTestToolStripMenuItem";
            this.scheduleWrittenTestToolStripMenuItem.Size =
                new System.Drawing.Size(203, 38);
            this.scheduleWrittenTestToolStripMenuItem.Text = "Schedule Written Test";

            // scheduleStreetTestToolStripMenuItem
            this.scheduleStreetTestToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.Street_Test_32;
            this.scheduleStreetTestToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scheduleStreetTestToolStripMenuItem.Name =
                "scheduleStreetTestToolStripMenuItem";
            this.scheduleStreetTestToolStripMenuItem.Size =
                new System.Drawing.Size(203, 38);
            this.scheduleStreetTestToolStripMenuItem.Text = "Schedule Street Test";

            // toolStripSeparator3
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(258, 6);

            // issueDrivingLicenseFirstTimeToolStripMenuItem
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Name =
                "issueDrivingLicenseFirstTimeToolStripMenuItem";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Size =
                new System.Drawing.Size(261, 38);
            this.issueDrivingLicenseFirstTimeToolStripMenuItem.Text =
                "&Issue Driving License (First Time)";

            // toolStripSeparator4
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(258, 6);

            // showLicenseToolStripMenuItem
            this.showLicenseToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.License_View_32;
            this.showLicenseToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size =
                new System.Drawing.Size(261, 38);
            this.showLicenseToolStripMenuItem.Text = "Show &License";

            // toolStripSeparator6
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(258, 6);

            // showPersonLicenseHistoryToolStripMenuItem
            this.showPersonLicenseHistoryToolStripMenuItem.Image =
                global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistoryToolStripMenuItem.ImageScaling =
                System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHistoryToolStripMenuItem.Name =
                "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size =
                new System.Drawing.Size(261, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text =
                "Show Person License History";

            // 
            // label2 - Records Count
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(31, 55, 85);
            this.label2.Location = new System.Drawing.Point(12, 510);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 120;
            this.label2.Text = "# Records:";

            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordsCount.ForeColor =
                System.Drawing.Color.FromArgb(37, 99, 170);
            this.lblRecordsCount.Location = new System.Drawing.Point(98, 510);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(22, 15);
            this.lblRecordsCount.TabIndex = 121;
            this.lblRecordsCount.Text = "??";

            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 1;
            this.btnClose.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(31, 55, 85);
            this.btnClose.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(235, 240, 247);
            this.btnClose.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(220, 230, 242);
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(31, 55, 85);
            this.btnClose.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation =
                System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.Location = new System.Drawing.Point(1024, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 32);
            this.btnClose.TabIndex = 118;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // frmListLocalDrivingLicesnseApplications
            // 
            this.AcceptButton = null;
            this.CancelButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate =
                System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1145, 539);

            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvLocalDrivingLicenseApplications);
            this.Controls.Add(this.btnClose);

            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListLocalDrivingLicesnseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Driving License Applications";

            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalDrivingLicenseApplications;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleTestsMenue;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem scheduleVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStreetTestToolStripMenuItem;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenseFirstTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelApplicaitonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    }
}
