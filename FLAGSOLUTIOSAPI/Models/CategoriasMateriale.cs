﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class CategoriasMateriale
{
    public int Id { get; set; }

    public string NombreCategoria { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Materiale> Materiales { get; set; } = new List<Materiale>();
}