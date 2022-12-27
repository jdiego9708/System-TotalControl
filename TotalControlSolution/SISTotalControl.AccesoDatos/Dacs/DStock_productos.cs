namespace SISTotalControl.AccesoDatos
{
    using SISTotalControl.Entidades.Modelos;
    using SISTotalControl.AccesoDatos.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class DStock_productos : IStock_productosDac
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
        public DStock_productos(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR STOCK
        public Task<string> InsertarStock(Stock_productos stock)
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
                    CommandText = "sp_Stock_productos_i",
                    CommandType = CommandType.StoredProcedure
                };

                #region VARIABLES
                SqlParameter Id_stock = new()
                {
                    ParameterName = "@Id_stock",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_stock);

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = stock.Id_producto,
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Fecha_stock = new()
                {
                    ParameterName = "@Fecha_stock",
                    SqlDbType = SqlDbType.Date,
                    Value = stock.Fecha_stock
                };
                SqlCmd.Parameters.Add(Fecha_stock);

                SqlParameter Hora_stock = new()
                {
                    ParameterName = "@Hora_stock",
                    SqlDbType = SqlDbType.Time,
                    Value = stock.Hora_stock
                };
                SqlCmd.Parameters.Add(Hora_stock);

                SqlParameter Cantidad_stock = new()
                {
                    ParameterName = "@Cantidad_stock",
                    SqlDbType = SqlDbType.Decimal,
                    Value = stock.Cantidad_stock
                };
                SqlCmd.Parameters.Add(Cantidad_stock);

                SqlParameter Observaciones_producto = new()
                {
                    ParameterName = "@Observaciones_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = stock.Observaciones_producto.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Observaciones_producto);
          
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
                    stock.Id_stock = Convert.ToInt32(SqlCmd.Parameters["@Id_stock"].Value);
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
    }
}
