using Learning.Data;
using Learning.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Learning.Web.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        public List<Course> Get()
        {
            ILearningRepository repo = new LearningRepository(new LearningContext());

            return repo.GetAllCourses().ToList();
        }

        public Course Get(int id)
        {
            ILearningRepository repo = new LearningRepository(new LearningContext());

            return repo.GetCourse(id);
        }
    }
}
