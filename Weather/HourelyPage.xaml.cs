using System;
using System.Collections.Generic;
//CultureInfo
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class HourelyPage : Page
    {
        public HourelyPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RootObject1 weather;
            // get current position
            var position = await LocationManager.GetPosition();
            if (position == null)
            {
                // Watertown
                weather = await HourelyWeather.GetWeather_Hourely(42.36214755982617, -71.148844559773557);
            }
            else
            {
                weather =
                    await HourelyWeather
                    .GetWeather_Hourely(
                        position.Coordinate.Point.Position.Latitude,
                        position.Coordinate.Point.Position.Longitude
                        );
            }
            // wind direction 
            var arr = new[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
           
            //image  http://openweathermap.org/img/w/{0}.png
            var ResultImage = new[] { ResultImage0, ResultImage1, ResultImage2, ResultImage3, ResultImage4, ResultImage5, ResultImage6, ResultImage7, ResultImage8, ResultImage9 };
            var TempTextBlock = new[] { TempTextBlock0, TempTextBlock1, TempTextBlock2, TempTextBlock3, TempTextBlock4, TempTextBlock5, TempTextBlock6, TempTextBlock7, TempTextBlock8, TempTextBlock9 };
            var DescriptionTextBlock = new[] { DescriptionTextBlock0, DescriptionTextBlock1, DescriptionTextBlock2, DescriptionTextBlock3, DescriptionTextBlock4, DescriptionTextBlock5, DescriptionTextBlock6, DescriptionTextBlock7, DescriptionTextBlock8, DescriptionTextBlock9 };
            var WindSpeedTextBlock = new[] { WindSpeedTextBlock0, WindSpeedTextBlock1, WindSpeedTextBlock2, WindSpeedTextBlock3, WindSpeedTextBlock4, WindSpeedTextBlock5, WindSpeedTextBlock6, WindSpeedTextBlock7, WindSpeedTextBlock8, WindSpeedTextBlock9 };
            var WindDirectionTextBlock = new[] { WindDirectionTextBlock0, WindDirectionTextBlock1, WindDirectionTextBlock2, WindDirectionTextBlock3, WindDirectionTextBlock4, WindDirectionTextBlock5, WindDirectionTextBlock6, WindDirectionTextBlock7, WindDirectionTextBlock8, WindDirectionTextBlock9 };
            var WindDirectionImage = new[] { WindDirectionImage0, WindDirectionImage1, WindDirectionImage2, WindDirectionImage3, WindDirectionImage4, WindDirectionImage5, WindDirectionImage6, WindDirectionImage7, WindDirectionImage8, WindDirectionImage9 };
            var TimeTextBlock = new[] { TimeTextBlock0, TimeTextBlock1, TimeTextBlock2, TimeTextBlock3, TimeTextBlock4, TimeTextBlock5, TimeTextBlock6, TimeTextBlock7, TimeTextBlock8, TimeTextBlock9 };
            // ms-appx:/// use for URIS
            for (int i=0; i< 10; i++) {
                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", weather.list[i].weather[0].icon);
                ResultImage[i].Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                // first index of wather list weather.weather[0]
                TempTextBlock[i].Text = ((int)weather.list[i].main.temp).ToString();
                DescriptionTextBlock[i].Text = weather.list[i].weather[0].description;

                // wind direction
                var val = (int)((weather.list[i].wind.deg / 22.5) + .5);
                var degree = arr[(val % 16)];
                WindSpeedTextBlock[i].Text = ((int)weather.list[i].wind.speed).ToString();
                WindDirectionTextBlock[i].Text = degree;
                string iconDirection = String.Format("ms-appx:///Assets/Navigation/{0}.png", degree);
                WindDirectionImage[i].Source = new BitmapImage(new Uri(iconDirection, UriKind.Absolute));

                //time
                var dateTime = weather.list[i].dt_txt;
                var time = dateTime.Split('-')[2].Split(' ')[1];
                TimeTextBlock[i].Text = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.CurrentCulture).ToString("h:mm tt"); 

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
