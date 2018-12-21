using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Bonfiglioli4p0;
using Bonfiglioli4p0.ViewModel;

namespace Utilities
{
    public  class KeyLogger
    {
        Bonfiglioli4p0MWVM _parent = new Bonfiglioli4p0MWVM();

        #region CONSTRUCTOR
        public KeyLogger(Bonfiglioli4p0MWVM parent)
        {
            //this._parent = new qFluidMW();
            this._parent = parent;

        }
        #endregion

        #region DllImport
        //These Dll's will handle the hooks. Yaaar mateys!
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // The two dll imports below will handle the window hiding.

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        public delegate void ComunicazioneEventHandler(object sender, ComunicazioneEventArgs a);
        /// <summary>
        /// La definizione dell'evento che il keylogger sarà in grado di scatenare
        /// </summary>
        public static event ComunicazioneEventHandler InviaComunicazione;

        #region Variabili
        const int SW_HIDE = 0;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static string sPatternOut = ""; // il codice in uscita per dire che l'iesimo pattern è presente nella queue di stringhe lette. se < 0 non c'è la stringa 
        private static StringBuilder sbBuffer = new StringBuilder();
        private static bool bRunning = false;
        private static StringBuilder sbTest = new StringBuilder();
        private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        #region Properties
        /// <summary>
        /// !!!SE LEGGI QUESTA VARIABILE, NE CANCELLI IL VALORE 
        /// </summary>
        public static string PatternOut
        {
            get
            {
                if (sPatternOut == "")
                    return "";
                string s = sPatternOut; // in questo modo lo leggo solo una volta
                sPatternOut = "";
                return s;
            }
        }
        public static bool Running
        {
            get
            {
                return bRunning;
            }
        }
        #endregion
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(
          int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);


                //if ((Keys)vkCode == Keys.Tab)
                    if (vkCode == 170)
                    {

                        sPatternOut = sbBuffer.ToString();

                    //filtro i dati che non so perchè ma mi arrivano con questi caratteri di m..
                    try
                    {
                        string ss = sPatternOut.Replace("LMenuNumPad", "").ToString();
                        string ssa = ss.Replace("NumPad", "").ToString();
                        string ssab = ssa.Replace("CANC", "").ToString();
                        string ssaz = ssab.Replace("", "").ToString();
                        int unicode = int.Parse(ssaz.Substring(0, 3));
                        char character = (char)unicode;
                        string conv = character.ToString();
                        string Fine = conv;

                        int yui = ssa.Length;
                        for (int i = 3; i < yui; i += 2)
                        {
                            unicode = int.Parse(ssab.Substring(i, 2));
                            character = (char)unicode;
                            Fine = Fine + character;
                        }



                        //(Bonfiglioli4._0.App.Current.MainWindow as Bonfiglioli4p0MW).txtBCodeRx.Text = "";
                        //(Bonfiglioli4._0.App.Current.MainWindow as Bonfiglioli4p0MW).txtBCodeRx.Text = Fine;

                    }
                    catch (Exception ee)
                    {
                        string ss = ee.Message;


                    }
                    sbBuffer.Length = 0;
                    sPatternOut = "";
                }
                else
                {

                }


            }
            sbTest.Append(string.Format("{0}_{1} ", nCode, lParam));
            //(App.Current.MainWindow as qFluidoMW).txtBCodeRx.Text = sbBuffer.ToString();


            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public static void Start()
        {
            _hookID = SetHook(_proc);
            bRunning = true;
        }
        public static void Stop()
        {
            UnhookWindowsHookEx(_hookID);
            bRunning = false;
        }

        internal static void Reset()
        {
            sbBuffer.Length = 0;
            sPatternOut = "";
        }

        /// <summary>
        /// Tutti i metodi della classe Shuttle che vogliono inviare una comunicazione
        /// lo faranno chiamando questo metodo.
        /// "protected" significa che è visibile solo dalle classi che estendono la classe Shuttle.
        /// "virtual" significa che la classe che estende Shuttle può fare un override di questo metodo
        /// </summary>
        /// <param name="e">Classe che rappresenta l'unità di informazione trasportata dall'evento</param>
        public   void OnInviaComunicazione(ComunicazioneEventArgs e)
        {
            ComunicazioneEventHandler handler = InviaComunicazione;

            // Se nessuna torre di controllo è in ascolto allora l'evento è null
            if (handler != null)
            {
                // L'evento è un delegato, quindi eseguendo il delegato eseguiamo tutti i metodi che
                // sono registrati.
                handler(this, e);
            }
        }
    }

    public class ComunicazioneEventArgs : EventArgs
    {
        public ComunicazioneEventArgs(string s)
        {
            comunicazione = s;
        }

        private string comunicazione;

        public string Comunicazione
        {
            get { return comunicazione; }
            set { comunicazione = value; }
        }
    }
}
