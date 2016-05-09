using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Author: Jeciel 
Busca en la base de datos los códigos EPSG (European Petroleum Survey Group)
dependiendo de el concepto y el texto 
concepto puede ser una Ciudad, un UTM Zone tipo numerico o un srid que viene siendo el mismo EPSG
     */
namespace DAL
{
    public class ETEPSGDAL
    {
        private string cs;

        public ETEPSGDAL(string cs)
        {
            this.cs = cs;
        }

        public string getEPSG(string concepto, string texto)
        {
            string resultado = "";
            try
            {
                if (concepto != "" && texto != "" && this.cs != string.Empty)
                {
                    using (var conn = new Npgsql.NpgsqlConnection(this.cs))
                    {
                        conn.Open();
                        using (var comm = conn.CreateCommand())
                        {
                            comm.CommandText = crearComando(concepto);
                            //comm.CommandType = System.Data.CommandType.StoredProcedure;
                            comm.Parameters.Add(new NpgsqlParameter());
                            comm.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                            comm.Parameters[0].Value = texto;
                            comm.Parameters[0].ParameterName = "n";

                            using (NpgsqlDataReader resQuery = comm.ExecuteReader())
                                while (resQuery.Read())
                                    resultado += string.Format("{0}\t{1} \n", resQuery[0], resQuery[1]);
                        }
                        //tran.Commit();                  
                        conn.Close();
                    }
                }
                return resultado;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string crearComando(string tipo)
        {
            string respuesta = "";
            if (tipo != "")
                switch (tipo)
                {
                    case "UTM Zone":
                        respuesta = "SELECT srid, trim(substring(srtext, position('[' in srtext)+1, position(',' in srtext)- position('[' in srtext)-1)) FROM spatial_ref_sys WHERE srtext LIKE '%UTM zone '||@n||'_%'";
                        break;
                    case "SRID":
                        respuesta = "SELECT srid, trim(substring(srtext, position('[' in srtext)+1, position(',' in srtext)- position('[' in srtext)-1)) FROM spatial_ref_sys WHERE srid = @n";
                        break;
                    case "Ciudad":
                        respuesta =  "SELECT srid, trim(substring(srtext, position('[' in srtext)+1, position(',' in srtext)- position('[' in srtext)-1)) FROM spatial_ref_sys WHERE srtext LIKE '%'||@n||'_%'";
                        break;
                    default:
                        respuesta = "SELECT srid, trim(substring(srtext, position('[' in srtext)+1, position(',' in srtext)- position('[' in srtext)-1)) FROM spatial_ref_sys WHERE srtext LIKE '%'||@n||'_%'";
                        break;
                }
            return respuesta;
        }
    }
}
