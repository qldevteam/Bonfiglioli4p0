//Installazione serilog per log in file
//Install-Package Serilog.Sinks.File
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadProgram4p0.Utilities
{
    public class mySerilog
    {
        public static void BatMobile()
        {
            //Install - Package Serilog.Sinks.RollingFile
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Environment.CurrentDirectory + @"\BatMobileLogs.txt", fileSizeLimitBytes: 1000000)
                .CreateLogger();
        }

        public static void StartEngine()
        {
            Log.Logger.Information("Engine Started");
    //        Log.Logger = new 
    //        Log.Logger.Information("Engine Started").MinimumLevel.Debug()
    //.WriteTo.File("log.txt")
    //.CreateLogger();


            //            Log.Logger = new LoggerConfiguration()
            ////.Enrich.With(new ThreadIdEnricher())
            //.WriteTo.Console(
            //    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            //.CreateLogger();
        }

        public static void LoadAmmunitions()
        {
            Log.Logger.Information("qFluid loaded");
        }

        public static void WriteLog(string p_S)
        {
            Log.Logger.Information(p_S);
        }
    }
}
