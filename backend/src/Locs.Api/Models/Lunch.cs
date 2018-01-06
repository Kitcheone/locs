namespace Locs.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NPoco;

    public class Lunch
    {
        public Guid Id { get; set; }

        public string NavigationUrl { get; set; }

        public string Location { get; set; }

        public string MenuUrl { get; set; }

        public List<Attendee> Attendees { get; }
    }
}
