namespace Locs.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NPoco;

    public class Attendee
    {
        public Guid Id { get; set; }

        public Guid LunchId { get; set; }

        public string Name { get; set; }

        public string Choice { get; set; }
    }
}
