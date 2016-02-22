//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchedulePresenter.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GlobalUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GlobalUser()
        {
            this.UserDepartment = new HashSet<UserDepartment>();
            this.UserRole = new HashSet<UserRole>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<System.DateTime> PASSWORD_EXPIRATION { get; set; }
        public bool PASSWORD_TEMPORARY { get; set; }
        public int USER_TYPE_DV_ID { get; set; }
        public System.DateTime DATE_CREATED { get; set; }
        public Nullable<System.DateTime> DATE_MODIFIED { get; set; }
        public Nullable<int> ID_CREATED { get; set; }
        public Nullable<int> ID_MODIFIED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDepartment> UserDepartment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}