namespace Locs.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NPoco;

    public class NavigationUrlService : INavigationUrlService
    {
        private readonly IDatabase db;

        public NavigationUrlService(IDatabase db)
        {
            this.db = db;
        }

        public string GenerateNavigationUrl()
        {
            string candidate = this.GenerateCandidate();
            using (this.db)
            {
                var urlsToCheck = this.db.Fetch<string>("select navigationUrl from locs.lunch");
                while (urlsToCheck.Contains(candidate))
                {
                    candidate = this.GenerateCandidate();
                }
            }

            return candidate;
        }

        private string GenerateCandidate()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            int length = 6;
            return new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
