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
    
    public partial class Dictionary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dictionary()
        {
            this.DictionaryValue = new HashSet<DictionaryValue>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public System.DateTime DATE_CREATED { get; set; }
        public Nullable<System.DateTime> DATE_MODIFIED { get; set; }
        public int ID_CREATED { get; set; }
        public Nullable<int> ID_MODIFIED { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DictionaryValue> DictionaryValue { get; set; }
    }
}
