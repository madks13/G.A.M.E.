using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GAME.Common.Core.Views
{
    public class GAMEWindowCommon : Window, IDisposable
    {
        #region Enums

        public enum ImageIndex
        {
            WIN_CLOSE_I,
            WIN_CLOSE_A,
            WIN_MAX_I,
            WIN_MAX_A,
            WIN_MIN_I,
            WIN_MIN_A,
            WIN_NORM_I,
            WIN_NORM_A,
            WIN_OPT_I,
            WIN_OPT_A,
            WIN_ABT_I,
            WIN_ABT_A
        }

        public enum ShowingPage
        {
            None = 0,
            Unknown = 1,
            Main = 1 << 1,
            Options = 1 << 2,
            About = 1 << 3
        }

        #endregion

        #region Properties

        public Label MainTitle
        {
            get { return _mainTitle; }
            set { _mainTitle = value; }
        }

        public Label MainInfo
        {
            get { return _mainInfo; }
            set { _mainInfo = value; }
        }

        public Button WindowAboutButton
        {
            get { return _winAboutButton; }
            set { _winAboutButton = value; }
        }

        public Button WindowOptionsButton
        {
            get { return _winOptionsButton; }
            set { _winOptionsButton = value; }
        }

        public Button WindowMinimizeButton
        {
            get { return _winMinimizeButton; }
            set { _winMinimizeButton = value; }
        }

        public Button WindowMaximizeButton
        {
            get { return _winMaximizeButton; }
            set { _winMaximizeButton = value; }
        }

        public Button WindowCloseButton
        {
            get { return _winCloseButton; }
            set { _winCloseButton = value; }
        }

        public Frame MainFrame
        {
            get { return _mainFrame; }
            set { _mainFrame = value; }
        }

        public Image MainBackground
        {
            get { return _mainBackground; }
            set { _mainBackground = value; }
        }

        public Page FirstAboutPage
        {
            get { return _firstAboutPage; }
            set
            {
                if (value == null)
                    WindowAboutButton.Visibility = System.Windows.Visibility.Hidden;
                else
                    WindowAboutButton.Visibility = System.Windows.Visibility.Visible;
                _firstAboutPage = value;
            }
        }

        public Page FirstOptionsPage
        {
            get { return _firstOptionsPage; }
            set
            {
                if (value == null)
                    WindowOptionsButton.Visibility = System.Windows.Visibility.Hidden;
                else
                    WindowOptionsButton.Visibility = System.Windows.Visibility.Visible;
                _firstOptionsPage = value;
            }
        }

        public Page FirstMainPage
        {
            get { return _firstMainPage; }
            set
            {
                if (value == null)
                    MainFrame.Content = null;
                else
                    MainFrame.Navigate(value);
                MainFrame.NavigationService.RemoveBackEntry();
                _firstMainPage = value;
            }
        }

        public ShowingPage CurrentPage
        {
            get { return _showingPage; }
        }

        public Boolean IsShowingMain
        {
            get { return _isShowingMain; }
        }

        public Boolean IsShowingOptions
        {
            get { return _isShowingOptions; }
        }

        public Boolean IsShowingAbout
        {
            get { return _isShowingAbout; }
        }

        #endregion

        #region Fields

        private String _windowTitle = null;
        private Dictionary<ImageIndex, BitmapImage> _images = new Dictionary<ImageIndex, BitmapImage>();
        private Label _mainTitle = null;
        private Label _mainInfo = null;
        private Button _winOptionsButton = null;
        private Button _winMinimizeButton = null;
        private Button _winMaximizeButton = null;
        private Button _winCloseButton = null;
        private Button _winAboutButton = null;
        private Frame _mainFrame = null;
        private Image _mainBackground = null;
        private Page _firstMainPage = null;
        private Page _firstAboutPage = null;
        private Page _firstOptionsPage = null;
        private ShowingPage _showingPage = ShowingPage.None;
        private Boolean _isShowingMain = false;
        private Boolean _isShowingAbout = false;
        private Boolean _isShowingOptions = false;
        private Boolean _disposed = false;

        #endregion

        #region Methods

        #region Initialisation

        private void Init()
        {
            DataContext = this;

            //Load the reused images
            _images[ImageIndex.WIN_CLOSE_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Close/GAME_Close_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_CLOSE_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Close/GAME_Close_active.png", UriKind.Absolute));
            _images[ImageIndex.WIN_MAX_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Max/GAME_max_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_MAX_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Max/GAME_max_active.png", UriKind.Absolute));
            _images[ImageIndex.WIN_MIN_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Min/GAME_min_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_MIN_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Min/GAME_min_active.png", UriKind.Absolute));
            _images[ImageIndex.WIN_NORM_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Normal/GAME_Normal_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_NORM_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Normal/GAME_Normal_active.png", UriKind.Absolute));
            _images[ImageIndex.WIN_OPT_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Options/GAME_Options_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_OPT_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/Options/GAME_Options_active.png", UriKind.Absolute));
            _images[ImageIndex.WIN_ABT_I] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/About/GAME_About_inactive.png", UriKind.Absolute));
            _images[ImageIndex.WIN_ABT_A] = new BitmapImage(new Uri("pack://application:,,,/GAME.Common.Core;component/Resources/Images/Buttons/About/GAME_About_active.png", UriKind.Absolute));
        }

        static GAMEWindowCommon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAMEWindowCommon),
                new FrameworkPropertyMetadata(typeof(GAMEWindowCommon)));
        }

        //Gets instances of window elements and sets up event handlers for them
        public override void OnApplyTemplate()
        {
            _winMinimizeButton = GetTemplateChild("WindowMinimizeButton") as Button;
            if (_winMinimizeButton != null)
            {
                _winMinimizeButton.PreviewMouseLeftButtonUp += WindowEvent_MouseLeftButtonUp;
                _winMinimizeButton.MouseEnter += WindowEvent_MouseEnter;
                _winMinimizeButton.MouseLeave += WindowEvent_MouseLeave;
                _winMinimizeButton.Content = _images[ImageIndex.WIN_MIN_I];
            }

            _winMaximizeButton = GetTemplateChild("WindowMaximizeButton") as Button;
            if (_winMaximizeButton != null)
            {
                _winMaximizeButton.PreviewMouseLeftButtonUp += WindowEvent_MouseLeftButtonUp;
                _winMaximizeButton.MouseEnter += WindowEvent_MouseEnter;
                _winMaximizeButton.MouseLeave += WindowEvent_MouseLeave;
                _winMaximizeButton.Content = _images[ImageIndex.WIN_MAX_I]; ;
            }

            _winCloseButton = GetTemplateChild("WindowCloseButton") as Button;
            if (_winCloseButton != null)
            {
                _winCloseButton.PreviewMouseLeftButtonUp += WindowEvent_MouseLeftButtonUp;
                _winCloseButton.MouseEnter += WindowEvent_MouseEnter;
                _winCloseButton.MouseLeave += WindowEvent_MouseLeave;
                _winCloseButton.Content = _images[ImageIndex.WIN_CLOSE_I];
            }

            _winOptionsButton = GetTemplateChild("WindowOptionsButton") as Button;
            if (_winOptionsButton != null)
            {
                _winOptionsButton.Visibility = System.Windows.Visibility.Hidden;
                _winOptionsButton.PreviewMouseLeftButtonUp += WindowEvent_MouseLeftButtonUp;
                _winOptionsButton.MouseEnter += WindowEvent_MouseEnter;
                _winOptionsButton.MouseLeave += WindowEvent_MouseLeave;
                _winOptionsButton.Content = _images[ImageIndex.WIN_OPT_I];
            }

            _winAboutButton = GetTemplateChild("WindowAboutButton") as Button;
            if (_winAboutButton != null)
            {
                _winAboutButton.Visibility = System.Windows.Visibility.Hidden;
                _winAboutButton.PreviewMouseLeftButtonUp += WindowEvent_MouseLeftButtonUp;
                _winAboutButton.MouseEnter += WindowEvent_MouseEnter;
                _winAboutButton.MouseLeave += WindowEvent_MouseLeave;
                _winAboutButton.Content = _images[ImageIndex.WIN_ABT_I];
            }

            Rectangle dragTop = GetTemplateChild("DragTop") as Rectangle;
            if (dragTop != null)
            {
                dragTop.PreviewMouseLeftButtonDown += Drag_ButtonDown;
            }

            Rectangle dragBottom = GetTemplateChild("DragBottom") as Rectangle;
            if (dragBottom != null)
            {
                dragBottom.PreviewMouseLeftButtonDown += Drag_ButtonDown;
            }

            _mainTitle = GetTemplateChild("MainTitle") as Label;
            if (_mainTitle != null)
            {
                _mainTitle.MouseLeftButtonDown += Drag_ButtonDown;
                _mainTitle.Content = _windowTitle;
            }

            _mainInfo = GetTemplateChild("MainInfo") as Label;
            if (_mainInfo != null)
            {
                _mainInfo.MouseLeftButtonDown += Drag_ButtonDown;
            }

            _mainFrame = GetTemplateChild("MainFrame") as Frame;

            _mainBackground = GetTemplateChild("MainBackground") as Image;

            Grid resizeGrid = GetTemplateChild("resizeGrid") as Grid;
            if (resizeGrid != null)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    Rectangle resizeRectangle = element as Rectangle;
                    if (resizeRectangle != null)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }

            base.OnApplyTemplate();
        }

        #endregion

        #region C/D tor

        public GAMEWindowCommon(String name)
            : base()
        {
            _windowTitle = name;
            PreviewMouseMove += OnPreviewMouseMove;
            Closing += GAMEWindowBase_Closing;
            Init();
        }



        #endregion

        #region Navigation

        public void ShowPage(Page page)
        {
            if (page != null)
                MainFrame.Navigate(page);
        }

        #endregion

        #region Events

        #region Click events

        private void WindowEvent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Name == WindowCloseButton.Name)
                base.Close();
            else if (b.Name == WindowAboutButton.Name)
                ShowPage(FirstAboutPage);
            else if (b.Name == WindowOptionsButton.Name)
                ShowPage(FirstOptionsPage);
            else if (b.Name == WindowMinimizeButton.Name)
                WindowState = WindowState.Minimized;
            else if (b.Name == WindowMaximizeButton.Name)
            {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            }
        }

        private void moveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Drag_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        #endregion

        #region Mouse move events

        private void WindowEvent_MouseEnter(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (_images != null && _images.Count > 0)
                b.Content = _images[(b.Name == WindowCloseButton.Name) ?
                    ImageIndex.WIN_CLOSE_A :
                    (b.Name == WindowAboutButton.Name) ?
                    ImageIndex.WIN_ABT_A :
                    (b.Name == WindowOptionsButton.Name) ?
                    ImageIndex.WIN_OPT_A :
                    (b.Name == WindowMinimizeButton.Name) ?
                    ImageIndex.WIN_MIN_A :
                    (WindowState == System.Windows.WindowState.Maximized) ?
                    ImageIndex.WIN_NORM_A :
                    ImageIndex.WIN_MAX_A];
        }

        private void WindowEvent_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsVisible)
                return;
            Button b = (Button)sender;
            if (_images != null && _images.Count > 0)
                b.Content = _images[(b.Name == WindowCloseButton.Name) ?
                    ImageIndex.WIN_CLOSE_I :
                    (b.Name == WindowAboutButton.Name) ?
                    ImageIndex.WIN_ABT_I :
                    (b.Name == WindowOptionsButton.Name) ?
                    ImageIndex.WIN_OPT_I :
                    (b.Name == WindowMinimizeButton.Name) ?
                    ImageIndex.WIN_MIN_I :
                    (WindowState == System.Windows.WindowState.Maximized) ?
                    ImageIndex.WIN_NORM_I :
                    ImageIndex.WIN_MAX_I];
        }

        protected void ResizeRectangle_MouseMove(Object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }

        protected void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        protected void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        #endregion

        #region Window events

        void GAMEWindowBase_Closing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                Dispose();
            }
        }

        #endregion

        #region Special

        #region Initialisation

        private HwndSource _hwndSource;

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        #endregion

        #region Flashing

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The Flash Status.
            /// </summary>
            public FlashWindowFlags dwFlags; //uint
            /// <summary>
            /// The number of times to Flash the window.
            /// </summary>
            public uint uCount;
            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }

        public enum FlashWindowFlags : uint
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state.
            /// </summary>
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption.
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button.
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground.
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        public static bool FlashWindow(IntPtr hWnd,
                                       FlashWindowFlags fOptions,
                                       uint FlashCount,
                                       uint FlashRate)
        {
            if (IntPtr.Zero != hWnd)
            {
                FLASHWINFO fi = new FLASHWINFO();
                fi.cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
                fi.dwFlags = fOptions;
                fi.uCount = FlashCount;
                fi.dwTimeout = FlashRate;
                fi.hwnd = hWnd;

                return FlashWindowEx(ref fi);
            }
            return false;
        }

        public static bool StopFlashingWindow(IntPtr hWnd)
        {
            if (IntPtr.Zero != hWnd)
            {
                FLASHWINFO fi = new FLASHWINFO();
                fi.cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
                fi.dwFlags = (uint)FlashWindowFlags.FLASHW_STOP;
                fi.hwnd = hWnd;

                return FlashWindowEx(ref fi);
            }
            return false;
        }

        public void FlashWindow(FlashWindowFlags fOptions, uint FlashCount, uint FlashRate)
        {
            if (_hwndSource != null)
                FlashWindow(_hwndSource.Handle, fOptions, FlashCount, FlashRate);
        }

        public void StopFlashingWindow()
        {
            if (_hwndSource != null)
                StopFlashingWindow(_hwndSource.Handle);
        }

        #endregion

        #region Informing

        public void Inform(object informationObject)
        {
            if (MainInfo != null)
                MainInfo.Content = informationObject;
        }

        #endregion

        #endregion

        #endregion

        #region Cleaning up

        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_images != null)
                {
                    foreach (var p in _images)
                    {
                        if (p.Value.StreamSource != null)
                            p.Value.StreamSource.Dispose();
                    }
                    _images.Clear();
                    _images = null;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}
