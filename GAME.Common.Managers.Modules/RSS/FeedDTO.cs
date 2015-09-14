using System;

namespace GAME.Common.Managers.RSS
{
    public class FeedDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTimeOffset PublishDate { get; set; }
 
        public string Faction { get; set; }

        public DateTimeOffset ExpireDate { get; set; }

        public Uri FluxUri { get; set; }

        public override string ToString()
        {
            return "FeedDTO [Id: " + Id + 
                ", Title: " + Title + 
                ", Author: " + Author + 
                ", Description: " + Description + 
                ", Publish Date: " + PublishDate + 
                ", Faction: " + Faction + 
                ", Expires: " + ExpireDate + 
                ", URI: " + FluxUri + "]\r\n";
        }
    }
}
