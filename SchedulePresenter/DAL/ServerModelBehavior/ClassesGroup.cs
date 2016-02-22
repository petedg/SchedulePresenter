using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class ClassesGroup
    {
        private serverDBEntities context;

        public ClassesGroup()
        {

        }

        public ClassesGroup(serverDBEntities context)
        {
            this.context = context;
        }
    }
}
