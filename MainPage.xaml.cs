using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Globalization;

namespace SketchesExample
{
  public partial class MainPage : PhoneApplicationPage 
  { 
    InkPresenter myInkPresenter = new InkPresenter(); 
    Grid grid = new Grid { Height = 800, Width = 480 }; 
    Image backGnd = new Image(); 
    Popup popup = new Popup { IsOpen = true }; 
    Dictionary strokes = new Dictionary(); 
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
