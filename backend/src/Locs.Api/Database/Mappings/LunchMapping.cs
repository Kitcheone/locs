namespace Locs.Api.Database.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Locs.Api.Models;
    using NPoco.FluentMappings;

    public class LunchMapping : Map<Lunch>
    {
        public LunchMapping()
        {
            this.PrimaryKey(x => x.Id, false);
            this.TableName("Lunch");
            this.Columns(x =>
            {
                x.Many(y => y.Attendees).WithName("Id").Reference(y => y.LunchId);
            });
        }
    }
}
