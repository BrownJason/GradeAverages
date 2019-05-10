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
using System.Web.UI.WebControls;

namespace GradeAverages
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Enabled = false;
            Chart1.Visible = true;
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
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {
            FillChart();
        }

        private void FillChart()
        {
            ResourceManager rm = new ResourceManager("GradeAverages.Resource1",
                                               Assembly.GetExecutingAssembly());

            string fileName = rm.GetString("student_mat");

            List<People> peopleArray = People.FromCsv(fileName);
            this.Chart1.Series.Clear();

            double fifteenMinutes = (from people in peopleArray where people.TravelTime == 1 select people.G3).Average();
            double fifteenToThirtyMinutes = (from people in peopleArray where people.TravelTime == 2 select people.G3).Average();
            double thirtyToHourMinutes = (from people in peopleArray where people.TravelTime == 3 select people.G3).Average();
            double greaterThanHourMinutes = (from people in peopleArray where people.TravelTime == 4 select people.G3).Average();

            this.Chart1.Titles.Add("Travel Time - Average Grade");
            this.Chart1.Series["Travel Time"].Points.AddXY("15", fifteenMinutes);
            this.Chart1.Series["Travel Time"].Points.AddXY("15 - 30", fifteenToThirtyMinutes);
            this.Chart1.Series["Travel Time"].Points.AddXY("30 - 60", thirtyToHourMinutes);
            this.Chart1.Series["Travel Time"].Points.AddXY("60+", greaterThanHourMinutes);

        }
    }
}