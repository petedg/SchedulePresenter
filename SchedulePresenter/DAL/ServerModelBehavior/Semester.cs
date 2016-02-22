using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Semester
    {
        private serverDBEntities context;
        private Subgroup subgroupBehavior;


        public Semester(serverDBEntities context)
        {
            this.context = context;
            this.subgroupBehavior = new Subgroup(context);
        }

        public List<Semester> GetList()
        {
            var semesters = from semester in context.Semester
                            //where semester.END_DATE > DateTime.Now
                            select semester;

            return semesters.ToList();
        }

        public Semester GetActiveSemester()
        {
            var semesters = from semester in context.Semester
                            where semester.IS_ACTIVE == true
                            select semester;

            return semesters.FirstOrDefault();
        }        
    }
}
