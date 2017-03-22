using System;
using System.Linq;
using Collectively.Common.Queries;
using Collectively.Common.Types;

namespace servicedesk.Services.Tickets.Extensions
{
    public static class PaginationExtensions
    {
        public static PagedResult<T> Paginate<T>(this IQueryable<T> values, IPagedQuery query)
            => values.Paginate(query.Page, query.Results);

        public static PagedResult<T> Paginate<T>(this IQueryable<T> values,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
                page = 1;

            if (resultsPerPage <= 0)
                resultsPerPage = 10;

            var isEmpty = values.Any() == false;
            if (isEmpty)
                return PagedResult<T>.Empty;

            var totalResults = values.Count();
            var totalPages = (int) Math.Ceiling((decimal) totalResults/resultsPerPage);
            var data = values.Limit(page, resultsPerPage).ToList();

            return PagedResult<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection, IPagedQuery query)
            => collection.Limit(query.Page, query.Results);

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
                page = 1;

            if (resultsPerPage <= 0)
                resultsPerPage = 10;

            var skip = (page - 1)*resultsPerPage;
            var data = collection.Skip(skip)
                .Take(resultsPerPage);

            return data;
        }
    }
}