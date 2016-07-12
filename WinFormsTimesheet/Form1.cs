using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSheet;

namespace WinFormsTimesheet
{
    public partial class TimeSheetWindow : Form
    {
        private DateTime start = new DateTime(2016, 7, 3);
        private TimeCard card;

        private int week = 0;

        public TimeSheetWindow()
        {
            card = new TimeCard(start);
            InitializeComponent();
            SetupDays(0);
        }

        private void SetupDays(int week)
        {
            int offset = week * 7;

            lblDay1.Text = start.AddDays(offset).ToShortDateString();
            lblDay2.Text = start.AddDays(offset + 1).ToShortDateString();
            lblDay3.Text = start.AddDays(offset + 2).ToShortDateString();
            lblDay4.Text = start.AddDays(offset + 3).ToShortDateString();
            lblDay5.Text = start.AddDays(offset + 4).ToShortDateString();
            lblDay6.Text = start.AddDays(offset + 5).ToShortDateString();
            lblDay7.Text = start.AddDays(offset + 6).ToShortDateString();

            txtWorking1.Text = "";
            txtWorking2.Text = "";
            txtWorking3.Text = "";
            txtWorking4.Text = "";
            txtWorking5.Text = "";
            txtWorking6.Text = "";
            txtWorking7.Text = "";

            txtSick1.Text = "";
            txtSick2.Text = "";
            txtSick3.Text = "";
            txtSick4.Text = "";
            txtSick5.Text = "";
            txtSick6.Text = "";
            txtSick7.Text = "";

            txtVacation1.Text = "";
            txtVacation2.Text = "";
            txtVacation3.Text = "";
            txtVacation4.Text = "";
            txtVacation5.Text = "";
            txtVacation6.Text = "";
            txtVacation7.Text = "";
        }

        private String[,] GetInputArray()
        {
            String[,] input = new String[7, 3];

            input[0, 0] = txtWorking1.Text;
            input[0, 1] = txtSick1.Text;
            input[0, 2] = txtVacation1.Text;

            input[1, 0] = txtWorking2.Text;
            input[1, 1] = txtSick2.Text;
            input[1, 2] = txtVacation2.Text;

            input[2, 0] = txtWorking3.Text;
            input[2, 1] = txtSick3.Text;
            input[2, 2] = txtVacation3.Text;

            input[3, 0] = txtWorking4.Text;
            input[3, 1] = txtSick4.Text;
            input[3, 2] = txtVacation4.Text;

            input[4, 0] = txtWorking5.Text;
            input[4, 1] = txtSick5.Text;
            input[4, 2] = txtVacation5.Text;

            input[5, 0] = txtWorking6.Text;
            input[5, 1] = txtSick6.Text;
            input[5, 2] = txtVacation6.Text;

            input[6, 0] = txtWorking7.Text;
            input[6, 1] = txtSick7.Text;
            input[6, 2] = txtVacation7.Text;

            return input;
        }
        
        private void btnWeek1_Click(object sender, EventArgs e)
        {
            week = 0;
            SetupDays(week);
        }

        private void btnWeek2_Click(object sender, EventArgs e)
        {
            week = 1;
            SetupDays(week);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            float regHour, sickHour, vacaHour;

            String[,] input = GetInputArray();

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (input[x, y].Equals(""))
                    {
                        input[x, y] = "0";
                    }
                }

                try
                {
                    regHour = (float)Double.Parse(input[x, 0]);
                    sickHour = (float)Double.Parse(input[x, 1]);
                    vacaHour = (float)Double.Parse(input[x, 2]);

                    card.SetHours((week * 7) + x, TimeSheet.Day.HourTypes.REGULAR, regHour);
                    card.SetHours((week * 7) + x, TimeSheet.Day.HourTypes.SICK, sickHour);
                    card.SetHours((week * 7) + x, TimeSheet.Day.HourTypes.VACATION, vacaHour);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    continue;
                }
            }

            lblTotalWorking.Text = "Total Working Hours: " + card.GetHours(TimeSheet.Day.HourTypes.REGULAR);
            lblTotalSick.Text = "Total Sick Hours: " + card.GetHours(TimeSheet.Day.HourTypes.SICK);
            lblTotalVacation.Text = "Total Vacation Hours: " + card.GetHours(TimeSheet.Day.HourTypes.VACATION);

            if (card.CalculateOverTime()[week] > 0)
            {
                lblTotalWorking.Text += " (" + card.CalculateOverTime()[week] + " hours overtime)";
            }
        }
    }
}
