using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Learning.Data;
using Learning.Entity;
using Learning.Web.Models;

namespace Learning.Web.Controllers
{
    public class CoursesController 
                        : BaseApiController
    {
        public CoursesController(ILearningRepository repo)
            : base(repo)
        {

        }

        [HttpGet]
        public object Get(int page = 0, int pageSize = 10)
        {
            try
            {
                IQueryable<Course> query = TheRepository.GetAllCourses().OrderBy(c => c.CourseSubject.Id);
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
                var course = TheRepository.GetCourse(id);

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

        [HttpPost]
        public IHttpActionResult Post([FromBody]CourseModel courseModel)
        {
            try
            {
                var entity = TheModelFactory.Parse(courseModel);

                if (entity == null)
                {
                    return BadRequest("Could not read subject/tutor from body");
                }
                if (TheRepository.Insert(entity) && TheRepository.SaveAll())
                {
                    return Created<Course>(Request.RequestUri + entity.Id.ToString(), entity);
                }
                else
                {
                    return BadRequest("Could not save to the database.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CourseModel courseModel)
        {
            try
            {
                var updatedCourse = TheModelFactory.Parse(courseModel);

                if (updatedCourse == null)
                {
                    return BadRequest("Could not read subject/tutor from body");
                }

                var originalCourse = TheRepository.GetCourse(id, false);

                if (originalCourse == null || originalCourse.Id != id)
                {
                    return BadRequest("Course is not found");
                }
                else
                {
                    updatedCourse.Id = id;
                }
                if (TheRepository.Update(originalCourse, updatedCourse) && TheRepository.SaveAll())
                {
                    return Ok<Course>(updatedCourse);
                }
                else
                {
                    return BadRequest("error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var entity = TheRepository.GetCourse(id);

                if (entity == null)
                {
                    return BadRequest("Not Request");
                }

                if (entity.Enrollments.Count > 0)
                {
                    return BadRequest("Can not delete course, students has enrollments in course.");
                }

                if (TheRepository.DeleteCourse(id) && TheRepository.SaveAll())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
