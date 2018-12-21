using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static qFluid.Utilities.CyclePlc;

namespace qFluid.Utilities
{
    public static class csPlcCommand
    {
        static string sPlc;
        static int iDBn;
        static int iOfn;
        static int iOfbn;
        static double result;
        static byte[] bRes = new byte[1];

        //csBasePlcCommand csBasePC;

        public static bool CheckRemBox0()
        {
            //sPlc = cPCass0.Read_Rem;
            //iDBn = App.obsPlcComunication.Where(x => x.sMnemo == sPlc).Select(x => x.DB).FirstOrDefault();
            //iOfn = App.obsPlcComunication.Where(x => x.sMnemo == sPlc).Select(x => x.DbMem).FirstOrDefault();
            //iOfbn = App.obsPlcComunication.Where(x => x.sMnemo == sPlc).Select(x => x.DbMemBit).FirstOrDefault();

            //result = App.Sharp7.ReadDBDataB(iDBn, iOfn, iOfbn, bRes);
            //App.ErrPlcS7.DescErr = App.Sharp7.ErrorT((int)result);

            //if (bRes[0] == 1 | App.Sharp7.SimPLCS7) return true;
            //else return false;
            return false;
        }
    }
}

