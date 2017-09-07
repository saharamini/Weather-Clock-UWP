using System;
using System.Collections.Generic;
//MemoryStream
using System.IO;
using System.Linq;
// HttpClient
using System.Net.Http;
// [DataContract]
using System.Runtime.Serialization;
// DataContractJsonSerializer
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class WeatherMapProxy
    {
        public static async Task<RootObject> GetWeather(double lat, double lon)
        {
            HttpClient http = new HttpClient();
            // URI &units=imperial
            var response = await http.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&APPID='Your API Key'", lat,lon));
            // response content - JSON format
            var result = await response.Content.ReadAsStringAsync();
            // give the type of the class that you want to work with it
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            // keep response in the memory
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            // get dara out of serializer - convert json to RootObject
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    // tell serializer that treat this like a class
    [DataContract]
    public class Coord
    {
        // tell serializer that treat this like a property
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }
}
