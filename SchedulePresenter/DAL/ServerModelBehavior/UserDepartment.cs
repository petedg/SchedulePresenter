using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class UserDepartment
    {
        private serverDBEntities context;

        public UserDepartment()
        {

        }

        public UserDepartment(serverDBEntities context)
        {
            this.context = context;
        }        
    }
}
