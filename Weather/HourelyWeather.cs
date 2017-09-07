using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class HourelyWeather
    {
        public static async Task<RootObject1> GetWeather_Hourely(double lat, double lon)
        {

            HttpClient http = new HttpClient();
            // URI &units=imperial
            var response = await http.GetAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=imperial&APPID='Your API Key'", lat, lon));
            // response content - JSON format
            var result = await response.Content.ReadAsStringAsync();
            // give the type of the class that you want to work with it
            var serializer = new DataContractJsonSerializer(typeof(RootObject1));
            // keep response in the memory
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            // get dara out of serializer - convert json to RootObject
            var data = (RootObject1)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class Main1
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_kf { get; set; }
    }
    [DataContract]
    public class Weather1
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
    public class Clouds1
    {
        [DataMember]
        public int all { get; set; }
    }
    [DataContract]
    public class Wind1
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }
    [DataContract]
    public class Rain1
    {
    }
    [DataContract]
    public class Sys1
    {
        [DataMember]
        public string pod { get; set; }
    }
    [DataContract]
    public class List1
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Main1 main { get; set; }
        [DataMember]
        public List<Weather1> weather { get; set; }
        [DataMember]
        public Clouds1 clouds { get; set; }
        [DataMember]
        public Wind1 wind { get; set; }
        [DataMember]
        public Rain1 rain { get; set; }
        [DataMember]
        public Sys1 sys { get; set; }
        [DataMember]
        public string dt_txt { get; set; }
    }
    [DataContract]
    public class Coord1
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lon { get; set; }
    }
    [DataContract]
    public class City1
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public string country { get; set; }
    }
    [DataContract]
    public class RootObject1
    {
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List<List1> list { get; set; }
        [DataMember]
        public City1 city { get; set; }
    }
}
