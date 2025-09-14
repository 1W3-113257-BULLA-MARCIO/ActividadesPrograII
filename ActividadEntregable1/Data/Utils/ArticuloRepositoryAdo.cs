using ActividadEntregable1.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ActividadEntregable1.Data.Utils
{
    public class ArticuloRepositoryAdo : IArticuloRepository
    {
        private SqlConnection _connection;

        public ArticuloRepositoryAdo()
        {
            _connection = new SqlConnection(Properties.Resources.cnnString);
        }

        public bool Delete(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_PRODUCTO", parameters);
            return rows == 1;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSPQuery("SP_RECUPERAR_PRODUCTOS", null);
            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["AticuloId"]);
                string nombre = row["n_producto"].ToString();
                float PrecioUnitario = Convert.ToInt32(row["PrecioUnitario"].ToString());
                
                Articulo oArticulo = new Articulo()
                {
                    Codigo = id,
                    Nombre = nombre,
                    PrecioUnitario = PrecioUnitario
                };
                lst.Add(oArticulo);
            }
            return lst;
        }

        public Articulo GetById(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTO_POR_CODIGO", parameters);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int codigo = Convert.ToInt32(row["codigo"]);
                string nombre = row["nombre"].ToString();
                float PrecioUnitario = Convert.ToInt32(row["precioUnitario"]);

                Articulo oArticulo = new Articulo()
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    PrecioUnitario = PrecioUnitario
                };
                return oArticulo;

            }
            return null;
        }

        public bool Save(Articulo oArticulo)
        {
            bool result = true;
            string query = "SP_GUARDAR_PRODUCTO";

            try
            {
                if (oArticulo != null)
                {
                    _connection.Open();
                    var cmd = new SqlCommand(query, _connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", oArticulo.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", oArticulo.Nombre);
                    cmd.Parameters.AddWithValue("@precioUnitario", oArticulo.PrecioUnitario);
                    result = cmd.ExecuteNonQuery() == 1; //ExecuteNonQuery: cantidad de filas afectadas!
                }
            }
            catch (SqlException sqlEx)
            {
                result = false;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
    }
    }
}
