using System;
using System.ComponentModel;
using System.Windows.Controls;


namespace GAME.Common.Core.Interfaces.Plugin
{
    public interface IModule
    {
        #region Properties

        String Name { get; }

        Boolean IsLaunched { get; }

        Boolean IsShowingMain { get; }

        Boolean IsShowingOptions { get; }

        Boolean IsVisible { get; }

        #endregion

        #region Events

        //event EventHandler Closed;

        //event EventHandler<Enum> VisibilityChanged;

        #endregion

        #region Methods

        Boolean ShowMain();

        Boolean ShowOptions();

        Boolean Hide();

        Boolean Stop();

        #endregion
    }
}
