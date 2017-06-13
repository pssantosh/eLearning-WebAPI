using Learning.Data;
using Learning.Entity;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Learning.Web.Controllers
{
    public class StudentsController : BaseApiController
    {
        public StudentsController(ILearningRepository repo)
            : base( repo)
        {

        }

        [HttpGet]
        public IHttpActionResult Get(int page = 0, int pageSize = 10)
        {
            try
            {
                IQueryable<Student> query = TheRepository.GetAllStudentsSummary().OrderBy(c => c.Id);

                var totalCnt    =   query.Count();
                var totalPages  =   (int)Math.Ceiling((double)totalCnt / pageSize);

                var urlHelper   =   new UrlHelper(Request);
                var prevLink    =   page > 0 ? urlHelper.Link("students", new { page = page - 1, pageSize = pageSize }) : "";
                var nextLink    =   page < totalPages - 1 ? urlHelper.Link("students", new { page = page + 1, pageSize = pageSize }) : "";

                var result = query
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .Select(s => TheModelFactory.Create(s));

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string userName)
        {
            try
            {
                Student student = TheRepository.GetStudent(userName);
                if (student != null)
                {
                    return Ok<Student>(student);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
