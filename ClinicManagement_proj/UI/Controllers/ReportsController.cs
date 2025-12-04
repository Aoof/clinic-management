using System;
using System.Windows.Forms;
using ClinicManagement_proj.BLL;
using ClinicManagement_proj.BLL.Services;
using ClinicManagement_proj.BLL.DTO;
using System.Drawing;
using System.Linq;

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

        private DataGridView dgvPatientRecords => (DataGridView)(panel.Controls.Find("dgvPatientRecords", true).FirstOrDefault() ?? throw new Exception("No control named [dgvPatientRecords] found."));
        private ComboBox cmbVwPatientSelect => (ComboBox)(panel.Controls.Find("cmbVwPatientSelect", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwPatientSelect] found."));
        private DataGridView dgvUpcomingAppointments => (DataGridView)(panel.Controls.Find("dgvUpcomingAppointments", true).FirstOrDefault() ?? throw new Exception("No control named [dgvUpcomingAppointments] found."));
        private ComboBox cmbVwDoctorSelect => (ComboBox)(panel.Controls.Find("cmbVwDoctorSelect", true).FirstOrDefault() ?? throw new Exception("No control named [cmbVwDoctorSelect] found."));
        private DataGridView dgvDoctorTodaySchedule => (DataGridView)(panel.Controls.Find("dgvDoctorTodaySchedule", true).FirstOrDefault() ?? throw new Exception("No control named [dgvDoctorTodaySchedule] found."));
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

            // Format columns
            if (dgvPatientRecords.Columns.Contains("PatientId")) dgvPatientRecords.Columns["PatientId"].Visible = false;
            if (dgvPatientRecords.Columns.Contains("PatientName")) dgvPatientRecords.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvPatientRecords.Columns.Contains("InsuranceNumber")) dgvPatientRecords.Columns["InsuranceNumber"].HeaderText = "Insurance Number";
            if (dgvPatientRecords.Columns.Contains("DateOfBirth")) dgvPatientRecords.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            if (dgvPatientRecords.Columns.Contains("PhoneNumber")) dgvPatientRecords.Columns["PhoneNumber"].HeaderText = "Phone Number";
            if (dgvPatientRecords.Columns.Contains("TotalAppointments")) dgvPatientRecords.Columns["TotalAppointments"].HeaderText = "Total Appointments";
            if (dgvPatientRecords.Columns.Contains("LastAppointmentDate")) dgvPatientRecords.Columns["LastAppointmentDate"].HeaderText = "Last Appointment Date";

            dgvPatientRecords.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvPatientRecords.Columns.Contains("DateOfBirth")) dgvPatientRecords.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientRecords.Columns.Contains("Age")) dgvPatientRecords.Columns["Age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientRecords.Columns.Contains("TotalAppointments")) dgvPatientRecords.Columns["TotalAppointments"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPatientRecords.AutoResizeColumns();
        }

        private void LoadUpcomingAppointments(int? doctorId = null)
        {
            dgvUpcomingAppointments.DataSource = viewsService.GetUpcomingAppointments(doctorId);

            // Format columns
            if (dgvUpcomingAppointments.Columns.Contains("AppointmentId")) dgvUpcomingAppointments.Columns["AppointmentId"].Visible = false;
            if (dgvUpcomingAppointments.Columns.Contains("PatientId")) dgvUpcomingAppointments.Columns["PatientId"].Visible = false;
            if (dgvUpcomingAppointments.Columns.Contains("DoctorId")) dgvUpcomingAppointments.Columns["DoctorId"].Visible = false;
            if (dgvUpcomingAppointments.Columns.Contains("PatientName")) dgvUpcomingAppointments.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvUpcomingAppointments.Columns.Contains("PhoneNumber")) dgvUpcomingAppointments.Columns["PhoneNumber"].HeaderText = "Phone Number";
            if (dgvUpcomingAppointments.Columns.Contains("DoctorName")) dgvUpcomingAppointments.Columns["DoctorName"].HeaderText = "Doctor Name";
            if (dgvUpcomingAppointments.Columns.Contains("AppointmentDate")) dgvUpcomingAppointments.Columns["AppointmentDate"].HeaderText = "Date";
            if (dgvUpcomingAppointments.Columns.Contains("HourOfDay")) dgvUpcomingAppointments.Columns["HourOfDay"].HeaderText = "Hour";
            if (dgvUpcomingAppointments.Columns.Contains("MinuteOfHour")) dgvUpcomingAppointments.Columns["MinuteOfHour"].HeaderText = "Minute";

            dgvUpcomingAppointments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvUpcomingAppointments.Columns.Contains("AppointmentDate")) dgvUpcomingAppointments.Columns["AppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvUpcomingAppointments.Columns.Contains("HourOfDay")) dgvUpcomingAppointments.Columns["HourOfDay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvUpcomingAppointments.Columns.Contains("MinuteOfHour")) dgvUpcomingAppointments.Columns["MinuteOfHour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUpcomingAppointments.AutoResizeColumns();
        }

        private void LoadDoctorTodaySchedule()
        {
            dgvDoctorTodaySchedule.DataSource = viewsService.GetDoctorTodaySchedule();

            // Format columns
            if (dgvDoctorTodaySchedule.Columns.Contains("AppointmentId")) dgvDoctorTodaySchedule.Columns["AppointmentId"].Visible = false;
            if (dgvDoctorTodaySchedule.Columns.Contains("DoctorId")) dgvDoctorTodaySchedule.Columns["DoctorId"].Visible = false;
            if (dgvDoctorTodaySchedule.Columns.Contains("PatientId")) dgvDoctorTodaySchedule.Columns["PatientId"].Visible = false;
            if (dgvDoctorTodaySchedule.Columns.Contains("DoctorName")) dgvDoctorTodaySchedule.Columns["DoctorName"].HeaderText = "Doctor Name";
            if (dgvDoctorTodaySchedule.Columns.Contains("Specialization")) dgvDoctorTodaySchedule.Columns["Specialization"].HeaderText = "Specialization";
            if (dgvDoctorTodaySchedule.Columns.Contains("PatientName")) dgvDoctorTodaySchedule.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvDoctorTodaySchedule.Columns.Contains("PatientPhone")) dgvDoctorTodaySchedule.Columns["PatientPhone"].HeaderText = "Phone";
            if (dgvDoctorTodaySchedule.Columns.Contains("DateOfBirth")) dgvDoctorTodaySchedule.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            if (dgvDoctorTodaySchedule.Columns.Contains("PatientAge")) dgvDoctorTodaySchedule.Columns["PatientAge"].HeaderText = "Age";
            if (dgvDoctorTodaySchedule.Columns.Contains("AppointmentDate")) dgvDoctorTodaySchedule.Columns["AppointmentDate"].HeaderText = "Date";
            if (dgvDoctorTodaySchedule.Columns.Contains("AppointmentTime")) dgvDoctorTodaySchedule.Columns["AppointmentTime"].HeaderText = "Time";
            if (dgvDoctorTodaySchedule.Columns.Contains("HourOfDay")) dgvDoctorTodaySchedule.Columns["HourOfDay"].Visible = false;
            if (dgvDoctorTodaySchedule.Columns.Contains("MinuteOfHour")) dgvDoctorTodaySchedule.Columns["MinuteOfHour"].Visible = false;
            if (dgvDoctorTodaySchedule.Columns.Contains("Status")) dgvDoctorTodaySchedule.Columns["Status"].HeaderText = "Status";
            if (dgvDoctorTodaySchedule.Columns.Contains("Notes")) dgvDoctorTodaySchedule.Columns["Notes"].HeaderText = "Notes";

            dgvDoctorTodaySchedule.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvDoctorTodaySchedule.Columns.Contains("AppointmentDate")) dgvDoctorTodaySchedule.Columns["AppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvDoctorTodaySchedule.Columns.Contains("AppointmentTime")) dgvDoctorTodaySchedule.Columns["AppointmentTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvDoctorTodaySchedule.Columns.Contains("PatientAge")) dgvDoctorTodaySchedule.Columns["PatientAge"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvDoctorTodaySchedule.Columns.Contains("Status")) dgvDoctorTodaySchedule.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDoctorTodaySchedule.AutoResizeColumns();
        }

        private void LoadPatientClinicalSummary(int? patientId = null)
        {
            dgvPatientClinicalSummary.DataSource = viewsService.GetPatientClinicalSummary(patientId);

            // Format columns
            if (dgvPatientClinicalSummary.Columns.Contains("PatientId")) dgvPatientClinicalSummary.Columns["PatientId"].Visible = false;
            if (dgvPatientClinicalSummary.Columns.Contains("PatientName")) dgvPatientClinicalSummary.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvPatientClinicalSummary.Columns.Contains("DateOfBirth")) dgvPatientClinicalSummary.Columns["DateOfBirth"].HeaderText = "Date of Birth";
            if (dgvPatientClinicalSummary.Columns.Contains("Age")) dgvPatientClinicalSummary.Columns["Age"].HeaderText = "Age";
            if (dgvPatientClinicalSummary.Columns.Contains("InsuranceNumber")) dgvPatientClinicalSummary.Columns["InsuranceNumber"].HeaderText = "Insurance Number";
            if (dgvPatientClinicalSummary.Columns.Contains("PhoneNumber")) dgvPatientClinicalSummary.Columns["PhoneNumber"].HeaderText = "Phone Number";
            if (dgvPatientClinicalSummary.Columns.Contains("CompletedVisits")) dgvPatientClinicalSummary.Columns["CompletedVisits"].HeaderText = "Completed Visits";
            if (dgvPatientClinicalSummary.Columns.Contains("CancelledVisits")) dgvPatientClinicalSummary.Columns["CancelledVisits"].HeaderText = "Cancelled Visits";
            if (dgvPatientClinicalSummary.Columns.Contains("LastVisitDate")) dgvPatientClinicalSummary.Columns["LastVisitDate"].HeaderText = "Last Visit Date";
            if (dgvPatientClinicalSummary.Columns.Contains("LastSeenDoctor")) dgvPatientClinicalSummary.Columns["LastSeenDoctor"].HeaderText = "Last Seen Doctor";
            if (dgvPatientClinicalSummary.Columns.Contains("NextAppointmentDate")) dgvPatientClinicalSummary.Columns["NextAppointmentDate"].HeaderText = "Next Appointment Date";

            dgvPatientClinicalSummary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dgvPatientClinicalSummary.Columns.Contains("DateOfBirth")) dgvPatientClinicalSummary.Columns["DateOfBirth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientClinicalSummary.Columns.Contains("Age")) dgvPatientClinicalSummary.Columns["Age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientClinicalSummary.Columns.Contains("CompletedVisits")) dgvPatientClinicalSummary.Columns["CompletedVisits"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientClinicalSummary.Columns.Contains("CancelledVisits")) dgvPatientClinicalSummary.Columns["CancelledVisits"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientClinicalSummary.Columns.Contains("LastVisitDate")) dgvPatientClinicalSummary.Columns["LastVisitDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dgvPatientClinicalSummary.Columns.Contains("NextAppointmentDate")) dgvPatientClinicalSummary.Columns["NextAppointmentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPatientClinicalSummary.AutoResizeColumns();
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

            selectedPatient = null;
            selectedDoctor = null;
            selectedPatientClinical = null;
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
