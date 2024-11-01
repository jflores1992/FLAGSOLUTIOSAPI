﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FLAGSOLUTIOSAPI.Models;

public partial class EstatusOrdene
{
    [Key]
    public int Id { get; set; }

    public string NombreEstatus { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public virtual ICollection<OrdenesTrabajoEncabezado> OrdenesTrabajoEncabezados { get; set; } = new List<OrdenesTrabajoEncabezado>();
}