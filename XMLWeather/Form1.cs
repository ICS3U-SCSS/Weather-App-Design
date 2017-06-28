using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace XMLWeather
{
    public partial class Form1 : Form
    {
        public static Day d = new Day();
        public static List<Day> days = new List<Day>();

        public Form1()
        {
            InitializeComponent();
            GetData();
            ExtractCurrent();
            ExtractForecast();

            // open weather screen for todays weather
            CurrentScreen cs = new CurrentScreen();
            this.Controls.Add(cs);
        }

        private static void GetData()
        {
            WebClient client = new WebClient();

            // one day forecast
            client.DownloadFile("http://api.openweathermap.org/data/2.5/weather?q=Stratford,CA&mode=xml&units=metric&appid=3f2e224b815c0ed45524322e145149f0", "WeatherData.xml");
            // mulit day forecast
            client.DownloadFile("http://api.openweathermap.org/data/2.5/forecast/daily?q=Stratford,CA&mode=xml&units=metric&cnt=7&appid=3f2e224b815c0ed45524322e145149f0", "WeatherData7Day.xml");
       
        }

        private void ExtractCurrent()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData.xml");

            //get information from specific nodes for the information that we need
            XmlNode city = doc.SelectSingleNode("current/city");
            XmlNode temperature = doc.SelectSingleNode("current/temperature");
            XmlNode windSpeed = doc.SelectSingleNode("current/wind/speed");
            XmlNode windDirection = doc.SelectSingleNode("current/wind/direction");
            XmlNode precipitation = doc.SelectSingleNode("current/precipitation");
            XmlNode condition = doc.SelectSingleNode("current/weather");

            //reset day object
            d = new Day();

            //attributes are stored in an object array which can be accessed with an index
            //value or by using the string value of the attribute name you want

            //set date value
            d.city = city.Attributes["name"].Value;

            //set temperature values
            d.maxTemp = temperature.Attributes["max"].Value;
            d.minTemp = temperature.Attributes["min"].Value;
            d.currentTemp = temperature.Attributes["value"].Value;

            //set wind values
            d.windSpeed = windSpeed.Attributes["value"].Value;
            d.windDirection = windDirection.Attributes["name"].Value;

            //set precipitation value
            d.precipitation = precipitation.Attributes["mode"].Value;

            //set condition value
            d.condition = condition.Attributes["value"].Value;

            //add day object to List
            days.Add(d);

            #region Old nested foreach method
            //create a node variable to represent the parent element
            XmlNode parent;
            parent = doc.DocumentElement;

            //check each child of the parent element
            foreach (XmlNode child in parent.ChildNodes)
            {
                if (child.Name == "city")
                {
                    d.city = child.Attributes["name"].Value;
                }

                if (child.Name == "temperature")
                {

                    d.maxTemp = child.Attributes["max"].Value;
                    d.minTemp = child.Attributes["min"].Value;
                    d.currentTemp = child.Attributes["value"].Value;
                }

                if (child.Name == "wind")
                {
                    //check each child of the wind element (grandChild of parent element)
                    foreach (XmlNode grandChild in child.ChildNodes)
                    {
                        if (grandChild.Name == "speed")
                        {
                            d.windSpeed = grandChild.Attributes["value"].Value;
                        }
                        if (grandChild.Name == "direction")
                        {
                            d.windDirection = grandChild.Attributes["name"].Value;
                        }
                    }
                }

                if (child.Name == "precipitation")
                {
                    d.precipitation = child.Attributes["mode"].Value;
                }

                if (child.Name == "weather")
                {
                    d.condition = child.Attributes["value"].Value;
                    days.Add(d);
                }

            }
            #endregion
        }

        private void ExtractForecast()
        {
            //open XML document
            XmlDocument doc = new XmlDocument();
            doc.Load("WeatherData7Day.xml");

            //Create a separate list for each of the types of element values we want.
            //Each entry in list will contain all info for a node, including attribute values.
            XmlNodeList dateList = doc.GetElementsByTagName("time");
            XmlNodeList tempList = doc.GetElementsByTagName("temperature");
            XmlNodeList cloudsList = doc.GetElementsByTagName("clouds");

            //use a loop to combine separate element lists into day objects
            //we are starting at i=1 since the values at i=0 are for today
            for (int i = 1; i < tempList.Count; i++)
            {
                //reset day object
                d = new Day();

                //set date value
                d.date = dateList[i].Attributes["day"].Value;

                //set temperature values
                d.minTemp = tempList[i].Attributes["min"].Value;
                d.maxTemp = tempList[i].Attributes["max"].Value;

                //set condition value
                d.condition = cloudsList[i].Attributes["value"].Value;

                //add day object to days List
                days.Add(d);          
            }

            #region Old nested foreach method

            //create a node variable to represent the parent element
            //XmlNode parent;
            //parent = doc.DocumentElement;

            //check each child of the parent element
            //foreach (XmlNode child in parent.ChildNodes)
            //{
            //    //if the "forecast" element is found search through each of it's sub elements
            //    if (child.Name == "forecast")
            //    {
            //        //check each child of the forecast element (grandChild of parent element)
            //        foreach (XmlNode grandChild in child.ChildNodes)
            //        {
            //            //check each child of the grandChild element (greatGrandChild of parent element)
            //            foreach (XmlNode greatGrandChild in grandChild.ChildNodes)
            //            {
            //                //adds the date to the object
            //                d.date = grandChild.Attributes["day"].Value;

            //                //if the "temperature" element is found add "max" and "min" attribute
            //                if (greatGrandChild.Name == "temperature")
            //                {
            //                    d.maxTemp = greatGrandChild.Attributes["max"].Value;
            //                    d.minTemp = greatGrandChild.Attributes["min"].Value;
            //                }
            //                // if the "clouds" element is found add "value" attribute (conditions)
            //                if (greatGrandChild.Name == "clouds")
            //                {
            //                    d.condition = greatGrandChild.Attributes["value"].Value;
            //                    days.Add(d);
            //                    d = new Day();
            //                }                          
            //            }
            //        }
            //    }
            //}

            // this will remove the first day that is added as it is the current days values
            // which we already have from the current day xml file. 
            //days.RemoveAt(1);

            #endregion

        }
    }
}
