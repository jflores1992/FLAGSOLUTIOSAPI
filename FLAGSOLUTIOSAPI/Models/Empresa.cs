﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FLAGSOLUTIOSAPI.Models;

public partial class Empresa
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Correo { get; set; }

    public string Telefono { get; set; }

    public DateTime Creacion { get; set; }

    public string Rtn { get; set; }

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();
}