Sketches
========

I have created Sketches Windows Phone App 

MainPage.xaml

<phone:PhoneApplicationPage
    x:Class="WindowsPhoneDemoTouchApp.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:phone="clr-namespace:Microsoft.Phone.Controls;assembly=Microsoft.Phone"
    xmlns:shell="clr-namespace:Microsoft.Phone.Shell;assembly=Microsoft.Phone"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d" d:DesignWidth="480" d:DesignHeight="768"
    FontFamily="{StaticResource PhoneFontFamilyNormal}"
    FontSize="{StaticResource PhoneFontSizeNormal}"
    Foreground="{StaticResource PhoneForegroundBrush}"
    SupportedOrientations="Portrait" Orientation="Portrait"
    shell:SystemTray.IsVisible="True">
 
    <!--LayoutRoot is the root grid where all page content is placed-->
    
 
    <!--Sample code showing usage of ApplicationBar-->
    <!--<phone:PhoneApplicationPage.ApplicationBar>
        <shell:ApplicationBar IsVisible="True" IsMenuEnabled="True">
            <shell:ApplicationBarIconButton IconUri="/Images/appbar_button1.png" Text="Button 1"/>
            <shell:ApplicationBarIconButton IconUri="/Images/appbar_button2.png" Text="Button 2"/>
            <shell:ApplicationBar.MenuItems>
                <shell:ApplicationBarMenuItem Text="MenuItem 1"/>
                <shell:ApplicationBarMenuItem Text="MenuItem 2"/>
            </shell:ApplicationBar.MenuItems>
        </shell:ApplicationBar>
    </phone:PhoneApplicationPage.ApplicationBar>-->
 
</phone:PhoneApplicationPage>

MainPage.xaml.cs

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Globalization;


namespace SketchesExample {
    public partial class MainPage : PhoneApplicationPage
        {
            InkPresenter myInkPresenter = new InkPresenter();
            Grid grid = new Grid { Height = 800, Width = 480 };
            Image backGnd = new Image();
            Popup popup = new Popup { IsOpen = true };
            Dictionary<int, Stroke> strokes = new Dictionary<int, Stroke>();
        }
    public MainPage()
        {
            InitializeComponent();
            this.grid.Children.Add(this.backGnd);
            this.grid.Children.Add(this.myInkPresenter);
            this.popup.Child = this.grid;
 
        }
    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.popup.IsOpen = true;
            Touch.FrameReported += Touch_Reported;
        }
    protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.popup.IsOpen = true;
            Touch.FrameReported += Touch_Reported;
        }
    void Touch_Reported(object sender, TouchFrameEventArgs e)
        {
 
            TouchPointCollection tpCollection = e.GetTouchPoints(this.myInkPresenter);
 
            foreach (TouchPoint tp in tpCollection)
            {
                if (tp.Action == TouchAction.Down)
                {
                    Stroke stroke = new Stroke();
                    stroke.DrawingAttributes.Color = Color.FromArgb(1, 0, 255, 0);
                    stroke.StylusPoints.Add(new StylusPoint(tp.Position.X, tp.Position.Y));
                    this.strokes[tp.TouchDevice.Id] = stroke;
                    this.myInkPresenter.Strokes.Add(stroke);
                }
                else if (tp.Action == TouchAction.Move)
                {
                    if (this.strokes.ContainsKey(tp.TouchDevice.Id))
                        this.strokes[tp.TouchDevice.Id].StylusPoints.Add(new StylusPoint(tp.Position.X,tp.Position.Y));
                }
                else if (tp.Action == TouchAction.Down)
                {
                    if (this.strokes.ContainsKey(tp.TouchDevice.Id))
                        this.strokes.Remove(tp.TouchDevice.Id);
                }
            }
        }
    }
