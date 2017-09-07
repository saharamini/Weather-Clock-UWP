# Fancy Clock and Weather display - Universal Windows Platform (UWP) app

A simple Weather app on Windows 10 IoT Core device.

Components
You will need the following components :

a Element 14 7" Pi Touchscreen LCD Display (http://www.microcenter.com/product/454804/7_Pi_Touchscreen_LCD_Display)

(Optional) a Case for the Official Raspberry Pi 7" Touchscreen Display - Adjustable angle (https://www.amazon.com/gp/product/B01HV97F64/ref=oh_aui_search_detailpage?ie=UTF8&psc=1)

a Raspberry Pi running Windows 10 IoT Core

Visual Studio 2017 (Community Edition - Free)

-----------------------------------------------------------------------------------------
First you need to install Windows 10 IoT on Raspberry Pi 3:
https://developer.microsoft.com/en-us/windows/iot/Docs/GetStarted/rpi3/sdcard/stable/GetStartedStep1.htm

Note: You have to format your SD card (using SDFormatter) before installing Windows 10 IoT

-------------------------------------------------------------------------------------------------
Please follow the steps to run the project on your pc: 

1. Run Weather.sln
2. Right click on the Solution, choose Build Solution
3. Run the project

--------------------

If you want to create a new project and copy and past the code, you need to follow these steps:

1. Open Visual Studio 2017
2. File -> New -> Project -> Windows Universal -> Blank App (Universal Windows)

--------------------

Using Open Weather Map API (openweathermap.org/api): 

To get access to weather API you need an API key: 
http://openweathermap.org/appid

You need to setup the API key on HourelyWeather.cs, WeatherMapProxy.cs, and SevenDays.cs
```
HttpClient http = new HttpClient();
          // URI &units=imperial
          var response = await http.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&units=imperial&APPID='Your API Key'", lat, lon));
          // response content - JSON format
          var result = await response.Content.ReadAsStringAsync();

```
------------------------------------------------------------------------------
You can use the following link to convert JSON to C#:

http://json2csharp.com/

------------------------------------------------------------------------------- 
Setup your location: 

in MainPage.xaml.cs page you need to setup your own location:

```
private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RootObject weather;
            // get current position
            var position = await LocationManager.GetPosition();
            if (position == null)
            {
                // Your Latitude and Longitude
                weather = await WeatherMapProxy.GetWeather("Your Latitude", "Your Longitude");
            }

```

--------------------------------------------------------------------------------
Clock:

in ClockPage.xaml.cs

Your can add more cities to display their time.

You just need to change FindSystemTimeZoneById input:

```
	// germany time
            var info1 = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTimeOffset germanyTime = TimeZoneInfo.ConvertTime(localServerTime, info1);
            TextBlock2.Text = "Berlin";
            TimeTextBlock2.Text = germanyTime.ToString().Split(' ')[1] + " "+germanyTime.ToString().Split(' ')[2].ToLower();
            DateTextBlock2.Text = germanyTime.ToString().Split(' ')[0];
```

Use this link to add FindSystemTimeZoneById input:
https://msdn.microsoft.com/en-us/library/gg154758.aspx

------------------------------------------------------------------------
There are two options to run the project:

1. Run on local machine
2. Run on remote machine which you have to add the machine's IP address. 
Please follow the steps:
1. Right click on the project
2. Go to Properties
3. Go to Debug tab
4. Click on Find...

----------------------------------------------------------------------
Note: Don't forget to choose ARM as a solution platforms next to the run button.  
-----------------------------------------------------------------------

There are two options for Solution Configuration:
1. Debug -> the app will not saved on the Raspberry Pi 
2. Release -> the app will saved on the  Raspberry Pi 

--------------------------------------------------------------------------
Note: go to the web-based Windows 10 IoT Core admin portal, go to the App tab and set your app as Startup
