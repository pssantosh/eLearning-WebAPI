Relations
==========
Each "Course" has a "Subject".
Each "Tutor" can tech multiple "Courses".
Each "Student" can enroll in multiple "Courses". So we’ll have Many-to-Many table to persist the relation called "Enrollment".


Actions:
========
Listing all available subjects, and listing single subject by querying ID.
Listing all available courses including the sub-models (Subject, and Tutor).
Getting single course by querying ID including the sub-models(Subject, Tutor, and Enrolled Students)
Listing all available students including all sub-models (Enrolled in Courses, Course Subject, and Tutor)
Get summary of all available students.
List all enrolled students in specific course by querying course ID.
List all classes for a certain student by querying Username.
Get certain student by querying Username.
Authenticate students by validating Username and Password.
Enroll an authenticated student in a course.
CRUD operations for Students.
CRUD operations for Courses.