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
    
    public partial class tbls_DropdownsXmlTypes
    {
        public int intDropdownValueID { get; set; }
        public int intXmlTypeID { get; set; }
        public int intPrimaryKeyID { get; set; }
        public string strXMLNode { get; set; }
    
        public virtual tbls_Xmltypes tbls_Xmltypes { get; set; }
    }
}
