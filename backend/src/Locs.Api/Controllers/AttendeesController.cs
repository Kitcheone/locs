namespace Locs.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Locs.Api.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using NPoco;

    [EnableCors("AllowAll")]
    public class AttendeesController : Controller
    {
        private readonly IDatabase db;

        public AttendeesController(IDatabase db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("api/attendees/{attendeeId}")]
        public Attendee GetSingle(Guid attendeeId)
        {
            using (this.db)
            {
                return this.db.SingleById<Attendee>(attendeeId);
            }
        }

        [HttpPost]
        [Route("api/attendees")]
        public IActionResult Insert([FromBody]Attendee attendee)
        {
            attendee.Id = Guid.NewGuid();
            using (this.db)
            {
                this.db.Save(attendee);
            }

            return this.NoContent();
        }

        [HttpPut]
        [Route("api/attendees")]
        public IActionResult Update([FromBody]Attendee attendee)
        {
            using (this.db)
            {
                this.db.Update(attendee);
            }

            return this.NoContent();
        }

        [HttpDelete]
        [Route("api/attendees")]
        public IActionResult Delete([FromBody]Guid attendeeId)
        {
            using (this.db)
            {
                var existingAttendee = this.db.SingleById<Attendee>(attendeeId);
                this.db.Delete(existingAttendee);
            }

            return this.NoContent();
        }
    }
}
