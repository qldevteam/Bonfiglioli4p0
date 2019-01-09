// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Chocolatey" file="SourcesViewModel.cs">
//   Copyright 2014 - Present Rob Reynolds, the maintainers of Chocolatey, and RealDimensions Software, LLC
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

//using Bonfiglioli4p0.Entity;
//using PadProgram4p0.ViewModel.Commands;
//using PadProgram4p0.ViewModel.Services;
using PadProgram4p0.Utilities;
using PadProgram4p0.ViewModel.Base;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
//using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
//using qFluid.ViewModel.MediatorMVVM;
//using PadProgram4p0.ViewModel.Commands;
//using qFluid.Properties;
using PadProgram4p0.View;
using System.Windows;
using nsUtils;
using System.Text;
using PadProgram4p0.ViewModel.Commands;
using System.IO;
using System.Xml;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PadProgram4p0.ViewModel
{
    public enum cpSequence
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,

        /// <summary>
        /// Anagrafica
        /// </summary>
        Anagrafica = 1,

        /// <summary>
        /// 
        /// </summary>
        bByte = 2,

        /// <summary>
        /// 
        /// </summary>
        iInt = 3,


        /// <summary>
        /// 
        /// </summary>
        iDint = 4,

        /// <summary>
        /// 
        /// </summary>
        fReal = 5,
    }

    public sealed class PadProgram4p0MainMVVM : ViewModelBase
    {
        #region PRIVATE
        private int selectedIndex; // Set the field to whichever tab you want to start on
        private object _selectedIte; // Set the field to whichever tab you want to start on
        string sedIt;
        string stringReader;
        List<valori> xhtt;
        List<valori> zhtt;
        string lbl_;
        string txb_;
        string nomePrg_;
        string nrPrg_;
        mySettings mys;

        #endregion

        #region PUBLIC
        public int SelectedI
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public object SelectedIte
        {
            get { return _selectedIte; }
            set
            {
                _selectedIte = value;
                //sedIt=tbcFields.SelectedItem as TabItem
                sedIt = ((TabItem)_selectedIte).Header.ToString();
                OnPropertyChanged("SelectedIt");
            }
        }

        internal Action CloseAction { get; set; }
        internal rbUtils rbu = new rbUtils();

        private string _tbwH0b { get; set; }
        public string tbwH0b
        {
            get
            {
                return _tbwH0b;
            }

            set
            {
                _tbwH0b = value;
                base.OnPropertyChanged("tbwH0b");
            }
        }

        private string _tbwH200b;
        public string tbwH200b
        {
            get
            {
                return _tbwH200b;
            }

            set
            {
                _tbwH200b = value;
                base.OnPropertyChanged("tbwH200b");
            }
        }

        private string _tbwH400b;
        public string tbwH400b
        {
            get
            {
                return _tbwH400b;
            }

            set
            {
                _tbwH400b = value;
                base.OnPropertyChanged("tbwH400b");
            }
        }

        #endregion

        #region COMMAND
        public ICommand cmdOpenMySetting { get; set; }
        public ICommand cmdCreaFile { get; set; }
        public ICommand cmdInviaFile { get; set; }
        public ICommand cmdCloseApp { get; set; }
        public ICommand cmdOpenPrg { get; set; }
        #endregion


        #region COSTRUCTOR
        public PadProgram4p0MainMVVM()
        {
            mys = new mySettings();

            cmdOpenMySetting = new vcsOpenMySettings(this);
            cmdCreaFile = new vcsCreaFile(this);
            cmdInviaFile = new vcsInviaFile(this);
            cmdCloseApp = new vcsCloseApp(this);
            cmdOpenPrg = new vcsOpenPrg(this);
        }
        #endregion


        internal void vcmdOpenMySettings()
        {
            //if (App.Usr.bUAdmin)
            //{
            mys = new mySettings();

            mys.ShowDialog();
            //}
        }

        public void vcmdCreaFile()
        {
            PadProgram4p0Main.Self.mySer();

            xhtt = new List<valori>();
            zhtt = new List<valori>();

            ReadXml_();
            WriteXml();
        }

        void ReadXml_()
        {
            switch (sedIt)
            {
                case "C3147":
                    stringReader = AppContext.BaseDirectory + "MainGrid.xml";
                    break;
                case "C3148":
                    stringReader = AppContext.BaseDirectory + "MainGrid1.xml";
                    break;
                case "C3149":
                    stringReader = AppContext.BaseDirectory + "MainGrid2.xml";
                    break;
                default:
                    break;
            }

            if (File.Exists(stringReader))
            {
                XmlTextReader reader = new XmlTextReader(stringReader);
                while (reader.Read())
                {
                    // Do some work here on the data.
                    //System.Diagnostics.Debug.WriteLine(reader.Name);
                    if (reader.Name == "Label")
                    {
                        try
                        {

                            //System.Diagnostics.Debug.WriteLine(reader.ReadString());
                            lbl_ = reader.ReadString();
                        }
                        catch (Exception ee)
                        {
                            string ddd = ee.Message;
                        }
                    }
                    if (reader.Name == "TextBox")
                    {
                        try
                        {

                            //System.Diagnostics.Debug.WriteLine(reader.ReadString());
                            txb_ = reader.ReadString();

                            if (lbl_.Substring(0, 2) == "X_")
                            {
                                xhtt.Add(new valori(lbl_, txb_));
                            }
                            else
                            {
                                zhtt.Add(new valori(lbl_, txb_));
                            }

                            if (lbl_.Length >= 8 && lbl_.Substring(0, 8) == "NOME_PRG")
                            {
                                xhtt.Add(new valori(lbl_, txb_));
                                if (txb_.Length > 0) nomePrg_ = txb_;
                            }

                            if (lbl_.Length >= 7 && lbl_.Substring(0, 7) == "NUM_PRG")
                            {
                                xhtt.Add(new valori(lbl_, txb_));
                                if (txb_.Length > 0) nrPrg_ = txb_;
                            }

                            if (lbl_.Length >= 9)
                            {
                                switch (lbl_.Substring(0, 9))
                                {
                                    case "N_MAX_COL":
                                    case "N_INT_COL":
                                    case "B_ESCL_ST":
                                    case "B_ESCL_EM":
                                        xhtt.Add(new valori(lbl_, txb_));
                                        break;
                                    default:
                                        break;
                                }
                            }


                            if (lbl_.Length >= 3 && lbl_.Substring(0, 3) == "RET")
                            {
                                break;
                            }

                        }
                        catch (Exception ee)
                        {
                            string dsfcs = ee.Message;
                        }
                    }
                }
                Console.ReadLine();
                return;
            }
        }

        public void WriteXml()
        {
            if (sedIt == null) return;

            string txtrPrgFolde = "";
            int kkk = -1;
            int sch = -1;
            try
            {

                switch (sedIt)
                {
                    case "C3147":
                        txtrPrgFolde = "tb4";
                        sch = 0;
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH00.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }
                        break;
                    case "C3148":
                        txtrPrgFolde = "tb204";
                        sch = 1;
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH200.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }
                        break;
                    case "C3149":
                        txtrPrgFolde = "tb404";
                        sch = 2;
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH400.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }

                        break;
                    default:
                        break;
                }

                PadProgram4p0Main.Self.mySerMain(sch,SearchSettingsFields(sedIt, txtrPrgFolde) + "" + sedIt + "_Prg_" + kkk.ToString("000") + ".xml");
            }
            catch (Exception ee)
            {
                string hh = ee.Message;
            }

            WriteTxt(SearchSettingsFields(sedIt, txtrPrgFolde), kkk);

            return;
        }

        public void WriteTxt(string p_Folder, int p_nPrg)
        {
            try
            {
                string sdes = ((System.Windows.Controls.TabItem)_selectedIte).Header.ToString();
                StreamWriter sw = new StreamWriter(p_Folder + "" + sedIt + "_Prg_" + p_nPrg.ToString("000") + ".txt");
                sw.WriteLine("NOME_PRG_M" + "[" + p_nPrg.ToString("") + "]" + @"=""" + p_nPrg.ToString("000") + @"""");

                foreach (valori item in xhtt)
                {
                    if (item._label.IndexOf("X_STEP") >= 0 || item._label.IndexOf("X_VEL") >= 0)
                    {
                        sw.WriteLine(item._label + "[" + p_nPrg.ToString("") + "]" + "=" + item._val);
                    }
                }

                foreach (valori item in zhtt)
                {
                    if (item._label.IndexOf("Z_STEP") >= 0 || item._label.IndexOf("Z_VEL") >= 0)
                    {
                        sw.WriteLine(item._label + "[" + p_nPrg.ToString("") + "]" + "=" + item._val);
                    }
                }

                foreach (valori item in xhtt)
                {
                    switch (item._label)
                    {
                        case "NOME_PRG_M":
                            string hii = item._val;
                            sw.WriteLine(item._label + "[" + p_nPrg.ToString("") + "]" + @"=" + item._val);
                            break;
                        case "NUM_PRG_MU1_M":
                        case "NUM_PRG_MU2_M":
                        case "N_MAX_COLONNE_M":
                        case "N_INT_COLONNE_M":
                        case "B_ESCL_STUDER_M":
                        case "B_ESCL_EMAG_M":
                            sw.WriteLine(item._label + "[" + p_nPrg.ToString("") + "]" + @"=" + item._val);
                            //sw.WriteLine(item._label + "[" + p_nPrg.ToString("") + "]" + @"="" + item._val);
                            break;
                        default:
                            break;
                    }

                }

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("M30   ; *** NON CANCELLARE !!! ***");
                sw.WriteLine("RET");
                sw.Close();
            }
            catch (Exception ee)
            {
                string hh = ee.Message;
            }
        }


        internal void vcmdCloseApp()
        {
            PadProgram4p0Main.Self.mySer();

            rbu.AppExit();
        }

        internal void vcmdInviaFile()
        {
            int kkk = -1;
            string sFile = "";
            string txtIpFtp = "";
            string txtUsr = "";
            string txtPsw = "";
            string txtFtpFolder = "";
            string txtLocFolder = "";
            try
            {

                switch (sedIt)
                {
                    case "C3147":
                        txtIpFtp = "0";
                        txtUsr = "1";
                        txtPsw = "2";
                        txtFtpFolder = "3";
                        txtLocFolder = "4";
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH00.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }
                        break;
                    case "C3148":
                        txtIpFtp = "200";
                        txtUsr = "201";
                        txtPsw = "202";
                        txtFtpFolder = "203";
                        txtLocFolder = "204";
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH200.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }
                        break;
                    case "C3149":
                        txtIpFtp = "400";
                        txtUsr = "401";
                        txtPsw = "402";
                        txtFtpFolder = "403";
                        txtLocFolder = "404";
                        if (!int.TryParse(PadProgram4p0Main.Self.tbwH400.Text, out kkk))
                        {
                            throw new System.ArgumentException("Programma pezzo non valido", "Prg");
                        }

                        break;
                    default:
                        break;
                }
                sFile = sedIt + "_Prg_" + kkk.ToString("000") + ".txt";
            }
            catch (Exception ee)
            {
                string hh = ee.Message;
            }

            string sIpFtp = "";
            string sUsr = "";
            string sPsw = "";
            string sFtpFolder = "";
            string sLocFolder = "";
            foreach (var item in mys.wpFields.Children)
            {
                if (item is TabControl)
                {
                    TabControl tc = (TabControl)item;
                    foreach (var item1 in tc.Items)
                    {
                        TabItem tci = (TabItem)item1;
                        if (tci.Header.ToString() == sedIt)
                        {
                            Grid gr = (Grid)tci.Content;
                            foreach (var item2 in gr.Children)
                            {
                                if (item2 is WrapPanel)
                                {
                                    WrapPanel wci = (WrapPanel)item2;

                                    foreach (var item3 in wci.Children)
                                    {
                                        if (item3 is TextBox)
                                        {
                                            if (((TextBox)item3).Name == "tb" + txtIpFtp)
                                            {
                                                sIpFtp = ((TextBox)item3).Text;
                                            }
                                            if (((TextBox)item3).Name == "tb" + txtUsr)
                                            {
                                                sUsr = ((TextBox)item3).Text;
                                            }
                                            if (((TextBox)item3).Name == "tb" + txtPsw)
                                            {
                                                sPsw = ((TextBox)item3).Text;
                                            }
                                            if (((TextBox)item3).Name == "tb" + txtFtpFolder)
                                            {
                                                sFtpFolder = ((TextBox)item3).Text;
                                            }
                                            if (((TextBox)item3).Name == "tb" + txtLocFolder)
                                            {
                                                sLocFolder = ((TextBox)item3).Text;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            FTPOperation.FTPSite = sIpFtp;
            FTPOperation.Usr = sUsr;
            FTPOperation.Psw = sPsw;
            FTPOperation.oPathToFTP = sFtpFolder;
            FTPOperation.PathLocalFTP = sLocFolder;
            FTPOperation.SourceFileLocalToFTP = sFile;

            if (!File.Exists(FTPOperation.PathLocalFTP + "\\" + sFile + ""))
            {
                MessageBox.Show("File inesistente !");
                return;
            }
            FTPOperation.PostDatatoFTP(sFile);
        }

        static string SearchSettingsFields(string p_TabSearch, string p_TxtSearc)
        {
            string sIpFtp = "";
            string sUsr = "";
            string sPsw = "";
            string sFtpFolder = "";
            string sLocFolder = "";

            string sapp = "";

            mySettings mys = new mySettings();

            foreach (var item in mys.wpFields.Children)
            {
                if (item is TabControl)
                {
                    TabControl tc = (TabControl)item;
                    foreach (var item1 in tc.Items)
                    {
                        TabItem tci = (TabItem)item1;
                        if (tci.Header.ToString() == p_TabSearch)
                        {
                            Grid gr = (Grid)tci.Content;
                            foreach (var item2 in gr.Children)
                            {
                                if (item2 is WrapPanel)
                                {
                                    WrapPanel wci = (WrapPanel)item2;

                                    foreach (var item3 in wci.Children)
                                    {
                                        if (item3 is TextBox)
                                        {
                                            if (((TextBox)item3).Name == p_TxtSearc)
                                            {
                                                sapp = ((TextBox)item3).Text;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return sapp;
        }

        internal void vcmdOpenPrg()
        {
            //Type type = this.GetType();
            //System.Reflection.Assembly assembly = type.Assembly;
            //Window win = (Window)assembly.CreateInstance(
            //    type.Namespace + "." + "OpenFile");

            //// Show the window.
            //var win = new DialogControls.OpenFile();
            //win.ShowDialog();



            //string filt = "";
            //switch (sedIt)
            //{
            //    case "C3147":
            //        filt = "C3147";
            //        break;
            //    case "C3148":
            //        filt = "MainGrid1.xml";
            //        break;
            //    case "C3149":
            //        filt = "MainGrid2.xml";
            //        break;
            //    default:
            //        break;
            //}

            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.InitialDirectory = "C:\\Bonfiglioli4p0";
            myDialog.Filter = "Program source files(*.*)|" + sedIt + "*.xml";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;

            if (myDialog.ShowDialog() == true)
            {

                switch (sedIt)
                {
                    case "C3147":
                        PadProgram4p0Main.Self.myDeser(0, myDialog.FileName);
                        break;
                    case "C3148":
                        PadProgram4p0Main.Self.myDeser(1, myDialog.FileName);
                        break;
                    case "C3149":
                        PadProgram4p0Main.Self.myDeser(2, myDialog.FileName);
                        break;
                    default:
                        break;
                }
            }

        }
    }

    class valori
    {
        public valori(string s, string m)
        {
            label_ = s;
            val_ = m;
        }

        private string label_;
        public string _label
        {
            get { return label_; }
            set { label_ = value; }
        }
        private string val_;
        public string _val
        {
            get { return val_; }
            set { val_ = value; }
        }
    }

}
