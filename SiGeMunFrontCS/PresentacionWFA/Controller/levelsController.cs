using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentacionWFA.Data;

namespace PresentacionWFA
{
    class levelsController
    {
        #region Variables
       
        #endregion
        
        #region Constructor
        
        #endregion

        #region Methods

        internal themes readLevelNames()
        {
            string fileDataPath = @"Data\levelNames.xml";
            try
            {
                if (System.IO.File.Exists(fileDataPath))
                {

                    themes themesLevels = new themes();
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(themesLevels.GetType());
                    System.IO.StreamReader file = new System.IO.StreamReader(fileDataPath);
                    themesLevels = (themes)reader.Deserialize(file);

                    return themesLevels;

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

        internal string getTypeFeature(string feature, themes themes)
        {
            try
            {
                var types = from c in themes.theme where c.name == feature select c;
                foreach (themesTheme t in types)
                {
                    return t.type;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        #endregion
    }

    /********************************JECIEL***************************************/
    class CategoriesController
    {
        #region Variables

        #endregion

        #region Constructor

        #endregion

        #region Methods
        //Lee un archivo xml y crea en base a los datos contenidos en el, un objeto themes
        internal themes readLevelCategories()
        {
            string fileDataPath = @"Data\categoriesNames.xml";
            try
            {
                if (System.IO.File.Exists(fileDataPath)) //Me marca Falso 
                {

                    themes themesLevels = new themes();
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(themesLevels.GetType());
                    System.IO.StreamReader file = new System.IO.StreamReader(fileDataPath);
                    themesLevels = (themes)reader.Deserialize(file);

                    return themesLevels;

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

        //obtiene una lista con los names de cierta categoría [manzana, clave manzana, ect..]
        internal List<string> getLevelNames(themes themes, string categoria)
        {
            try
            {
                List<string> listNames = new List<string>();

                var types = from nom in themes.theme where nom.category == categoria select nom;

                foreach (themesTheme nom in types)
                {
                    listNames.Add(nom.name);
                }

                return listNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>(); //Regresa una lista vacía
            }
        }

        //Obtiene una lista con las categorías [Cartografía catastral, cartografía x, etc..]
        internal List<string> getCategories(themes themes)
        {
            try
            {
                List<string> listCategories = new List<string>();

                var categorias = from category in themes.theme group category by new { category.category } into mycategory select mycategory.FirstOrDefault();

                foreach (themesTheme c in categorias)
                {
                    listCategories.Add(c.category);
                }

                return listCategories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //Busca y btiene el atributo type (string feature) dentro de un theme [point, line, etc...]
        internal string getTypeFeature(string feature, themes themes)
        {
            try
            {
                var types = from c in themes.theme where c.name == feature select c;
                foreach (themesTheme t in types)
                {
                    return t.type;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        #endregion
    }
    /*******************************FIN JECIEL************************************/

}
