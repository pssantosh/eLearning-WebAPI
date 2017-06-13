using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Learning.Data;
using Learning.Entity;
using Learning.Web.Models;

namespace Learning.Web.Controllers
{
    public class EnrollmentsController : BaseApiController
    {
        public EnrollmentsController(ILearningRepository repo)
                    : base(repo)
        {

        }

        [HttpGet]
        public IEnumerable<StudentBaseModel> Get(int courseId, int page = 0, int pageSize = 10)
        {
            IQueryable<Student> query;

            query = TheRepository.GetEnrolledStudentsInCourse(courseId).OrderBy(s => s.LastName);
            var totalCount = query.Count();

            System.Web.HttpContext.Current.Response.Headers.Add("X-InlineCount", totalCount.ToString());

            var results = query
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .Select(s => TheModelFactory.CreateSummary(s));

            return results;
        }

        [HttpPost]
        public IHttpActionResult Post(int courseId, [FromUri]string userName, [FromBody]Enrollment enrollment)
        {
            try
            {
                if (!TheRepository.CourseExists(courseId))
                {
                    return BadRequest("Could not find Course");
                }

                var student = TheRepository.GetStudent(userName);
                if (student == null)
                {
                    return BadRequest("Could not find Student");
                }

                var result = TheRepository.EnrollStudentInCourse(student.Id, courseId, enrollment);

                if (result == 1)
                {
                    return Created<Enrollment>("", enrollment);
                }
                else if (result == 2)
                {
                    return BadRequest("Student already enrolled in this course");
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
