using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using Npgsql;

namespace DAL
{
    public class ETBatchProcDAL
    {
        private string cs;

        public ETBatchProcDAL(string cs)
        {
            this.cs = cs;
        }

        public bool insertScript(Stream fileZip)
        {
            try
            {
                if (this.cs != string.Empty)
                {
                    string sql = converStream2String(ref fileZip);
                    if (sql != null)
                    {
                        TaskScheduler ts;
                        if (SynchronizationContext.Current != null)
                        {
                            ts = TaskScheduler.FromCurrentSynchronizationContext();
                        }
                        else
                        {
                            ts = TaskScheduler.Default;
                        }

                        Task t = Task.Factory.StartNew(() =>
                        {
                            SendQueryAsync(sql, cs);
                        }).ContinueWith(o =>
                        {

                        }, ts);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void SendQueryAsync(string sql, string cs)
        {
            int numBlock = 30;
            string[] sep = new string[] { "@@" };
            string[] sepLines = new string[] { ";" };
            string[] split1 = sql.Split(sep, StringSplitOptions.RemoveEmptyEntries);


            if (split1.Length == 2)
            {
                string carta = split1[0];
                string[] split2 = split1[1].Split(sepLines, StringSplitOptions.RemoveEmptyEntries);
                if (split2.Length > 0)
                {
                    int numDiv = split2.Length / numBlock;
                    int rem = split2.Length % numBlock;
                    for (int i = 0; i < numDiv; i++)
                    {
                        string[] segmArray = SubArray(ref split2, i * numBlock, numBlock);
                        processArray(cs, segmArray, carta + "_" + i);
                    }
                    if (rem > 0)
                    {
                        string[] segmArrayFin = SubArray(ref split2, numDiv * numBlock, rem);
                        processArray(cs, segmArrayFin, carta + "_" + numDiv);
                    }
                }
            }
        }

        private static void processArray(string cs, string[] src, string carta)
        {
            string v_src = String.Join(";", src);
            try
            {
                using (var conn = new Npgsql.NpgsqlConnection(cs))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        using (var comm = conn.CreateCommand())
                        {
                            comm.Parameters.Add(new NpgsqlParameter("src", v_src));
                            comm.Parameters.Add(new NpgsqlParameter("dgn", carta));
                            comm.CommandText = "insert into et_batch_proc(script, carta) values(:src,:dgn)";
                            comm.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    using (var tran1 = conn.BeginTransaction())
                    {
                        using (var comm1 = conn.CreateCommand())
                        {
                            comm1.CommandText = "select pkg__string_to_qry_void(s.carta, s.script,';') from et_batch_proc s where trim(s.carta)= '" + carta + "' order by s.id desc limit 1";
                            comm1.ExecuteNonQuery();
                        }
                        tran1.Commit();
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return;
        }

        private static string[] SubArray(ref string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private string converStream2String(ref Stream fileZip)
        {
            try
            {
                String script = null;
                MemoryStream ms = new MemoryStream();
                fileZip.CopyTo(ms);
                fileZip.Close();
                ms.Seek(0, SeekOrigin.Begin);
                using (var mso = new MemoryStream())
                {
                    using (ZipFile zip = ZipFile.Read(ms))
                    {
                        zip[0].Extract(mso);
                    }
                    mso.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(mso, System.Text.Encoding.Default);
                    script = sr.ReadToEnd();
                    sr.Close();

                }
                if (script.Length > 0)
                {
                    return script;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
