using System;
using System.Collections.Generic;
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
//BitmapImage
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RootObject weather;
            // get current position
            var position = await LocationManager.GetPosition();
            if (position == null)
            {
                // Your Latitude and Longitude
                weather = await WeatherMapProxy.GetWeather('Your Latitude', 'Your Longitude');
            }
            else
            {
                weather =
                    await WeatherMapProxy
                    .GetWeather(
                        position.Coordinate.Point.Position.Latitude,
                        position.Coordinate.Point.Position.Longitude
                        );
            }
            //image  http://openweathermap.org/img/w/{0}.png
            // ms-appx:/// use for URI
            string icon = String.Format("ms-appx:///Assets/Weather/{0}.png",weather.weather[0].icon);
            ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            // first index of wather list weather.weather[0]
            TempTextBlock.Text = ((int)weather.main.temp).ToString();
            DescriptionTextBlock.Text = weather.weather[0].description;
            LocationTextBlock.Text = weather.name;

            // wind direction 
            var arr = new[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

            // wind direction
            var val = (int)((weather.wind.deg / 22.5) + .5);
            var degree = arr[(val % 16)];
            WindSpeedTextBlock.Text = ((int)weather.wind.speed).ToString();
            WindDirectionTextBlock.Text = degree;
            string iconDirection = String.Format("ms-appx:///Assets/Navigation/{0}.png", degree);
            WindDirectionImage.Source = new BitmapImage(new Uri(iconDirection, UriKind.Absolute));
            HumidityTextBlock.Text = weather.main.humidity.ToString()+"%";

            //time
            var timestamp = weather.sys.sunrise;
            // First make a System.DateTime equivalent to the UNIX Epoch.
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // Add the number of seconds in UNIX timestamp to be converted.
            dateTime = dateTime.AddSeconds(timestamp);
            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.
            string sunrise = dateTime.ToLocalTime().ToString().Split(' ')[1] + " " + dateTime.ToLocalTime().ToString().Split(' ')[2].ToLower();
            SunriseTextBlock.Text = sunrise;

            //time
            var timestamp1 = weather.sys.sunset;
            // First make a System.DateTime equivalent to the UNIX Epoch.
            DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // Add the number of seconds in UNIX timestamp to be converted.
            dateTime1 = dateTime1.AddSeconds(timestamp1);
            // The dateTime now contains the right date/time so to format the string,
            // use the standard formatting methods of the DateTime object.
            string sunset = dateTime1.ToLocalTime().ToString().Split(' ')[1] + " " + dateTime1.ToLocalTime().ToString().Split(' ')[2].ToLower();
            SunsetTextBlock.Text = sunset;

            TempMinTextBlock.Text = ((int)weather.main.temp_min).ToString();
            TempMaxTextBlock.Text = ((int)weather.main.temp_max).ToString();

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
