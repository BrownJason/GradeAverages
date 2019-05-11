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
            Chart1.ChartAreas[0].AxisX.Interval = 1;

            Chart1.Series[0].LegendText = "Grade Averges";
            Chart1.Titles.Add("Travel Time - Average Grades");
            Chart1.Width = 650;
            Chart1.Height = 450;
        }
    }
}