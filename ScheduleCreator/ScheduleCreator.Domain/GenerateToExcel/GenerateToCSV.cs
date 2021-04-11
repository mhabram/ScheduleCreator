using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.GenerateToExcel
{
    public class GenerateToCSV
    {
        string FilePath = "C:\\Temp\\CSV\\Schedule.csv"; // need to fix while folder not exists it has to be created by itself or choosen by schedule creator
        public void ToCSV()
        {
            StringBuilder csv = new StringBuilder();

            string first = "ASDSDASDASDSAD";
            string second = "QWEWQEWQEQWEWQEWQEWQ";

            string newLine = string.Format($"{first};{second}");

            csv.AppendLine(newLine);

            File.WriteAllText(FilePath, csv.ToString());
        }
    }
}
