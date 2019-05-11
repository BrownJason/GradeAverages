using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;

namespace GradeAverages
{
    public partial class _Default : Page
    {
        Boolean hdeLoadChart = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Enabled = false;

                if (!hdeLoadChart)
                {
                    DispalyChart();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("GradeAverages.Resource1",
                                                Assembly.GetExecutingAssembly());

            string fileName = rm.GetString("student_mat");

            List<People> peopleArray = People.FromCsv(fileName);
            var internet = from people in peopleArray
                           where people.Internet.Equals("yes")
                           select people.G3;
            var noInternet = from people in peopleArray
                             where people.Internet.Equals("no")
                             select people.G3;
            string[] internets = { "Internet", "No Internet" };
            double[] hasInternets = { internet.Average(), noInternet.Average() };
            //n if 1 <= n < 3
            var pastFailures = (from people in peopleArray where people.Failures <= 1 || people.Failures < 3 select people.G3).Average();
            var mostFails = (from people in peopleArray where people.Failures >= 3 select people.G3).Average();

            var studyTime = from people in peopleArray
                            where people.StudyTime != 0
                            select people;

            double twoHourStudyTime = (from people in peopleArray where people.StudyTime == 1 select people.G3).Average();
            double fiveHourStudyTime = (from people in peopleArray where people.StudyTime == 2 select people.G3).Average();
            double tenHourStudyTime = (from people in peopleArray where people.StudyTime == 3 select people.G3).Average();
            double tenPlusHourStudyTime = (from people in peopleArray where people.StudyTime == 4 select people.G3).Average();

            double lessThanTenAbsences = (from people in peopleArray where people.Absences < 10 select people.G3).Average();
            double betweenTenAnd25Absences = (from people in peopleArray where people.Absences >= 10 && people.Absences <= 25 select people.G3).Average();
            double greaterThan25Absences = (from people in peopleArray where people.Absences > 25 select people.G3).Average();

            TextBox1.Text = RadioButtonList1.Items[0].Selected ?
                String.Format("{0}, average grade: {1:0.00} \n{2}, average grade: {3:0.00}", internets[0], hasInternets[0], internets[1], hasInternets[1])
                : RadioButtonList1.Items[1].Selected ? String.Format("Past Failures < {0}, average grade: {1:0.00} \nPast Failures >= {2}, average grade: {3:0.00}", 3, pastFailures, 3, mostFails)
                : RadioButtonList1.Items[2].Selected ? String.Format("Study Time \nLess than 2 hours, Average Grade: {0:0.00} " +
                "\n2 and 5 hours, Average Grade: {1:0.00}" +
                "\n5 and 10 hours, Average Grade: {2:0.00}" +
                "\n10+ hours, Average Grade: {3:0.00}", twoHourStudyTime, fiveHourStudyTime, tenHourStudyTime, tenPlusHourStudyTime)
               : String.Format("Number of Absences" +
               "\nLess than 10, Average Grade: {0:0.00}" +
               "\n10 to 25, Average Grade: {1:0.00}" +
               "\n25 or more, Averge Grade: {2:0.00}", lessThanTenAbsences, betweenTenAnd25Absences, greaterThan25Absences);

            hdeLoadChart = true;
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }

        protected void DispalyChart()
        {
            ResourceManager rm = new ResourceManager("GradeAverages.Resource1",
                                               Assembly.GetExecutingAssembly());

            string fileName = rm.GetString("student_mat");

            List<People> peopleArray = People.FromCsv(fileName);

            double fifteenMinutes = (from people in peopleArray where people.TravelTime == 1 select people.G3).Average();
            double fifteenToThirtyMinutes = (from people in peopleArray where people.TravelTime == 2 select people.G3).Average();
            double thirtyToHourMinutes = (from people in peopleArray where people.TravelTime == 3 select people.G3).Average();
            double greaterThanHourMinutes = (from people in peopleArray where people.TravelTime == 4 select people.G3).Average();

            double[] yVal = { double.Parse(String.Format("{0:0.00}",fifteenMinutes)),
                double.Parse(String.Format("{0:0.00}", fifteenToThirtyMinutes)),
                double.Parse(String.Format("{0:0.00}", thirtyToHourMinutes)),
                double.Parse(String.Format("{0:0.00}",greaterThanHourMinutes)) };
            string[] xName = { "15", "15-30", "30-60", "60+" };

            Chart1.Series.Add("Series1");
            Chart1.Series[0].Points.DataBindXY(xName, yVal);
            Chart1.Series[0].Font = new System.Drawing.Font("Times", 16f);

            Chart1.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].AxisLabel = "Travel time in minutes";

            Chart1.ChartAreas.Add("ChartArea1");
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart1.ChartAreas[0].AxisX.Minimum = 0;
            Chart1.ChartAreas[0].AxisX.Title = "Travel time in minutes";
            Chart1.ChartAreas[0].AxisX.Interval = 1;

            Chart1.Series[0].LegendText = "Grade Averges";
            Chart1.Titles.Add("Travel Time - Average Grades");
            Chart1.Width = 650;
            Chart1.Height = 450;

