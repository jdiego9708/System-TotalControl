namespace SISTotalControl.AccesoDatos
{
    using SISTotalControl.Entidades.Modelos;
    using SISTotalControl.AccesoDatos.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;
    using SISTotalControl.Entidades.ModelosBindeo;
    using SISTotalControl.Entidades.Helpers;

    public class DProductos : IProductosDac
    {
        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_respuesta = e.Message;
        }
        #endregion

        #region VARIABLES
        string _mensaje_respuesta;
        public string Mensaje_respuesta
        {
            get
            {
                return _mensaje_respuesta;
            }

            set
            {
                _mensaje_respuesta = value;
            }
        }
        #endregion

        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DProductos(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR PRODUCTOS
        public Task<string> InsertarProducto(Productos producto)
        {
            string rpta = string.Empty;

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Productos_i",
                    CommandType = CommandType.StoredProcedure
                };

                #region VARIABLES
                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Id_tipo_producto = new()
                {
                    ParameterName = "@Id_tipo_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_tipo_producto,
                };
                SqlCmd.Parameters.Add(Id_tipo_producto);

                SqlParameter Nombre_producto = new()
                {
                    ParameterName = "@Nombre_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Nombre_producto.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Nombre_producto);

                SqlParameter Precio_producto = new()
                {
                    ParameterName = "@Precio_producto",
                    SqlDbType = SqlDbType.Decimal,
                    Value = producto.Precio_producto
                };
                SqlCmd.Parameters.Add(Precio_producto);

                SqlParameter Descripcion_producto = new()
                {
                    ParameterName = "@Descripcion_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = producto.Descripcion_producto.Trim()
                };
                SqlCmd.Parameters.Add(Descripcion_producto);

                SqlParameter Estado_producto = new()
                {
                    ParameterName = "@Estado_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Estado_producto.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Estado_producto);

                #endregion

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";

                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
                else
                {
                    producto.Id_producto = Convert.ToInt32(SqlCmd.Parameters["@Id_producto"].Value);
                }
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO EDITAR PRODUCTOS
        public Task<string> EditarProducto(Productos producto)
        {
            string rpta = string.Empty;

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Productos_u",
                    CommandType = CommandType.StoredProcedure
                };

                #region VARIABLES
                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_producto
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Id_tipo_producto = new()
                {
                    ParameterName = "@Id_tipo_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_tipo_producto,
                };
                SqlCmd.Parameters.Add(Id_tipo_producto);

                SqlParameter Nombre_producto = new()
                {
                    ParameterName = "@Nombre_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Nombre_producto.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Nombre_producto);

                SqlParameter Precio_producto = new()
                {
                    ParameterName = "@Precio_producto",
                    SqlDbType = SqlDbType.Decimal,
                    Value = producto.Precio_producto
                };
                SqlCmd.Parameters.Add(Precio_producto);

                SqlParameter Descripcion_producto = new()
                {
                    ParameterName = "@Descripcion_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = producto.Descripcion_producto.Trim()
                };
                SqlCmd.Parameters.Add(Descripcion_producto);

                SqlParameter Estado_producto = new()
                {
                    ParameterName = "@Estado_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Estado_producto.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Estado_producto);

                #endregion

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";

                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO BUSCAR PRODUCTOS
        public Task<(DataTable dtProductos, string rpta)> BuscarProductos(string tipo_busqueda, string texto_busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Productos");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Productos_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                    DtResultado = null;               
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion
    }
}
