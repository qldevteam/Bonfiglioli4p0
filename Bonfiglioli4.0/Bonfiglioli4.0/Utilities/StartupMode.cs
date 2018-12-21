//using qFluid;
//using qFluid.DynamicComparer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utilities
{
    [Serializable]
    public class StartupMode
    {
        public enum eMode
        {
            eRestart,               // Usare per indicare un riavvio a seguito login
            eUserUser,            // Usare per indicare avvio con  utente user
            eUserAdmin,              // Usare per indicare avvio con  utente admin
            Fatal,              // Opz
        }

        protected DateTime when_;       // L'istante in cui è stata generata la segnalazione
        protected eMode what_;       // Il tipo di segnalazione (Info, Warning, Error...)
        protected string text_;     // Il testo della segnalazione
        protected string file_;     // Il nome del file
        protected string method_;       // La funzione da cui proviene la segnalazione
        protected string class_;        // La classe da cui proviene la segnalazione
        protected string stack_;        // Lo stato dello stack al momento dell'eccezione

        /// <summary>
        /// Costruttore da chiamare in assenza di eccezioni
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        internal StartupMode(eMode p, String t)
        {
            when_ = DateTime.Now;
            what_ = p;
            text_ = t;
        }


    }
}
