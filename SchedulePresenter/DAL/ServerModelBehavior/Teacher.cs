﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Teacher
    {
        private serverDBEntities context;

        public Teacher(serverDBEntities context)
        {
            this.context = context;
        }

        public List<Teacher> GetTeachersForDepartment(Department department)
        {
            var teachers = from departmentTeacher in context.DepartmentTeacher
                           join teacher in context.Teacher on departmentTeacher.Teacher_ID equals teacher.ID
                           where departmentTeacher.Department_ID == department.ID
                          select teacher;

            return teachers.ToList();
        }

        public List<Teacher> GetList()
        {
            var teachers = from teacher in context.Teacher
                           where teacher.ID != 3
                           select teacher;

            return teachers.ToList();
        }

        public List<Teacher> GetListWithExternalOption()
        {
            var teachers = from teacher in context.Teacher
                           select teacher;

            return teachers.ToList();
        }

        public Teacher GetTeacherByID(int teacherID)
        {
            var teachers = from teacher in context.Teacher
                           where teacher.ID == teacherID
                           select teacher;

            return teachers.FirstOrDefault();
        }
    }
}
