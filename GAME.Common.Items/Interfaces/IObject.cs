using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Common.Items.Interfaces
{
    public interface IObject
    {
        //The names need to be fromated but the depths of each section is different
        //Thus the name represents the depth in the object tree

        /// <summary>
        /// This represents the section to which the item belongs
        /// Item, Currency, Blueprint or Miscellanous
        /// </summary>
        Enum Section { get; set; }

        /// <summary>
        /// This represents the category of the item
        /// ie : Ducats for Currency, Warframe for Item
        /// </summary>
        Enum Category { get; set; }

        /// <summary>
        /// This represents the Group in which the item belongs
        /// ie : Sentinel for Pet, Weapon for Archwing
        /// </summary>
        Enum Group { get; set; }

        /// <summary>
        /// This represents the Type or the item
        /// ie : Main for Weapon in Warframe
        /// </summary>
        Enum Type { get; set; }

        /// <summary>
        /// This represents the part of an item
        /// </summary>
        Enum Part { get; set; }

        /// <summary>
        /// This represents the version fo the item
        /// ie : Rakta (Red veil), Prime...
        /// </summary>
        Enum Version { get; set; }
    }
}
