//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaRotatoria.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Autorizacao = new HashSet<Autorizacao>();
            this.Autorizacao1 = new HashSet<Autorizacao>();
        }
    
        public int UsuCod { get; set; }
        public string UsuNom { get; set; }
        public string UsuMai { get; set; }
        public string UsuEmp { get; set; }
        public string UsuPer { get; set; }
        public string UsuSit { get; set; }
        public string UsuSen { get; set; }
    
        public virtual ICollection<Autorizacao> Autorizacao { get; set; }
        public virtual ICollection<Autorizacao> Autorizacao1 { get; set; }
    }
}
