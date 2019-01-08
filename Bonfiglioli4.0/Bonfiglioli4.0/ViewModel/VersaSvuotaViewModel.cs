// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Chocolatey" file="SourcesViewModel.cs">
//   Copyright 2014 - Present Rob Reynolds, the maintainers of Chocolatey, and RealDimensions Software, LLC
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

//using Bonfiglioli4p0.Entity;
using Bonfiglioli4p0.ViewModel.Services;
using Bonfiglioli4p0.Utilities;
using Bonfiglioli4p0.ViewModel.Base;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Bonfiglioli4p0.ViewModel;
using System.Windows;
using Bonfiglioli4p0.ViewModel.Commands;
using System.ComponentModel.DataAnnotations;
using System.Windows.Shapes;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Xaml;
using Bonfiglioli4p0.View;
using Bonfiglioli4p0.DialogControls;

namespace Bonfiglioli4p0.ViewModel
{
    public abstract class VersaSvuotaViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        //public abstract string DisplayedImagePath { get; set; }

        #region PUBLIC FIELD

        #endregion

        string _DisplayedImagePath;
        public string DisplayedImagePath
        {
            get
            {
                return _DisplayedImagePath;
                //return @"D:\QUICKLOAD\TLM 3160\SW.Net\25\qFluid\qFluid\bin\Debug\myRes\TLM_3160_ SINOTTICO.png";
            }
            set
            {
                _DisplayedImagePath = value;
                base.OnPropertyChanged("DisplayedImagePath");
            }
        }

        string _DisplayedImagePath1;
        public string DisplayedImagePath1
        {
            get
            {
                return _DisplayedImagePath1;
                //return @"D:\QUICKLOAD\TLM 3160\SW.Net\25\qFluid\qFluid\bin\Debug\myRes\TLM_3160_ SINOTTICO.png";
            }
            set
            {
                _DisplayedImagePath1 = value;
                base.OnPropertyChanged("DisplayedImagePath1");
            }
        }


        public int iVCassReqBox1 { get; set; }

        #region Command


        public abstract void vClose();

        public ICommand RemoveCommand { get; set; }

