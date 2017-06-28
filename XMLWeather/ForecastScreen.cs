using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLWeather
{
    public partial class ForecastScreen : UserControl
    {
        public ForecastScreen()
        {
            InitializeComponent();
            DisplayForecast();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();

            CurrentScreen cs = new CurrentScreen();
            f.Controls.Add(cs);

            f.Controls.Remove(this);
        }

        public void DisplayForecast()
        {
            cityOutput.Text = Form1.days[0].city;

            day1.Text = Form1.days[1].date;
            min1.Text = Form1.days[1].minTemp;
            max1.Text = Form1.days[1].maxTemp;
            cond1.Text = Form1.days[1].condition;

            day2.Text = Form1.days[2].date;
            min2.Text = Form1.days[2].minTemp;
            max2.Text = Form1.days[2].maxTemp;
            cond2.Text = Form1.days[2].condition;
        }
    }
}
