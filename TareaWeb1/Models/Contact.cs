//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TareaWeb1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public int ContactsId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
    }
}
