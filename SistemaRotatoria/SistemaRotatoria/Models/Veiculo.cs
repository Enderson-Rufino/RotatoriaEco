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
    
    public partial class Veiculo
    {
        public Veiculo()
        {
            this.Autorizacao = new HashSet<Autorizacao>();
        }
    
        public string VeiPla { get; set; }
        public string VeiMar { get; set; }
        public string VeiMod { get; set; }
        public string VeiCor { get; set; }
        public Nullable<short> VeiEix { get; set; }
        public string VeiSit { get; set; }
    
        public virtual ICollection<Autorizacao> Autorizacao { get; set; }
    }
}
