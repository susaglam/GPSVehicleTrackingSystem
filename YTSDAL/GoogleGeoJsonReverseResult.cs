using Newtonsoft.Json;
using System.Collections.Generic;

namespace YTSDAL
{
    public class adresiGetir
    {
        [JsonProperty("address_components", NullValueHandling = NullValueHandling.Ignore)]
        public AddressComponent[] adresAl { get; set; }

        [JsonProperty("formatted_address", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
        public Geometry Geometry { get; set; }
    }


    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Northeast2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Northeast2 northeast { get; set; }
        public Southwest2 southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
        public Bounds bounds { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
