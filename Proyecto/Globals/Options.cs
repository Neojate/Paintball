using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Proyecto
{
    public sealed class Options
    {
        //constantes
        public const String XML_FILE = "./config.xml";

        public Vector2 resolution { get; set; }
        public Boolean fullScreen { get; set; }
        public int difficult { get; set; }
        public int language { get; set; }

        public Options()
        {
            resolution = new Vector2(800, 600);
            fullScreen = false;
            difficult = 0;
            language = Language.SPANISH;
        }

        /** Guardar datos en fichero */
        public void save()
        {
            StreamWriter file = new StreamWriter(XML_FILE);
            XmlSerializer writer = new XmlSerializer(this.GetType());
            writer.Serialize(file, this);
            file.Close();
        }

        /** Cargar datos de un fichero */
        public Options load()
        {
            if (!File.Exists(XML_FILE)) save();
            
            StreamReader file = new StreamReader(XML_FILE);
            XmlSerializer reader = new XmlSerializer(this.GetType()); 
            Options option = (Options)reader.Deserialize(file);
            file.Close();
            return option;
        }
    }
}
