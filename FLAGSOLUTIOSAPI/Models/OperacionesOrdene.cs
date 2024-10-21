﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class OperacionesOrdene
{
    public int Id { get; set; }

    public int IdOrden { get; set; }

    public string TextoBreveReparacion { get; set; }

    public decimal DuracionOperacion { get; set; }

    public int IdPuestoTrabajo { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual OrdenesTrabajoEncabezado IdOrdenNavigation { get; set; }

    public virtual PuestosTrabajo IdPuestoTrabajoNavigation { get; set; }

    public virtual ICollection<NotificacionManoObra> NotificacionManoObras { get; set; } = new List<NotificacionManoObra>();
}