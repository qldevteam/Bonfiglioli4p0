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
using Bonfiglioli4p0.ViewModel.Commands;

namespace Bonfiglioli4p0
{
    public enum enMio
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

    public sealed class mySettingsMvvm : ViewModelBase
    {
        #region PRIVATE

        #endregion

        #region PUBLIC
        internal rbUtils rbu = new rbUtils();
        internal Action CloseAction { get; set; }
        #endregion

        #region COMMAND

        public ICommand cmdOpenSetting { get; set; }

        #endregion


        #region COSTRUCTOR
        public mySettingsMvvm()
        {
            //cmdOpenSetting = new vcsOpenMySettings(this);
        }
        #endregion

        internal void vcmdOpenMySettings()
        {
            //if (App.Usr.bUAdmin)
            //{
                //var mys = new mySettings();
                //mys.ShowDialog();
            //}
        }

    }
}
