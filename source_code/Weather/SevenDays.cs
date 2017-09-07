using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class SevenDays
    {
        public static async Task<RootObject2> GetWeather_SevenDays(double lat, double lon)
        {

            HttpClient http = new HttpClient();
            // URI &units=imperial
            var response = await http.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&units=imperial&APPID='Your API Key'&cnt=7", lat, lon));
            // response content - JSON format
            var result = await response.Content.ReadAsStringAsync();
            // give the type of the class that you want to work with it
            var serializer = new DataContractJsonSerializer(typeof(RootObject2));
            // keep response in the memory
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            // get dara out of serializer - convert json to RootObject
            var data = (RootObject2)serializer.ReadObject(ms);

            return data;
        }
    }

    public class City2
    {
        public int geoname_id { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string iso2 { get; set; }
        public string type { get; set; }
        public int population { get; set; }
    }

    public class Temp2
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Weather2
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class List2
    {
        public int dt { get; set; }
        public Temp2 temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public List<Weather2> weather { get; set; }
        public double speed { get; set; }
        public double deg { get; set; }
        public int clouds { get; set; }
        public double rain { get; set; }
        public double snow { get; set; }
    }

    public class RootObject2
    {
        public string cod { get; set; }
        public double message { get; set; }
        public City2 city { get; set; }
        public int cnt { get; set; }
        public List<List2> list { get; set; }
    }
   
}
