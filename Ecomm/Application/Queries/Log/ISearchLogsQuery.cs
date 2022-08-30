using Application.DataTransfer.Log;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Log
{
    public interface ISearchLogsQuery : IQuery<LogSearch, PagedResponse<GetLogDto>>
    {
    }
}
