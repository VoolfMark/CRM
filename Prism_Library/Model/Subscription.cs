//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prism_Library.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscription
    {
        public int IDSubscription { get; set; }
        public int IDReader { get; set; }
        public System.DateTime DateOfIssueOfTheBook { get; set; }
        public System.DateTime SuggestedReturnDate { get; set; }
        public System.DateTime RealReturnDate { get; set; }
        public int IDAnInstance { get; set; }
    
        public virtual Reader Reader { get; set; }
        public virtual AnInstance AnInstance { get; set; }
    }
}
