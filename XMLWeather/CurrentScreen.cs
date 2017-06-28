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
    public partial class CurrentScreen : UserControl
    {
        public CurrentScreen()
        {
            InitializeComponent();
            DisplayCurrent();
        }

        public void DisplayCurrent()
        {
            cityOutput.Text = Form1.days[0].city;
            currentOutput.Text = Form1.days[0].currentTemp;
            windOutput.Text = Form1.days[0].windDirection;
            windSpeedOutput.Text = Form1.days[0].windSpeed;

            dateLabel.Text = DateTime.Now.ToString("dd-MM-yy");
            conditionOutput.Text = Form1.days[0].condition;
        }

        private void forecastLabel_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            
            ForecastScreen fs = new ForecastScreen();
            f.Controls.Add(fs);

            f.Controls.Remove(this);
        }
    }
}
