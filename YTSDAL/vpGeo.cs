using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
namespace YTSDAL
{
    public class vpGeo
    {
        public enum Result
        {
            OK,
            ZERO_RESULTS,
            OVER_QUERY_LIMIT,
            REQUEST_DENIED,
            INVALID_REQUEST,
            UNKNOWN_ERROR
        }
        //point_of_interest için url https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}&language=tr&region=tr&result_type=point_of_interest
        const string GGeoCodeJsonServiceUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}&language=tr&region=tr";
        const string GGeoCodeJsonReverseServiceUrl = "https://maps.googleapis.com/maps/api/geocode/json?latlng={0}&key={1}&language=tr&region=tr";
        public string Key { get; set; }
        public Result GeoResult { get; set; }

        ///// <summary>
        /////  var g = new vpGeo();
        /////  g.Key = "AIzaSyB9_AGN72g4n33AnSKzJ1dDIRqv-rp4VMs";
        /////    var r = g.GoogleGeoCodeInfo(new Address { Address1 = address, City = city, State = state, Zip = zip });
        /////    if (g.GeoResult == vpGeo.Result.OK)
        /////    {
        /////        //view results on variable r
        /////    }
        ///// </summary>
        ///// <param name="address"></param>
        ///// <returns></returns>
        //public GeoResult GoogleGeoCodeInfo(Address address)
        //{
        //    if (string.IsNullOrEmpty(Key))
        //    {
        //        throw new MissingFieldException("Google API Key hatalý yada eksik");
        //    }

        //    using (var client = new WebClient())
        //    {
        //        string formatAddress = string.Format(GGeoCodeJsonServiceUrl, EncodeAddress(address), Key);
        //        var result = client.DownloadString(formatAddress);
        //        var O = JsonConvert.DeserializeObject<GeoResult>(result);
        //        SetGeoResultFlag(O.Status);
        //        return O;
        //    }
        //}

        /// <summary>
        ///  var g = new vpGeo();
        ///  g.Key = "AIzaSyB9_AGN72g4n33AnSKzJ1dDIRqv-rp4VMs";
        ///    var r = g.GoogleGeoCodeReverse(Latlng);
        ///    if (g.GeoResult == vpGeo.Result.OK)
        ///    {
        ///        //view results on variable r
        ///    }
        /// </summary>
        /// <param name="Latlng"></param>
        /// <returns></returns>
        public RootObject GoogleGeoCodeReverse(string Latlng)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new MissingFieldException("Google API Key hatalý yada eksik");
            }

            using (var client = new WebClient())
            {
                string formatAddress = string.Format(GGeoCodeJsonReverseServiceUrl, Latlng, Key);
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(formatAddress);                
                var r = JsonConvert.DeserializeObject<RootObject>(result);
                SetGeoResultFlag(r.status);
                return r;
            }
        }

        public RootObject GoogleGeoCodeReverse(string Latlng, string url)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new MissingFieldException("Google API Key hatalý yada eksik");
            }

            using (var client = new WebClient())
            {
                string formatAddress = string.Format(url, Latlng, Key);
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(formatAddress);
                var r = JsonConvert.DeserializeObject<RootObject>(result);
                SetGeoResultFlag(r.status);
                return r;
            }
        }

        public RootObject GoogleGeoCodeReverse2(string Latlng)
        {
            if (string.IsNullOrEmpty(Key))
            {
                throw new MissingFieldException("Google API Key hatalý yada eksik");
            }

  
            using (var client = new WebClient())
            {
                string formatAddress = string.Format("http://maps.googleapis.com/maps/api/geocode/json?latlng={0}&sensor=false", Latlng);
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("Referer", "http://www.google.com");
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(formatAddress);
                var r = JsonConvert.DeserializeObject<RootObject>(result);
                SetGeoResultFlag(r.status);
                return r;
            }
        }

        //private string EncodeAddress(Address address)
        //{
        //    var sb = new StringBuilder();


        //    if (!string.IsNullOrEmpty(address.Address1))
        //        sb.Append(Uri.EscapeUriString(" " + address.Address1));

        //    if (!string.IsNullOrEmpty(address.Address2))
        //        sb.Append(Uri.EscapeUriString(" " + address.Address2));

        //    if (!string.IsNullOrEmpty(address.City))
        //        sb.Append(Uri.EscapeUriString(" " + address.City));

        //    if (!string.IsNullOrEmpty(address.State))
        //        sb.Append(Uri.EscapeUriString(" " + address.State));

        //    if (!string.IsNullOrEmpty(address.Zip))
        //        sb.Append(Uri.EscapeUriString(" " + address.Zip));


        //    return sb.ToString();
        //}

        private void SetGeoResultFlag(string status)
        {
            switch (status)
            {
                case "OK":
                    GeoResult = Result.OK;
                    break;
                case "ZERO_RESULTS":
                    GeoResult = Result.ZERO_RESULTS;
                    break;
                case "OVER_QUERY_LIMIT":
                    GeoResult = Result.OVER_QUERY_LIMIT;
                    break;
                case "REQUEST_DENIED":
                    GeoResult = Result.REQUEST_DENIED;
                    break;
                case "INVALID_REQUEST":
                    GeoResult = Result.INVALID_REQUEST;
                    break;
                case "UNKNOWN_ERROR":
                    GeoResult = Result.UNKNOWN_ERROR;
                    break;
            }
        }


    }
}
