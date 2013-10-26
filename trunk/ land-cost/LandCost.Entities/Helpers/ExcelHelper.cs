using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace LandCost.Entities.Helpers
{
    public static class ExcelHelper
    {
        private static Microsoft.Office.Interop.Excel.ApplicationClass appExcel;
        private static Workbook newWorkbook = null;
        private static _Worksheet objsheet = null;

        //Method to initialize opening Excel
        public static bool Init(String path)
        {
            bool bRet = true;
            appExcel = new ApplicationClass();

            if (System.IO.File.Exists(path))
            {
                // then go and load this into excel
                newWorkbook = appExcel.Workbooks.Open(path, true, true);
                objsheet = (_Worksheet)appExcel.ActiveWorkbook.ActiveSheet;
            }
            else
            {
                bRet = false;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                appExcel = null;
            }
            return bRet;
        }

        //Method to get value; cellname is A1,A2, or B1,B2 etc...in excel.
        public static string GetValue(string cellname)
        {
            string value = string.Empty;
            try
            {
                value = objsheet.get_Range(cellname).get_Value().ToString();
            }
            catch
            {
                value = "";
            }

            return value;
        }

        //Method to close excel connection
        public static bool Close()
        {
            bool bRet = true;
            if (appExcel != null)
            {
                try
                {
                    newWorkbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                    appExcel = null;
                    objsheet = null;
                }
                catch (Exception ex)
                {
                    bRet = false;
                    appExcel = null;
                }
                finally
                {
                    GC.Collect();
                }
            }
            return bRet;
        }
    }
}
