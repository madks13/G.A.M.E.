using System;
using System.Windows;
using GAME.Common.Core.Models;

namespace GAME.Common.Core.Models.Extensions
{
    public class StylesDictionaryRefExtension : StyleRefExtension
    {
        static StylesDictionaryRefExtension()
        {
            RD = new ResourceDictionary()
                        {
                            Source = new Uri("pack://application:,,,/GAME.Common.Core;component/Templates/Default.xaml", UriKind.Absolute)
                        };
        }
    }
}
