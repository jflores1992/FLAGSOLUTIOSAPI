using FLAGSOLUTIOSAPI.Clases;
using FLAGSOLUTIOSAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FLAGSOLUTIOSAPI.DataAcces
{
    public class DataRepository
    {
        private readonly Conexion _conexion;
        public DataRepository(Conexion conexion) { 
        _conexion = conexion;
        }  
        public async Task<List<Menu>> DameListaMenusByUsuarioAll(int Usuario,int perfilid)
        {
            List<Menu> list = new List<Menu>();
            try
            {

                using (SqlConnection connection = new SqlConnection(_conexion.MantenimientoDbConection()))

                {
                    await connection.OpenAsync();
                    string query = "[dbo].[msSP_Select_MenusByUsuario_PerfilId]";

                    using (SqlCommand command = new SqlCommand())
                    
                     {
                        command.CommandText = query;

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@UserId", Usuario);
                        command.Parameters.AddWithValue("@PerfilId", perfilid);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read() && reader.HasRows)
                            {

                                Menu menu = new Menu(){
                                    Id =(int) reader["Id"],
                                    Codigo = (string)reader["Codigo"],
                                    Nombre = (string)reader["Nombre"],
                                    Creacion = (DateTime)reader["Creacion"],
                                    PerfilId = (int)reader["PerfilId"],
                                    Icon = (string)reader["Icon"],
                                    Url= (string)reader["Url"],
                                    IsVisible = (bool)reader["IsVisible"],
                                    IsReporte = (bool)reader["IsReporte"]

                                };
                                list.Add(menu);
                               
                            }
                        }
                    }
                } 
                    return list;
            }
            catch (Exception ex)
            {

                throw new Exception("An error ocurred while retrieving data", ex);
            }


        }
        public async Task<Usuario> ObtenerDatosUsuario(LoginModel login)
        {
            Usuario usuario=new Usuario();
            Sucursale sucursale=new Sucursale();
            Perfil perfil=new Perfil();
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion.MantenimientoDbConection()))
                {
                    await connection.OpenAsync();
                    string query = "[dbo].[msSP_Select_ValidaUsuarioLogin]";

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@username", login.Username);
                        command.Parameters.AddWithValue("@Password", login.Password);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read() && reader.HasRows)
                            {
                                sucursale.EmpresaId= (int)reader["EmpresaId"];
                                perfil.Nombre = reader["Perfil"] as string;
                                perfil.Id = (int)reader["PerfilId"];
                                usuario.Id = (int)reader["Id"];
                                usuario.Email = reader["Email"] as string;
                                usuario.Password = reader["Password"] as string;
                                usuario.PrimerNombre = reader["PrimerNombre"] as string;
                                usuario.PrimerApellido = reader["PrimerApellido"] as string;
                                usuario.Activo = (bool)reader["Activo"];
                                usuario.FechaInicioValidez =(DateTime) reader["FechaInicioValidez"] ;
                                usuario.FechaFinValidez = (DateTime)reader["FechaFinValidez"];
                                usuario.EstadoBorrado = (bool)reader["EstadoBorrado"];
                                usuario.IdUsuarioCreador = (int)reader["IdUsuarioCreador"];
                                usuario.FechaCreacion = (DateTime)reader["FechaCreacion"];
                                usuario.Alias = reader["Alias"] as string;
                                usuario.SucursalId = (int)reader["SucursalId"];
                                usuario.ContrasenaTemporal = reader["ContrasenaTemporal"] as string;
                                usuario.Sucursal = sucursale;
                                
                                usuario.Perfil = perfil;


                            }
                            await reader.CloseAsync();
                        }
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception("An error ocurred while retrieving data",ex);
            }                
        }
        public async Task<Usuario> ObtenerIdUsuario(string Username)
        {
            Usuario usuario = new Usuario();
            Sucursale sucursale = new Sucursale();

            Perfil perfil = new Perfil();
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion.MantenimientoDbConection()))
                {
                    await connection.OpenAsync();
                    string query = "[dbo].[msSP_Select_Usuario_ByUserName]";

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.AddWithValue("@username", Username);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read() && reader.HasRows)
                            {
                                sucursale.EmpresaId = (int)reader["EmpresaId"];

                                perfil.Nombre = reader["Perfil"] as string;
                                perfil.Id = (int)reader["PerfilId"];
                                usuario.Id = (int)reader["Id"];
                                usuario.Email = reader["Email"] as string;
                                usuario.Password = reader["Password"] as string;
                                usuario.PrimerNombre = reader["PrimerNombre"] as string;
                                usuario.PrimerApellido = reader["PrimerApellido"] as string;
                                usuario.Activo = (bool)reader["Activo"];
                                usuario.FechaInicioValidez = (DateTime)reader["FechaInicioValidez"];
                                usuario.FechaFinValidez = (DateTime)reader["FechaFinValidez"];
                                usuario.EstadoBorrado = (bool)reader["EstadoBorrado"];
                                usuario.IdUsuarioCreador = (int)reader["IdUsuarioCreador"];
                                usuario.FechaCreacion = (DateTime)reader["FechaCreacion"];
                                usuario.Alias = reader["Alias"] as string;
                                usuario.SucursalId = (int)reader["SucursalId"];
                                usuario.ContrasenaTemporal = reader["ContrasenaTemporal"] as string;
                                usuario.Sucursal = sucursale;
                                usuario.Perfil = perfil;


                            }
                            await reader.CloseAsync();
                        }
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {

                throw new Exception("An error ocurred while retrieving data", ex);
            }
        }
    }
}