        private void Execute(object parameter)
        {
            //_viewModel.SelectedItemQeAnagrafica = _viewModel.SelectedItemQeAnagrafica as qeAnagrafica;
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region Constructor

        public VersaSvuotaViewModel()
        {
            this.Initialize();

            //CanClose = true;

        }

        private void Initialize()
        {


            // Update bindings
            base.OnPropertyChanged("obsListaPlay0");
            base.OnPropertyChanged("obsListaPlay1");

        }

        private void DrawStation1Op(int p_C)
        {

        }

        private void DrawStation0Op(int p_C)
        {
            //#### disegno stazione operatore 1
            if (p_C >= 0)
            {
            }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnObsListaPlayChanged0(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnObsListaPlayChanged1(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnObsListaPlayChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }


        #endregion

        #region Data Properties


        /// <summary>
        /// Cassetto in pos operatore Box 0
        /// </summary>
        int _CassBox0_P2_Op;
        public int CassBox0_P2_Op
        {
            get { return _CassBox0_P2_Op; }

            set
            {
                _CassBox0_P2_Op = value;
                base.OnPropertyChanged("CassBox0_P2_Op");
            }
        }

        /// <summary>
        /// Cassetto in pos operatore Box 0
        /// </summary>
        int _CassBox1_P2_Op;
        public int CassBox1_P2_Op
        {
            get { return _CassBox1_P2_Op; }

            set
            {
                _CassBox1_P2_Op = value;
                base.OnPropertyChanged("CassBox1_P2_Op");
            }
        }

        #endregion



        #region SELECTED ITEM


    }
    #endregion

    public abstract class AskStartVersa
    {
        public abstract int oAskStartVersa(ObservableCollection<csListCassetti> p_Obx);
        internal int CassReq;

        public AskStartVersa(ObservableCollection<csListCassetti> p_Obx)
        {

        }
    }



    public class AskStartVersaAll : AskStartVersa
    {
        public AskStartVersaAll(ObservableCollection<csListCassetti> p_Obx) : base(p_Obx)
        {
        }

        public override int oAskStartVersa(ObservableCollection<csListCassetti> p_Obx)
        {
            return CassReq;
        }
    }
    public class AskStartVersa0 : AskStartVersa
    {
        public AskStartVersa0(ObservableCollection<csListCassetti> p_Obx) : base(p_Obx)
        {
        }

        public override int oAskStartVersa(ObservableCollection<csListCassetti> p_Obx)
        {
            return CassReq;
        }
    }


    public class AskStartVersa1 : AskStartVersa
    {
        public AskStartVersa1(ObservableCollection<csListCassetti> p_Obx) : base(p_Obx)
        {
        }

        public override int oAskStartVersa(ObservableCollection<csListCassetti> p_Obx)
        {
            return CassReq;
        }
    }

    public class AskStartChiudiPos0
    {
        int CassReq;
        public AskStartChiudiPos0(int p_CassReq)
        {
            CassReq = p_CassReq;
        }

        public void oAskStartChiudiPos()
        {
        }
    }

    public class AskStartChiudiPos1
    {
        int CassReq;
        public AskStartChiudiPos1(int p_CassReq)
        {
            CassReq = p_CassReq;
        }

        public void oAskStartChiudiPos()
        {
            //qFluidMW.Self.cplc.plcClosePost1Op = new csPlcCicloChiudiCass1_P2Op(CassReq);
        }
    }

    public class csListCassetti
    {
    }


    internal class bclsReqCicloVersa
    {
        internal ObservableCollection<csListCassetti> obsLvwCassetti;
        internal int _CassBox_P2_Op;
        internal int iCassetto;
        internal csListCassetti _SelectedItemObsListCassetti;
        //internal DispatcherTimer dtm;
        internal bool CicloVersaOk { get; set; }

        internal bclsReqCicloVersa(ObservableCollection<csListCassetti> p_obsListCassetti, csListCassetti p_SelectedItemObsListCassetti)
        {
            obsLvwCassetti = p_obsListCassetti;
        }
    }
}

internal class clsReqCicloVersa0 : bclsReqCicloVersa
{

    DispatcherTimer dtmUserVersa0;
    private AskStartVersaAll ask;

    //internal override bool CicloVersaOk { get; set; }

    //internal vCi/*c*/loAutoVersa0(int ssss, int CassBox_P2_Op, int p_iCassetto) : base(ssss, CassBox_P2_Op, p_iCassetto)
    internal clsReqCicloVersa0(ObservableCollection<csListCassetti> p_obsLC, int p_CassBox_P2_Op, int p_iCassetto, csListCassetti p_Slc)
        : base(p_obsLC, p_Slc)
    {

        dtmUserVersa0 = new DispatcherTimer();
        dtmUserVersa0.Tick += new EventHandler(dtmUserVersa0_Tick);
        dtmUserVersa0.Interval = TimeSpan.FromSeconds(2);
        dtmUserVersa0.Stop();

    }

    private void dtmUserVersa0_Tick(object sender, EventArgs e)
    {
        string _sVersa;

    }



}


internal class clsReqCicloVersa1 : bclsReqCicloVersa
{
    DispatcherTimer dtmUserVersa1;
    //internal override bool CicloVersaOk { get; set; }

    internal clsReqCicloVersa1(ObservableCollection<csListCassetti> p_obsLC, int p_CassBox_P2_Op, int p_iCassetto, csListCassetti p_Slc)
            : base(p_obsLC, p_Slc)
    {


    }
}



internal class clsReqCicloChiudi
{
    internal int CassBox_P2_Op;
    //internal int iCassetto;
    internal AskStartChiudiPos0 askStartChiudiPos0;

    internal clsReqCicloChiudi(int p_CassBox_P2_Op)
    {
        CassBox_P2_Op = p_CassBox_P2_Op;

    }

    private void ResetPlcCicloApriCass()
    {
        //throw new NotImplementedException();
    }

    internal class clsReqCicloChiudiCassPos0 : clsReqCicloChiudi
    {
        DispatcherTimer dtmUserChiudi0;
        DispatcherTimer dtmUserChiudi1;

        internal clsReqCicloChiudiCassPos0(int p_CassBox_P2_Op)
                : base(p_CassBox_P2_Op)
        {
        }
    }

    internal class clsReqCicloChiudiCassPos1 : clsReqCicloChiudi
    {
        DispatcherTimer dtmUserChiudi0;
        DispatcherTimer dtmUserChiudi1;
        internal clsReqCicloChiudiCassPos1(int p_CassBox_P2_Op)
                : base(p_CassBox_P2_Op)
        {

        }
    }

    public class DrawerSchema : ViewModelBase, ISequencedObject
    {

        public void AddPoint()
        {
            _SchPPoints.Add(new Point(Xa0, Ya0));
            _SchPPoints.Add(new Point(Xa1, Ya1));
            _SchPPoints.Add(new Point(Xb0, Yb0));
            _SchPPoints.Add(new Point(Xb1, Yb1));
            _SchPPoints.Add(new Point(Xc0, Yc0));
            _SchPPoints.Add(new Point(Xc1, Yc1));
        }

        private System.Windows.Media.PointCollection _SchPPoints = new System.Windows.Media.PointCollection();
        public System.Windows.Media.PointCollection SchPPoints
        {
            get { return _SchPPoints; }
        }

        #region FIELDS
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Description("Id ")]
        public int DrawerSchemaID { get; set; }//

        private string _legend;
        public string legend
        {
            get { return _legend; }
            set
            {
                if (_legend != value)
                {
                    _legend = value;
                    base.OnPropertyChanged("legend");
                }
            }
        }

        private string _legendVal;
        public string legendVal
        {
            get { return _legendVal; }
            set
            {
                if (_legendVal != value)
                {
                    _legendVal = value;
                    base.OnPropertyChanged("legendVal");
                }
            }
        }

        private Thickness _thkLgdMargin;
        public Thickness thkLgdMargin
        {
            get { return _thkLgdMargin; }
            set
            {
                if (_thkLgdMargin != value)
                {
                    _thkLgdMargin = value;
                    base.OnPropertyChanged("thkLgdMargin");
                }
            }

        }





        #region PUNTI PER DISEGNO POLYLINE
        [Required]
        [Description("Xa0")]
        public int Xa0 { get; set; }

        [Required]
        [Description("Ya0")]
        public int Ya0 { get; set; }

        [Required]
        [Description("Xa1")]
        public int Xa1 { get; set; }

        [Required]
        [Description("Ya1")]
        public int Ya1 { get; set; }


        [Required]
        [Description("Xb0")]
        public int Xb0 { get; set; }

        [Required]
        [Description("Yb0")]
        public int Yb0 { get; set; }

        [Required]
        [Description("Xb1")]
        public int Xb1 { get; set; }

        [Required]
        [Description("Yb1")]
        public int Yb1 { get; set; }

        [Required]
        [Description("Xc0")]
        public int Xc0 { get; set; }

        [Required]
        [Description("Yc0")]
        public int Yc0 { get; set; }

        [Required]
        [Description("Xc1")]
        public int Xc1 { get; set; }

        [Required]
        [Description("Yc1")]
        public int Yc1 { get; set; }

        [Required]
        [Description("Xr0")]
        public int Xr0 { get; set; }

        [Required]
        [Description("Yr0")]
        public int Yr0 { get; set; }
        #endregion

        #endregion




        int sequenceNumber = 1;
        public int SequenceNumber
        {
            get
            {

                //// Resequence
                //foreach (ISequencedObject sequencedObject in targetCollection)
                //{
                //    sequencedObject.SequenceNumber = sequenceNumber;
                //    sequenceNumber++;
                //}
                return sequenceNumber;
            }
            set
            {
                sequenceNumber = value;
            }
        }
    }
}

