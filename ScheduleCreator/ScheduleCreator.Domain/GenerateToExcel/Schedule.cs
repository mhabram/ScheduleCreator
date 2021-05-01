using ClosedXML.Excel;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleCreator.Domain.GenerateToExcel
{
    public class Schedule
    {
        private IList<Employee> Employees;

        private int startRow = 2;
        private int startCol = 2;
        private XLWorkbook workbook;
        private IXLWorksheet worksheet;

        public Schedule(IList<Employee> employees)
        {
            Employees = employees;
        }

        public void Create(string filePath)
        {
            DateTime currentDate = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day + 1);
            int currentRow = startRow;
            int currentCol = startCol;
            string fullName = "";

            try
            {
                workbook = new XLWorkbook();
                worksheet = workbook.Worksheets.Add("Schedule");

                #region Header
                worksheet.Cell(currentRow, currentCol++).Value = "Date";
                worksheet.Cell(currentRow, currentCol++).Value = "DOW";

                foreach (Employee e in Employees)
                {
                    fullName = String.Concat(e.Name.First().ToString().ToUpper(), e.Name.Substring(1).ToLower(), " ", e.LastName.First().ToString().ToUpper(), e.LastName.Substring(1).ToLower());
                    worksheet.Cell(currentRow, currentCol).Value = fullName;
                    currentCol++;
                }
                #endregion

                #region Column styling

                for (int i = startCol; i < currentCol; i++)
                {
                    worksheet.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(currentRow, i).Style.Border.SetInsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, i).Style.Font.SetBold();
                    worksheet.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.Gainsboro);
                }
                #endregion

                #region Body
                XLColor color = XLColor.Gainsboro;
                currentCol = startCol;
                for (int i = 0; i < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); i++)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, currentCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(currentRow, currentCol).Style.Border.SetInsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, currentCol).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, currentCol).Style.Fill.SetBackgroundColor(XLColor.BattleshipGrey);
                    worksheet.Cell(currentRow, currentCol++).Value = currentDate.AddDays(i).Date.ToString();
                    
                    worksheet.Cell(currentRow, currentCol).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(currentRow, currentCol).Style.Border.SetInsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, currentCol).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
                    worksheet.Cell(currentRow, currentCol).Style.Fill.SetBackgroundColor(XLColor.BattleshipGrey);
                    worksheet.Cell(currentRow, currentCol--).Value = currentDate.AddDays(i).DayOfWeek;

                    for (int j = startCol; j < (Employees.Count + 2 + startCol); j++)
                    {
                        if ((currentDate.AddDays(i).DayOfWeek.ToString() == "Sunday") || (currentDate.AddDays(i).DayOfWeek.ToString() == "Saturday"))
                            color = XLColor.AshGrey;
                        else
                            color = XLColor.Gainsboro;

                        worksheet.Cell(currentRow, j).Style.Font.SetBold();
                        worksheet.Cell(currentRow, j).Style.Fill.SetBackgroundColor(color);
                    }
                }


                // correcting Cells before loop
                currentCol++;
                for (int i = 0; i < Employees.Count; i++)
                {
                    // correcting Rows
                    currentRow = startRow;
                    currentCol++;

                    for (int j = 0; j < Employees[i].Days.Count; j++)
                    {
                        currentRow++;
                        if (Employees[i].Days[j].Shift == "Free")
                            worksheet.Cell(currentRow, currentCol).Value = "";
                        else
                            worksheet.Cell(currentRow, currentCol).Value = Employees[i].Days[j].Shift;
                    }
                }
                #endregion
                IXLRange range;
                range = worksheet.Range(worksheet.Cell(startRow + 1, startCol + 2).Address, worksheet.Cell(currentRow, currentCol).Address);
                range.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
                range.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
            }
            catch (Exception)
            {
            }
        }
    }
}
