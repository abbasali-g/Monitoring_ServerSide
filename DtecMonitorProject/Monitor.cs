using System;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DtecMonitorProject
{
    public class Monitor
    {
        public async Task<string> readSiteAsyn(string siteResponse)
        {
            string siteData = ""; 
            try
            {
                siteResponse = siteResponse.Replace("'</body></html>", "").Replace("<html><body>b'", "");
                siteData = de_asymetricAbbas(siteResponse);
                SiteResponse sc = JsonConvert.DeserializeObject<SiteResponse>(siteData);
                
                DateTime dt = DateTime.Parse(sc.Date);
                if (DateTime.Now > dt.AddMinutes(45))
                    siteData+="##Monitor Service is not working, plz check(" + sc.Date.ToString() +")";

                await Task.Delay(1);
            }
            catch (Exception e)
            {
                siteData = e.Message;
            }

            return siteData;
        }

        public async Task checkSitesAsyn(SiteList site, string siteResponse)
        {
            try
            {
                //siteResponse ="<html><body>b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIE1haW4gREIiLCJkYmNvbiI6MSwiY3B1X3BlicmNlibnQiOjE4LjIsaImNwdV9sab2FkIjowLjAbsaIm1libV9wZXJjZW50IjoxNC40LCJtZW1fdG90YWwiOjUxMi4wLCJtZW1fdXNliZCI6NzMuNSwibWVtX2F2YWlisaYWJlibCI6NDM4LjUsaImRpc2saiOjEsaImRpc2tfcGVyY2VudF9liOj0iOjE3LjksaImRpc2tfdG90YWxfZTo9Ijo2ODMuNTksaImRpc2tfZnJliZV9liOj0iOjU2MS40NSwiZGliza191c2VkX2U6PSI6MTIyLjE0LCJkaXNrIjoxLCJkaXNrX3BlicmNlibnRfZzo9IjoyOC4xLCJkaXNrX3RvdGFsaX2c6PSI6NjgzLjU5LCJkaXNrX2ZyZWVfZzo9Ijo0OTEuMzMsaImRpc2tfdXNliZF9nOj0iOjE5Mi4yNywiZGlizayI6MSwiZGliza19wZXJjZW50X2g6PSI6MC44LCJkaXNrX3RvdGFsaX2g6PSI6Mzc3LjkzLCJkaXNrX2ZyZWVfaDo9IjozNzQuOTgsaImRpc2tfdXNliZF9oOj0iOjIuOTUsaInVzZXJzIjoic3VzZXIobmFtZT1EdGVjLCB0ZXJtaW5hbD1Ob25liLCBob3N0PTAbuNC4wLjAbsaIHN0YXJ0ZWQ9MTU5MTU5ODU2Ny4zMzM4MTg0LCBwaWQ9Tm9uZSksaIHN1c2VyKG5hbWU9QWRtaW5pc3RyYXRvciwgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE1OTE4NTY4NDMuMzQ3OTg0OCwgcGlikPU5vbmUpIiwiYmFja3VwIjoxLCJsaYXRlic3RfZmlisaZSI6Ikc6XERhaWx5QmFja3VwXERhaWx5XEVtZGFkX01vc2hhcmVrYXRfYmFja3VwXzIwMjBfMDZfMTBfMjMwMDAbxXzk1MjE2MDQuYmFrIiwiTWFudWZhY3R1cmVyIjoidm13YXJliLGliuYy4iLCJNb2RlibCI6InZtd2FyZXZpcnR1YWxwbGF0Zm9ybSIsaIkNvcmVzQ291bnQiOiI0IiwiQ3B1TnVtIjoiMTYiLCJTZXJpYWwyIjoiOTE2ODRkNTYtNTlikMi0yMmMzLTk3N2MtZjJhYzY3Yzk3NzI5IiwiU2VyaWFsaMSI6InZtd2FyZS01NjRkNjg5MWQyNTlijMzIyLTk3N2NmMmFjNjdjOTc3MjkiLCJEYXRliIjoiMjAbyMC0wNi0xMSAbxMjozNTo0OS4xMzc0MjciLCJUaW1liIjoiMTI6MzUiLCJlicnJtc2ciOiIifQ=='</body></html>";
                //siteResponse ="<html><body>b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIERiIiwiZGJjb24iOjEsaImNwdV9wZXJjZW50IjoyNS4wLCJjcHVfbG9hZCI6MC4wLCJtZW1fcGVyY2VudCI6MTEuOSwibWVtX3RvdGFsaIjo1MTIuMCwibWVtX3VzZWQiOjYwLjksaIm1libV9hdmFpbGFiZWwiOjQ1MS4xLCJkaXNrIjoxLCJkaXNrX3BlicmNlibnRfZTo9IjoxNy43LCJkaXNrX3RvdGFsaX2U6PSI6NjgzLjU5LCJkaXNrX2ZyZWVfZTo9Ijo1NjIuNDMsaImRpc2tfdXNliZF9liOj0iOjEyMS4xNiwiZGlizayI6MSwiZGliza19wZXJjZW50X2c6PSI6MjUuMywiZGliza190b3RhbF9nOj0iOjY4My41OSwiZGliza19mcmVliX2c6PSI6NTEwLjQ0LCJkaXNrX3VzZWRfZzo9IjoxNzMuMTUsaImRpc2saiOjEsaImRpc2tfcGVyY2VudF9oOj0iOjAbuNiwiZGliza190b3RhbF9oOj0iOjM3Ny45MywiZGliza19mcmVliX2g6PSI6Mzc1LjU3LCJkaXNrX3VzZWRfaDo9IjoyLjM2LCJ1c2VycyI6InN1c2VyKG5hbWU9RHRliYywgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE1ODc4NzM3MDQuMTcxMTM2LCBwaWQ9Tm9uZSksaIHN1c2VyKG5hbWU9QWRtaW5pc3RyYXRvciwgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE1ODc4ODIyMjYuMTQzNjk2LCBwaWQ9Tm9uZSkiLCJiYWNrdXAbiOjEsaImxhdGVzdF9maWxliIjoiRzpcRGFpbHliCYWNrdXBcRGFpbHlicRW1kYWRfTW9zaGFyZWthdF9iYWNrdXBfMjAbyMF8wNF8yNV8yMzAbwMDFfNDEwNjQ5Mi5iYWsaiLCJNYW51ZmFjdHVyZXIiOiJ2bXdhcmUsaaW5jLiIsaIk1vZGVsaIjoidm13YXJlidmliydHVhbHBsaYXRmb3JtIiwiQ29yZXNDb3VudCI6IjQiLCJDcHVOdW0iOiIxNiIsaIliNlicmlihbDIiOiI5MTY4NGQ1Ni01OWQyLTIyYzMtOTc3Yy1mMmFjNjdjOTc3MjkiLCJTZXJpYWwxIjoidm13YXJliLTU2NGQ2ODkxZDI1OWMzMjItOTc3Y2YyYWM2N2M5NzcyOSIsaIkRhdGUiOiIyMDIwLTAb0LTI2IDExOjAbwOjQ5Ljk2ODI3NyIsaIliRpbWUiOiIxMTowMCIsaImVycm1zZyI6IiJ9'</body></html>";
               //b'
                string[] allSiteResponse = siteResponse.Replace("'</body></html>", "").Replace("<html><body>", "").Replace("b'","").Replace("'","").Replace("######", "###").Split("###",StringSplitOptions.RemoveEmptyEntries);
                int statuscodeAll = 1;
                
                string jsonDataAll = "[";
                SiteResponse scAll = new SiteResponse(); 
                scAll.projectname = site.ProjectName;
                scAll.backup=1;scAll.dbcon=1;scAll.disk=1;scAll.webservice=1;
                scAll.cpu_percent=0;scAll.mem_percent=0;

                foreach(var item in allSiteResponse)
                {
                    int statuscode = 1;
                    string jsonData = de_asymetricAbbas(item);
                    jsonDataAll+= jsonData + ",";
                    SiteResponse sc = JsonConvert.DeserializeObject<SiteResponse>(jsonData);
                    string smsError = sc.projectname;

                    if(scAll.Date==null)
                    {
                        scAll.Date = sc.Date;
                        scAll.Time = sc.Time;
                    }
                    
                    DateTime dt = DateTime.Parse(sc.Date);
                    if (DateTime.Now > dt.AddMinutes(45))
                    {
                        statuscode = 0;
                        statuscodeAll=0;
                        smsError += "##Monitor Service is not working" +"# now:" +DateTime.Now.ToString()+  "#Sys:" +sc.Date.ToString();
                    }
                    if (DateTime.Now.AddMinutes(10) < dt)
                    {
                        statuscode = 0;
                        statuscodeAll=0;
                        smsError += "##Check Server DateTime";
                    }
                    
                    if(sc.mem_percent>scAll.mem_percent)
                        scAll.mem_percent=sc.mem_percent;
                    
                    if(sc.cpu_percent>scAll.cpu_percent)
                        scAll.cpu_percent=sc.cpu_percent;

                    
                    if (sc.disk == 0 || sc.dbcon == 0 || sc.backup == 0 || sc.webservice == 0)
                    {
                        statuscode = 0;
                        statuscodeAll=0;

                        if (sc.disk == 0)
                        {
                            smsError += "##Disk Is Full or Will be Full";
                            scAll.disk = 0;
                        }
                        if (sc.dbcon == 0)
                        {
                            smsError += "##Database Connection Error";
                            scAll.dbcon = 0;
                        }
                        if (sc.backup == 0)
                        {
                            smsError += "##Database Backup Error";
                            scAll.backup = 0;
                        }
                        if (sc.webservice == 0)
                        {
                            smsError += "##Webservice Error";
                            scAll.webservice = 0;
                        }


                    }
                    
                    if (statuscode == 0)
                    {
                       try{
                        string smsText = sc.projectname + ":" + smsError;
                        Notification nf = new Notification();
                        await Task.Run(() => nf.sendSmsAsyn(site.CellPhones, smsText));
                        await Task.Run(() => nf.sendEmailAsync(site.Receivers, smsText,
                        DateTime.Now.ToLongDateString() + "#" + smsText + "\n \t" + jsonData));
                       }catch(Exception exNt)
                       {
                           await Utility.writeErrorAsync(exNt.Message);
                       }
                    }
                    
                }
                jsonDataAll += "]";
                StoreData st = new StoreData();
                await Task.Run(() => st.saveDataAsyn(scAll, statuscodeAll, jsonDataAll));
                
            }
            catch (Exception e)
            {
                StoreData st = new StoreData();
                await Task.Run(() => st.writeEmptyData(site,e.Message.ToString(),false));
                await Utility.writeErrorAsync("checkSitesAsyn:"+site.ProjectName + "##" + siteResponse + "##"+ e.Message);
            }
            
        }
       private  string de_asymetricAbbas(string txt)
        {
            //#Ab sa li
            string base64Encoded = txt;
            base64Encoded = txt.Replace("Ab", "A").Replace("sa", "s").Replace("li", "l");
            byte[] data = System.Convert.FromBase64String(base64Encoded);
            string base64Decoded = System.Text.UTF8Encoding.UTF8.GetString(data);
            //base64Decoded = base64Decoded.Replace("\"", "'").Replace("﻿{", "{");
            base64Decoded = base64Decoded.Replace("﻿{", "{").Replace("\\", "/");
            return base64Decoded;
        }
    }
}