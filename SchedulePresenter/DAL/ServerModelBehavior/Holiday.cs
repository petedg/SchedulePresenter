using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Holiday
    {
        private serverDBEntities context;

        public Holiday()
        {

        }

        public Holiday(serverDBEntities context)
        {
            this.context = context;
        }

        public List<Holiday> GetHolidaysForSemester(Semester semester)
        {
            var holidays = from holiday in context.Holiday
                           where holiday.SEMESTER_ID == semester.ID
                           select holiday;

            return holidays.ToList();
        }                
    }
}
