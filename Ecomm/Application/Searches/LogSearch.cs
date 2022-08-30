using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class LogSearch : PagedSearch
    {
        public int UserId { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; } 

        public string UseCase { get; set; }
    }
}
