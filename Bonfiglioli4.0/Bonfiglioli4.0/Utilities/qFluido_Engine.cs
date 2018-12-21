using nsUtils;
//using qFluid.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Serilog;

namespace qFluid.Cls
{
    internal class qFluid_Engine
    {
        rbUtils rbu = new rbUtils();
        //ushort ID;
        //ushort StartAddress;
        System.Windows.Threading.DispatcherTimer tmrJournaly;
        System.Windows.Threading.DispatcherTimer tmrEveryMinutes;

        private enum tip
        {
            niente,
            radBits,
            radByte,
            radWord,
            radString
        }


        internal qFluid_Engine()
        {
            //Log.Logger.Information("qFluido engine Application start");
            tmrJournaly = new DispatcherTimer();
            tmrJournaly.Tick += new EventHandler(tmrJournaly_Tick);
            tmrJournaly.Interval = TimeSpan.FromMinutes(1);
            //tmrJournaly.Start();

            tmrEveryMinutes = new DispatcherTimer();
            tmrEveryMinutes.Tick += new EventHandler(tmrEveryMinutes_Tick);
            tmrEveryMinutes.Interval = TimeSpan.FromSeconds(1);
            tmrEveryMinutes.Start();


        }

        /// <summary>
        /// Evento giornaliero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrJournaly_Tick(object sender, EventArgs e)
        {
            var DailyTime = "00:01:00";
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                       int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => MyMethod());

            Console.Read();
        }

        /// <summary>
        /// Evento ogni 10 min
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrEveryMinutes_Tick(object sender, EventArgs e)
        {
        }

        private void DoEvery10Min()
        {

        }

        private double GoalMin()
        {
            int mii = 10 - DateTime.Now.Minute % 10;
            return mii;
        }

        private void MyMethod()
        {
            if(Bonfiglioli4._0.App.IsIDE())     Console.Beep();

        }


        // ------------------------------------------------------------------------
        /// <summary>Destroy master instance.</summary>
        ~qFluid_Engine()
        {
            Dispose();
        }

        // ------------------------------------------------------------------------
        /// <summary>Destroy master instance</summary>
        public void Dispose()
        {
          
        }

    

        private void SaveDB()
        {
        
        }

     
    }

 

}
