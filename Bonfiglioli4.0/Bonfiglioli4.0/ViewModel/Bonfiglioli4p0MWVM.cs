// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Chocolatey" file="SourcesViewModel.cs">
//   Copyright 2014 - Present Rob Reynolds, the maintainers of Chocolatey, and RealDimensions Software, LLC
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

//using Bonfiglioli4p0.Entity;
//using Bonfiglioli4p0.ViewModel.Commands;
//using Bonfiglioli4p0.ViewModel.Services;
using Bonfiglioli4p0.Utilities;
using Bonfiglioli4p0.ViewModel.Base;
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
//using Bonfiglioli4p0.ViewModel.Commands;
//using qFluid.Properties;
using Bonfiglioli4p0.View;
using System.Windows;
using nsUtils;
using System.Text;

namespace Bonfiglioli4p0.ViewModel
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

    public sealed class Bonfiglioli4p0MWVM : ViewModelBase
    {
        #region PRIVATE

        //// Property variables
        //ObservableCollection<qe3Cluster> _ClusterVMList;
        //ObservableCollection<qeStorage> _obsQeStorage00;
        //ObservableCollection<qeStorage> _obsQeStorage01;
        //ObservableCollection<StorGroup> _obsListaPlay;
        //ObservableCollection<AutoEngine> _obsAutoEngine;
        //ObservableCollection<EngineProduction> _obsEngineProduction;
        //ObservableCollection<StatiLavCluster> _obsStatiLavClusters;
        //ObservableCollection<StatiLavCluster> _obsStatiLavStor00;
        //ObservableCollection<StatiLavCluster> _obsStatiLavStor01;
        //ObservableCollection<qe1Box> _obsClusterEnabled00;
        //ObservableCollection<myEntitySetting> _obsMySettings;
        //ObservableCollection<PlcComunication> _oldobsPlcComunication;

        //int p_ItemCountCluster;
        //int p_ItemCountStorage00;
        //int p_ItemCountStorage01;
        //int p_ItemCountClusterEnabled00;
        ////int p_ItemCountClusterEnabled01;
        //AutoEngine _SelectedItemAutoEngine;
        //StorGroup _SelectedItemListPlay;
        //StatiLavCluster _SelectedItemStatiLavCluster;
        //readonly Mediator _mediator;
        //string _objectBText;
        //string _whatObjectASays;
        //PLCS7 sps7;
        //private int PlcPos0Op;
        //private int PlcPos1Op;
        //private int PlcPos0Rbt;
        //private int PlcPos1Rbt;

        //int CassWorkRbt;
        //int iCassReqStartProd;
        //bool bForzaFineProdArticolo;
        //bool bForzaArrestoProd;
        //private bool bBypassFineGrzArrestoTemp;
        //StaticForBinding sfb = StaticForBinding.Instance;

        #endregion

        #region PUBLIC
        internal rbUtils rbu = new rbUtils();

      
        #endregion

        #region COMMAND

        public ICommand StartProdEvent { get; set; }
        public ICommand StopProdEvent { get; set; }
        public ICommand OpenSemiAutoEvent { get; set; }
        public ICommand OpenVersamentiEvent { get; set; }
        public ICommand OpenSvuotamentiEvent { get; set; }
        public ICommand OpenMySettingsEvent { get; set; }
        public ICommand OpenAnagrEvent { get; set; }
        public ICommand OpenSinottEvent { get; set; }
        public ICommand cmdEnDrawer0Event { get; set; }
        public ICommand cmdChangeNrGrzEvent { get; set; }
        public ICommand cmdChangeCassFinitoEvent { get; set; }
        public ICommand SimPrelEvent { get; set; }
        public ICommand SimPrelNoPzEvent { get; set; }
        public ICommand SimDepoEvent { get; set; }

        #endregion


        #region COSTRUCTOR
        //public qFluidMWViewModel(Mediator mediator)
        //{
        //    _mediator = mediator;

        //    _mediator.Register(
        //        MediatorMessages.ObjectASaidSomething,
        //        param =>
        //        {
        //            this.WhatObjectASays = (string)param;
        //        });

        //    if (App.SimPlcS7) sps7 = new PLCS7();
        //    else sps7 = new PLCS7("152.0.4.0", 0, 2);

        //    this.Initialize();

        //    //abilito semiauto
        //    btnSemiAutoEnable = true;

        //    //abilito gestione spegnimento pc remoto
        //    btnPCRemOffEnable = true;


        //}
        #endregion

 
    }
}
