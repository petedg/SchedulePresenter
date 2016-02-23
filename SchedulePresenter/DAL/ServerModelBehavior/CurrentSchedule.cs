using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulePresenter.DAL
{
    public partial class CurrentSchedule
    {
        private serverDBEntities context;

        public CurrentSchedule()
        {

        }

        public CurrentSchedule(serverDBEntities context)
        {
            this.context = context;
        }

        public CurrentSchedule GetCurrentSchedule(int groupID, int weekID)
        {
            var schedules = from schedule in context.CurrentSchedule
                            where schedule.GROUP_ID == groupID && schedule.WEEK_ID == weekID
                            select schedule;

            return schedules.FirstOrDefault();
        }

        public bool HasCurrentSchedule(int groupID)
        {
            var schedules = from schedule in context.CurrentSchedule
                            where schedule.GROUP_ID == groupID
                            select schedule;

            return schedules.ToList().Count > 0 ? true : false;
        }
    }
}