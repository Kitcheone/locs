namespace Locs.Api.Database.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Locs.Api.Models;
    using NPoco;
    using NPoco.FluentMappings;

    public class AttendeeMapping : Map<Attendee>
    {
        public AttendeeMapping()
        {
            this.PrimaryKey(x => x.Id, false);
            this.TableName("Attendee");
        }
    }
}
