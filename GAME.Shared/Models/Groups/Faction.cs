using System;


namespace GAME.Shared.Models.Groups
{
    public enum E_Factions
    {
        None,
        Grineer,
        Corpus,
        Infestation
    }
    public class Faction
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
