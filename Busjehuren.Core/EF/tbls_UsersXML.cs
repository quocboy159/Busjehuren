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
    
    public partial class tbls_UsersXML
    {
        public int intID { get; set; }
        public int intXMLTypeID { get; set; }
        public int intUserID { get; set; }
        public string strUserXML { get; set; }
    
        public virtual tbls_Users tbls_Users { get; set; }
    }
}
