using System;

namespace RaizenTestFuncional.Setup
{
    public class Configuration
    {
        public string project { get; set; }
        public string browser { get; set; }
        public string url { get; set; }
        public TimeSpan timeOut { get; set; }
        public bool   jiraRegisterTestCycle { get; set; }
        public string jiraProjectKey { get; set; }
        public string jiraTestRunName { get; set; }
        public string devOps { get; set; }
        public bool   activedReportTest { get; set; }
        public string reportFolder { get; set; }
        public string screenshots { get; set; }
        public bool safeCredentials { get; set; }
        public string nodePath { get; set; }
        public string appiumPath { get; set; }
        public string mobileExecution { get; set; }
        public bool   mobileAndroid { get; set; }
        public bool   mobileiOS { get; set; }
    }
}
