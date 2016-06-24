using Entity;
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

        public IEPSGRepository getEPSG(string concepto, string texto)
        {
            IEPSGRepository resultado = new EPSGRepository();
            if (concepto != "" && texto != "" && this.cs != string.Empty)
            {
                using (var conn = new NpgsqlConnection(this.cs))
                {
                    conn.Open();
                    using (var comm = conn.CreateCommand())
                    {
                        comm.CommandText = crearComando(concepto, texto);
                        //comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Parameters.Add(new NpgsqlParameter());
                        comm.Parameters[0].NpgsqlDbType = NpgsqlDbType.Text;
                        comm.Parameters[0].Value = texto;
                        comm.Parameters[0].ParameterName = "n";

                        using (NpgsqlDataReader resQuery = comm.ExecuteReader())
                            while (resQuery.Read())
                                resultado.Add(new EPSGEntity { epsg = (Int32)resQuery[0], texto = resQuery[1].ToString() });
                    }
                    //tran.Commit();                  
                    conn.Close();
                }
            }
            return resultado;

        }

        private string crearComando(string tipo, string texto)
        {
            string first = "SELECT srid, trim(BOTH '\"' FROM trim(substring(srtext, position('[' in srtext)+1, position(',' in srtext)- position('[' in srtext)-1))) FROM spatial_ref_sys WHERE";
            string respuesta = "";
            if (tipo != "")
                switch (tipo)
                {
                    case "UTM Zone":
                        respuesta = first + " srtext LIKE '%UTM zone '||@n||'_%' order by srid";
                        break;
                    case "SRID":
                        string t = "";
                        for (int i = 0; i < texto.Length; i++)
                        {
                            t += 9;
                        }
                        respuesta = first + " srid = to_number(@n, '" + t + "') order by srid";
                        break;
                    case "Ciudad":
                        respuesta = first + " srtext LIKE '%'||@n||'_%' order by srid";
                        break;
                    default:
                        respuesta = first + " srtext LIKE '%'||@n||'_%' order by srid";
                        break;
                }
            return respuesta;
        }
    }
}
