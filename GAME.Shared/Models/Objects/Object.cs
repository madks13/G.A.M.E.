using System;

namespace GAME.Shared.Models.Objects
{
    public enum E_ObjectCategory
    {
        None,
        Warframe,
        Archwing,
        Weapon,
        Sentinel,
        Companion,
        Appearance,
        Gear,
        Key,
        Resource,
        Miscelanous
    }
    public enum E_WarframeWeaponSubCategory
    {
        None,
        Main,
        Secondary,
        Melee,
        Sentinel
    }
    public enum E_WarframePart
    {
        None,
        Systems,
        Chassis,
        Helmet
    }
    public enum E_ArchwingPart
    {
        None,
        Harness,
        Systems,
        Wings
    }

    public class Object
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsBlueprint { get; set; }
    }
}
