using System;
using System.Collections.Generic;
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

namespace GAME.Common.Core.Controls.User
{
    /// <summary>
    /// Interaction logic for ValueFromInterval.xaml
    /// </summary>
    public partial class ValueFromInterval : UserControl
    {
        public static DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(Decimal), typeof(ValueFromInterval), new PropertyMetadata((Decimal)0));
        public static DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(Decimal), typeof(ValueFromInterval), new PropertyMetadata((Decimal)0));
        public static DependencyProperty CurrentProperty = DependencyProperty.Register("Current", typeof(Decimal), typeof(ValueFromInterval), new PropertyMetadata((Decimal)0));
        public static DependencyProperty SliderOpacityProperty = DependencyProperty.Register("SliderOpacity", typeof(Decimal), typeof(ValueFromInterval), new PropertyMetadata((Decimal)1));
        public static DependencyProperty TextBoxOpacityProperty = DependencyProperty.Register("TextBoxOpacity", typeof(Decimal), typeof(ValueFromInterval), new PropertyMetadata((Decimal)1));

        public static DependencyProperty SliderForegroundProperty = DependencyProperty.Register("SliderForeground", typeof(Brush), typeof(ValueFromInterval), new PropertyMetadata((Brush)Brushes.White));
        public static DependencyProperty SliderBackgroundProperty = DependencyProperty.Register("SliderBackground", typeof(Brush), typeof(ValueFromInterval), new PropertyMetadata((Brush)Brushes.Transparent));

        public static DependencyProperty TextBoxForegroundProperty = DependencyProperty.Register("TextBoxForeground", typeof(Brush), typeof(ValueFromInterval), new PropertyMetadata((Brush)Brushes.White));
        public static DependencyProperty TextBoxBackgroundProperty = DependencyProperty.Register("TextBoxBackground", typeof(Brush), typeof(ValueFromInterval), new PropertyMetadata((Brush)Brushes.Transparent));

        public Decimal Maximum
        {
            get { return (Decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public Decimal Minimum
        {
            get { return (Decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public Decimal Current
        {
            get { return (Decimal)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        public Decimal SliderOpacity
        {
            get { return (Decimal)GetValue(SliderOpacityProperty); }
            set { SetValue(SliderOpacityProperty, value); }
        }

        public Decimal TextBoxOpacity
        {
            get { return (Decimal)GetValue(TextBoxOpacityProperty); }
            set { SetValue(TextBoxOpacityProperty, value); }
        }

        public Brush SliderForeground
        {
            get { return (Brush)GetValue(SliderForegroundProperty); }
            set { SetValue(SliderForegroundProperty, value); }
        }

        public Brush SliderBackground
        {
            get { return (Brush)GetValue(SliderBackgroundProperty); }
            set { SetValue(SliderBackgroundProperty, value); }
        }

        public Brush TextBoxForeground
        {
            get { return (Brush)GetValue(TextBoxForegroundProperty); }
            set { SetValue(TextBoxForegroundProperty, value); }
        }

        public Brush TextBoxBackground
        {
            get { return (Brush)GetValue(TextBoxBackgroundProperty); }
            set { SetValue(TextBoxBackgroundProperty, value); }
        }

        public ValueFromInterval()
        {
            InitializeComponent();
        }
        
        public Decimal Interval
        {
            get { return (Decimal)MainSlider.ActualWidth / (Maximum - Minimum); }
        }
    }
}
