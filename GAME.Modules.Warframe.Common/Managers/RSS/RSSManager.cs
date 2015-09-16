using GAME.Common.Core.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GAME.Modules.Warframe.Common.Managers.RSS
{
    public class RSSManager : IManagerDataGetter
    {
        public RSSManager()
        {

        }

        private async Task<SyndicationFeed> GetFeed(string uri)
        {
            //String[] wuris = { 
            //                    "http://content.warframe.com/dynamic/worldState.php"//, 
            //                    //"http://content.ps4.warframe.com/dynamic/worldState.php", 
            //                    //"http://content.xb1.warframe.com/dynamic/worldState.php" 
            //                };
            //foreach (string wuri in wuris)
            //{
            //    WebClient wc = new WebClient();
            //    wc.DownloadStringCompleted += wc_DownloadStringCompleted;
            //    wc.DownloadStringAsync(new Uri(wuri));
            //    //DateTimeOffset d = DateTimeOffset.Now;
            //}

            XmlReader r;
            try
            {
               r = XmlReader.Create(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("RSSManager.GetFeed : Error reading feed input : {0}", e.Message);
                return null;
            }

            SyndicationFeed feed = SyndicationFeed.Load(r);

            r.Close();
            r.Dispose();
            return feed;
        }

        //void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    WebClient wc = sender as WebClient;
        //    dynamic jo = Json.Decode(e.Result);
        //    dynamic alerts = jo.Alerts;
        //    dynamic invasions = jo.Invasions;
        //    dynamic events = jo.Events;
        //    dynamic badlands = jo.BadlandNodes;
        //    dynamic daily = jo.DailyDeals;
        //    dynamic flash = jo.FlashSales;
        //    dynamic global = jo.GlobalUpgrades;
        //    dynamic goals = jo.Goals;
        //    dynamic hub = jo.HubEvents;
        //    dynamic lib = jo.LibraryInfo;
        //    dynamic voidItems = jo.VoidTrades;
        //    wc.Dispose();
        //}

        private List<FeedDTO> FeedToFeed(SyndicationFeed feed)
        {
            if (feed == null)
                feed = new SyndicationFeed();
            List<FeedDTO> lst = new List<FeedDTO>();
            foreach (var item in feed.Items)
            {
                FeedDTO f = new FeedDTO();
                //Always present, even if null

                if (item.Id != null)
                    f.Id = item.Id;
                if (item.Title != null)
                    f.Title = item.Title.Text;
                if (item.Summary != null)
                    f.Description = item.Summary.Text;
                if (item.Authors != null && item.Authors.Count > 0)
                    f.Author = item.Authors[0].Email;
                if (item.PublishDate != null)
                    f.PublishDate = item.PublishDate;

                if (item.ElementExtensions.Where(p => p.OuterName == "faction").Count() > 0)
                    f.Faction = item.ElementExtensions.Where(p => p.OuterName == "faction").First().GetObject<XElement>().Value;
                if (item.ElementExtensions.Where(p => p.OuterName == "expiry").Count() > 0)
                    f.ExpireDate = DateTimeOffset.Parse(item.ElementExtensions.Where(p => p.OuterName == "expiry").First().GetObject<XElement>().Value);
                lst.Add(f);
            }
            return lst;
        }

        private List<ExpandoObject> FeedToExpando(SyndicationFeed feed)
        {
            if (feed == null)
                feed = new SyndicationFeed();
            List<ExpandoObject> lst = new List<ExpandoObject>();
            foreach (var item in feed.Items)
            {
                dynamic f = new ExpandoObject();
                //Always present, even if null

                if (item.Id != null)
                    f.Id = item.Id;
                if (item.Title != null)
                    f.Title = item.Title.Text;
                if (item.Summary != null)
                    f.Description = item.Summary.Text;
                if (item.Authors != null && item.Authors.Count > 0)
                    f.Author = item.Authors[0].Email;
                if (item.PublishDate != null)
                    f.PublishDate = item.PublishDate;

                if (item.ElementExtensions.Where(p => p.OuterName == "faction").Count() > 0)
                    f.Faction = item.ElementExtensions.Where(p => p.OuterName == "faction").First().GetObject<XElement>().Value;
                if (item.ElementExtensions.Where(p => p.OuterName == "expiry").Count() > 0)
                    f.ExpireDate = DateTimeOffset.Parse(item.ElementExtensions.Where(p => p.OuterName == "expiry").First().GetObject<XElement>().Value);
                lst.Add(f);
            }
            return lst;
        }

        public async Task<List<FeedDTO>> GetFeeds(string source)
        {
            List<FeedDTO> lst = null;
            try
            {
                SyndicationFeed feed = await GetFeed(source);
                lst = FeedToFeed(feed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Managers.RSS.GetFeeds : {0}", e.Message);
            }
            return lst;
        }

        public async Task<List<ExpandoObject>> GetExpando(string source)
        {
            return FeedToExpando(await GetFeed(source));
        }
    }
}
