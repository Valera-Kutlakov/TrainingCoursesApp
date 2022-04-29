//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingCoursesApp.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.CourseEducatorTopic = new HashSet<CourseEducatorTopic>();
            this.CoursePeople = new HashSet<CoursePeople>();
        }
    
        public int CourseID { get; set; }
        public string Program { get; set; }
        public int IDOrganization { get; set; }
        public System.DateTime PlanStart { get; set; }
        public System.DateTime PlanEnd { get; set; }
        public Nullable<int> CountHours { get; set; }
        public Nullable<int> CountPeopleMax { get; set; }
        public Nullable<int> CountPeopleNow { get; set; }
        public int IDCourseForm { get; set; }
        public int IDStatus { get; set; }
        public int IDQualification { get; set; }
        public string Description { get; set; }
        public string Percon { get; set; }
    
        public virtual CourseForm CourseForm { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseEducatorTopic> CourseEducatorTopic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoursePeople> CoursePeople { get; set; }
    }
}