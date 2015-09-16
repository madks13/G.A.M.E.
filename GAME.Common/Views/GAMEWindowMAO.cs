﻿using GAME.Common.Core.Interfaces.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GAME.Common.Core.Views
{
    public class GAMEWindowMAO : GAMEWindowBase
    {
        #region Fields

        private Page _main = null;
        private Page _options = null;
        private Boolean _templateLoaded = false;
        private Boolean _disposed = false;

         #endregion

        #region Properties

        public Page MainPage
        {
            get { return _main; }
            set { _main = value; }
        }

        public Page OptionsPage
        {
            get { return _options; }
            set { _options = value; }
        }

        public Boolean IsShowingMain
        {
            get { return MainFrame!= null && MainFrame.Content == _main; }
        }

        public Boolean IsShowingOptions
        {
            get { return MainFrame != null && MainFrame.Content == _options; }
        }

        #endregion

        #region Initializers

        private void Init()
        {
            //GAMEWindowClosing += GAMEWindowMAO_Closing;
            //DataContext = this;
            //if (MainFrame != null)
            //MainFrame.Content = _main;
        }

        #endregion

        #region Ctor

        public GAMEWindowMAO(String name, Page main, Page options) : base(name)
        {
            _main = main;
            _options = options;
            Init();
        }

        public GAMEWindowMAO(String name)
            : base(name)
        {
            Init();
        }

        #endregion

        #region Display

        public Boolean ShowMain()
        {
            if (_main == null || MainFrame == null)
                return false;
            MainFrame.Content = _main;
            Show();
            return true;
        }

        public Boolean ShowOptions()
        {
            if (_options == null || MainFrame == null)
                return false;
            MainFrame.Content = _options;
            Show();
            return true;
        }

        #endregion

        #region Events

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            WindowOptionsButton.Visibility = System.Windows.Visibility.Visible;
            WindowOptionsButton.PreviewMouseLeftButtonUp += WindowOptionsButton_PreviewMouseLeftButtonUp;
        }

        void WindowOptionsButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame != null)
            {
                if (MainFrame.Content == _options)
                    MainFrame.Content = _main;
                else
                    MainFrame.Content = _options;
            }
        }

        #endregion

        #region Cleaning up

        protected override void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _main = null;
                _options = null;
            }
            _disposed = true;
            base.Dispose(disposing);
        }

        #endregion
    }
}
