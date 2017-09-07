using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClockPage : Page
    {
        public ClockPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // current time
            TextBlock0.Text = "Boston";
            TimeTextBlock0.Text = DateTime.Now.ToString().Split(' ')[1] + " "+DateTime.Now.ToString().Split(' ')[2].ToLower();
            DateTextBlock0.Text = DateTime.Now.ToString().Split(' ')[0];

            // tehran time
            var info = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset tehranTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            TextBlock1.Text = "Tehran";
            TimeTextBlock1.Text = tehranTime.ToString().Split(' ')[1] + " "+tehranTime.ToString().Split(' ')[2].ToLower();
            DateTextBlock1.Text = tehranTime.ToString().Split(' ')[0];

            // germany time
            var info1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTimeOffset germanyTime = TimeZoneInfo.ConvertTime(localServerTime, info1);
            TextBlock2.Text = "Berlin";
            TimeTextBlock2.Text = germanyTime.ToString().Split(' ')[1] + " "+germanyTime.ToString().Split(' ')[2].ToLower();
            DateTextBlock2.Text = germanyTime.ToString().Split(' ')[0];

        }

        private void CurrentWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void HourelyWeatherkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HourelyPage));
        }

        private void SevenDaysWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SevenDaysPage));
        }

        private void ClockButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ClockPage));
        }
    }
}
