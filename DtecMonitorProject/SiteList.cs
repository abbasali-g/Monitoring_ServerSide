using System;
using System.Collections.Generic;

namespace DtecMonitorProject
{
    public class SiteListCol
    {
        public List<SiteList> SiteLists { get; set; }
    }

    public class SiteList
    {
        public string ProjectName  { get; set; }
        public string ProjectUrl   { get; set; }
        public string Receivers    { get; set; }
        public string CellPhones   { get; set; }
        
    }

    public class SiteResponse
    {
        public string projectname { get; set; }
        public int dbcon       { get; set; }
        public float cpu_percent { get; set; }
        public float mem_percent { get; set; }
        public int disk        { get; set; }
        public int backup      { get; set; }
        public string Date      { get; set; }
        public string Time      { get; set; }
        public string Serial1 { get; set; }

    }
    
    public class MySqlConnectionParam
    {
        public string username        { get; set; }
        public string DatabaseName     { get; set; }
        public string Password         { get; set; }
        public string  ServerAddress   { get; set; }
        public string  Port            { get; set; }
        
    }
    
   

}