using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class DepartmentTeacher
    {
        private serverDBEntities context;

        public DepartmentTeacher()
        {

        }

        public DepartmentTeacher(serverDBEntities context)
        {
            this.context = context;
        }        
    }
}
