using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;
using PresentacionWFA.Entity_temp;

namespace PresentacionWFA.Controller
{
    class filesController
    {
        #region Constructor
        public filesController() { }
        #endregion

        #region Methods
        internal bool copyFile(ref DgnFileEntity dgnFileEntity1, ref DgnFileEntity dgnFileEntity2)
        {
            try
            {
                System.IO.File.Copy(System.IO.Path.Combine(dgnFileEntity1.Ruta, dgnFileEntity1.Nombre),
                    System.IO.Path.Combine(dgnFileEntity2.Ruta, dgnFileEntity2.Nombre), 
                    true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool setFile(ref System.Windows.Forms.OpenFileDialog openFileDialog, ref DgnFileEntity DgnFileEntity)
        {
            try
            {
                if (openFileDialog.FileNames.Count() == 1)
                {
                    DgnFileEntity.Nombre = System.IO.Path.GetFileName(openFileDialog.FileName).ToString();
                    DgnFileEntity.Ruta = System.IO.Path.GetDirectoryName(openFileDialog.FileNames[0]) + @"\";
                    //TODO : Obtener el tamaño del archivo
                    DgnFileEntity.Tamanio = "0MB";
                }
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        internal bool setFile(string file, ref DgnFileEntity dgn)
        {
            try
            {
                dgn.Nombre = System.IO.Path.GetFileName(file).ToString();
                dgn.Ruta = System.IO.Path.GetDirectoryName(file) + @"\";
                //TODO : Obtener el tamaño del archivo
                dgn.Tamanio = "0";
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal string makeZIP(string fileSQL)
        {
            try
            {
                FileInfo fileToCompress = new FileInfo(fileSQL);
                if (fileToCompress.Exists)
                {
                    File.Delete(fileToCompress.Name.Split('.')[0] + ".zip");
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(fileToCompress.FullName, fileToCompress.Name.Split('.')[0]);
                        zip.Save(fileToCompress.DirectoryName + "\\" + fileToCompress.Name.Split('.')[0] + ".zip");
                    }
                }
                return fileToCompress.DirectoryName + "\\" + fileToCompress.Name.Split('.')[0] + ".zip";
            }
            catch
            {
                return string.Empty;
            }
        }

        internal void removeFile(string p)
        {
            try
            {
                File.Delete(p);
            }
            catch
            {
                throw;
            }
        }

        internal void writeLines(string filename, ref List<string> listSQLs)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
                using (TextWriter tw = new StreamWriter(filename))
                {
                    tw.WriteLine(Path.GetFileNameWithoutExtension(filename) + "@@");
                    foreach (string sql in listSQLs)
                    {
                        tw.WriteLine(sql);
                    }
                    tw.Close();
                }
            }

            else if (File.Exists(filename))
            {
                using (TextWriter _tw = new StreamWriter(filename, true))
                {
                    foreach (string sql in listSQLs)
                    {
                        _tw.WriteLine(sql);
                    }
                    _tw.Close();
                }
            }
        }
        #endregion

        internal void writeLines(string filename, ref string SingleSQLLine)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Dispose();
                using (TextWriter tw = new StreamWriter(filename))
                {
                    tw.WriteLine(filename + "@@");

                    tw.WriteLine(SingleSQLLine);

                    tw.Close();
                }
            }

            else if (File.Exists(filename))
            {
                using (TextWriter _tw = new StreamWriter(filename, true))
                {
                    _tw.WriteLine(SingleSQLLine);
                    _tw.Close();
                }
            }
        }

        internal byte[] getArrayBytes(string zipFile)
        {
            try
            {
                if (File.Exists(zipFile))
                {
                    return File.ReadAllBytes(zipFile);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
