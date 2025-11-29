using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagement_proj.UI
{
    /// <summary>
    /// Controller for the Doctor Scheduling panel
    /// </summary>
    public class SchedulingController : IPanelController
    {
        private readonly Panel panel;
        private AdminDashboard adminDashboard => (AdminDashboard)(panel.FindForm() 
                ?? throw new Exception("Form not found for panel."));
        private GroupBox grpScheduling => (GroupBox)(panel.Controls["grpDoctorScheduling"] 
                ?? throw new Exception("No control named [grpDoctorScheduling] found in panel controls collection."));
        private TableLayoutPanel schedulingLayout => (TableLayoutPanel)(grpScheduling.Controls["layoutSchedulingContent"] 
                ?? throw new Exception("No control named [layoutSchedulingContent] found in grpScheduling controls collection."));
        private ListBox lbSunday => (ListBox)(schedulingLayout.Controls["lbSunday"] 
                ?? throw new Exception("No control named [lbSunday] found in schedulingLayout controls collection."));
        private ListBox lbMonday => (ListBox)(schedulingLayout.Controls["lbMonday"] 
                ?? throw new Exception("No control named [lbMonday] found in schedulingLayout controls collection."));
        private ListBox lbTuesday => (ListBox)(schedulingLayout.Controls["lbTuesday"] 
                ?? throw new Exception("No control named [lbTuesday] found in schedulingLayout controls collection."));
        private ListBox lbWednesday => (ListBox)(schedulingLayout.Controls["lbWednesday"] 
                ?? throw new Exception("No control named [lbWednesday] found in schedulingLayout controls collection."));
        private ListBox lbThursday => (ListBox)(schedulingLayout.Controls["lbThursday"] 
                ?? throw new Exception("No control named [lbThursday] found in schedulingLayout controls collection."));
        private ListBox lbFriday => (ListBox)(schedulingLayout.Controls["lbFriday"] 
                ?? throw new Exception("No control named [lbFriday] found in schedulingLayout controls collection."));
        private ListBox lbSaturday => (ListBox)(schedulingLayout.Controls["lbSaturday"] 
                ?? throw new Exception("No control named [lbSaturday] found in schedulingLayout controls collection."));

        public Panel Panel => panel;

        public SchedulingController(Panel panel)
        {
            this.panel = panel;
        }

        public void Initialize()
        {
            SetupSchedulingListViews();
            adminDashboard.ResizeEnd += new System.EventHandler(AdminDashboard_ResizeEnd);
        }

        public void OnShow()
        {
            RefreshSchedulingListViews();
        }

        private void AdminDashboard_ResizeEnd(object sender, System.EventArgs e)
        {
            if (panel.Visible)
                RefreshSchedulingListViews();
        }

        /// <summary>
        /// Refresh the scheduling list views to adapt to window size changes
        /// </summary>
        public void RefreshSchedulingListViews()
        {
            List<ListBox> dayListBoxes = new List<ListBox>
            {
                lbSunday, lbMonday, lbTuesday, lbWednesday,
                lbThursday, lbFriday, lbSaturday
            };

            foreach (ListBox lb in dayListBoxes)
            {
                lb.Items.Clear();
                for (int i = 0; i < 24; i++)
                {
                    lb.Items.Add("");
                }
            }
        }

        /// <summary>
        /// Setup the scheduling list views with custom drawing
        /// </summary>
        private void SetupSchedulingListViews()
        {
            List<ListBox> dayListBoxes = new List<ListBox>
            {
                lbSunday, lbMonday, lbTuesday, lbWednesday,
                lbThursday, lbFriday, lbSaturday
            };

            RefreshSchedulingListViews();

            foreach (ListBox lb in dayListBoxes)
            {
                lb.DrawMode = DrawMode.OwnerDrawVariable;
                lb.MeasureItem += (s, e) =>
                {
                    int totalHeight = lb.ClientSize.Height;
                    int baseHeight = totalHeight / 24;
                    int remainder = totalHeight % 24;
                    e.ItemHeight = baseHeight + (e.Index < remainder ? 1 : 0);
                };

                lb.DrawItem += (s, e) =>
                {
                    e.DrawBackground();

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        e.Graphics.FillRectangle(Brushes.LightSkyBlue, e.Bounds);
                    else
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

                    e.Graphics.DrawRectangle(Pens.Gray, e.Bounds);
                };

                lb.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        lb.SelectedIndex = -1;
                    }
                };
            }
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
