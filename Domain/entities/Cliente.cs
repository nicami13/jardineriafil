using System;
using System.Collections.Generic;


namespace Domain.entities
{
    public class Cliente:BaseEntity
    {
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string? NombreContacto { get; set; }
        public string? ApellidoContacto { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string LineaDireccion1 { get; set; }
        public string? LineaDireccion2 { get; set; }
        public string Ciudad { get; set; }
        public string? Region { get; set; }
        public string? Pais { get; set; }
        public string? CodigoPostal { get; set; }
        public decimal? LimiteCredito { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }

    

    
}
