using System;

namespace GAME.Modules.Warframe.Common.Missions.Models.Activity
{
    public class Alert : Activity
    {

        #region Resources

        #region Fields

        private String _description;
        private DateTimeOffset _expirationDate;

        #endregion

        #region Proprieties

        public DateTimeOffset ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                _expirationDate = value;
                NotifyPropertyChanged("ExpirationDate");
                NotifyPropertyChanged("TimeLeft");
            }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string TimeLeft
        {
            get
            {
                if (ExpirationDate.CompareTo(DateTime.UtcNow) > 0)
                {
                    TimeSpan tl = ExpirationDate - DateTime.UtcNow;
                    return (tl.Days != 0 ? tl.Days + "d " : "") + (tl.Hours != 0 ? tl.Hours + "h " : "") + (tl.Minutes != 0 ? tl.Minutes + "m " : "") + tl.Seconds + "s";
                }
                else
                    return "0s";
            }
        }

        #endregion

        #endregion

        #region CTor / DTor

        #region CTor

        public Alert(String info) : base(info)
        {
            Type = Enums.Type.Alert;
        }

        #endregion

        #endregion

        #region Implementations

        #endregion
    }
}
