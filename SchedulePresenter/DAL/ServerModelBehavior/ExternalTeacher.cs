using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class ExternalTeacher
    {
        private serverDBEntities context;

        public ExternalTeacher(serverDBEntities context)
        {
            this.context = context;
        }

        public ExternalTeacher GetExternalTeacherById(int externalTeacherId)
        {
            var externalTeachers = from externalTeacher in context.ExternalTeacher
                                   where externalTeacher.ID == externalTeacherId
                                   select externalTeacher;

            return externalTeachers.FirstOrDefault();
        }

        public ExternalTeacher GetExternalTeacherByIdWithLocalSearch(int externalTeacherId)
        {
            var externalTeachersLocal = from externalTeacher in context.ExternalTeacher.Local
                                        where externalTeacher.ID == externalTeacherId
                                        select externalTeacher;

            ExternalTeacher t = externalTeachersLocal.FirstOrDefault();

            if (t != null)
            {
                return t;
            }

            var externalTeachers = from externalTeacher in context.ExternalTeacher
                                   where externalTeacher.ID == externalTeacherId
                                   select externalTeacher;

            return externalTeachers.FirstOrDefault();
        }
    }
}
