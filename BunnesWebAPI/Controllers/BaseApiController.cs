using Core.Entities;
using Core.Interfaces;
using HelloWorldWebAPI.RequestHelpers;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> _repo, ISpecification<T> spec,
            int pageIndex, int pageSize) where T : BaseEntity
        {
            var items = await _repo.ListAsync(spec);
            var count = await _repo.CountAsync(spec);

            var pagination = new Pagination<T>(pageIndex,pageSize,count,items);

            return Ok(pagination);
        }
    }
}
