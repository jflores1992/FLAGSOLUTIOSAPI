﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FLAGSOLUTIOSAPI.Models;

public partial class Perfil
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; }

    public DateTime Creacion { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}