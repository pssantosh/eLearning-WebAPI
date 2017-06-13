using Learning.Data;
using Learning.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Learning.Web.Controllers
{
    public class CoursesController : ApiController
    {
        ILearningRepository repo = new LearningRepository(new LearningContext());

        [HttpGet]
        public object Get(int page = 0, int pageSize = 10)
        {
            try
            {
                IQueryable<Course> query = repo.GetAllCourses().OrderBy(c => c.CourseSubject.Id);
                var totalCnt = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalCnt / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 0 ? urlHelper.Link("courses", new { page = page - 1, pageSize = pageSize}) : "";
                var nextLink = page < totalPages - 1 ? urlHelper.Link("courses", new { page = page + 1, pageSize = pageSize}) : "";

                var result = query
                                .Skip(pageSize * page)
                                .Take(pageSize)
                                .ToList();

                return new
                {
                    TotalCount      =   totalCnt,
                    TotalPages      =   totalPages,
                    PrevPageLink    =   prevLink,
                    NextPageLink    =   nextLink,
                    Result          =   result
                };

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var course = repo.GetCourse(id);

                if (course == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok<Course>(course);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
