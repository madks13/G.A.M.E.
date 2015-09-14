using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAME.Common.Core.Interfaces.Managers;
using System.Dynamic;
using System.Net;
using System.Web.Helpers;

namespace GAME.Common.Managers.Modules.Deathsnack
{
    class DeathsnackManager : IManagerDataGetter
    {
        private static async Task<List<String>> GetFeed(string uri)
        {
            List<String> lst = new List<String>();
            String[] wuris = { 
                                uri + "alerts_raw.txt", 
                                uri + "invasion_raw.txt"
                            };
            foreach (string wuri in wuris)
            {
                WebClient wc = new WebClient();
                
                lst.Add(wc.DownloadString(new Uri(wuri)));
                //DateTimeOffset d = DateTimeOffset.Now;
            }
            //XmlReader r;
            //try
            //{
            //   r = XmlReader.Create(uri);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("RSSManager.GetFeed : Error reading feed input : {0}", e.Message);
            //    return null;
            //}
            //SyndicationFeed feed = SyndicationFeed.Load(r);

            //r.Close();
            //r.Dispose();
            return lst;
        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            WebClient wc = sender as WebClient;
            dynamic jo = Json.Decode(e.Result);
            dynamic alerts = jo.Alerts;
            dynamic invasions = jo.Invasions;
            dynamic events = jo.Events;
            dynamic badlands = jo.BadlandNodes;
            dynamic daily = jo.DailyDeals;
            dynamic flash = jo.FlashSales;
            dynamic global = jo.GlobalUpgrades;
            dynamic goals = jo.Goals;
            dynamic hub = jo.HubEvents;
            dynamic lib = jo.LibraryInfo;
            dynamic voidItems = jo.VoidTrades;
            wc.Dispose();
        }

        private List<ExpandoObject> FeedToExpando(List<String> feed)
        {
            if (feed == null)
                feed = new List<String>();
            List<ExpandoObject> lst = new List<ExpandoObject>();
            foreach (var item in feed)
            {
                dynamic f = new ExpandoObject();
                //Always present, even if null

                //if (item.Id != null)
                //    f.Id = item.Id;
                //if (item.Title != null)
                //    f.Title = item.Title.Text;
                //if (item.Summary != null)
                //    f.Description = item.Summary.Text;
                //if (item.Authors != null && item.Authors.Count > 0)
                //    f.Author = item.Authors[0].Email;
                //if (item.PublishDate != null)
                //    f.PublishDate = item.PublishDate;

                //if (item.ElementExtensions.Where(p => p.OuterName == "faction").Count() > 0)
                //    f.Faction = item.ElementExtensions.Where(p => p.OuterName == "faction").First().GetObject<XElement>().Value;
                //if (item.ElementExtensions.Where(p => p.OuterName == "expiry").Count() > 0)
                //    f.ExpireDate = DateTimeOffset.Parse(item.ElementExtensions.Where(p => p.OuterName == "expiry").First().GetObject<XElement>().Value);
                lst.Add(f);
            }
            return lst;
        }

        public async Task<List<ExpandoObject>> GetExpando(string source)
        {
            return FeedToExpando(await GetFeed(source));
        }
    }
}
