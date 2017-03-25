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

namespace servicedesk.Services.Tickets.Controllers
{
    public class BaseController<T, TDto> : Controller
        where T : class, IIdentifiable, new()
        where TDto : class, new()
    {
        protected readonly IBaseService service;
        protected readonly ILoggerFactory loggerFactory;

        public BaseController(IBaseService service, ILoggerFactory loggerFactory)
        {
            this.service = service;
            this.loggerFactory = loggerFactory;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(GetAll query)
        {
            var all = service.Query<T>(r => true);
            var count = all.Count();

            var page = query.Page <= 0 ? 1 : query.Page; 
            var resultsPerPage = query.Results <= 0 ? 100 : query.Results;
            var skip = (page - 1) * resultsPerPage;

            var data = await all.Skip(skip).Take(resultsPerPage).ToListAsync();
            

            Response.Headers.Add("X-Total-Count", count.ToString());

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);
            return Ok(query);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]T create)
        {
            await service.CreateAsync(create);
            return Created(create.Id.ToString(), create);
        }

        [Route("{id}")]
        [HttpPut]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody]T update)
        {
            await service.UpdateAsync(update);
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

    public class BaseCrudController<T, TDto> : Controller
        where T : class, IDependently, IIdentifiable, new()
        where TDto : class, new()
    {
        protected readonly IBaseService service;
        protected readonly ILoggerFactory loggerFactory;

        public BaseCrudController(IBaseService service, ILoggerFactory loggerFactory)
        {
            this.service = service;
            this.loggerFactory = loggerFactory;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get(GetByReferenceId query)
        {
            var all = service.Query<T>(r => r.ReferenceId == query.ReferenceId);
            var count = all.Count();

            var page = query.Page <= 0 ? 100 : query.Page; 
            var resultsPerPage = query.Results <= 0 ? 100 : query.Results;
            var skip = (page - 1) * resultsPerPage;

            var data = await all.Skip(skip).Take(resultsPerPage).ToListAsync();

            Response.Headers.Add("X-Total-Count", count.ToString());

            return Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var query = await service.GetByIdAsync<T>(id);
            return Ok(query);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]T create)
        {
            await service.CreateAsync(create);
            return Created(create.Id.ToString(), create);
        }

        [Route("{id}")]
        [HttpPut]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody]T update)
        {
            await service.UpdateAsync(update);
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
        public virtual async Task<IActionResult> Get(GetByReferenceId query)
        {
            var all = service.Query<T>(r => r.ReferenceId == query.ReferenceId);
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
            return Created(_create.Id.ToString(), create);
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
}
