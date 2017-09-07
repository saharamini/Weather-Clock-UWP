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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SevenDaysPage : Page
    {
        public SevenDaysPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RootObject2 weather;
            // get current position
            var position = await LocationManager.GetPosition();
            if (position == null)
            {
                // Your Latitude and Longitude
                weather = await WeatherMapProxy.GetWeather("Your Latitude", "Your Longitude");
            }
            else
            {
                weather =
                    await SevenDays
                    .GetWeather_SevenDays(
                        position.Coordinate.Point.Position.Latitude,
                        position.Coordinate.Point.Position.Longitude
                        );
            }

            var ResultImage = new[] { ResultImage0, ResultImage1, ResultImage2, ResultImage3, ResultImage4, ResultImage5, ResultImage6};
            var TempTextBlock = new[] { TempTextBlock0, TempTextBlock1, TempTextBlock2, TempTextBlock3, TempTextBlock4, TempTextBlock5, TempTextBlock6};
            var DescriptionTextBlock = new[] { DescriptionTextBlock0, DescriptionTextBlock1, DescriptionTextBlock2, DescriptionTextBlock3, DescriptionTextBlock4, DescriptionTextBlock5, DescriptionTextBlock6};
            var WindSpeedTextBlock = new[] { WindSpeedTextBlock0, WindSpeedTextBlock1, WindSpeedTextBlock2, WindSpeedTextBlock3, WindSpeedTextBlock4, WindSpeedTextBlock5, WindSpeedTextBlock6};
            var WindDirectionTextBlock = new[] { WindDirectionTextBlock0, WindDirectionTextBlock1, WindDirectionTextBlock2, WindDirectionTextBlock3, WindDirectionTextBlock4, WindDirectionTextBlock5, WindDirectionTextBlock6};
            var WindDirectionImage = new[] { WindDirectionImage0, WindDirectionImage1, WindDirectionImage2, WindDirectionImage3, WindDirectionImage4, WindDirectionImage5, WindDirectionImage6};
            var TimeTextBlock = new[] { TimeTextBlock0, TimeTextBlock1, TimeTextBlock2, TimeTextBlock3, TimeTextBlock4, TimeTextBlock5, TimeTextBlock6};
            var TempMaxTextBlock = new[] { TempMaxTextBlock0, TempMaxTextBlock1, TempMaxTextBlock2, TempMaxTextBlock3, TempMaxTextBlock4, TempMaxTextBlock5, TempMaxTextBlock6 };
            
            // ms-appx:/// use for URIS
            // wind direction 
            var arr = new[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

            for (int i = 0; i < 7; i++)
            {
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", weather.list[i].weather[0].icon);
                ResultImage[i].Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                // first index of wather list weather.weather[0]
                TempTextBlock[i].Text = ((int)weather.list[i].temp.min).ToString();
                TempMaxTextBlock[i].Text = ((int)weather.list[i].temp.max).ToString();
                DescriptionTextBlock[i].Text = weather.list[i].weather[0].description;

                // wind direction
                var val = (int)((weather.list[i].deg / 22.5) + .5);
                var degree = arr[(val % 16)];
                WindSpeedTextBlock[i].Text = ((int)weather.list[i].speed).ToString();
                WindDirectionTextBlock[i].Text = degree;
                string iconDirection = String.Format("ms-appx:///Assets/Navigation/{0}.png", degree);
                WindDirectionImage[i].Source = new BitmapImage(new Uri(iconDirection, UriKind.Absolute));

                //time
                var timestamp = weather.list[i].dt;
                // First make a System.DateTime equivalent to the UNIX Epoch.
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                // Add the number of seconds in UNIX timestamp to be converted.
                dateTime = dateTime.AddSeconds(timestamp);
                // The dateTime now contains the right date/time so to format the string,
                // use the standard formatting methods of the DateTime object.
                string date = dateTime.ToLocalTime().ToString().Split(' ')[0];
                TimeTextBlock[i].Text = date;
            }
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
