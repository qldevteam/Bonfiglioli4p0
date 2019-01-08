using nsUtils;
using Bonfiglioli4p0.DialogControls;
//using Bonfiglioli4p0.Entity;
using Bonfiglioli4p0.Utilities;
using Bonfiglioli4p0.View;
using Bonfiglioli4p0.ViewModel.Base;
using Bonfiglioli4p0.ViewModel.Commands;
//using Bonfiglioli4p0.ViewModel.MediatorMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bonfiglioli4p0.ViewModel
{
    public abstract class VSViewModel : VersaSvuotaViewModel
    {
        internal VSViewModel() : base()
        {
            //_mediator = mediator;

            //_mediator.Register(
            //    MediatorMessages.ObjectASaidSomething,
            //    param =>
            //    {
            //        this.WhatObjectASays = (string)param;
            //    });

            iNumPzBarCode = -1;
        }
            int _iNumPzBarCode;
        internal int iNumPzBarCode
        {
            get
            {
                return _iNumPzBarCode;
            }
            set
            {
                _iNumPzBarCode = value;
            }
        }

        int _iNumPz;
        internal int iNumPz
        {
            get
            {
                return _iNumPz;
            }
            set
            {
                _iNumPz = value;
            }
        }

        string _sTextChanged;
        public string sTextChanged
        {
            get
            {
                return _sTextChanged;
            }
            set
            {
                _sTextChanged = value;
                //if (_sTextChanged.IndexOf("§") == 0 & sTextChanged.LastIndexOf("¬") == 0)


            }
        }

        string _sOldTextChanged;
        public string sOldTextChanged
        {
            get
            {
                return _sOldTextChanged;
            }
            set
            {
                _sOldTextChanged = value;

            }
        }


        /// <summary>
        /// Procedura di versamento/svuotamento
        /// </summary>
        internal abstract void vStopVersa();

        private string _sArt;
        public string sArt
        {
            get
            {
                return _sArt;
            }
            set
            {
                _sArt = value;
                base.OnPropertyChanged("sArt");
            }
        }

        private int _iPzArt;
        public int iPzArt
        {
            get
            {
                return _iPzArt;
            }
            set
            {
                _iPzArt = value;
                base.OnPropertyChanged("iPzArt");
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnObsPlayListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        #region METODI PUBBLICI

        #endregion

        #region MEDIATOR


        //private override List<qeAnagrafica> Lqea { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        #endregion

    }

    public class VersaViewModel : VSViewModel
    {

        bool vInsStorageOk;
        //    //csPlcCommand ojComPlc = new csPlcCommand();
        //    DispatcherTimer dtmEnebleBtn;
        //    /// <summary>
        //    /// A obsListaPlay list.
        //    /// </summary>

        //    bool _btnCloseDraw;
        //    public bool btnCloseDraw
        //    {
        //        get
        //        {

        //            return _btnCloseDraw;
        //        }
        //        set
        //        {
        //            _btnCloseDraw = value;
        //            base.OnPropertyChanged("btnCloseDraw");
        //        }
        //    }


        public VersaViewModel() : base()
        {
        }

        public override void vClose()
        {
        }

        internal override void vStopVersa()
        {
            //bInVersa = false;
        }



    }

}
