﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FLAGSOLUTIOSAPI.Models;

public partial class PuestosTrabajo
{
    [Key]
    public int Id { get; set; }

    public string NombrePuesto { get; set; }

    public string Denominacion { get; set; }

    public decimal ValorPuestoTrabajoHora { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<OperacionesOrdene> OperacionesOrdenes { get; set; } = new List<OperacionesOrdene>();
}