using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeAverages
{
    public class People
    {

        public string School { get; set; }
        public char Sex { get; set; }
        public int Age { get; set; }
        public char Address { get; set; }
        public string FamSize { get; set; }
        public char PStatus { get; set; }
        public int Medu { get; set; }
        public int Fedu { get; set; }
        public string MJob { get; set; }
        public string FJob { get; set; }
        public string Reason { get; set; }
        public string Guardian { get; set; }
        public int TravelTime { get; set; }
        public int StudyTime { get; set; }
        public int Failures { get; set; }
        public string SchoolsUp { get; set; }
        public string FamsUp { get; set; }
        public string Paid { get; set; }
        public string Activities { get; set; }
        public string Nursery { get; set; }
        public string Higher { get; set; }
        public string Internet { get; set; }
        public string Romantic { get; set; }
        public int FamRel { get; set; }
        public int FreeTime { get; set; }
        public int GoOut { get; set; }
        public int Dalc { get; set; }
        public int Walc { get; set; }
        public int Health { get; set; }
        public int Absences { get; set; }
        public string G1 { get; set; }
        public string G2 { get; set; }
        public int G3 { get; set; }

        internal static List<People> FromCsv(string line)
        {
            List<People> listOfPeps = new List<People>();

            List<string> list = line.Split('\n').Skip(1).ToList();

            var newList = list.Last();

            list.Remove(newList);

            foreach(var s in list)
            {
                string[] x = s.Replace("\"", "").Split(';');

                People people = new People
                {
                    School = x[0],
                    Sex = Convert.ToChar(x[1]),
                    Age = int.Parse(x[2]),
                    Address = char.Parse(x[3]),
                    FamSize = x[4],
                    PStatus = Convert.ToChar(x[5]),
                    Medu = int.Parse(x[6]),
                    Fedu = int.Parse(x[7]),
                    MJob = x[8],
                    FJob = x[9],
                    Reason = x[10],
                    Guardian = x[11],
                    TravelTime = int.Parse(x[12]),
                    StudyTime = int.Parse(x[13]),
                    Failures = int.Parse(x[14]),
                    SchoolsUp = x[15],
                    FamsUp = x[16],
                    Paid = x[17],
                    Activities = x[18],
                    Nursery = x[19],
                    Higher = x[20],
                    Internet = x[21],
                    Romantic = x[22],
                    FamRel = int.Parse(x[23]),
                    FreeTime = int.Parse(x[24]),
                    GoOut = int.Parse(x[25]),
                    Dalc = int.Parse(x[26]),
                    Walc = int.Parse(x[27]),
                    Health = int.Parse(x[28]),
                    Absences = int.Parse(x[29]),
                    G1 = x[30],
                    G2 = x[31],
                    G3 = int.Parse(x[32])
                };
                listOfPeps.Add(people);
            }
            
            return listOfPeps;
        }
    }
}