using System;

namespace GAME.Shared.Models.Groups
{
    public enum E_Syndicates
    {
        ArbitersOfHexis,
        CephalonSuda,
        NewLoka,
        RedVeil,
        SteelMeridian,
        ThePerrinSequence
    }
    public class Syndicate
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
