﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Department
    {
        private serverDBEntities context;
        private Location locationBehavior;

        public Department(serverDBEntities context)
        {
            this.context = context;
            locationBehavior = new Location(context);
        }

        public List<Department> GetList()
        {
            var departments = from department in context.Department
                              where department.ID != 1
                              select department;

            return departments.ToList();
        }

        public Department GetDepartmentById(int departmentID)
        {
            var departments = from department in context.Department
                              where department.ID == departmentID
                              select department;

            return departments.FirstOrDefault();
        }

        public List<Department> GetAssignedDepartmentsByUserId(int userId)
        {
            var departmentIds = from userDepartment in context.UserDepartment
                                where userDepartment.GlobalUser_ID == userId
                                select userDepartment.Department_ID;

            var departments = from department in context.Department
                              where departmentIds.Contains(department.ID)
                              select department;

            return departments.ToList();
        }

        public List<Department> GetAssignedDepartmentsByTeacherId(int teacherID)
        {
            var departmentIds = from departmentTeacher in context.DepartmentTeacher
                                where departmentTeacher.Teacher_ID == teacherID
                                select departmentTeacher.Department_ID;

            var departments = from department in context.Department
                              where departmentIds.Contains(department.ID)
                              select department;

            return departments.ToList();
        }                

        public List<Location> LocationsList { get; set; }
    }
}
