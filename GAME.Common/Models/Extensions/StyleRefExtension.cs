using System;
using System.Windows;
using System.Windows.Markup;

namespace GAME.Common.Core.Models.Extensions
{
    public abstract class StyleRefExtension : MarkupExtension
    {
        /// <summary>
        /// Property for specific resource dictionary
        /// </summary>
        protected static ResourceDictionary RD;
        /// <summary>
        /// Resource key which we want to extract
        /// </summary>
        public String ResourceKey { get; set; }
        /// <summary>
        /// Overriding base function which will return key from RD
        /// </summary>
        /// <param name="serviceProvider">Not used</param>
        /// <returns>Object from RD</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if(RD == null) throw new Exception(
                @"You should define RD before usage. 
                Please make it in static constructor of extending class!");
            return RD[ResourceKey];
        }
    }
}
