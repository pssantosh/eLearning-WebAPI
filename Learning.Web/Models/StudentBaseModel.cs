﻿namespace Learning.Web.Models
{
    public class StudentBaseModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Entity.Enums.Gender Gender { get; set; }
        public int EnrollmentsCount { get; set; }
    }
}