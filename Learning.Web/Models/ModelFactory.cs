﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Learning.Data;
using Learning.Entity;
using System.Web.Http.Routing;
using System.Net.Http;

namespace Learning.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ILearningRepository _repo;
        public ModelFactory(HttpRequestMessage request, ILearningRepository repo)
        {
            _repo = repo;
            _UrlHelper = new UrlHelper(request);
        }

        #region Course
        public CourseModel Create(Course course)
        {
            return new CourseModel()
            {
                Url     =   _UrlHelper.Link("courses", new { id = course.Id}),
                Id      =   course.Id,
                Name    =   course.Name,
                Description =   course.Description,
                Duration    =   course.Duration,
                Subject     =   Create(course.CourseSubject),
                Tutor       =   Create(course.CourseTutor)
            };
        }

        public Course Parse(CourseModel model)
        {
            return new Course()
            {
                Name = model.Name,
                Description = model.Description,
                Duration = model.Duration,
                CourseTutor = _repo.GetTutor(model.Tutor.Id),
                CourseSubject = _repo.GetSubject(model.Subject.Id)
            };
        }
        #endregion

        #region Student
        public StudentModel Create(Student student)
        {
            return new StudentModel()
            {
                Url                 =   _UrlHelper.Link("students", new { userName = student.UserName}),
                Id                  =   student.Id,
                Email               =   student.Email,
                UserName            =   student.UserName,
                FirstName           =   student.FirstName,
                LastName            =   student.LastName,
                DateOfBirth         =   student.DateOfBirth,
                RegistrationDate    =   student.RegistrationDate.Value,
                EnrollmentsCount    =   student.Enrollments.Count(),
                Enrollments         =   student.Enrollments.Select(e => Create(e))
            };
        }

        public StudentBaseModel CreateSummary(Student student)
        {
            return new StudentBaseModel()
            {
                Url             =   _UrlHelper.Link("students", new { userName = student.UserName }),
                Id              =   student.Id,
                FirstName       =   student.FirstName,
                LastName        =   student.LastName,
                Gender          =   student.Gender,
                EnrollmentsCount =  student.Enrollments.Count
            };
        }
        #endregion

        public TutorModel Create(Tutor tutor)
        {
            return new TutorModel()
            {
                Id      =   tutor.Id,
                Email   =   tutor.Email,
                FirstName=  tutor.FirstName,
                LastName =  tutor.LastName,
                Gender  =   tutor.Gender,
                UserName=   tutor.UserName 
            };
        }

        public SubjectModel Create(Subject subject)
        {
            return new SubjectModel()
            {
                Id      =   subject.Id,
                Name    =   subject.Name
            };
        }

        public EnrollmentModel Create(Enrollment enrollment)
        {
            return new EnrollmentModel()
            {
                Course = Create(enrollment.Course),
                EnrollmentDate = enrollment.EnrollmentDate
            };
        }
    }
}