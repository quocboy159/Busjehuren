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
    
    public partial class tbls_Dropdowns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbls_Dropdowns()
        {
            this.tbls_Dropdownvalues = new HashSet<tbls_Dropdownvalues>();
        }
    
        public int intID { get; set; }
        public string strDropDownName { get; set; }
        public string strTableName { get; set; }
        public string strTableValueField { get; set; }
        public string strTableTextField { get; set; }
        public string strWhere { get; set; }
        public Nullable<int> blnEditable { get; set; }
        public Nullable<int> blnSystem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbls_Dropdownvalues> tbls_Dropdownvalues { get; set; }
    }
}
