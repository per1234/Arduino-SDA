﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase generica encargada de almacenar y leer archivos binarios de tipo <T>. 
    ///     NOTA: La clase pasada para instanciar este objeto debe ser Serializable.
    /// </summary>
    public class DataSerializer<T>
    {
        // ES: _fileName  = Nombre del archivo donde se guardara o se deberá leer los datos serializados  
        private string _fileDirection;
        // ES: _formatter = Objeto para darle formato binario a los archivos a escribir                     
        private IFormatter _formatter;

        public string FileDirection
        {
            get { return _fileDirection; }
        }

        private Stream WriteStream() { return new FileStream(_fileDirection, FileMode.Append, FileAccess.Write, FileShare.ReadWrite); }

        private Stream ReadStream() { return new FileStream(_fileDirection, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite); }

        /// <summary>
        /// ES: Constructor de la clase DataSerializer, la cual la dirección del archivo será la dirección de instalación mas el nombre de la clase.
        /// </summary>
        public DataSerializer()
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + typeof(T).Name + ".data";
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".DataSerializer()"); }
        }

        /// <summary>
        /// ES: Constructor de la clase DataSerializer.
        /// </summary>
        /// <param name="FileName">ES: Nombre del archivo binario.</param>
        public DataSerializer(string FileName)
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + FileName;
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".DataSerializer(string)"); }
        }

        /// <summary>
        /// ES: Guarda elementos en el archivo binario.
        /// </summary>
        /// <param name="data">ES: Dato a almacenar en el archivo binario.</param>
        public void SaveData(T data)
        {
            try
            {
                _formatter.Serialize(WriteStream(), data);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".SaveData(T)"); }
        }

        /// <summary>
        /// ES: Guarda todos los elementos almacenados en una lista en el archivo binario.
        /// </summary>
        /// <param name="data">ES: Lista que contiene los datos.</param>
        public void SaveData(List<T> data)
        {
            try
            {
                for (int i = 0; i < data.Count; ++i) { SaveData(data[i]); }
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".SaveData(List<T>)"); }
        }

        /// <summary>
        /// ES: Recupera todos los datos almacenados en el archivo binario en ese momento en una lista.
        /// </summary>
        public List<T> RecoverData()
        {
            List<T> list = new List<T>();
            try
            {
                Stream stream = ReadStream();
                while (stream.Position < stream.Length)
                {
                    T result = (T)_formatter.Deserialize(stream);
                    list.Add(result);
                }
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".RecoverAllData()"); }
            return list;
        }

        /// <summary>
        /// ES: Recupera un registro que se encuentre en el archivo binario.
        /// </summary>
        /// <param name="IdRegister">ES: Registro a devolver</param>
        public T RecoverData(int IdRegister)
        {
            try
            {
                Stream stream = ReadStream();
                int typeSize = Marshal.SizeOf(typeof(T));
                stream.Seek((IdRegister - 1) * typeSize, SeekOrigin.Begin);
                return (T)_formatter.Deserialize(stream);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".RecoverData(int)"); }
            return default(T);
        }

        /// <summary>
        /// ES: Elimina todos los datos del archivo binario.
        /// PRECAUCIÓN: Una vez eliminados, no es posible recuperarlos.
        /// </summary>
        public void ClearBinary()
        {
            try { WriteStream().SetLength(0); }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).DeclaringMethod + ".ClearBinary()"); }
        }
    }
}
