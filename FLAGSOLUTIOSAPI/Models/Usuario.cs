﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PrimerNombre { get; set; }

    public string PrimerApellido { get; set; }

    public bool? Activo { get; set; }

    public DateTime FechaInicioValidez { get; set; }

    public DateTime FechaFinValidez { get; set; }

    public bool? EstadoBorrado { get; set; }

    public int IdUsuarioCreador { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificador { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string Alias { get; set; }

    public int? PerfilId { get; set; }

    public int? SucursalId { get; set; }

    public string ContrasenaTemporal { get; set; }

    public virtual ICollection<Equipo> EquipoIdusuariocreadorNavigations { get; set; } = new List<Equipo>();

    public virtual ICollection<Equipo> EquipoIdusuariomodificadorNavigations { get; set; } = new List<Equipo>();

    public virtual Perfil Perfil { get; set; }

    public virtual Sucursale Sucursal { get; set; }

    public virtual ICollection<UsuariosMenu> UsuariosMenus { get; set; } = new List<UsuariosMenu>();
}