namespace Locs.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Locs.Api.Models;
    using Locs.Api.Services;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using NPoco;

    [EnableCors("AllowAll")]
    public class LunchesController : Controller
    {
        private readonly IDatabase db;
        private readonly INavigationUrlService navigationUrlService;

        public LunchesController(IDatabase db, INavigationUrlService navigationUrlService)
        {
            this.db = db;
            this.navigationUrlService = navigationUrlService;
        }

        [HttpGet]
        [Route("api/lunches")]
        public IEnumerable<Lunch> Get()
        {
            using (this.db)
            {
                return this.db.Query<Lunch>().IncludeMany(p => p.Attendees).ToList();
            }
        }

        [HttpGet]
        [Route("api/lunches/{navigationUrl}")]
        public Lunch Get(string navigationUrl)
        {
            using (this.db)
            {
                return this.db.Query<Lunch>().IncludeMany(p => p.Attendees).Where(p => p.NavigationUrl == navigationUrl).SingleOrDefault();
            }
        }

        [HttpPost]
        [Route("api/lunches")]
        public string Post([FromBody]Lunch lunch)
        {
            lunch.Id = Guid.NewGuid();
            lunch.NavigationUrl = this.navigationUrlService.GenerateNavigationUrl();

            using (this.db)
            {
                this.db.Insert(lunch);
            }

            return lunch.NavigationUrl;
        }
    }
}
