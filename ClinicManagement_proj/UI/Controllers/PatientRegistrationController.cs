using ClinicManagement_proj.BLL;
using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.BLL.Utils;
using System;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{

    /// <summary>
    /// Controller for the Patient Registration panel
    /// </summary>
    public class PatientRegistrationController : IPanelController
    {
        private readonly PatientService patientService;



        private readonly Panel panel;
        private DataGridView dgvPatients => (DataGridView)(panel.Controls.Find("dgvRegPatients", true).FirstOrDefault() ?? throw new Exception("No control named [dgvRegPatients] found."));
        private TextBox txtPatientId => (TextBox)(panel.Controls.Find("txtPatientId", true).FirstOrDefault() ?? throw new Exception("No control named [txtPatientId] found."));
        private TextBox txtPFName => (TextBox)(panel.Controls.Find("txtPFName", true).FirstOrDefault() ?? throw new Exception("No control named [txtPFName] found."));
        private TextBox txtPLName => (TextBox)(panel.Controls.Find("txtPLName", true).FirstOrDefault() ?? throw new Exception("No control named [txtPLName] found."));
        private TextBox txtMedicalNumber => (TextBox)(panel.Controls.Find("txtMedicalNumber", true).FirstOrDefault() ?? throw new Exception("No control named [txtMedicalNumber] found."));
        private DateTimePicker dtpDoB => (DateTimePicker)(panel.Controls.Find("dtpDoB", true).FirstOrDefault() ?? throw new Exception("No control named [dtpDoB] found."));
        private TextBox txtPPhone => (TextBox)(panel.Controls.Find("txtPPhone", true).FirstOrDefault() ?? throw new Exception("No control named [txtPPhone] found."));
        private Button btnPatientCreate => (Button)(panel.Controls.Find("btnPCreate", true).FirstOrDefault() ?? throw new Exception("No control named [btnPCreate] found."));
        private Button btnPatientUpdate => (Button)(panel.Controls.Find("btnPUpdate", true).FirstOrDefault() ?? throw new Exception("No control named [btnPUpdate] found."));
        private Button btnPatientCancel => (Button)(panel.Controls.Find("btnPCancel", true).FirstOrDefault() ?? throw new Exception("No control named [btnPCancel] found."));
        private Button btnPatientDisplay => (Button)(panel.Controls.Find("btnPDisplay", true).FirstOrDefault() ?? throw new Exception("No control named [btnPDisplay] found."));
        private Button btnPatientSearch => (Button)(panel.Controls.Find("btnPSearch", true).FirstOrDefault() ?? throw new Exception("No control named [btnPSearch] found."));

        public Panel Panel => panel;

        public PatientRegistrationController(Panel panel)
        {
            this.patientService = ClinicManagementApp.PatientService;
            this.panel = panel;
        }

        public void Initialize()
        {
            btnPatientCreate.Click += new EventHandler(btnPatientCreate_Click);
            btnPatientUpdate.Click += new EventHandler(btnPatientUpdate_Click);
            btnPatientCancel.Click += new EventHandler(btnPatientCancel_Click);
            //btnPatientDelete.Click += new EventHandler(btnPatientDelete_Click);
            btnPatientDisplay.Click += new EventHandler(btnPatientDisplay_Click);
            btnPatientSearch.Click += new EventHandler(btnPatientSearch_Click);
            dgvPatients.Click += new EventHandler(dgvPatients_Click);

            // Ensure scrollbars are enabled
            dgvPatients.ScrollBars = ScrollBars.Both;
        }

        public void OnShow()
        {
            LoadPatients();
            ResetPatientForm();

        }

        private void LoadPatients()
        {
            var users = patientService.GetAllPatients();
            dgvPatients.DataSource = users;
            dgvPatients.AutoGenerateColumns = true;

            dgvPatients.Columns["Appointments"].Visible = false;
        }


        /// <summary>
        /// Reset the patient registration form to initial state
        /// </summary>
        private void ResetPatientForm()
        {
            dgvPatients.ClearSelection();
            txtPatientId.Text = string.Empty;
            txtPFName.Text = string.Empty;
            txtPLName.Text = string.Empty;
            txtMedicalNumber.Text = string.Empty;
            dtpDoB.Value = DateTime.Now;
            txtPPhone.Text = string.Empty;
        }

        /// <summary>
        /// Enter edit mode for patient registration
        /// </summary>
        private void EnterPatientEditMode()
        {
            // TODO: Implementation pending
        }

        /// <summary>
        /// Handle patient selection from grid
        /// </summary>
        private void dgvPatients_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow != null)
            {
                PatientDTO clickedOnPatient = (PatientDTO)dgvPatients.CurrentRow.DataBoundItem;
                txtPatientId.Text = clickedOnPatient.Id.ToString();
                txtPFName.Text = clickedOnPatient.FirstName;
                txtPLName.Text = clickedOnPatient.LastName;
                dtpDoB.Value = clickedOnPatient.DateOfBirth;
                txtMedicalNumber.Text = clickedOnPatient.InsuranceNumber;
                txtPPhone.Text = clickedOnPatient.PhoneNumber;
            }
        }

        /// <summary>
        /// Cancel patient form operation
        /// </summary>
        private void btnPatientCancel_Click(object sender, EventArgs e)
        {
            LoadPatients();
            ResetPatientForm();
        }



        private void btnPatientDisplay_Click(object sender, EventArgs e)
        {
            LoadPatients();
            ResetPatientForm();
        }

        private void btnPatientSearch_Click(object sender, EventArgs e)
        {
            string idText = txtPatientId.Text.Trim();
            if (string.IsNullOrWhiteSpace(idText))
            {
                NotificationManager.AddNotification("Please enter a Patient ID to search.", NotificationType.Warning);
                return;
            }

            if (!int.TryParse(idText, out int id))
            {
                NotificationManager.AddNotification("Enter a valid Patient ID.", NotificationType.Warning);
                return;
            }

            try
            {
                var results = patientService.Search(id);

                if (!results.Any())
                {
                    NotificationManager.AddNotification("Patient not found.", NotificationType.Info);
                    return;
                }

                var result = results.First();

                ResetPatientForm();

                txtPatientId.Text = result.Id.ToString();
                txtPFName.Text = result.FirstName;
                txtPLName.Text = result.LastName;
                txtMedicalNumber.Text = result.InsuranceNumber.ToString();
                txtPPhone.Text = result.PhoneNumber;
                dtpDoB.Text = result.DateOfBirth.ToString();

                dgvPatients.DataSource = new[] { result };
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error during patient search: {ex.Message}", NotificationType.Error);
            }

        }

        /// <summary>
        /// Submit patient form and Create new patient
        /// </summary>
        private void btnPatientCreate_Click(object sender, EventArgs e)
        {

            //if (!int.TryParse(txtPatientId.Text, out int id))
            //{
            //    MessageBox.Show("Patient ID must be a number.");
            //    return;
            //}

            if (/*string.IsNullOrWhiteSpace(txtPatientId.Text) ||*/
                string.IsNullOrWhiteSpace(txtPFName.Text)
                ||
                string.IsNullOrWhiteSpace(txtPLName.Text)
                ||
                string.IsNullOrWhiteSpace(dtpDoB.Text)
                ||
                string.IsNullOrWhiteSpace(txtMedicalNumber.Text)
                ||
                string.IsNullOrWhiteSpace(txtPPhone.Text))
            {
                NotificationManager.AddNotification("All fields are required.", NotificationType.Warning);
                return;
            }
            //if (patientService.Exists(id))
            //{
            //    MessageBox.Show("A patient with this ID already exists. Please use a different ID.");
            //    return;
            //}

            var dto = new PatientDTO
            {
                //Id = id,
                FirstName = txtPFName.Text,
                LastName = txtPLName.Text,
                InsuranceNumber = txtMedicalNumber.Text,
                DateOfBirth = dtpDoB.Value,
                PhoneNumber = txtPPhone.Text
            };

            try
            {
                patientService.AddPatient(dto);
                LoadPatients();
                ResetPatientForm();
                NotificationManager.AddNotification("Patient created successfully.", NotificationType.Info);
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error creating patient: {ex.Message}", NotificationType.Error);
            }
        }
        /// <summary>
        /// Submit patient form and Update existing patient
        /// </summary>
        private void btnPatientUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                NotificationManager.AddNotification("Please select a patient to update.", NotificationType.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPFName.Text)
                || string.IsNullOrWhiteSpace(txtPLName.Text)
                || string.IsNullOrWhiteSpace(txtMedicalNumber.Text)
                || string.IsNullOrWhiteSpace(txtPPhone.Text))
            {
                NotificationManager.AddNotification("Please fill out the required fields.", NotificationType.Warning);
                return;
            }

            PatientDTO selectedPatient = (PatientDTO)dgvPatients.CurrentRow.DataBoundItem;
            // Note: Id shouldnt be modifiable 
            selectedPatient.FirstName = txtPFName.Text;
            selectedPatient.LastName = txtPLName.Text;
            selectedPatient.DateOfBirth = dtpDoB.Value;
            selectedPatient.InsuranceNumber = txtMedicalNumber.Text;
            selectedPatient.PhoneNumber = txtPPhone.Text;

            try
            {
                patientService.UpdatePatient(selectedPatient);
                dgvPatients.Refresh();
                NotificationManager.AddNotification("Patient updated successfully.", NotificationType.Info);
            }
            catch (Exception ex)
            {
                NotificationManager.AddNotification($"Error updating patient: {ex.Message}", NotificationType.Error);
            }
        }

        /// <summary>
        /// Delete selected patient
        /// </summary>
        //private void btnPatientDelete_Click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(txtPatientId.Text, out int id))
        //    {
        //        MessageBox.Show("Enter a valid ID.");
        //        return;
        //    }

        //    if (!patientService.Exists(id))
        //    {
        //        MessageBox.Show("This patient ID does not exist. Cannot delete.");
        //        return;
        //    }


        //    patientService.DeletePatient(id);
        //    LoadPatients();
        //    ResetPatientForm();
        //}

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
