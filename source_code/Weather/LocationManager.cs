using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Geolocator
using Windows.Devices.Geolocation;

namespace Weather
{
    public class LocationManager
    {
        // get current position
        public async static Task<Geoposition> GetPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus == GeolocationAccessStatus.Denied) { return null; }
            else
            {
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 }; // default value give what you get

                var position = await geolocator.GetGeopositionAsync();

                return position;
            }
        }
    }
}
