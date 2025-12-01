using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Appointment Management panel
    /// </summary>
    public class AppointmentManagementController : IPanelController
    {
        private readonly Panel panel;

        // Access to existing controls
        private GroupBox grpApptMgmt => (GroupBox)(panel.Controls["grpApptMgmt"] ?? throw new Exception("No control named [grpApptMgmt] found in panel controls collection."));
        private DataGridView dgvAppointments => (DataGridView)(panel.Controls["dgvAppointments"] ?? throw new Exception("No control named [dgvAppointments] found in layoutApptMgmt controls collection."));
        private TableLayoutPanel layoutApptButtons => (TableLayoutPanel)(grpApptMgmt.Controls["layoutApptButtons"] ?? throw new Exception("No control named [layoutApptButtons] found in layoutApptMgmt controls collection."));
        private Button btnApptCreate => (Button)(layoutApptButtons.Controls["btnApptCreate"] ?? throw new Exception("No control named [btnApptCreate] found in layoutApptButtons controls collection."));
        private Button btnApptUpdate => (Button)(layoutApptButtons.Controls["btnApptUpdate"] ?? throw new Exception("No control named [btnApptUpdate] found in layoutApptButtons controls collection."));
        private Button btnApptCancel => (Button)(layoutApptButtons.Controls["btnApptCancel"] ?? throw new Exception("No control named [btnApptCancel] found in layoutApptButtons controls collection."));
        private Button btnApptDisplay => (Button)(layoutApptButtons.Controls["btnApptDisplay"] ?? throw new Exception("No control named [btnApptDisplay] found in layoutApptButtons controls collection."));
        private Button btnApptSearch => (Button)(layoutApptButtons.Controls["btnApptSearch"] ?? throw new Exception("No control named [btnApptSearch] found in layoutApptButtons controls collection."));
        private TextBox txtApptDoctor => (TextBox)(grpApptMgmt.Controls["txtApptDoctor"] ?? throw new Exception("No control named [txtDoctor] found in layoutApptMgmt controls collection."));
        private TextBox txtApptPatient => (TextBox)(grpApptMgmt.Controls["txtApptPatient"] ?? throw new Exception("No control named [txtPatient] found in layoutApptMgmt controls collection."));
        private TextBox txtApptPhone => (TextBox)(grpApptMgmt.Controls["txtPhone"] ?? throw new Exception("No control named [txtPhone] found in layoutApptMgmt controls collection."));
        private DateTimePicker dtpApptDate => (DateTimePicker)(grpApptMgmt.Controls["dtpApptDate"] ?? throw new Exception("No control named [dtpApptDate] found in layoutApptMgmt controls collection."));
        private FlowLayoutPanel flpApptTimeSlots => (FlowLayoutPanel)(grpApptMgmt.Controls["flpApptTimeSlots"] ?? throw new Exception("No control named [flpApptTimeSlots] found in layoutApptMgmt controls collection."));
        private TextBox txtApptNotes => (TextBox)(grpApptMgmt.Controls["txtApptNotes"] ?? throw new Exception("No control named [txtApptNotes] found in layoutApptMgmt controls collection."));
        private ComboBox cmbApptStatus => (ComboBox)(grpApptMgmt.Controls["cmbApptStatus"] ?? throw new Exception("No control named [cmbStatus] found in layoutApptMgmt controls collection."));

        public Panel Panel => panel;

        public AppointmentManagementController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            // Populate status cmb
            cmbApptStatus.Items.Clear();
            cmbApptStatus.Items.AddRange(new string[] { "Pending", "Cancelled", "Confirmed", "Completed" });
            cmbApptStatus.SelectedIndex = 0; // Default to Pending

            // Wire event handlers (scaffolding, no implementation)
            btnApptCreate.Click += new EventHandler(btnApptCreate_Click);
            btnApptUpdate.Click += new EventHandler(btnApptUpdate_Click);
            btnApptCancel.Click += new EventHandler(btnApptCancel_Click);
            btnApptDisplay.Click += new EventHandler(btnApptDisplay_Click);
            btnApptSearch.Click += new EventHandler(btnApptSearch_Click);
            dgvAppointments.Click += new EventHandler(dgvAppointments_Click);
            dtpApptDate.ValueChanged += new EventHandler(dtpApptDate_ValueChanged);
            txtApptDoctor.TextChanged += new EventHandler(txtApptDoctor_TextChanged);
            txtApptPatient.TextChanged += new EventHandler(txtApptPatient_TextChanged);
        }

        public void OnShow()
        {
            // Theoretical: LoadAppointments();
            // ResetAppointmentForm();
        }

        private void LoadAppointments()
        {
            // Theoretical: var appointments = appointmentService.GetAll();
            // dgvAppointments.DataSource = appointments;
            // dgvAppointments.AutoGenerateColumns = true;
        }

        private void ResetAppointmentForm()
        {
            txtApptDoctor.Text = string.Empty;
            txtApptPatient.Text = string.Empty;
            dtpApptDate.Value = DateTime.Now;
            flpApptTimeSlots.Controls.Clear();
            txtApptNotes.Text = string.Empty;
            cmbApptStatus.SelectedIndex = 0;
        }

        private void EnterAppointmentEditMode()
        {
            // TODO: Implementation pending
        }

        private void dgvAppointments_Click(object sender, EventArgs e)
        {
            // Theoretical: Handle selection
        }

        private void btnApptCancel_Click(object sender, EventArgs e)
        {
            ResetAppointmentForm();
        }

        private void btnApptDisplay_Click(object sender, EventArgs e)
        {
            LoadAppointments();
            ResetAppointmentForm();
        }

        private void btnApptSearch_Click(object sender, EventArgs e)
        {
            // Theoretical: Search logic
        }

        private void btnApptCreate_Click(object sender, EventArgs e)
        {
            // Theoretical: Create logic
        }

        private void btnApptUpdate_Click(object sender, EventArgs e)
        {
            // Theoretical: Update logic
        }

        private void dtpApptDate_ValueChanged(object sender, EventArgs e)
        {
            // Theoretical: Load available time slots for selected date
            // flpApptTimeSlots.Controls.Clear();
            // var slots = appointmentService.GetAvailableTimeSlots(dtpApptDate.Value);
            // foreach (var slot in slots)
            // {
            //     var btn = new RadioButton { Text = slot.Time.ToString("HH:mm"), Appearance = Appearance.Button };
            //     flpApptTimeSlots.Controls.Add(btn);
            // }
        }

        private void txtApptDoctor_TextChanged(object sender, EventArgs e)
        {
            // Theoretical: Highlight based on selection
            // if (appointmentService.IsDoctorSelected(txtApptDoctor.Text)) txtDoctor.BackColor = Color.Green; else txtDoctor.BackColor = Color.Red;
        }

        private void txtApptPatient_TextChanged(object sender, EventArgs e)
        {
            // Theoretical: Highlight based on selection
            // if (appointmentService.IsPatientSelected(txtApptPatient.Text)) txtPatient.BackColor = Color.Green; else txtPatient.BackColor = Color.Red;
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