            IEnumerable<string> healthOneG1 = (from people in peopleArray where people.Health == 1 select people.G1);
            IEnumerable<string> healthTwoG1 = (from people in peopleArray where people.Health == 2 select people.G1);
            IEnumerable<string> healthThreeG1 = (from people in peopleArray where people.Health == 3 select people.G1);
            IEnumerable<string> healthFourG1 = (from people in peopleArray where people.Health == 4 select people.G1);
            IEnumerable<string> healthFiveG1 = (from people in peopleArray where people.Health == 5 select people.G1);
            IEnumerable<string> healthOneG2 = (from people in peopleArray where people.Health == 1 select people.G2);
            IEnumerable<string> healthTwoG2 = (from people in peopleArray where people.Health == 2 select people.G2);
            IEnumerable<string> healthThreeG2 = (from people in peopleArray where people.Health == 3 select people.G2);
            IEnumerable<string> healthFourG2 = (from people in peopleArray where people.Health == 4 select people.G2);
            IEnumerable<string> healthFiveG2 = (from people in peopleArray where people.Health == 5 select people.G2);
            double healthOneG3 = (from people in peopleArray where people.Health == 1 select people.G3).Average();
            double healthTwoG3 = (from people in peopleArray where people.Health == 2 select people.G3).Average();
            double healthThreeG3 = (from people in peopleArray where people.Health == 3 select people.G3).Average();
            double healthFourG3 = (from people in peopleArray where people.Health == 4 select people.G3).Average();
            double healthFiveG3 = (from people in peopleArray where people.Health == 5 select people.G3).Average();

            double h1G1 = 0, h2G1 = 0, h3G1 = 0, h4G1 = 0, h5G1 = 0;
            foreach(string s in healthOneG1)
            {
                h1G1 += int.Parse(s);
            }

            foreach (string s in healthTwoG1)
            {
                h2G1 += int.Parse(s);
            }
            foreach (string s in healthThreeG1)
            {
                h3G1 += int.Parse(s);
            }
            foreach (string s in healthFourG1)
            {
                h4G1 += int.Parse(s);
            }
            foreach (string s in healthFiveG1)
            {
                h5G1 += int.Parse(s);
            }

            h1G1 = h1G1 / healthOneG1.Count();
            h2G1 = h2G1 / healthTwoG1.Count();
            h3G1 = h3G1 / healthThreeG1.Count();
            h4G1 = h4G1 / healthFourG1.Count();
            h5G1 = h5G1 / healthFiveG1.Count();

            double h1G2 = 0, h2G2 = 0, h3G2 = 0, h4G2 = 0, h5G2 = 0;
            foreach (string s in healthOneG2)
            {
                h1G2 += int.Parse(s);
            }

            foreach (string s in healthTwoG2)
            {
                h2G2 += int.Parse(s);
            }
            foreach (string s in healthThreeG2)
            {
                h3G2 += int.Parse(s);
            }
            foreach (string s in healthFourG2)
            {
                h4G2 += int.Parse(s);
            }
            foreach (string s in healthFiveG2)
            {
                h5G2 += int.Parse(s);
            }

            h1G2 = h1G2 / healthOneG2.Count();
            h2G2 = h2G2 / healthTwoG2.Count();
            h3G2 = h3G2 / healthThreeG2.Count();
            h4G2 = h4G2 / healthFourG2.Count();
            h5G2 = h5G2 / healthFiveG2.Count();

            double[] healthG1 = { double.Parse(String.Format("{0:0.00}", h1G1)),
                double.Parse(String.Format("{0:0.00}", h2G1)),
                double.Parse(String.Format("{0:0.00}", h3G1)),
                double.Parse(String.Format("{0:0.00}", h4G1)),
                double.Parse(String.Format("{0:0.00}", h5G1)) };
            double[] healthG2 = { double.Parse(String.Format("{0:0.00}", h1G2)),
                double.Parse(String.Format("{0:0.00}", h2G2)),
                double.Parse(String.Format("{0:0.00}", h3G2)),
                double.Parse(String.Format("{0:0.00}", h4G2)),
                double.Parse(String.Format("{0:0.00}", h5G2)) };
            double[] healthG3 = { healthOneG3, healthTwoG3, healthThreeG3, healthFourG3, healthFiveG3};
            double[] xVals = { 1, 2, 3, 4, 5 };

            Chart2.Series.Add("Series2");
            Chart2.Series[0].Points.DataBindXY(xVals, healthG1);
            Chart2.Series[0].Font = new System.Drawing.Font("Times", 16f);

            Chart2.Series.Add("Series3");
            Chart2.Series[1].Points.DataBindXY(xVals, healthG2);
            Chart2.Series[1].Font = new System.Drawing.Font("Times", 16f);

            Chart2.Series.Add("Series4");
            Chart2.Series[2].Points.DataBindXY(xVals, healthG3);
            Chart2.Series[2].Font = new System.Drawing.Font("Times", 16f);

            Chart2.Series[0].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
            Chart2.Series[0].IsValueShownAsLabel = true;
            Chart2.Series[1].IsValueShownAsLabel = true;
            Chart2.Series[2].IsValueShownAsLabel = true;
            Chart2.Series[0].AxisLabel = "Overall Health Scale";

            Chart2.ChartAreas.Add("ChartArea2");
            Chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas[0].AxisX.Minimum = 0;
            Chart2.ChartAreas[0].AxisX.Title = "Overall Health Scale";
            Chart2.ChartAreas[0].AxisX.Interval = 1;

            Chart2.Series[0].LegendText = "G1";
            Chart2.Series[1].LegendText = "G2";
            Chart2.Series[2].LegendText = "G3";
            Chart2.Titles.Add("Overall Health - Average Grades");
            Chart2.Width = 650;
            Chart2.Height = 450;

        }
    }
}