using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Week
    {
        private serverDBEntities context;

        public Week(serverDBEntities context)
        {
            this.context = context;
        }

        public List<Week> GetList()
        {
            var weeks = from week in context.Week
                        select week;

            return weeks.ToList();
        }

        public List<Week> GetListForSemester(Semester semester)
        {
            var weeks = from week in context.Week
                        where week.SEMESTER_ID == semester.ID
                        select week;

            return weeks.ToList();
        }

        public List<int> getDistinctSemesterIDs()
        {
            var weeks = from week in context.Week
                        select week.SEMESTER_ID;

            return weeks.Distinct().ToList();
        }        

        private DateTime NextSpecificDayOfWeek(DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }

        public Week GetCurrentWeek()
        {
            foreach (Week week in GetListForSemester(new Semester(context).GetActiveSemester()))
            {
                if (week.START_DATE <= DateTime.Now && week.END_DATE >= DateTime.Now)
                {
                    return week;
                }
            }

            return null;
        }
    }
}
