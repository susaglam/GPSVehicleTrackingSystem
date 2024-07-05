using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;namespace YTSDAL
{
    public class BasarSoft
    {
        private static string baseUri;

        private static string POIUri;

        static BasarSoft()
        {
            BasarSoft.baseUri = "http://maps.basarsoft.com.tr/yb5.ashx?f=rg&x={0}&y={1}";
            BasarSoft.POIUri = "http://maps.basarsoft.com.tr/yb5.ashx?f=pl&r=177&x={0}&y={1}";
        }

        public BasarSoft()
        {
        }

        public BasarResults GetGeographicInfo(string latitude, string longitude)
        {
            string lat = latitude.Replace(',', '.');
            string lng = longitude.Replace(',', '.');
            string requestUri = string.Format(BasarSoft.baseUri, lat, lng);
            WebClient wc = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            return JsonConvert.DeserializeObject<BasarResults>(wc.DownloadString(requestUri));
        }

        public List<BasarPOI> GetPOIInfo(string latitude, string longitude)
        {
            string lat = latitude.Replace(',', '.');
            string lng = longitude.Replace(',', '.');
            string requestUri = string.Format(BasarSoft.POIUri, lat, lng);
            WebClient wc = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            string results = wc.DownloadString(requestUri);
            BasarPOI fxp = new BasarPOI();
            return JsonConvert.DeserializeObject<List<BasarPOI>>(results);
        }
    }
}