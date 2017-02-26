using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class LogProvider
    { 
        private static string LogFolder = "Log/";
 
         static LogProvider()
         {
             Directory.CreateDirectory(LogFolder);
         }
         public static void AppendRecord(string record)
         {
             File.AppendAllLines(LogFolder + DateTime.Today.Date.ToString(), new string[] { DateTime.Now.ToShortTimeString() + record});
         }
    }
}
