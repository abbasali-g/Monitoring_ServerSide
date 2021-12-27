using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace DtecMonitorProject
{
    public static class Utility
    {
        
        public static int getDateInShamsiDate(DateTime dt)
        {
            PersianCalendar p = new PersianCalendar();
            int year = p.GetYear(dt);
            int month = p.GetMonth(dt);
            int day = p.GetDayOfMonth(dt);
            int monitorDate = int.Parse(string.Format("{0}{1}{2}", year, month.ToString("D2"), day.ToString("D2")));
            return monitorDate;
        }
        
        
        static ReaderWriterLock locker = new ReaderWriterLock();
        public static  async Task  writeErrorAsync(string error)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);
                error=Strings.Left(DateTime.Now.ToString("HH:mm"),5)+":"+ error;
                string errorFilePath = AppDomain.CurrentDomain.BaseDirectory + "log_" +
                                       getDateInShamsiDate(DateTime.Now).ToString() + ".txt";
                TextWriter tx = new StreamWriter(errorFilePath,true);
                await tx.WriteLineAsync(error);
                tx.Close();
            }
            catch (Exception e)
            {
                await Utility.writeErrorAsync(e.Message);
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
           
        }
        
    }
}