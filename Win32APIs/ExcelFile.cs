using System;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;

namespace Win32APIs
{
    public class ExcelFile
    {

        public static void ExcelDoc(string filePath)
        {
            Excel.Application excelApp = new Excel.Application();

            try
            {
                if (excelApp is null)
                {
                    throw new InvalidOperationException("Excel is not properly installed.");
                }
                    excelApp.Visible = false;
                    Excel.Workbook workBook = excelApp.Workbooks.Add(Type.Missing);
                    Excel.Worksheet workSheet= (Excel.Worksheet)workBook.Worksheets[1];

                    workSheet.Name = "MySheet";
                    //populate worksheet with numbers from 1 to 10
                    for (int i = 0; i <= 10; i++)
                    {
                        workSheet.Cells[1] = i;
                        workSheet.Cells[2] = "Item "+i.ToString();

                    }

                    workBook.SaveAs(filePath);
                    workBook.Close();
                
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                excelApp.Quit();
            }
        }

    }
}
