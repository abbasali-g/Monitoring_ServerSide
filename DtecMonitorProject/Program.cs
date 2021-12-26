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
            string siteContents = "b'eyJwcm9qZWN0bmFtZSI6IkRhZGUgVGFtaW4iLCJkYmNvbiI6MSwiY3B1X3BlicmNlibnQiOjAbuMCwiY3B1X2xvYWQiOjAbuMjMsaIm1libV9wZXJjZW50Ijo2OC44LCJtZW1fdG90YWwiOjcuNywibWVtX3VzZWQiOjQuMiwibWVtX2F2YWlisaYWJlibCI6Mi40LCJkaXNrIjowLCJkaXNrX3BlicmNlibnRfLz0iOjU0LjgsaImRpc2tfdG90YWxfLz0iOjU5LjMzLCJkaXNrX2ZyZWVfLz0iOjI1LjQ1LCJkaXNrX3VzZWRfLz0iOjMwLjg0LCJkaXNrIjowLCJkaXNrX3BlicmNlibnRfLz0iOjU0LjgsaImRpc2tfdG90YWxfLz0iOjU5LjMzLCJkaXNrX2ZyZWVfLz0iOjI1LjQ1LCJkaXNrX3VzZWRfLz0iOjMwLjg0LCJ3ZWJzZXJ2aWNliU3RhdHVzQ29kZSI6IjIwMCIsaIndliYnNlicnZpY2UiOjAbsaInVzZXJzIjoic3VzZXIobmFtZT1hYmJhcywgdGVybWliuYWw9LTAbsaIGhvc3Q9bG9jYWxob3N0LCBzdGFydGVkPTE2NDAb0OTQ0NjQuMCwgcGlikPTIxMDcpIiwiYmFja3VwIjoyLCJTZXJpYWwxIjoiMDAbwMDAbwMDAbwMCIsaIkRhdGUiOiIyMDIxLTEyLTI2IDE0OjI2OjIzLjYxNTg3NSIsaIliRpbWUiOiIxNDoyNiIsaImVycm1zZyI6IndliYnNlicnZpY2U9cnpzeXM9bW9kdWxliIGRtaWRliY29kZSBoYXMgbm8gYXR0cmliidXRliIERNSURliY29kZSJ9'###b'eyJwcm9qZWN0bmFtZSI6IkRhZGUgVGFtaW4iLCJkYmNvbiI6MSwiY3B1X3BlicmNlibnQiOjIwLjAbsaImNwdV9sab2FkIjowLjIzLCJtZW1fcGVyY2VudCI6NjguNywibWVtX3RvdGFsaIjo3LjcsaIm1libV91c2VkIjo0LjIsaIm1libV9hdmFpbGFiZWwiOjIuNCwiZGlizayI6MCwiZGliza19wZXJjZW50Xy89Ijo1NC44LCJkaXNrX3RvdGFsaXy89Ijo1OS4zMywiZGliza19mcmVliXy89IjoyNS40NSwiZGliza191c2VkXy89IjozMC44NCwiZGlizayI6MCwiZGliza19wZXJjZW50Xy89Ijo1NC44LCJkaXNrX3RvdGFsaXy89Ijo1OS4zMywiZGliza19mcmVliXy89IjoyNS40NSwiZGliza191c2VkXy89IjozMC44NCwid2Vic2VydmlijZVN0YXR1c0NvZGUiOiIyMDAbiLCJ3ZWJzZXJ2aWNliIjowLCJ1c2VycyI6InN1c2VyKG5hbWU9YWJiYXMsaIHRlicm1pbmFsaPS0wLCBob3N0PWxvY2FsaaG9zdCwgc3RhcnRliZD0xNjQwNDk0NDY0LjAbsaIHBpZD0yMTAb3KSIsaImJhY2t1cCI6MiwiU2VyaWFsaMSI6IjAbwMDAbwMDAbwMDAbiLCJEYXRliIjoiMjAbyMS0xMi0yNiAbxNDoyNjoyNC4wNTUyNDgiLCJUaW1liIjoiMTQ6MjYiLCJlicnJtc2ciOiJ3ZWJzZXJ2aWNliPXJ6c3lizPW1vZHVsaZSBkbWlikZWNvZGUgaGFzIG5vIGF0dHJpYnV0ZSBETUliEZWNvZGUifQ=='###'eyJwcm9qZWN0bmFtZSI6IkVrcmFtIE9ubGliuZSIsaImRiY29uIjoyLCJjcHVfcGVyY2VudCI6NDcuMSwiY3B1X2xvYWQiOjAbuMCwibWVtX3BlicmNlibnQiOjIzLjcsaIm1libV90b3RhbCI6NjQuMCwibWVtX3VzZWQiOjE1LjIsaIm1libV9hdmFpbGFiZWwiOjQ4LjgsaImRpc2saiOjAbsaImRpc2tfcGVyY2VudF9jOj0iOjk4LjQsaImRpc2tfdG90YWxfYzo9Ijo0NC41MSwiZGliza19mcmVliX2M6PSI6MC43LCJkaXNrX3VzZWRfYzo9Ijo0My44MSwidXNlicnMiOiJzdXNlicihuYW1liPWZhcmhhZCwgdGVybWliuYWw9Tm9uZSwgaG9zdD0wLjAbuMC4wLCBzdGFydGVkPTE1OTAb1NzU2MjQuODkwNTksaIHBpZD1Ob25liKSwgc3VzZXIobmFtZT1BZG1pbmlizdHJhdG9yLCB0ZXJtaW5hbD1Ob25liLCBob3N0PU5vbmUsaIHN0YXJ0ZWQ9MTU5NTQ5Mzk2Ny40NzYwNzExLCBwaWQ9Tm9uZSkiLCJiYWNrdXAbiOjIsaIk1hbnVmYWN0dXJliciI6InZtd2FyZSxpbmMuIiwiTW9kZWwiOiJ2bXdhcmV2aXJ0dWFsacGxhdGZvcm0iLCJDb3Jlic0NvdW50IjoiNCIsaIkNwdU51bSI6IjE2IiwiU2VyaWFsaMiI6IjhliOTk0ZDU2LWU5NjYtOGM1MS0yNjc2LTRliNWY3N2ViOGIwNCIsaIliNlicmlihbDEiOiJ2bXdhcmUtNTY0ZDk5OGU2NmU5NTE4Yy0yNjc2NGU1Zjc3ZWI4YjAb0IiwiRGF0ZSI6IjIwMjAbtMDctMjggMTU6NDk6MjAbuNzAb3OTIzIiwiVGlitZSI6IjE1OjQ5IiwiZXJybXNnIjoiIn0='###";
            string siteListJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+ "sites.json").Replace("\"", "'").Replace("\n", "").Replace("\r", "")
                    .Replace("\t", "");
            SiteListCol sitelistCol = JsonConvert.DeserializeObject<SiteListCol>(siteListJson);
            Monitor monitor1 = new Monitor();
            await monitor1.checkSitesAsyn(sitelistCol.SiteLists[0], siteContents);
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
                            }
                            catch (Exception ex)
                            {
                                await Task.Delay(5000);
                                await client.GetAsync(projSite.ProjectUrl.Replace("nositescan_", ""));
                            }
                            StoreData st = new StoreData();
                            await Task.Run(() => st.writeEmptyData(projSite, "", false));
                            continue;
                        }
                        
                        //else get the scan file from host
                        HttpResponseMessage response = null;
                        try
                        {
                            response = await client.GetAsync(projSite.ProjectUrl);
                        }
                        catch (Exception ex)
                        {
                            await Task.Delay(35000);
                            response = await client.GetAsync(projSite.ProjectUrl);
                        }

                        var siteContents = await response.Content.ReadAsStringAsync();
                        Monitor monitor = new Monitor();
                        await Task.Run(() => monitor.checkSitesAsyn(projSite,siteContents));

                    }
                    catch (Exception ex)
                    {
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