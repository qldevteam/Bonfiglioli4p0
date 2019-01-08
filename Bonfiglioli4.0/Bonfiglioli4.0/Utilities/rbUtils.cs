//using BarcodeReader;
using qFluid.Cls;
//using qFluid.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace nsUtils
{
    internal class rbUtils
    {
        //public IQueryable<BarcodeReader.Entity.Pallet> plt;
        public bool bDebug = true;//log errori
        public bool bSim = false;//simulazione dati

        internal bool Ping(string p_IP)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try
            {

                PingReply reply = pingSender.Send(p_IP, timeout, buffer, options);

                if (reply.Status != IPStatus.Success)
                {
                    //Console.WriteLine("Address: {0}", reply.Address.ToString());
                    //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                    //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                    //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                    //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                    //} else {

                    //MessageBox.Show("Comando Ping su : " + p_IP + "          fallito !!", p_IP.ToString());
                    return false;
                }
            }
            catch (Exception ee)
            {
                string asa = ee.Message;
                return false;
            }


            return true;
        }

        /// <summary>
        /// end application stop application
        /// </summary>
        internal void AppExit()
        {

            try
            {
                var exitCode = 0;
                string appExitCodeTextBox_Text = "0";
                int.TryParse(appExitCodeTextBox_Text, out exitCode);
                System.Windows.Application.Current.Shutdown(exitCode);

                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception)
            {

            }

            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {

            }

            //try
            //{
            //    Bonfiglioli4p0.App.Current.Shutdown();
            //}
            //catch (Exception)
            //{

            //}
            return;
        }


        internal void GestErr(Exception ee)
        {
            Log.Logger?.Error(ee.Message);

        }
        internal void GestErr(Exception ee, string pCustom)
        {
            Log.Logger?.Error(ee, pCustom);

        }
        internal void Warning(string pCustom)
        {
            Log.Logger?.Warning(pCustom);

        }

        /// <summary>
        /// Calcolo ordinata all'ascissa avendo coeff angolare
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pSlope"> Coeff angolare </param>
        /// <returns></returns>
        internal float OrdAAsc(int pX, int pY, float pSlope)
        {
            //y = mx + q --> q= y - mx
            return pY - (pSlope * pX);
        }

        /// <summary>
        ///  Calcolo coeff angolare con equazione della retta passante per 2 punti
        /// </summary>
        /// <param name="pXPrec"></param>
        /// <param name="pYPrec"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        internal float CoeffAng(Single pXPrec, Single pYPrec, Single pX, Single pY)
        {

            //y = mx + q
            //q = y - mx
            //y - mx = y1 - mx1  --> y - y1 = mx - mx1 --> y- y1 = m (x - x1) --> (y - y1) / (x - x1) = m

            if ((pXPrec - pX) != 0)
            {
                //pYPrec = 20;
                //pXPrec = 20;
                //pY = 40;
                //pX = 40;
                float a1 = (pYPrec - pY);
                float a2 = (pXPrec - pX);
                float a3 = a1 / a2;
                Single ree = (pYPrec - pY) / (pXPrec - pX);

                return (pYPrec - pY) / (pXPrec - pX);
            }
            else
            {
                return 0;
            }
        }

        public void ResetMemDB()
        {
            string sPlc;
            int sDBn;
            int sOfn;
            int sOfbn;
            byte[] bRes = new byte[1];
            double result;

            //using (ModelQFluid _cntx = new ModelQFluid())
            //{

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "ReqCicloVersa").FirstOrDefault();
            //    cpc.bReq = false;

            //    //cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "PUserReqCicloVersa").FirstOrDefault();
            //    //cpc.bReq = false;

            //    //cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "UserReqCicloVersa").FirstOrDefault();
            //    //cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Anagrafica").FirstOrDefault();
            //    cpc.sValue = null;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Versamenti").FirstOrDefault();
            //    cpc.sValue = null;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Svuotamenti").FirstOrDefault();
            //    cpc.sValue = null;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Semiautomatico").FirstOrDefault();
            //    cpc.sValue = null;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "OpenBox0Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "OpenBox0Rbt").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "OpenBox1Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "OpenBox1Rbt").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "CloseBox0Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "CloseBox0Rbt").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "CloseBox1Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "CloseBox1Rbt").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "VersaCloseBox0Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "VersaCloseBox1Op").FirstOrDefault();
            //    cpc.sValue = "-1";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "RobotInProd").FirstOrDefault();
            //    cpc.sValue = "0";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "RobotInAlm").FirstOrDefault();
            //    cpc.sValue = "0";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Mag1InAlm").FirstOrDefault();
            //    cpc.sValue = "0";
            //    cpc.bReq = false;

            //    cpc = _cntx.cPCComunications.Where(x => x.Mnemo == "Mag2InAlm").FirstOrDefault();
            //    cpc.sValue = "0";
            //    cpc.bReq = false;

            //    _cntx.SaveChanges();

            //}
        }



        //#### Necessaria quando sql viene interrogato a tempo per evitare la sovrascrittura causa immediata interrogazione dopo update
        public static readonly int iWaitCloseConnection = 80;

        //lele da rimettere
        //internal void SaveWithSleep( ModelQFluid p_cntx)
        //{
        //    p_cntx.SaveChanges();
        //    System.Threading.Thread.Sleep(iWaitCloseConnection);
        //}

        internal string GestTryCatch(Exception exc_,string comment_)
        {
            StringBuilder sb = new StringBuilder("\n");
            sb.Append("\n***** Error! *****");
            sb.Append("\nMember name: " + exc_.TargetSite);
            sb.Append("\nClass defining member: " + exc_.TargetSite.DeclaringType);
            sb.Append("\nMember type: " + exc_.TargetSite.MemberType);
            sb.Append("\nMessage: " + exc_.Message);
            sb.Append("\nSource: " + exc_.Source);
            sb.Append("\nStack: " + exc_.StackTrace);
            sb.Append("\nComment : " + "_cntxp.cPCComunications.Where(x => x.bReq == true && (x.cPCComunicationID > 14 && x.cPCComunicationID < 23))");
            sb.Append("\nHelp Link: " + exc_.HelpLink);
            sb.Append("\n***********************************************");
            sb.Append("\n");

            return sb.ToString();
        }
    }

    interface iSaveSleep{
        void SaveChanges();
        }
}
