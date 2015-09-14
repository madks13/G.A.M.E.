using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using GAME.Shared.Models;
using GAME.Shared.Interfaces.Managers;

namespace GAME.Shared.ViewModels
{
    class ManagerRSS : IManagersRSS
    {
        public ManagerRSS()
        {            
        }

        private async Task<SyndicationFeed> GetFeed(string uri)
        {
            XmlReader r = XmlReader.Create(uri);
            SyndicationFeed feed = SyndicationFeed.Load(r);
            r.Close();
            return feed;
        }

        private List<FeedDTO> FeedToFeed(SyndicationFeed feed)
        {
            List<FeedDTO> lst = new List<FeedDTO>();
            foreach (var item in feed.Items)
            {
                FeedDTO f = new FeedDTO();
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
            return FeedToFeed(await GetFeed(source));
        }
    }
}
