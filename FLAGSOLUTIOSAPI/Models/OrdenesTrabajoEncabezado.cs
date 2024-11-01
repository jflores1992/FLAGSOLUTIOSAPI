﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class OrdenesTrabajoEncabezado
{
    public int Id { get; set; }

    public int IdClaseOrden { get; set; }

    public int IdPrioridadOrden { get; set; }

    public int IdEquipo { get; set; }

    public int IdCentroEmplazamiento { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdestatusOrden { get; set; }

    public string DescripcionGeneralOrden { get; set; }

    public DateTime FechaInicioOrden { get; set; }

    public TimeSpan HoraInicioOrden { get; set; }

    public DateTime? FechaFinOrden { get; set; }

    public TimeSpan? HoraFinOrden { get; set; }

    public bool? ServicioExterno { get; set; }

    public bool? Activo { get; set; }

    public bool? EstadoBorrado { get; set; }

    public virtual ICollection<ComponentesOrdene> ComponentesOrdenes { get; set; } = new List<ComponentesOrdene>();

    public virtual ICollection<HistorialMantenimientoCreado> HistorialMantenimientoCreados { get; set; } = new List<HistorialMantenimientoCreado>();

    public virtual ClasesdeOrden IdClaseOrdenNavigation { get; set; }

    public virtual Equipo IdEquipoNavigation { get; set; }

    public virtual PrioridadesMantenimientos IdPrioridadOrdenNavigation { get; set; }

    public virtual EstatusOrdene IdestatusOrdenNavigation { get; set; }

    public virtual ICollection<NotificacionManoObra> NotificacionManoObras { get; set; } = new List<NotificacionManoObra>();

    public virtual ICollection<OperacionesOrdene> OperacionesOrdenes { get; set; } = new List<OperacionesOrdene>();
}