﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FLAGSOLUTIOSAPI.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Codigo { get; set; }

    public string Nombre { get; set; }

    public DateTime Creacion { get; set; }

    public int PerfilId { get; set; }

    public string Icon { get; set; }

    public string Url { get; set; }

    public bool IsVisible { get; set; }

    public bool IsReporte { get; set; }

    public virtual Perfil Perfil { get; set; }

    public virtual ICollection<UsuariosMenu> UsuariosMenus { get; set; } = new List<UsuariosMenu>();
}