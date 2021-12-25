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
            //Monitor monitor1 = new Monitor();
            //await monitor1.checkSitesAsyn(null, "");
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
                        
                        //Else get the scan file from host
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