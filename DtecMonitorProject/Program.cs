using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using  Newtonsoft.Json;


namespace DtecMonitorProject
{
    class Program
    {

        static async Task Main(string[] args)
        {
            // Notification nt = new Notification();
            // await Task.Run(() => nt.sendSmsAsyn("09143011491", "تست"));
            // string siteContents = "b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIE1vc2hhcmVrYXQiLCJkYmNvbiI6MiwiY3B1X3BlicmNlibnQiOjAbuMCwiY3B1X2xvYWQiOjAbuMCwibWVtX3BlicmNlibnQiOjEyLjcsaIm1libV90b3RhbCI6MzIuMCwibWVtX3VzZWQiOjQuMSwibWVtX2F2YWlisaYWJlibCI6MjcuOSwiZGlizayI6MSwiZGliza19wZXJjZW50X2M6PSI6MjcuNiwiZGliza190b3RhbF9jOj0iOjI5OS41MSwiZGliza19mcmVliX2M6PSI6MjE2LjkxLCJkaXNrX3VzZWRfYzo9Ijo4Mi42LCJ3ZWJzZXJ2aWNliIjoiMiIsaInVzZXJzIjoic3VzZXIobmFtZT1mYXJoYWQsaIHRlicm1pbmFsaPU5vbmUsaIGhvc3Q9Tm9uZSwgc3RhcnRliZD0xNjQwNTkxNjU3LjExMzM0NjgsaIHBpZD1Ob25liKSwgc3VzZXIobmFtZT1BZG1pbmlizdHJhdG9yLCB0ZXJtaW5hbD1Ob25liLCBob3N0PU5vbmUsaIHN0YXJ0ZWQ9MTY0MDYxMjI1My43MTIxMDEyLCBwaWQ9Tm9uZSkiLCJiYWNrdXAbiOjIsaIk1hbnVmYWN0dXJliciI6InZtd2FyZSxpbmMuIiwiTW9kZWwiOiJ2bXdhcmV2aXJ0dWFsacGxhdGZvcm0iLCJDb3Jlic0NvdW50IjoiNCIsaIkNwdU51bSI6IjE2IiwiU2VyaWFsaMiI6IjU4YTAbxMTQyLTAbyM2QtNTQ5OS05Yjc3LWVhMzc2ZTJkM2E2NCIsaIliNlicmlihbDEiOiJ2bXdhcmUtNDIxMWEwNTgzZDAbyOTk1NC05Yjc3ZWEzNzZliMmQzYTY0IiwiRGF0ZSI6IjIwMjEtMTItMjcgMjAb6NTU6NDcuODgyNDg4IiwiVGlitZSI6IjIwOjU1IiwiZXJybXNnIjoiIn0='###b'eyJwcm9qZWN0bmFtZSI6IkVtZGFkIE1vc2hhcmVrYXRfU2FuZG9nIiwiZGJjb24iOjEsaImNwdV9wZXJjZW50IjowLjAbsaImNwdV9sab2FkIjowLjAbsaIm1libV9wZXJjZW50IjoxMi43LCJtZW1fdG90YWwiOjMyLjAbsaIm1libV91c2VkIjo0LjEsaIm1libV9hdmFpbGFiZWwiOjI3LjksaImRpc2saiOjEsaImRpc2tfcGVyY2VudF9jOj0iOjI3LjYsaImRpc2tfdG90YWxfYzo9IjoyOTkuNTEsaImRpc2tfZnJliZV9jOj0iOjIxNi45MSwiZGliza191c2VkX2M6PSI6ODIuNiwid2Vic2VydmlijZSI6IjIiLCJ1c2VycyI6InN1c2VyKG5hbWU9ZmFyaGFkLCB0ZXJtaW5hbD1Ob25liLCBob3N0PU5vbmUsaIHN0YXJ0ZWQ9MTY0MDU5MTY1Ny4xMTMzNDY4LCBwaWQ9Tm9uZSksaIHN1c2VyKG5hbWU9QWRtaW5pc3RyYXRvciwgdGVybWliuYWw9Tm9uZSwgaG9zdD1Ob25liLCBzdGFydGVkPTE2NDAb2MTIyNTMuNzEyMTAbxMiwgcGlikPU5vbmUpIiwiYmFja3VwIjoyLCJNYW51ZmFjdHVyZXIiOiJ2bXdhcmUsaaW5jLiIsaIk1vZGVsaIjoidm13YXJlidmliydHVhbHBsaYXRmb3JtIiwiQ29yZXNDb3VudCI6IjQiLCJDcHVOdW0iOiIxNiIsaIliNlicmlihbDIiOiI1OGEwMTE0Mi0wMjNkLTU0OTktOWI3Ny1liYTM3NmUyZDNhNjQiLCJTZXJpYWwxIjoidm13YXJliLTQyMTFhMDU4M2QwMjk5NTQtOWI3N2VhMzc2ZTJkM2E2NCIsaIkRhdGUiOiIyMDIxLTEyLTI3IDIwOjU1OjQ4LjUyMzEzNiIsaIliRpbWUiOiIyMDo1NSIsaImVycm1zZyI6IiJ9'###";
            // string siteListJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "sites.json").Replace("\"", "'").Replace("\n", "").Replace("\r", "")
            //          .Replace("\t", "");
            //  SiteListCol sitelistCol = JsonConvert.DeserializeObject<SiteListCol>(siteListJson);
            //  Monitor monitor1 = new Monitor();
            //  await monitor1.checkSitesAsyn(sitelistCol.SiteLists[0], siteContents);
            //Console.WriteLine(args.Length);
            if(args.Length==1)
            {
               Monitor monitor = new Monitor();
               string readData = await monitor.readSiteAsyn(args[0]);
               Console.WriteLine(readData);
            }
            else
               await startMonitoringAsync();
            

        }
       private static async Task startMonitoringAsync()
        {
            try
            {
                string siteListJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "sites.json").Replace("\"", "'").Replace("\n", "").Replace("\r", "")
                    .Replace("\t", "");
                SiteListCol sitelistCol = JsonConvert.DeserializeObject<SiteListCol>(siteListJson);
                
                foreach (SiteList projSite in sitelistCol.SiteLists)
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        //Just Ping site
                        
                        if (projSite.ProjectUrl.Contains("nositescan_"))
                        {
                            try
                            {
                                await client.GetAsync(projSite.ProjectUrl.Replace("nositescan_", ""));
                                StoreData st = new StoreData();
                                await Task.Run(() => st.writeEmptyData(projSite, "", false));
                                continue;
                            }
                            catch (Exception ex)
                            {
                                await Utility.writeErrorAsync(ex.Message);
                                StoreData st = new StoreData();
                                await Task.Run(() => st.writeEmptyData(projSite, "", true));
                            }
                           
                        }
                        
                        //else get the scan file from host
                        HttpResponseMessage response = null;
                        try
                        {
                            response = await client.GetAsync(projSite.ProjectUrl);
                        }
                        catch (Exception ex)
                        {
                            await Utility.writeErrorAsync(ex.Message);
                            await Task.Delay(35000);
                            response = await client.GetAsync(projSite.ProjectUrl);
                        }

                        
                        var siteContents = await response.Content.ReadAsStringAsync();
                        Monitor monitor = new Monitor();
                        await Task.Run(() => monitor.checkSitesAsyn(projSite,siteContents));

                    }
                    catch (Exception ex)
                    {
                        await Utility.writeErrorAsync(ex.Message);
                        StoreData st = new StoreData();
                        await Task.Run(() => st.writeEmptyData(projSite,ex.Message.ToString(),true));
                    }

                }
            }
            catch (Exception e)
            {
                await Utility.writeErrorAsync("Main:"+e.Message);
            }
            
        }

      

    }

}