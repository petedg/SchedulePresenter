using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class ClassesWeek
    {
        private serverDBEntities context;

        public ClassesWeek()
        {

        }

        public ClassesWeek(serverDBEntities context)
        {
            this.context = context;
        }        

        public List<ClassesWeek> GetClassesWeekList(Classes classes)
        {
            var classesWeekList = from classesWeek in context.ClassesWeek
                                  where classesWeek.Classes_ID == classes.ID
                                  select classesWeek;

            return classesWeekList.ToList();
        }        
    }
}
