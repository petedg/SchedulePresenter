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
    
    public partial class ClassesWeek
    {
        public int ID { get; set; }
        public int Classes_ID { get; set; }
        public int Week_ID { get; set; }
    
        public virtual Classes Classes { get; set; }
        public virtual Week Week { get; set; }
    }
}
