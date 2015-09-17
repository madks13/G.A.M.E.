﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GAME.Common.Core.Controls.Custom
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GAME.Common.Core.Controls.Custom"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GAME.Common.Core.Controls.Custom;assembly=GAME.Common.Core.Controls.Custom"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:GAMEOutlinedLabel/>
    ///
    /// </summary>
    public class GAMEOutlinedLabel : Label
    {
        static GAMEOutlinedLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAMEOutlinedLabel), new FrameworkPropertyMetadata(typeof(GAMEOutlinedLabel)));
        }

        public GAMEOutlinedLabel()
        {
            OutlineForeColor = Brushes.Green;
            OutlineWidth = 2;
            this.BorderBrush = Brushes.HotPink;
        }

        public Brush OutlineForeColor { get; set; }

        public float OutlineWidth { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                this.Content.ToString(),
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(this.FontFamily.ToString()),
                this.FontSize,
                this.Foreground);


            //// Set a maximum width and height. If the text overflows these values, an ellipsis "..." appears.
            formattedText.MaxTextWidth = this.ActualWidth;
            formattedText.MaxTextHeight = this.ActualHeight;
            //Point p = new Point(Padding.Left, Padding.Top);

            // Use a larger font size beginning at the first (zero-based) character and continuing for 5 characters.
            // The font size is calculated in terms of points -- not as device-independent pixels.
            //formattedText.SetFontSize(36 * (96.0 / 72.0), 0, 5);

            // Use a Bold font weight beginning at the 6th character and continuing for 11 characters.
            //formattedText.SetFontWeight(FontWeights.Bold, 6, 11);

            RadialGradientBrush rgb = new RadialGradientBrush(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255));
            // Use a linear gradient brush beginning at the 6th character and continuing for 11 characters.
            //formattedText.SetForegroundBrush(
            //                        new LinearGradientBrush(
            //                        Colors.Orange,
            //                        Colors.Teal,
            //                        90.0),
            //                        6, 11);
            //formattedText.SetForegroundBrush(rgb);
            drawingContext.DrawGeometry(Brushes.Green, new Pen(Brushes.Green, 4), formattedText.BuildGeometry(new Point(0, 0)).GetOutlinedPathGeometry());

            // Use an Italic font style beginning at the 28th character and continuing for 28 characters.
            //formattedText.SetFontStyle(FontStyles.Italic, 28, 28);

            // Draw the formatted text string to the DrawingContext of the control.
            //drawingContext.DrawRectangle(BorderBrush, new Pen(BorderBrush, BorderThickness.Bottom), new Rect(0, 0, ActualWidth, ActualHeight));
            //drawingContext.DrawText(formattedText, new Point(0, 0));
        }
    }
}
