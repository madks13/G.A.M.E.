using System;

using GAME.Common.Missions.Models;

namespace GAME.Common.Missions.Models.Activity
{
    public class Outbreak : Activity
    {
        #region CTor / DTor

        #region CTor

        public Outbreak(String info) : base(info)
        {
            Type = Enums.Type.Outbreak;
        }

        #endregion

        #endregion
    }
}
