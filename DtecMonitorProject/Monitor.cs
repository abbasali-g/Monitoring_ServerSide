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
                //siteResponse ="<html><body>b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIFdliYiBTZXJ2aWNliIiwiZGJjb24iOjIsaImNwdV9wZXJjZW50Ijo4LjMsaImNwdV9sab2FkIjowLjAbsaIm1libV9wZXJjZW50IjoxNi45LCJtZW1fdG90YWwiOjMyLjAbsaIm1libV91c2VkIjo1LjQsaIm1libV9hdmFpbGFiZWwiOjI2LjYsaImRpc2tfcGVyY2VudF9jOj0iOjU0LjMsaImRpc2tfdG90YWxfYzo9Ijo5NC41MSwiZGliza19mcmVliX2M6PSI6NDMuMTYsaImRpc2tfdXNliZF9jOj0iOjUxLjM1LCJkaXNrIjoxLCJ3ZWJzZXJ2aWNliU3RhdHVzQ29kZV9TYWJ0QWh2YWxQaW5nPSI6IjIwMCIsaIndliYnNlicnZpY2VfU2FidEFodmFsaUGliuZz0iOiIxIiwid2Vic2VydmlijZURlidGFpbF9TYWJ0QWh2YWxQaW5nPSI6IiIsaIndliYnNlicnZpY2UiOiIxIiwidXNlicnMiOiJzdXNlicihuYW1liPUFkbWliuaXN0cmF0b3IsaIHRlicm1pbmFsaPU5vbmUsaIGhvc3Q9Mi4xODYuMTE3LjE3Niwgc3RhcnRliZD0xNjYwOTk0NjU0Ljg3NzY3NCwgcGlikPU5vbmUpIiwiYmFja3VwIjoyLCJNYW51ZmFjdHVyZXIiOiJ2bXdhcmUsaaW5jLiIsaIk1vZGVsaIjoidm13YXJlidmliydHVhbHBsaYXRmb3JtIiwiQ29yZXNDb3VudCI6IjgiLCJDcHVOdW0iOiIxNiIsaIliNlicmlihbDIiOiI5MjlimMTE0Mi01MzZkLTM3ZjItMzBiMS00ODEzODAb1M2Q5MjEiLCJTZXJpYWwxIjoidm13YXJliLTQyMTE5ZjkyNmQ1M2YyMzctMzBiMTQ4MTM4MDUzZDkyMSIsaIkRhdGUiOiIyMDIyLTAb4LTIwIDE1OjU3OjAb4LjMzMjE1NyIsaIliRpbWUiOiIxNTo1NyIsaImVycm1zZyI6IiJ9'###b'b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIEVrcmFtIiwiZGJjb24iOjIsaImNwdV9wZXJjZW50IjoiMCIsaIm1libV9wZXJjZW50IjoiMCIsaImRpc2saiOiIwIiwid2Vic2VydmlijZSI6IjIiLCJ1c2VycyI6IliNlicnZliciBOb3QgUmVjaGFibGUiLCJiYWNrdXAbiOiIyIn0=''###b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIERhdGFiYXNliIE1haW4iLCJkYmNvbiI6MSwiY3B1X3BlicmNlibnQiOjAbuMCwiY3B1X2xvYWQiOjAbuMCwibWVtX3BlicmNlibnQiOjcxLjIsaIm1libV90b3RhbCI6NTEyLjAbsaIm1libV91c2VkIjozNjQuNiwibWVtX2F2YWlisaYWJlibCI6MTQ3LjQsaImRpc2tfcGVyY2VudF9jOj0iOjE4LjEsaImRpc2tfdG90YWxfYzo9IjozMDIuMzksaImRpc2tfZnJliZV9jOj0iOjI0Ny42NiwiZGliza191c2VkX2M6PSI6NTQuNzMsaImRpc2tfcGVyY2VudF9liOj0iOjU0LjQsaImRpc2tfdG90YWxfZTo9Ijo2ODMuNTksaImRpc2tfZnJliZV9liOj0iOjMxMS44OSwiZGliza191c2VkX2U6PSI6MzcxLjcxLCJkaXNrX3BlicmNlibnRfZzo9IjozMC45LCJkaXNrX3RvdGFsaX2c6PSI6NjgzLjU5LCJkaXNrX2ZyZWVfZzo9Ijo0NzIuNTMsaImRpc2tfdXNliZF9nOj0iOjIxMS4wNiwiZGliza19wZXJjZW50X2g6PSI6MS4yLCJkaXNrX3RvdGFsaX2g6PSI6Mzc3LjkzLCJkaXNrX2ZyZWVfaDo9IjozNzMuMjYsaImRpc2tfdXNliZF9oOj0iOjQuNjcsaImRpc2saiOjEsaIndliYnNlicnZpY2UiOiIyIiwid2Vic2VydmlijZSI6IjEiLCJ1c2VycyI6InN1c2VyKG5hbWU9cGFrZGVsaLCB0ZXJtaW5hbD1Ob25liLCBob3N0PU5vbmUsaIHN0YXJ0ZWQ9MTY2MDk2ODI5Mi4wNjU3OTM4LCBwaWQ9Tm9uZSksaIHN1c2VyKG5hbWU9QWRtaW5pc3RyYXRvciwgdGVybWliuYWw9Tm9uZSwgaG9zdD0yLjE4Ni4xMTcuMTc2LCBzdGFydGVkPTE2NjAb5ODMzMzUuOTk2MjIwNCwgcGlikPU5vbmUpLCBzdXNlicihuYW1liPWZhcmhhZCwgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE2NTcyOTg0MDkuNjI2NTAbxNiwgcGlikPU5vbmUpIiwiYmFja3VwIjoyLCJNYW51ZmFjdHVyZXIiOiJ2bXdhcmUsaaW5jLiIsaIk1vZGVsaIjoidm13YXJlidmliydHVhbHBsaYXRmb3JtIiwiQ29yZXNDb3VudCI6IjgiLCJDcHVOdW0iOiIzMiIsaIliNlicmlihbDIiOiI5MTY4NGQ1Ni01OWQyLTIyYzMtOTc3Yy1mMmFjNjdjOTc3MjkiLCJTZXJpYWwxIjoidm13YXJliLTU2NGQ2ODkxZDI1OWMzMjItOTc3Y2YyYWM2N2M5NzcyOSIsaIkRhdGUiOiIyMDIyLTAb4LTIwIDE1OjU1OjQ1Ljg3NTc4NSIsaIliRpbWUiOiIxNTo1NSIsaImVycm1zZyI6IiJ9'###b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIERhdGFiYXNliIFJlicCIsaImRiY29uIjoxLCJjcHVfcGVyY2VudCI6MC4wLCJjcHVfbG9hZCI6MC4wLCJtZW1fcGVyY2VudCI6NTAbuNSwibWVtX3RvdGFsaIjo2NC4wLCJtZW1fdXNliZCI6MzIuMywibWVtX2F2YWlisaYWJlibCI6MzEuNywiZGliza19wZXJjZW50X2M6PSI6NDYuOCwiZGliza190b3RhbF9jOj0iOjM0OS41MSwiZGliza19mcmVliX2M6PSI6MTg1Ljc5LCJkaXNrX3VzZWRfYzo9IjoxNjMuNzIsaImRpc2tfcGVyY2VudF9liOj0iOjI1LjgsaImRpc2tfdG90YWxfZTo9Ijo1MDAbuMCwiZGliza19mcmVliX2U6PSI6MzcxLjAb0LCJkaXNrX3VzZWRfZTo9IjoxMjguOTUsaImRpc2saiOjEsaIndliYnNlicnZpY2UiOiIyIiwid2Vic2VydmlijZSI6IjEiLCJ1c2VycyI6InN1c2VyKG5hbWU9ZmFyaGFkLCB0ZXJtaW5hbD1Ob25liLCBob3N0PU5vbmUsaIHN0YXJ0ZWQ9MTY1MzgyMTE5Mi42OTcwNCwgcGlikPU5vbmUpLCBzdXNlicihuYW1liPUFkbWliuaXN0cmF0b3IsaIHRlicm1pbmFsaPU5vbmUsaIGhvc3Q9Tm9uZSwgc3RhcnRliZD0xNjYwNTUzMTMxLjcwMzQwMDksaIHBpZD1Ob25liKSIsaImJhY2t1cCI6MiwiTWFudWZhY3R1cmVyIjoidm13YXJliLGliuYy4iLCJNb2RlibCI6InZtd2FyZXZpcnR1YWxwbGF0Zm9ybSIsaIkNvcmVzQ291bnQiOiI0IiwiQ3B1TnVtIjoiOCIsaIliNlicmlihbDIiOiI5MzM5MTE0Mi02MzQ0LWI2NjItN2E0Ny02N2IwZmI3YTRjNmQiLCJTZXJpYWwxIjoidm13YXJliLTQyMTEzOTkzNDQ2MzYyYjYtN2E0NzY3YjBmYjdhNGM2ZCIsaIkRhdGUiOiIyMDIyLTAb4LTIwIDE1OjUzOjM3Ljk2NDgzMCIsaIliRpbWUiOiIxNTo1MyIsaImVycm1zZyI6IiJ9'###</body></html>";
                //siteResponse ="<html><body>b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIERiIiwiZGJjb24iOjEsaImNwdV9wZXJjZW50IjoyNS4wLCJjcHVfbG9hZCI6MC4wLCJtZW1fcGVyY2VudCI6MTEuOSwibWVtX3RvdGFsaIjo1MTIuMCwibWVtX3VzZWQiOjYwLjksaIm1libV9hdmFpbGFiZWwiOjQ1MS4xLCJkaXNrIjoxLCJkaXNrX3BlicmNlibnRfZTo9IjoxNy43LCJkaXNrX3RvdGFsaX2U6PSI6NjgzLjU5LCJkaXNrX2ZyZWVfZTo9Ijo1NjIuNDMsaImRpc2tfdXNliZF9liOj0iOjEyMS4xNiwiZGlizayI6MSwiZGliza19wZXJjZW50X2c6PSI6MjUuMywiZGliza190b3RhbF9nOj0iOjY4My41OSwiZGliza19mcmVliX2c6PSI6NTEwLjQ0LCJkaXNrX3VzZWRfZzo9IjoxNzMuMTUsaImRpc2saiOjEsaImRpc2tfcGVyY2VudF9oOj0iOjAbuNiwiZGliza190b3RhbF9oOj0iOjM3Ny45MywiZGliza19mcmVliX2g6PSI6Mzc1LjU3LCJkaXNrX3VzZWRfaDo9IjoyLjM2LCJ1c2VycyI6InN1c2VyKG5hbWU9RHRliYywgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE1ODc4NzM3MDQuMTcxMTM2LCBwaWQ9Tm9uZSksaIHN1c2VyKG5hbWU9QWRtaW5pc3RyYXRvciwgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE1ODc4ODIyMjYuMTQzNjk2LCBwaWQ9Tm9uZSkiLCJiYWNrdXAbiOjEsaImxhdGVzdF9maWxliIjoiRzpcRGFpbHliCYWNrdXBcRGFpbHlicRW1kYWRfTW9zaGFyZWthdF9iYWNrdXBfMjAbyMF8wNF8yNV8yMzAbwMDFfNDEwNjQ5Mi5iYWsaiLCJNYW51ZmFjdHVyZXIiOiJ2bXdhcmUsaaW5jLiIsaIk1vZGVsaIjoidm13YXJlidmliydHVhbHBsaYXRmb3JtIiwiQ29yZXNDb3VudCI6IjQiLCJDcHVOdW0iOiIxNiIsaIliNlicmlihbDIiOiI5MTY4NGQ1Ni01OWQyLTIyYzMtOTc3Yy1mMmFjNjdjOTc3MjkiLCJTZXJpYWwxIjoidm13YXJliLTU2NGQ2ODkxZDI1OWMzMjItOTc3Y2YyYWM2N2M5NzcyOSIsaIkRhdGUiOiIyMDIwLTAb0LTI2IDExOjAbwOjQ5Ljk2ODI3NyIsaIliRpbWUiOiIxMTowMCIsaImVycm1zZyI6IiJ9'</body></html>";
               //b'
                string[] allSiteResponse = siteResponse.Replace("</body></html>", "").Replace("<html><body>", "").Replace("b'","").Replace("'","").Replace("######", "###").Split("###",StringSplitOptions.RemoveEmptyEntries);
                int statuscodeAll = 1;
                
                string jsonDataAll = "[";
                SiteResponse scAll = new SiteResponse(); 
                scAll.projectname = site.ProjectName;
                scAll.backup=1;scAll.dbcon=1;scAll.disk=1;scAll.webservice=1;
                scAll.cpu_percent=0;scAll.mem_percent=0;

                foreach(var item in allSiteResponse)
                {
                    try
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
                    catch (Exception exFor)
                    {
                        await Utility.writeErrorAsync(exFor.Message);
                    }
                    
                }
                jsonDataAll += "]";
                jsonDataAll = jsonDataAll.Replace(",]","]");
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