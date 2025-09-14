using ActividadEntregable1.Domain;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadEntregable1.Data.Utils
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool Delete(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_PRODUCTO", parameters);
            return rows == 1;
        }

        private DetalleFactura ReadDetail(DataRow row)
        {
            DetalleFactura detalle = new DetalleFactura();
            detalle.Articulo = new Articulo
            {
                Codigo = Convert.ToInt32(row["codigo"].ToString()),
                Nombre = row["n_producto"].ToString(),
                PrecioUnitario = Convert.ToInt32(row["stock"].ToString()),
                
            };
            detalle.PrecioUnitario = Convert.ToSingle(row["precio"].ToString());
            detalle.Cantidad = Convert.ToInt32(row["cantidad"].ToString());
            return detalle;
        }

        public List<Factura> GetAll()
        {
            List<Factura> lst = new List<Factura>();
            Factura? oFactura = null;
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSPQuery("SP_RECUPERAR_PRESUPUESTOS", null);
            foreach (DataRow row in t.Rows)
            {
                //leer la primer fila y tomar datos del maestro y primer detalle
                if (oFactura == null || oFactura.Id != Convert.ToUInt32(row["id"].ToString()))
                {

                    oFactura = new Factura();
                    oFactura.Cliente = row["cliente"].ToString();
                    oFactura.Fecha = Convert.ToDateTime(row["fecha"].ToString());
                    oFactura.Id = Convert.ToInt32(row["id"].ToString());
                    oFactura.AddDetail(ReadDetail(row));
                    lst.Add(oFactura);
                }
                else
                {
                    //mientras no cambia el Id del maestro, leer datos de detalles.
                    oFactura.AddDetail(ReadDetail(row));
                }
            }
            return lst;
        }

        public Factura GetById(int id)
        {
            Factura? oFactura = null;
            var helper = DataHelper.GetInstance();
            var parameter = new ParameterSQL("@id", id);
            var parameters = new List<ParameterSQL>();
            parameters.Add(parameter);

            var t = helper.ExecuteSPQuery("SP_RECUPERAR_PRESUPUESTO_POR_ID", parameters);
            foreach (DataRow row in t.Rows)
            {
                if (oFactura == null)
                {
                    oFactura = new Factura();
                    oFactura.Cliente = row["cliente"].ToString();
                    oFactura.Fecha = Convert.ToDateTime(row["fecha"].ToString());
                    oFactura.FormaPagoId = Convert.ToInt32(row["vigencia"].ToString());
                    oFactura.Id = Convert.ToInt32(row["id"].ToString());
                    oFactura.AddDetail(ReadDetail(row));
                }
                else
                {
                    oFactura.AddDetail(ReadDetail(row));
                }
            }
            return oFactura;
        }

        public bool Save(Factura oFactura)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_INSERTAR_MAESTRO", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                //parámetro de entrada:
                cmd.Parameters.AddWithValue("@cliente", oBudget.Client);
                cmd.Parameters.AddWithValue("@vigencia", oBudget.Expiration);
                //parámetro de salida:
                SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int FacturaId = (int)param.Value;

                int detalleId = 1;
                foreach (var detail in oBudget.GetDetails())
                {
                    var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@presupuesto", budgetId);
                    cmdDetail.Parameters.AddWithValue("@id_detalle", nroDetail);
                    cmdDetail.Parameters.AddWithValue("@producto", detail.Product.Codigo);
                    cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);
                    cmdDetail.Parameters.AddWithValue("@precio", detail.Price);
                    cmdDetail.ExecuteNonQuery();
                    detalleId++;
                }

                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
    }
    }
}
