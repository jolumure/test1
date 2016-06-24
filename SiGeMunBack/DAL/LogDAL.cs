using Entity;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL
{
    public class LogDAL
    {
        private String cs;

        public LogDAL(string cs)
        {
            this.cs = cs;
        }

        public IEnumerable<LogEntity> getLog()
        {
            IEnumerable<LogEntity> resultado = null;

            using (var conn = new NpgsqlConnection(this.cs))
            {
                conn.Open();
                using (var comm = conn.CreateCommand())
                {
                    comm.CommandText = "SELECT pkg_getLog()";
                    using (NpgsqlDataReader resQuery = comm.ExecuteReader())
                        while (resQuery.Read())
                        {
                            resultado = JsonConvert.DeserializeObject<IEnumerable<LogEntity>>(resQuery.GetString(0));

                        }
                }
            }

            return resultado;
        }
    }
}
