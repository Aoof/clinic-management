using ClinicManagement_proj.BLL.Utils;
using System;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Doctor Management panel
    /// </summary>
    public class DoctorManagementController : IPanelController
    {
        private readonly Panel panel;
        private DataGridView dgvDoctors => (DataGridView)(panel.Controls["dgvDoctors"] 
                ?? throw new Exception("No control named [dgvDoctors] found in panel controls collection."));
        private GroupBox grpDoctorMgmt => (GroupBox)(panel.Controls["grpDoctorMgmt"] 
                ?? throw new Exception("No control named [grpDoctorMgmt] found in panel controls collection."));
        private TableLayoutPanel actionsLayout => (TableLayoutPanel)(grpDoctorMgmt.Controls["layoutDoctorActions"] 
                ?? throw new Exception("No control named [layoutDoctorActions] found in grpDoctorMgmt controls collection."));
        private TextBox txtDoctorFName => (TextBox)(grpDoctorMgmt.Controls["txtDoctorFName"] 
                ?? throw new Exception("No control named [txtDoctorFName] found in grpDoctorMgmt controls collection."));
        private TextBox txtDoctorLName => (TextBox)(grpDoctorMgmt.Controls["txtDoctorLName"] 
                ?? throw new Exception("No control named [txtDoctorLName] found in grpDoctorMgmt controls collection."));
        private ComboBox cmbSpecialization => (ComboBox)(grpDoctorMgmt.Controls["cmbSpecialization"] 
                ?? throw new Exception("No control named [cmbSpecialization] found in grpDoctorMgmt controls collection."));
        private Button btnDoctorCreate => (Button)(actionsLayout.Controls["btnDoctorCreate"] 
                ?? throw new Exception("No control named [btnDoctorCreate] found in actionsLayout controls collection."));
        private Button btnDoctorUpdate => (Button)(actionsLayout.Controls["btnDoctorUpdate"] 
                ?? throw new Exception("No control named [btnDoctorUpdate] found in actionsLayout controls collection."));
        private Button btnDoctorCancel => (Button)(actionsLayout.Controls["btnDoctorCancel"] 
                ?? throw new Exception("No control named [btnDoctorCancel] found in actionsLayout controls collection."));
        private Button btnDoctorDelete => (Button)(actionsLayout.Controls["btnDoctorDelete"] 
                ?? throw new Exception("No control named [btnDoctorDelete] found in actionsLayout controls collection."));

        public Panel Panel => panel;

        public DoctorManagementController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            btnDoctorCreate.Click += new EventHandler(btnDoctorCreate_Click);
            btnDoctorUpdate.Click += new EventHandler(btnDoctorUpdate_Click);
            btnDoctorCancel.Click += new EventHandler(btnDoctorCancel_Click);
            btnDoctorDelete.Click += new EventHandler(btnDoctorDelete_Click);
            dgvDoctors.Click += new EventHandler(dgvDoctors_Click);
        }

        public void OnShow()
        {
            ResetDoctorForm();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            // var doctors = doctorService.GetAllDoctors();
            // dgvDoctors.DataSource = doctors;
        }

        /// <summary>
        /// Reset the doctor management form to initial state
        /// </summary>
        private void ResetDoctorForm()
        {
            txtDoctorFName.Text = string.Empty;
            txtDoctorLName.Text = string.Empty;

            // TODO: Load specializations when enum is available
            cmbSpecialization.SelectedIndex = -1;

            dgvDoctors.ClearSelection();
        }

        /// <summary>
        /// Handle doctor selection from grid
        /// </summary>
        private void dgvDoctors_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.CurrentRow != null)
            {
                // Load selected doctor data
                txtDoctorFName.Text = dgvDoctors.CurrentRow.Cells["FirstName"].Value.ToString();
                txtDoctorLName.Text = dgvDoctors.CurrentRow.Cells["LastName"].Value.ToString();
            }
        }

        /// <summary>
        /// Cancel doctor form operation
        /// </summary>
        private void btnDoctorCancel_Click(object sender, EventArgs e)
        {
            ResetDoctorForm();
        }

        /// <summary>
        /// Submit doctor form and Create or doctor
        /// </summary>
        private void btnDoctorCreate_Click(object sender, EventArgs e)
        {
            NotificationManager.AddNotification("Doctor operation simulated!", NotificationType.Info);
            ResetDoctorForm();
        }

        /// <summary>
        /// Submit doctor form and Update existing doctor
        /// </summary>
        private void btnDoctorUpdate_Click(object sender, EventArgs e)
        {
            NotificationManager.AddNotification("Doctor operation simulated!", NotificationType.Info);
            ResetDoctorForm();
        }

        /// <summary>
        /// Delete selected doctor
        /// </summary>
        private void btnDoctorDelete_Click(object sender, EventArgs e)
        {
            NotificationManager.AddNotification("Doctor delete simulated!", NotificationType.Info);
            ResetDoctorForm();
        }

        public void OnHide()
        {
            // Cleanup when leaving panel
        }

        public void Cleanup()
        {
            // Dispose resources if needed
        }
    }
}
