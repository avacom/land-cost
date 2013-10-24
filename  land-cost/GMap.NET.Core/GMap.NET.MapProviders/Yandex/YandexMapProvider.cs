
namespace GMap.NET.MapProviders
{
    using System;
    using System.Collections.Generic;
    using GMap.NET.Projections;
    using System.Diagnostics;
    using GMap.NET.Internals;
    using System.Xml;
    using System.Globalization;

   public abstract class YandexMapProviderBase : GMapProvider
   {
      #region GMapProvider Members
      public override Guid Id
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override string Name
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override PureProjection Projection
      {
         get
         {
            return MercatorProjectionYandex.Instance;
         }
      }

      GMapProvider[] overlays;
      public override GMapProvider[] Overlays
      {
         get
         {
            if(overlays == null)
            {
               overlays = new GMapProvider[] { this };
            }
            return overlays;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         throw new NotImplementedException();
      }
      #endregion

      protected string Version = "2.48.0";
   }

   /// <summary>
   /// YandexMap provider
   /// </summary>
   public class YandexMapProvider : YandexMapProviderBase
   {
      public static readonly YandexMapProvider Instance;

      YandexMapProvider()
      {
         RefererUrl = "http://maps.yandex.ru/";
      }

      static YandexMapProvider()
      {
         Instance = new YandexMapProvider();
      }

      #region GMapProvider Members

      readonly Guid id = new Guid("82DC969D-0491-40F3-8C21-4D90B67F47EB");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "YandexMap";
      public override string Name
      {
         get
         {
            return name;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         string url = MakeTileImageUrl(pos, zoom, LanguageStr);

         return GetTileImageUsingHttp(url);
      }

      #endregion

      string MakeTileImageUrl(GPoint pos, int zoom, string language)
      {
         // http://vec01.maps.yandex.ru/tiles?l=map&v=2.10.2&x=1494&y=650&z=11
         // http://vec03.maps.yandex.net/tiles?l=map&v=2.19.5&x=579&y=326&z=10&g=Gagarin
         // http://vec02.maps.yandex.net/tiles?l=map&v=2.26.0&x=586&y=327&z=10&lang=ru-RU
         //http://vec03.maps.yandex.net/tiles?l=map&v=2.44.0&x=289&y=164&z=9&lang=en_US

         return string.Format(UrlFormat, UrlServer, GetServerNum(pos, 4) + 1, Version, pos.X, pos.Y, zoom, language);
      }

      static readonly string UrlServer = "vec";
      static readonly string UrlFormat = "http://{0}0{1}.maps.yandex.net/tiles?l=map&v={2}&x={3}&y={4}&z={5}&lang={6}";                                
   }

   public class YandexMapProviderUA : YandexMapProviderBase
   {
       public static readonly YandexMapProviderUA Instance;

       YandexMapProviderUA()
       {
           RefererUrl = "http://maps.yandex.ua/";
       }

       static YandexMapProviderUA()
       {
           Instance = new YandexMapProviderUA();
       }

       #region GMapProvider Members

       readonly Guid id = new Guid("7A05FB21-9E7F-4D7B-881F-B920183C8899");
       public override Guid Id
       {
           get
           {
               return id;
           }
       }

       readonly string name = "YandexMap";
       public override string Name
       {
           get
           {
               return name;
           }
       }
       public override PureImage GetTileImage(GPoint pos, int zoom)
       {
           string url = MakeTileImageUrl(pos, zoom, LanguageStr);

           return GetTileImageUsingHttp(url);
       }

       #endregion

       string MakeTileImageUrl(GPoint pos, int zoom, string language)
       {
           // http://vec01.maps.yandex.ru/tiles?l=map&v=2.10.2&x=1494&y=650&z=11
           // http://vec03.maps.yandex.net/tiles?l=map&v=2.19.5&x=579&y=326&z=10&g=Gagarin
           // http://vec02.maps.yandex.net/tiles?l=map&v=2.26.0&x=586&y=327&z=10&lang=ru-RU
           //http://vec03.maps.yandex.net/tiles?l=map&v=2.44.0&x=289&y=164&z=9&lang=en_US

           return string.Format(UrlFormat, UrlServer, GetServerNum(pos, 4) + 1, Version, pos.X, pos.Y, zoom, language);
       }

       static readonly string UrlServer = "vec";
       static readonly string UrlFormat = "http://{0}0{1}.maps.yandex.net/tiles?l=map&v={2}&x={3}&y={4}&z={5}&lang={6}";
       static readonly string ReverseGeocodeUrlFormat = "http://geocode-maps.yandex.ru/1.x/?geocode={0},{1}&lang=uk-UA";
       static readonly string GeocodeUrlFormat = "http://geocode-maps.yandex.ru/1.x/?geocode={0}&lang=uk-UA";

       public Placemark? GetPlacemark(PointLatLng location, out GeoCoderStatusCode status)
       {
           return GetPlacemarkFromReverseGeocoderUrl(MakeReverseGeocoderUrl(location), out status);
       }

