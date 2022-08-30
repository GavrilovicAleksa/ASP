using Application.DataTransfer.Log;
using Application.Queries;
using Application.Queries.Log;
using Application.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries.Log
{
    public class EfGetLogQuery : ISearchLogsQuery
    {
        private readonly Context _context;

        public EfGetLogQuery(Context context)
        {
            this._context = context;
        }

        public int Id => 12387;

        public string Name => "Log Search";

        public PagedResponse<GetLogDto> Execute(LogSearch search)
        {
            var query = _context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.UseCase) || !string.IsNullOrWhiteSpace(search.UseCase))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCase.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.DateFrom))
            {
                query = query.Where(x => x.Date > DateTime.Parse(search.DateFrom));
            }

            if (!string.IsNullOrEmpty(search.DateTo))
            {
                query = query.Where(x => x.Date < DateTime.Parse(search.DateTo));
            } 

            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<GetLogDto>
            {
                Page = search.Page,
                PerPage = search.PerPage,
                Count = query.Count(),
                Data = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetLogDto
                {
                    Id = x.Id,
                    Date = x.Date.ToString(),
                    UseCaseName = x.UseCaseName 
                }).ToList()
            };

            return reponse;
        }
    }
}
