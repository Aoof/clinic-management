using ClinicManagement_proj.BLL;
using ClinicManagement_proj.BLL.DTO;
using ClinicManagement_proj.BLL.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Reports panel
    /// </summary>
    public class ReportsController : IPanelController
    {
        private readonly Panel panel;
        private ViewsService viewsService;
        private DoctorService doctorService;
        private PatientService patientService;

        private PatientDTO selectedPatient = null;
        private DoctorDTO selectedDoctor = null;

        private bool isUpdatingPatientCombo = false;
        private bool isUpdatingDoctorCombo = false;

        private PatientDTO selectedPatientClinical = null;
        private bool isUpdatingPatientClinicalCombo = false;

        private DoctorDTO selectedDoctorSchedule = null;
        private bool isUpdatingDoctorScheduleCombo = false;

        private DataGridView dgvPatientRecords => (DataGridView)(panel.Controls.Find("dgvPatientRecords", true).FirstOrDefault() ?? throw new Exception("No control named [dgvPatientRecords] found."));
        private ComboBox cmbVwPatientSelect => (ComboBox)(panel.Controls.Find("cmbVwPatientSelect", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwPatientSelect] found."));
        private DataGridView dgvUpcomingAppointments => (DataGridView)(panel.Controls.Find("dgvUpcomingAppointments", true).FirstOrDefault() ?? throw new Exception("No control named [dgvUpcomingAppointments] found."));
        private ComboBox cmbVwDoctorSelect => (ComboBox)(panel.Controls.Find("cmbVwDoctorSelect", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwDoctorSelect] found."));
        private DataGridView dgvDoctorTodaySchedule => (DataGridView)(panel.Controls.Find("dgvDoctorTodaySchedule", true).FirstOrDefault() ?? throw new Exception("No control named [dgvDoctorTodaySchedule] found."));
        private ComboBox cmbVwDoctorScheduleSelect => (ComboBox)(panel.Controls.Find("cmbVwDoctorScheduleSelect", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwDoctorScheduleSelect] found."));
        private DataGridView dgvPatientClinicalSummary => (DataGridView)(panel.Controls.Find("dgvPatientClinicalSummary", true).FirstOrDefault() ?? throw new Exception("No control named [dgvPatientClinicalSummary] found."));
        private ComboBox cmbVwPatientSelectClinical => (ComboBox)(panel.Controls.Find("cmbVwPatientSelectClinical", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwPatientSelectClinical] found."));

        public Panel Panel => panel;

        public ReportsController(Panel panel)
        {
            this.panel = panel;
            viewsService = ClinicManagementApp.ViewsService;
            doctorService = ClinicManagementApp.DoctorService;
            patientService = ClinicManagementApp.PatientService;
        }

        public void Initialize()
        {
            // Setup initial state
            cmbVwPatientSelect.TextChanged += new EventHandler(cmbVwPatientSelect_TextChanged);
            cmbVwPatientSelect.SelectedIndexChanged += new EventHandler(cmbVwPatientSelect_SelectedIndexChanged);
            cmbVwDoctorSelect.TextChanged += new EventHandler(cmbVwDoctorSelect_TextChanged);
            cmbVwDoctorSelect.SelectedIndexChanged += new EventHandler(cmbVwDoctorSelect_SelectedIndexChanged);
            cmbVwPatientSelectClinical.TextChanged += new EventHandler(cmbVwPatientSelectClinical_TextChanged);
            cmbVwPatientSelectClinical.SelectedIndexChanged += new EventHandler(cmbVwPatientSelectClinical_SelectedIndexChanged);
            cmbVwDoctorScheduleSelect.TextChanged += new EventHandler(cmbVwDoctorScheduleSelect_TextChanged);
            cmbVwDoctorScheduleSelect.SelectedIndexChanged += new EventHandler(cmbVwDoctorScheduleSelect_SelectedIndexChanged);
        }

        public void OnShow()
        {
            // Refresh reports when panel becomes visible
            LoadPatientRecords();
            LoadUpcomingAppointments();
            LoadDoctorTodaySchedule();
            LoadPatientClinicalSummary();
            ResetCombos();
        }

        private void LoadPatientRecords(int? patientId = null)
        {
            dgvPatientRecords.DataSource = viewsService.GetPatientRecordsSummary(patientId);

            // Hide ID columns
            dgvPatientRecords.Columns["PatientId"].Visible = false;
            dgvPatientRecords.Columns["AppointmentId"].Visible = false;

            // Set column headers
            dgvPatientRecords.Columns["PatientName"].HeaderText = "Patient Name";
            dgvPatientRecords.Columns["InsuranceNumber"].HeaderText = "Insurance Number";
            dgvPatientRecords.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvPatientRecords.Columns["PhoneNumber"].HeaderText = "Phone Number";
            dgvPatientRecords.Columns["VisitNumber"].HeaderText = "Visit Number";
            dgvPatientRecords.Columns["VisitDate"].HeaderText = "Visit Date";
            dgvPatientRecords.Columns["Age"].HeaderText = "Age";
            dgvPatientRecords.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvPatientRecords.Columns["Specialization"].HeaderText = "Specialization";
            dgvPatientRecords.Columns["VisitStatus"].HeaderText = "Visit Status";
            dgvPatientRecords.Columns["ClinicalNotes"].HeaderText = "Clinical Notes";
            dgvPatientRecords.Columns["DaysSinceVisit"].HeaderText = "Days Since Visit";

            // Set default alignment
            dgvPatientRecords.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Center-align specific columns
            dgvPatientRecords.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientRecords.Columns["Age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientRecords.Columns["VisitNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientRecords.Columns["VisitDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientRecords.Columns["DaysSinceVisit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Auto-resize columns
            dgvPatientRecords.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void LoadUpcomingAppointments(int? doctorId = null)
        {
            dgvUpcomingAppointments.DataSource = viewsService.GetUpcomingAppointments(doctorId);

            // Hide ID columns
            dgvUpcomingAppointments.Columns["AppointmentId"].Visible = false;
            dgvUpcomingAppointments.Columns["PatientId"].Visible = false;
            dgvUpcomingAppointments.Columns["DoctorId"].Visible = false;

            // Set column headers
            dgvUpcomingAppointments.Columns["PatientName"].HeaderText = "Patient Name";
            dgvUpcomingAppointments.Columns["PatientPhone"].HeaderText = "Phone Number";
            dgvUpcomingAppointments.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvUpcomingAppointments.Columns["AppointmentDate"].HeaderText = "Date";
            dgvUpcomingAppointments.Columns["HourOfDay"].HeaderText = "Hour";
            dgvUpcomingAppointments.Columns["MinuteOfHour"].HeaderText = "Minute";
            dgvUpcomingAppointments.Columns["Specialization"].HeaderText = "Specialization";
            dgvUpcomingAppointments.Columns["DayOfWeek"].HeaderText = "Day of Week";
            dgvUpcomingAppointments.Columns["AppointmentTime"].HeaderText = "Appointment Time";
            dgvUpcomingAppointments.Columns["Status"].HeaderText = "Status";
            dgvUpcomingAppointments.Columns["Notes"].HeaderText = "Notes";
            dgvUpcomingAppointments.Columns["DaysUntil"].HeaderText = "Days Until";

            // Set default alignment
            dgvUpcomingAppointments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Center-align specific columns
            dgvUpcomingAppointments.Columns["AppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["HourOfDay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["MinuteOfHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["DayOfWeek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["AppointmentTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUpcomingAppointments.Columns["DaysUntil"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Auto-resize columns
            dgvUpcomingAppointments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void LoadDoctorTodaySchedule(int? doctorId = null)
        {
            dgvDoctorTodaySchedule.DataSource = viewsService.GetDoctorTodaySchedule(doctorId);

            // Hide ID columns
            dgvDoctorTodaySchedule.Columns["AppointmentId"].Visible = false;
            dgvDoctorTodaySchedule.Columns["DoctorId"].Visible = false;
            dgvDoctorTodaySchedule.Columns["PatientId"].Visible = false;
            dgvDoctorTodaySchedule.Columns["HourOfDay"].Visible = false;
            dgvDoctorTodaySchedule.Columns["MinuteOfHour"].Visible = false;

            // Set column headers
            dgvDoctorTodaySchedule.Columns["DoctorName"].HeaderText = "Doctor Name";
            dgvDoctorTodaySchedule.Columns["Specialization"].HeaderText = "Specialization";
            dgvDoctorTodaySchedule.Columns["PatientName"].HeaderText = "Patient Name";
            dgvDoctorTodaySchedule.Columns["PatientPhone"].HeaderText = "Phone";
            dgvDoctorTodaySchedule.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvDoctorTodaySchedule.Columns["PatientAge"].HeaderText = "Age";
            dgvDoctorTodaySchedule.Columns["AppointmentDate"].HeaderText = "Date";
            dgvDoctorTodaySchedule.Columns["AppointmentTime"].HeaderText = "Time";
            dgvDoctorTodaySchedule.Columns["Status"].HeaderText = "Status";
            dgvDoctorTodaySchedule.Columns["Notes"].HeaderText = "Notes";

            // Set default alignment
            dgvDoctorTodaySchedule.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Center-align specific columns
            dgvDoctorTodaySchedule.Columns["AppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoctorTodaySchedule.Columns["AppointmentTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoctorTodaySchedule.Columns["PatientAge"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoctorTodaySchedule.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Auto-resize columns
            dgvDoctorTodaySchedule.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void LoadPatientClinicalSummary(int? patientId = null)
        {
            dgvPatientClinicalSummary.DataSource = viewsService.GetPatientClinicalSummary(patientId);

            // Hide ID columns
            dgvPatientClinicalSummary.Columns["PatientId"].Visible = false;

            // Set column headers
            dgvPatientClinicalSummary.Columns["PatientName"].HeaderText = "Patient Name";
            dgvPatientClinicalSummary.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            dgvPatientClinicalSummary.Columns["Age"].HeaderText = "Age";
            dgvPatientClinicalSummary.Columns["InsuranceNumber"].HeaderText = "Insurance Number";
            dgvPatientClinicalSummary.Columns["PhoneNumber"].HeaderText = "Phone Number";
            dgvPatientClinicalSummary.Columns["CompletedVisits"].HeaderText = "Completed Visits";
            dgvPatientClinicalSummary.Columns["CancelledVisits"].HeaderText = "Cancelled Visits";
            dgvPatientClinicalSummary.Columns["LastVisitDate"].HeaderText = "Last Visit Date";
            dgvPatientClinicalSummary.Columns["LastSeenDoctor"].HeaderText = "Last Seen Doctor";
            dgvPatientClinicalSummary.Columns["NextAppointmentDate"].HeaderText = "Next Appointment Date";

            // Set default alignment
            dgvPatientClinicalSummary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Center-align specific columns
            dgvPatientClinicalSummary.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientClinicalSummary.Columns["Age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientClinicalSummary.Columns["CompletedVisits"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientClinicalSummary.Columns["CancelledVisits"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientClinicalSummary.Columns["LastVisitDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPatientClinicalSummary.Columns["NextAppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Auto-resize columns
            dgvPatientClinicalSummary.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ResetCombos()
        {
            cmbVwPatientSelect.DataSource = patientService.GetAllPatients();
            cmbVwPatientSelect.DisplayMember = null;
            cmbVwPatientSelect.ValueMember = null;
            cmbVwPatientSelect.SelectedIndex = -1;
            cmbVwPatientSelect.Text = string.Empty;
            cmbVwPatientSelect.DropDownStyle = ComboBoxStyle.DropDown;
            cmbVwPatientSelect.BackColor = SystemColors.Window;

            cmbVwDoctorSelect.DataSource = doctorService.GetAllDoctors();
            cmbVwDoctorSelect.DisplayMember = null;
            cmbVwDoctorSelect.ValueMember = null;
            cmbVwDoctorSelect.SelectedIndex = -1;
            cmbVwDoctorSelect.Text = string.Empty;
            cmbVwDoctorSelect.DropDownStyle = ComboBoxStyle.DropDown;
            cmbVwDoctorSelect.BackColor = SystemColors.Window;

            cmbVwPatientSelectClinical.DataSource = patientService.GetAllPatients();
            cmbVwPatientSelectClinical.DisplayMember = null;
            cmbVwPatientSelectClinical.ValueMember = null;
            cmbVwPatientSelectClinical.SelectedIndex = -1;
            cmbVwPatientSelectClinical.Text = string.Empty;
            cmbVwPatientSelectClinical.DropDownStyle = ComboBoxStyle.DropDown;
            cmbVwPatientSelectClinical.BackColor = SystemColors.Window;

            cmbVwDoctorScheduleSelect.DataSource = doctorService.GetAllDoctors();
            cmbVwDoctorScheduleSelect.DisplayMember = null;
            cmbVwDoctorScheduleSelect.ValueMember = null;
            cmbVwDoctorScheduleSelect.SelectedIndex = -1;
            cmbVwDoctorScheduleSelect.Text = string.Empty;
            cmbVwDoctorScheduleSelect.DropDownStyle = ComboBoxStyle.DropDown;
            cmbVwDoctorScheduleSelect.BackColor = SystemColors.Window;

            selectedPatient = null;
            selectedDoctor = null;
            selectedPatientClinical = null;
            selectedDoctorSchedule = null;
        }

        private void cmbVwPatientSelect_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingPatientCombo) return;

            isUpdatingPatientCombo = true;

            string currentText = cmbVwPatientSelect.Text;
            int selStart = cmbVwPatientSelect.SelectionStart;
            int selLen = cmbVwPatientSelect.SelectionLength;
            var trimmed = currentText.Trim();
            var filtered = string.IsNullOrEmpty(trimmed) ? patientService.GetAllPatients() : patientService.Search(trimmed);
            if (filtered.Count == 0) filtered = patientService.GetAllPatients();
            cmbVwPatientSelect.DataSource = filtered;
            cmbVwPatientSelect.SelectedIndex = -1;
            if (cmbVwPatientSelect.Text != trimmed)
            {
                cmbVwPatientSelect.Text = trimmed;
                int newSelStart = Math.Min(selStart, trimmed.Length);
                int newSelLen = Math.Min(selLen, trimmed.Length - newSelStart);
                cmbVwPatientSelect.SelectionStart = newSelStart;
                cmbVwPatientSelect.SelectionLength = newSelLen;
            }
            if (filtered.Count == 1 && !string.IsNullOrEmpty(trimmed))
            {
                cmbVwPatientSelect.SelectedIndex = 0;
                dgvPatientRecords.Focus();
            }

            isUpdatingPatientCombo = false;
        }

        private void cmbVwPatientSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPatient = cmbVwPatientSelect.SelectedIndex != -1 ? (PatientDTO)cmbVwPatientSelect.SelectedItem : null;
            cmbVwPatientSelect.BackColor = selectedPatient != null ? System.Drawing.Color.LightGreen : SystemColors.Window;
            LoadPatientRecords(selectedPatient?.Id);
        }

        private void cmbVwDoctorSelect_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingDoctorCombo) return;

            isUpdatingDoctorCombo = true;

            string currentText = cmbVwDoctorSelect.Text;
            int selStart = cmbVwDoctorSelect.SelectionStart;
            int selLen = cmbVwDoctorSelect.SelectionLength;
            var trimmed = currentText.Trim();
            var filtered = string.IsNullOrEmpty(trimmed) ? doctorService.GetAllDoctors() : doctorService.Search(trimmed);
            if (filtered.Count == 0) filtered = doctorService.GetAllDoctors();
            cmbVwDoctorSelect.DataSource = filtered;
            cmbVwDoctorSelect.SelectedIndex = -1;
            if (cmbVwDoctorSelect.Text != trimmed)
            {
                cmbVwDoctorSelect.Text = trimmed;
                int newSelStart = Math.Min(selStart, trimmed.Length);
                int newSelLen = Math.Min(selLen, trimmed.Length - newSelStart);
                cmbVwDoctorSelect.SelectionStart = newSelStart;
                cmbVwDoctorSelect.SelectionLength = newSelLen;
            }
            if (filtered.Count == 1 && !string.IsNullOrEmpty(trimmed))
            {
                cmbVwDoctorSelect.SelectedIndex = 0;
                dgvUpcomingAppointments.Focus();
            }

            isUpdatingDoctorCombo = false;
        }

        private void cmbVwDoctorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDoctor = cmbVwDoctorSelect.SelectedIndex != -1 ? (DoctorDTO)cmbVwDoctorSelect.SelectedItem : null;
            cmbVwDoctorSelect.BackColor = selectedDoctor != null ? System.Drawing.Color.LightGreen : SystemColors.Window;
            LoadUpcomingAppointments(selectedDoctor?.Id);
        }

        private void cmbVwPatientSelectClinical_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingPatientClinicalCombo) return;

            isUpdatingPatientClinicalCombo = true;

            string currentText = cmbVwPatientSelectClinical.Text;
            int selStart = cmbVwPatientSelectClinical.SelectionStart;
            int selLen = cmbVwPatientSelectClinical.SelectionLength;
            var trimmed = currentText.Trim();
            var filtered = string.IsNullOrEmpty(trimmed) ? patientService.GetAllPatients() : patientService.Search(trimmed);
            if (filtered.Count == 0) filtered = patientService.GetAllPatients();
            cmbVwPatientSelectClinical.DataSource = filtered;
            cmbVwPatientSelectClinical.SelectedIndex = -1;
            if (cmbVwPatientSelectClinical.Text != trimmed)
            {
                cmbVwPatientSelectClinical.Text = trimmed;
                int newSelStart = Math.Min(selStart, trimmed.Length);
                int newSelLen = Math.Min(selLen, trimmed.Length - newSelStart);
                cmbVwPatientSelectClinical.SelectionStart = newSelStart;
                cmbVwPatientSelectClinical.SelectionLength = newSelLen;
            }
            if (filtered.Count == 1 && !string.IsNullOrEmpty(trimmed))
            {
                cmbVwPatientSelectClinical.SelectedIndex = 0;
                dgvPatientClinicalSummary.Focus();
            }

            isUpdatingPatientClinicalCombo = false;
        }

        private void cmbVwPatientSelectClinical_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPatientClinical = cmbVwPatientSelectClinical.SelectedIndex != -1 ? (PatientDTO)cmbVwPatientSelectClinical.SelectedItem : null;
            cmbVwPatientSelectClinical.BackColor = selectedPatientClinical != null ? System.Drawing.Color.LightGreen : SystemColors.Window;
            LoadPatientClinicalSummary(selectedPatientClinical?.Id);
        }

        private void cmbVwDoctorScheduleSelect_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingDoctorScheduleCombo) return;

            isUpdatingDoctorScheduleCombo = true;

            string currentText = cmbVwDoctorScheduleSelect.Text;
            int selStart = cmbVwDoctorScheduleSelect.SelectionStart;
            int selLen = cmbVwDoctorScheduleSelect.SelectionLength;
            var trimmed = currentText.Trim();
            var filtered = string.IsNullOrEmpty(trimmed) ? doctorService.GetAllDoctors() : doctorService.Search(trimmed);
            if (filtered.Count == 0) filtered = doctorService.GetAllDoctors();
            cmbVwDoctorScheduleSelect.DataSource = filtered;
            cmbVwDoctorScheduleSelect.SelectedIndex = -1;
            if (cmbVwDoctorScheduleSelect.Text != trimmed)
            {
                cmbVwDoctorScheduleSelect.Text = trimmed;
                int newSelStart = Math.Min(selStart, trimmed.Length);
                int newSelLen = Math.Min(selLen, trimmed.Length - newSelStart);
                cmbVwDoctorScheduleSelect.SelectionStart = newSelStart;
                cmbVwDoctorScheduleSelect.SelectionLength = newSelLen;
            }
            if (filtered.Count == 1 && !string.IsNullOrEmpty(trimmed))
            {
                cmbVwDoctorScheduleSelect.SelectedIndex = 0;
                dgvDoctorTodaySchedule.Focus();
            }

            isUpdatingDoctorScheduleCombo = false;
        }

        private void cmbVwDoctorScheduleSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDoctorSchedule = cmbVwDoctorScheduleSelect.SelectedIndex != -1 ? (DoctorDTO)cmbVwDoctorScheduleSelect.SelectedItem : null;
            cmbVwDoctorScheduleSelect.BackColor = selectedDoctorSchedule != null ? System.Drawing.Color.LightGreen : SystemColors.Window;
            LoadDoctorTodaySchedule(selectedDoctorSchedule?.Id);
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
