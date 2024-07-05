using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace YTSDAL
{
    public class armoli
    {
        public string ArmoliGeoCodeReverse(string Lat, string Lng)
        {
            using (var client = new WebClient())
            {
               string url = "http://takip.artemobil.net/webservice/web-service2.php?lat=" + Lat + "&lng=" + Lng + "&type=getAddress&format=json&bi=8889&us=yigittakip&pa=ygt11A";
                string formatAddress = string.Format(url);
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(formatAddress);
                //var r = JsonConvert.DeserializeObject(result);
                string adres = result.Replace("\r\n", ""); ;
                
                if (adres.Length<5)
                {
                    Lat = Lat.Substring(0, 7);
                    string url2 = "http://demo.gpswox.com/api/geo_address?lat=" + Lat + "&lon=" + Lng;
                    string formatAddress2 = string.Format(url2);
                    client.Encoding = Encoding.UTF8;
                    var result2 = client.DownloadString(formatAddress2);

                    adres = result2;

                    //var r2 = JsonConvert.DeserializeObject<RootObject>(result2);                   
                    //if (string.IsNullOrEmpty(r2.ToString()))
                    //{
                    //    adres = "___";
                    //}
                    //else
                    //{
                    //    adres = result2;
                    //}

                }
                return adres;
            }
        }
    }
}
