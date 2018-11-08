
#region Referencias

using System;
using System.Collections.Generic;
using System.IO;

#endregion


namespace DBE.Net.Facel.LibFacel.Clases
{
    public class FileHelper
    {

        #region Metodos publicos estaticos

        #region CreateDirectory

        /// <summary>
        /// Crea carpeta si no existe
        /// </summary>
        /// <param name="path">Nombre de la carpeta a crear</param>
        /// <returns>Retorna mensaje de error si ocurre</returns>
        public static string CreateDirectory(string path)
        {
            string errorMessage = string.Empty;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception excepcion)
            {
                errorMessage = string.Format(Constantes.TEMPLATE_ERROR, "CreateDirectory", excepcion.Message);
            }

            return errorMessage;
        }

        #endregion

        #region GetFiles

        /// <summary>
        /// Devuelve lista de Archivos contenidos en una carpeta específica
        /// </summary>
        /// <param name="folder">Ruta de la carpeta</param>
        /// <param name="pattern">Patron de nombre de los archivos</param>
        /// <param name="exclude">Nombre de archivo a excluir porque no se necesita</param>
        /// <returns>Retorna objeto del tipo List<string></returns>
        public static List<string> GetFiles(string folder, string pattern, string exclude = "")
        {

            #region Variables de trabajo

            List<string> list = new List<string>();

            #endregion

            if (Directory.Exists(folder))
            {
                string[] files = Directory.GetFiles(folder, pattern);
                if ((files != null) && (files.Length > 0))
                {
                    foreach (string file in files)
                    {
                        bool sirve = true;
                        string filename = file.Replace(folder, "").Replace(@"\", "");

                        if (!string.IsNullOrWhiteSpace(exclude))
                        {
                            if (filename.Equals(exclude))
                            {
                                sirve = false;
                            }
                        }

                        if (sirve)
                        {
                            list.Add(filename);
                        }
                    }
                }
            }

            return list;
        }

        #endregion

        #region GetFileContent

        /// <summary>
        /// Devuelve contenido texto de un archivo específico
        /// </summary>
        /// <param name="filename">Nombre del archivo</param>
        /// <returns>Devuelve cadena de texto</returns>
        public static string GetFileContent(string filename)
        {
            string content = string.Empty;

            if (File.Exists(filename))
            {
                content = File.ReadAllText(filename);
            }
            return content;
        }

        #endregion

        #region PutFileContent

        /// <summary>
        /// Establece contenido de un archivo especifico
        /// </summary>
        /// <param name="filename">Nombre del archivo</param>
        /// <param name="content">Contenido del archivo</param>
        /// <returns>Devuelve mensaje de error si ocurre</returns>
        public static string PutFileContent(string filename, string content)
        {

            #region Variables de trabajo

            string errorMessage = string.Empty;

            #endregion

            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                File.WriteAllText(filename, content);
            }
            catch (Exception excepcion)
            {
                errorMessage = string.Format(Constantes.TEMPLATE_ERROR, "PutFileContent", excepcion.Message);
            }

            return errorMessage;
        }

        #endregion

        #region RemoveFile

        public static void RemoveFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        #endregion

        #region RenameFile

        public static void RenameFile(string oldName, string newName)
        {
            if (File.Exists(oldName))
            {
                File.Move(oldName, newName);
            }
        }

        #endregion

        #endregion
    }
}