       public List<Placemark?> GetPlacemarks(string searchString, out GeoCoderStatusCode status)
       {
           List<Placemark?> ret = new List<Placemark?>();
           status = GeoCoderStatusCode.Unknow;
           string url = MakeGeocoderUrl(searchString);
           try
           {
               string geo = GMaps.Instance.UsePlacemarkCache ? Cache.Instance.GetContent(url, CacheType.PlacemarkCache) : string.Empty;

               bool cache = false;

               if (string.IsNullOrEmpty(geo))
               {
                   geo = GetContentUsingHttp(url);

                   if (!string.IsNullOrEmpty(geo))
                   {
                       cache = true;
                   }
               }

               if (!string.IsNullOrEmpty(geo))
               {
                   if (geo.StartsWith("<?ymaps") && geo.Contains("<featureMember"))
                   {
                       if (cache && GMaps.Instance.UsePlacemarkCache)
                       {
                           Cache.Instance.SaveContent(url, CacheType.PlacemarkCache, geo);
                       }
                   }

                   XmlDocument doc = new XmlDocument();
                   doc.LoadXml(geo);
                   {
                       XmlNodeList nodes = doc.GetElementsByTagName("GeoObject");
                       if (nodes != null && nodes.Count > 0)
                       {
                           foreach (XmlNode node in nodes)
                           {
                               XmlDocument subDoc = new XmlDocument();
                               subDoc.LoadXml(node.OuterXml);

                               XmlNodeList subNodes = subDoc.GetElementsByTagName("text");
                               if (subNodes != null && subNodes.Count > 0)
                               {
                                   Placemark p = new Placemark(subNodes[0].InnerText);

                                   subNodes = subDoc.GetElementsByTagName("CountryName");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.CountryName = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("CountryNameCode");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.CountryNameCode = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("AdministrativeAreaName");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.AdministrativeAreaName = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("LocalityName");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.LocalityName = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("ThoroughfareName");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.ThoroughfareName = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("PremiseNumber");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       p.HouseNo = subNodes[0].InnerText;
                                   }

                                   subNodes = subDoc.GetElementsByTagName("pos");
                                   if (subNodes != null && subNodes.Count > 0)
                                   {
                                       double dLat = 0;
                                       double dLng = 0;
                                       string[] parsed = subNodes[0].InnerText.Split(' ');

                                       if (parsed != null && parsed.Length >= 2)
                                       {
                                           double.TryParse(parsed[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dLat);
                                           double.TryParse(parsed[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dLng);
                                       }
                                       p.Lat = dLat;
                                       p.Lng = dLng;
                                   }
                                   Placemark? r = p;
                                   ret.Add(r);
                               }
                           }
                           status = GeoCoderStatusCode.G_GEO_SUCCESS;
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               status = GeoCoderStatusCode.ExceptionInCode;
               Debug.WriteLine("Get placemarks from geocode: " + ex);
           }
           return ret;
       }

       private Placemark? GetPlacemarkFromReverseGeocoderUrl(string url, out GeoCoderStatusCode status)
       {
           status = GeoCoderStatusCode.Unknow;
           Placemark? ret = null;
           try
           {
               string geo = GMaps.Instance.UsePlacemarkCache ? Cache.Instance.GetContent(url, CacheType.PlacemarkCache) : string.Empty;

               bool cache = false;

               if (string.IsNullOrEmpty(geo))
               {
                   geo = GetContentUsingHttp(url);

                   if (!string.IsNullOrEmpty(geo))
                   {
                       cache = true;
                   }
               }

               if (!string.IsNullOrEmpty(geo))
               {
                   if (geo.StartsWith("<?ymaps") && geo.Contains("<featureMember"))
                   {
                       if (cache && GMaps.Instance.UsePlacemarkCache)
                       {
                           Cache.Instance.SaveContent(url, CacheType.PlacemarkCache, geo);
                       }
                   }

                   XmlDocument doc = new XmlDocument();
                   doc.LoadXml(geo);
                   {
                       XmlNodeList nodes = doc.GetElementsByTagName("text");
                       if (nodes != null && nodes.Count > 0)
                       {
                           Placemark p = new Placemark(nodes[0].InnerText);

                           nodes = doc.GetElementsByTagName("CountryName");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.CountryName = nodes[0].InnerText;
                           }

                           nodes = doc.GetElementsByTagName("CountryNameCode");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.CountryNameCode = nodes[0].InnerText;
                           }

                           nodes = doc.GetElementsByTagName("AdministrativeAreaName");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.AdministrativeAreaName = nodes[0].InnerText;
                           }

                           nodes = doc.GetElementsByTagName("LocalityName");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.LocalityName = nodes[0].InnerText;
                           }

                           nodes = doc.GetElementsByTagName("ThoroughfareName");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.ThoroughfareName = nodes[0].InnerText;
                           }

                           nodes = doc.GetElementsByTagName("PremiseNumber");
                           if (nodes != null && nodes.Count > 0)
                           {
                               p.HouseNo = nodes[0].InnerText;
                           }

                           ret = p;
                           status = GeoCoderStatusCode.G_GEO_SUCCESS;
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               ret = null;
               status = GeoCoderStatusCode.ExceptionInCode;
               Debug.WriteLine("GetPlacemarkFromReverseGeocoderUrl: " + ex);
           }

           return ret;
       }

       private string MakeReverseGeocoderUrl(PointLatLng location)
       {
           return string.Format(CultureInfo.InvariantCulture, ReverseGeocodeUrlFormat, location.Lng, location.Lat);
       }

       private string MakeGeocoderUrl(string searchString)
       {
           return string.Format(CultureInfo.InvariantCulture, GeocodeUrlFormat, searchString);
       }
   }
}