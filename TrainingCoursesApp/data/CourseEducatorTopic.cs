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
    
    public partial class CourseEducatorTopic
    {
        public int IDCourse { get; set; }
        public int IDEducator { get; set; }
        public int IDTopic { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Educator Educator { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
