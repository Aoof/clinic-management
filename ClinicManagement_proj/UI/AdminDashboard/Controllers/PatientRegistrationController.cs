using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.BLL.Utils;
using System;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{

    /// <summary>
    /// Controller for the Patient Registration panel
    /// </summary>
    public class PatientRegistrationController : IPanelController
    {
        private readonly PatientService patientService = new PatientService();



        private readonly Panel panel;
        private DataGridView dgvPatients => (DataGridView)panel.Controls["dgvRegPatients"];
        private GroupBox grpPatientRegistration => (GroupBox)panel.Controls["grpPatientRegistration"];
        private TextBox txtPId => (TextBox)grpPatientRegistration.Controls["txtPId"];

        private TextBox txtPFName => (TextBox)grpPatientRegistration.Controls["txtPFName"];
        private TextBox txtPLName => (TextBox)grpPatientRegistration.Controls["txtPLName"];
        private TextBox txtMedicalNumber => (TextBox)grpPatientRegistration.Controls["txtMedicalNumber"];
        private DateTimePicker dateDoB => (DateTimePicker)grpPatientRegistration.Controls["dateDoB"];
        private TextBox txtPPhone => (TextBox)grpPatientRegistration.Controls["txtPPhone"];
        private TableLayoutPanel layoutPatientButtons => (TableLayoutPanel)grpPatientRegistration.Controls["layoutPatientButtons"];
        private Button btnPatientCreate => (Button)layoutPatientButtons.Controls["btnPCreate"];
        private Button btnPatientUpdate => (Button)layoutPatientButtons.Controls["btnPUpdate"];
        private Button btnPatientCancel => (Button)layoutPatientButtons.Controls["btnPCancel"];
        private Button btnPatientDelete => (Button)layoutPatientButtons.Controls["btnPDelete"];
        private Button btnPatientDisplay => (Button)layoutPatientButtons.Controls["btnPDisplay"];
        private Button btnPatientSearch => (Button)layoutPatientButtons.Controls["btnPSearch"];
        private Button btnPatientRefresh => (Button)layoutPatientButtons.Controls["btnPRefresh"];

        public Panel Panel => panel;

        public PatientRegistrationController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            btnPatientCreate.Click += new EventHandler(btnPatientCreate_Click);
            btnPatientUpdate.Click += new EventHandler(btnPatientUpdate_Click);
            btnPatientCancel.Click += new EventHandler(btnPatientCancel_Click);
            btnPatientDelete.Click += new EventHandler(btnPatientDelete_Click);
            btnPatientDisplay.Click += new EventHandler(btnPatientDisplay_Click);
            btnPatientSearch.Click += new EventHandler(btnPatientSearch_Click);
            dgvPatients.Click += new EventHandler(dgvPatients_Click);
        }

        public void OnShow()
        {
            LoadPatients();
            ResetPatientForm();
            
        }

        private void LoadPatients()
        {
            var users = patientService.GetAll();
            dgvPatients.DataSource = users;
            dgvPatients.AutoGenerateColumns = true;
        }

      
        /// <summary>
        /// Reset the patient registration form to initial state
        /// </summary>
        private void ResetPatientForm()
        {
            
            txtPFName.Text = string.Empty;
            txtPLName.Text = string.Empty;
            txtMedicalNumber.Text = string.Empty;
            dateDoB.Value = DateTime.Now;
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
            if(dgvPatients.CurrentRow != null)
            {
                txtPFName.Text = dgvPatients.CurrentRow.Cells[0].Value?.ToString();
                txtPLName.Text = dgvPatients.CurrentRow.Cells[1].Value?.ToString();
                dateDoB.Text = dgvPatients.CurrentRow.Cells[2].Value?.ToString();
                txtMedicalNumber.Text = dgvPatients.CurrentRow.Cells[3].Value?.ToString();
                txtPPhone.Text = dgvPatients.CurrentRow.Cells[4].Value?.ToString();

            }
        }

        /// <summary>
        /// Cancel patient form operation
        /// </summary>
        private void btnPatientCancel_Click(object sender, EventArgs e)
        {
            ResetPatientForm();
        }

   

        private void btnPatientDisplay_Click(object sender, EventArgs e)
        {
            LoadPatients();
            ResetPatientForm();
        }

        private void btnPatientSearch_Click(object sender, EventArgs e)
        {
            ResetPatientForm();
            if (!int.TryParse(txtPId.Text, out int id))
            {
                MessageBox.Show("Enter a valid Patient ID.");
                return;
            }

            var result = patientService.Search(id);

            if (result == null)
            {
                MessageBox.Show("Student not found.");

                return;
            }

            txtPId.Text = result.Id.ToString();
            txtPFName.Text = result.FirstName;
            txtPLName.Text = result.LastName;
            txtMedicalNumber.Text = result.InsuranceNumber.ToString();
            txtPPhone.Text = result.PhoneNumber;
            dateDoB.Text = result.DateOfBirth.ToString();

            dgvPatients.DataSource = new[] { result };

        }

        /// <summary>
        /// Submit patient form and Create new patient
        /// </summary>
        private void btnPatientCreate_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(txtPId.Text, out int id))
            {
                MessageBox.Show("Patient ID must be a number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPId.Text) ||
                string.IsNullOrWhiteSpace(txtPFName.Text)
                ||
                string.IsNullOrWhiteSpace(txtPLName.Text)
                ||
                string.IsNullOrWhiteSpace(dateDoB.Text)
                ||
                string.IsNullOrWhiteSpace(txtMedicalNumber.Text)
                ||
                string.IsNullOrWhiteSpace(txtPPhone.Text))
            {
                MessageBox.Show("All fields are required.");
                return;
            }
            if (patientService.Exists(id))
            {
                MessageBox.Show("A patient with this ID already exists. Please use a different ID.");
                return;
            }

            var dto = new PatientDTO
            {
                Id = id,
                FirstName = txtPFName.Text,
                LastName = txtPLName.Text,
                InsuranceNumber = txtMedicalNumber.Text,
                DateOfBirth = DateTime.Parse(dateDoB.Text),
                PhoneNumber = txtPPhone.Text
            };

            patientService.AddPatient(dto);
            LoadPatients();
            ResetPatientForm();
        }
        /// <summary>
        /// Submit patient form and Update existing patient
        /// </summary>
        private void btnPatientUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPId.Text, out int id))
            {
                MessageBox.Show("Enter a valid ID.");
                return;
            }

            if (!patientService.Exists(id))
            {
                MessageBox.Show("This patient ID does not exist. Cannot update.");
                return;
            }

            var dto = new PatientDTO
            {
                Id = id,
                FirstName = txtPFName.Text,
                LastName = txtPLName.Text,
                InsuranceNumber = txtMedicalNumber.Text,
                DateOfBirth = DateTime.Parse(dateDoB.Text),
                PhoneNumber = txtPPhone.Text
            };

            patientService.UpdatePatient(dto);
            LoadPatients();
            ResetPatientForm();
        }

        /// <summary>
        /// Delete selected patient
        /// </summary>
        private void btnPatientDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPId.Text, out int id))
            {
                MessageBox.Show("Enter a valid ID.");
                return;
            }

            if (!patientService.Exists(id))
            {
                MessageBox.Show("This patient ID does not exist. Cannot delete.");
                return;
            }


            patientService.DeletePatient(id);
            LoadPatients();
            ResetPatientForm();
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
