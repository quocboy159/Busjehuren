//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Busjehuren.Core.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbls_Dropdownvalues
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbls_Dropdownvalues()
        {
            this.tbls_Dropdownlanguagevalues = new HashSet<tbls_Dropdownlanguagevalues>();
        }
    
        public int intID { get; set; }
        public int intDropDownID { get; set; }
        public string strValue { get; set; }
        public string strDescription { get; set; }
        public Nullable<int> intOrderID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbls_Dropdownlanguagevalues> tbls_Dropdownlanguagevalues { get; set; }
        public virtual tbls_Dropdowns tbls_Dropdowns { get; set; }
    }
}
