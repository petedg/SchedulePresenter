using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Major
    {
        private serverDBEntities context;
        private Subgroup subgroupBehavior;

        public Major(serverDBEntities context)
        {
            this.context = context;
            subgroupBehavior = new Subgroup(context);
        }

        public List<Major> GetMajorsForDepartment(Department department)
        {
            var majors = from major in context.Major
                         where major.DEPARTMENT_ID == department.ID
                         select major;

            return majors.ToList();
        } 

        public List<object> SubgroupsList { get; set; }
        public List<dynamic> CompositeSubgroupsList { get; set; }
    }
}
