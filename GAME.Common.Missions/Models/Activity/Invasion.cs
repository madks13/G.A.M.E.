using System;

using GAME.Common.Missions.Models;

namespace GAME.Common.Missions.Models.Activity
{
    public class Invasion : Activity
    {
        #region CTor / DTor

        #region CTor

        public Invasion(String info) : base(info)
        {
            Type = Enums.Type.Invasion;
        }

        #endregion

        #endregion

    }
}
