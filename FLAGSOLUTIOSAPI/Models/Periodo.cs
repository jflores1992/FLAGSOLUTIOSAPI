﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class Periodo
{
    public int Id { get; set; }

    public string NombrePeriodo { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    //public virtual ICollection<PresupuestoEquipo> PresupuestoEquipos { get; set; } = new List<PresupuestoEquipo>();
}