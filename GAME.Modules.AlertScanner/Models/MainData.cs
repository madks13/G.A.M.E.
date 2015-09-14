using GAME.Common.Core.Interfaces.Plugin;
using GAME.Common.Core.ViewModels;
using GAME.Common.Missions.Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace GAME.Modules.Warframe.AlertScanner.Models
{
    public class MainData : PropertiesObserver, IDataPlugin
    {
        #region Resources

        #region Fields

        private List<IActivity> _activities = new List<IActivity>();

        #endregion

        #region Proprieties

        public List<IActivity> Activities
        {
            get { return _activities; }
            set 
            { 
                _activities = value;
                NotifyPropertyChanged("Activities");
            }
        }

        #endregion

        #endregion

        #region CTor / DTor

        public MainData()
        {
        }

        #endregion

        public void LoadDefaults()
        {
            if (Activities != null)
                Activities.Clear();
        }
    }
}
