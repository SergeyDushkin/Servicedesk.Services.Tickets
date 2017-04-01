using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using servicedesk.Common.Domain;
using servicedesk.Common.Services;
using servicedesk.Common.Queries;
using Microsoft.EntityFrameworkCore;
using servicedesk.Services.Tickets.Queries;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace servicedesk.Services.Tickets.Controllers
{
    public class BaseController<T, TDto, TCreate, TUpdate> : Controller
        where T : class, IIdentifiable, new()
        where TDto : class, new()
        where TCreate : class, new()
        where TUpdate : class, new()
    {
        protected readonly IBaseService service;
        protected readonly ILoggerFactory loggerFactory;
        protected readonly IMapper mapper;

        public BaseController(IBaseService service, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.service = service;
            this.loggerFactory = loggerFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(Queries.GetAll query)
        {
            var all = service.Query<T>(r => true, GenericExtension.StringToProperties<T>(query.Include).ToArray());
            return await PagedResult(all, query);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);
            return Ok(query);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TCreate create)
        {
            var _create = mapper.Map<T>(create);
            await service.CreateAsync(_create);
            
            //return CreatedAtAction("GetById", new { id = _create.Id });
            //var link = Url.Link("Get", new { id = _create.Id });
            var link = _create.Id.ToString();
            return Created(link, _create);
        }

        [Route("{id}")]
        [HttpPut]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody]TUpdate update)
        {
            var _update = mapper.Map<T>(update);
            await service.UpdateAsync(_update);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);

            if (query == null)
            {
                return NotFound();
            }

            await service.DeleteAsync(query);

            return Ok();
        }

        [NonAction]
        public async Task<ActionResult> PagedResult<TType>(IQueryable<TType> data, IPagedQuery query)
        {
            var count = data.Count();

            var page = query.Page <= 0 ? 1 : query.Page; 
            var resultsPerPage = query.Results <= 0 ? 100 : query.Results;
            var skip = (page - 1) * resultsPerPage;

            var result = await data.Skip(skip).Take(resultsPerPage).ToListAsync();

            Response.Headers.Add("X-Total-Count", count.ToString());
            
            return Ok(result);
        }
    }

    public class BaseCrudController<T, TDto, TCreate, TUpdate> : Controller
        where T : class, IDependently, IIdentifiable, new()
        where TDto : class, new()
        where TCreate : class, new()
        where TUpdate : class, new()
    {
        protected readonly IBaseService service;
        protected readonly ILoggerFactory loggerFactory;
        protected readonly IMapper mapper;

        public BaseCrudController(IBaseService service, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this.service = service;
            this.loggerFactory = loggerFactory;
            this.mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(Queries.GetByReferenceId query)
        {
            var all = service.Query<T>(r => r.ReferenceId == query.ReferenceId, GenericExtension.StringToProperties<T>(query.Include).ToArray());
            var count = all.Count();

            var page = query.Page <= 0 ? 1 : query.Page; 
            var resultsPerPage = query.Results <= 0 ? 100 : query.Results;
            var skip = (page - 1) * resultsPerPage;

            var data = await all.Skip(skip).Take(resultsPerPage).ToListAsync();

            Response.Headers.Add("X-Total-Count", count.ToString());

            var result = data.Select(r => mapper.Map<TDto>(r));

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);
            return Ok(query);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TCreate create)
        {
            var _create = mapper.Map<T>(create);
            await service.CreateAsync(_create);

            var link = _create.Id.ToString();
            return Created(link, _create);
        }

        [Route("{id}")]
        [HttpPut]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody]TUpdate update)
        {
            var _update = mapper.Map<T>(update);
            await service.UpdateAsync(_update);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);

            if (query == null)
            {
                return NotFound();
            }

            await service.DeleteAsync(query);

            return Ok();
        }
    }

    static class GenericExtension
    {
        public static IEnumerable<Expression<Func<T, object>>> StringToProperties<T>(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                yield break;

            var array = str.ToLower().Split(',');

            var props = GenericExtension.IterateProps(typeof(T)).ToArray();

            foreach (var a in array)
            {
                var p = props.Where(r => r.ToLower() == a).SingleOrDefault();

                if (p != null)
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(T), "i");
                    MemberExpression property = Expression.Property(parameter, p);

                    var delegateType = typeof(Func<T, object>);
                    dynamic lambda = Expression.Lambda(delegateType, property, parameter);

                    yield return lambda;
                }
            }
        }

        public static IEnumerable<string> IterateProps(Type baseType)
        {
            return IteratePropsInner(baseType, baseType.Name);
        }

        private static IEnumerable<string> IteratePropsInner(Type baseType, string baseName)
        {
            var props = baseType.GetProperties();

            foreach (var property in props)
            {
                var name = property.Name;
                var type = property.PropertyType;

                if (type.FullName.StartsWith("System"))
                    continue;

                if (type.GetTypeInfo().IsClass)
                {
                    foreach (var info in IteratePropsInner(type, name))
                        yield return string.Format("{0}.{1}", baseName, info);

                    yield return property.Name;
                }
            }
        }
    }
}