using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace DtecMonitorProject
{
    public class StoreData
    {
        private  MySqlConnection GetConnection()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "sqlparam.json").Replace("\"","'").Replace("\n","").Replace("\r","").Replace("\t","");
            // json ="{'username':'u3cu6bor98'}";//,'DatabaseName': 'u3cu6bor98','Password': 'aC2joLi1hW','ServerAddress': 'remotemysql.com','Port': '3306' }}";
            MySqlConnectionParam mySqlConnectionParam =
                JsonConvert.DeserializeObject<MySqlConnectionParam>(json);
            MySqlConnection con = new MySqlConnection("Server=" + mySqlConnectionParam.ServerAddress + ";Port=" +
                                                      mySqlConnectionParam.Port + ";Database=" +
                                                      mySqlConnectionParam.DatabaseName + ";Uid=" +
                                                      mySqlConnectionParam.username + ";Pwd=" +
                                                      mySqlConnectionParam.Password + ";");

            return con;
        }
         public  async Task saveDataAsyn(SiteResponse sc,int statuscode,string JSONDATA)
        {
            
            DateTime dt = DateTime.Parse(sc.Date);
            int MonitorDate = Utility.getDateInShamsiDate(dt);

            int website = 1; //site is running ..
            if (JSONDATA == "")
                website = 0; //site is down
            
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string insert_query = (" INSERT INTO INDEP_SITEMONITOR " +
                                           " (MONITORDATE,MONITORTIME, PROJECTNAME,WEBSITE, STATUSCODE, DISK, CPU, MEM, DBCON, BACKUP, WEBSERVICE, JSONDATA) " +
                                           " VALUES("+MonitorDate+",'" + sc.Time + "' ,'" + sc.projectname + "'," +website+","+
                                           statuscode + ","+sc.disk+","+sc.cpu_percent+", "+sc.mem_percent+", "+sc.dbcon+","+sc.backup+", "+sc.webservice+",'"+JSONDATA+"');");
                    MySqlCommand cmd = new MySqlCommand(insert_query, conn);
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    await Utility.writeErrorAsync("saveDataAsyn:"+ex.Message);
                }
                
            }
        }
         
         public  async Task writeEmptyData(SiteList site,string ex,bool Notify = false)
         {
             try
             {
                 if (string.IsNullOrEmpty(site.ProjectName) || string.IsNullOrWhiteSpace(site.ProjectName))
                     return;

                 if (string.IsNullOrEmpty(ex))
                     ex = "";
                 
                 string smsText = site.ProjectName + "("+Strings.Left(DateTime.Now.ToString("HH:mm"), 5)+") : Site is  Down ...";
                 SiteResponse sc = new SiteResponse();
                 sc.backup = 2;
                 sc.cpu_percent = 2;
                 sc.dbcon = 2;
                 sc.disk = 2;
                 sc.mem_percent = 2;
                 sc.webservice  = 2;
                 sc.projectname = site.ProjectName;
                 sc.Date = DateTime.Now.ToShortDateString();
                 sc.Time = Strings.Left(DateTime.Now.ToString("HH:mm"), 5);
                 
         
                 int statuscode = 1;
                 string JSONDATA = "{}";
                 if (Notify == true)
                 {
                     statuscode = 0;
                     JSONDATA = "";
                 }

                 await saveDataAsyn(sc, statuscode, JSONDATA);
                 
                 if (Notify == true)
                 {
                     Notification nf = new Notification();
                     await Task.Run(() => nf.sendSmsAsyn(site.CellPhones, smsText));
                     await Task.Run(() => nf.sendEmailAsync(site.Receivers, smsText,
                         DateTime.Now.ToLongDateString() + "#" + smsText + "\n \t" + ex));
                 }
             }
             catch (Exception e)
             {
                 await Utility.writeErrorAsync("writeEmptyData:"+e.Message);
             }
           
         }

    }
}