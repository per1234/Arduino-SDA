﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'MeasureUnit'
    /// </summary>
    internal class MeasureUnitApplication
    {
        public MeasureUnitApplication()
        {
        }

        public SDA_Core.Business.Arrays.MeasureUnitArray GetData() => Data.MeasureUnitsDataArray;

        /// <summary>
        /// ES: Guarda los nuevos datos ingresados al binario.
        /// </summary>
        /// <param name="data">ES: Nuevos datos a guardar</param>
        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            SDA_Core.Data.MeasureUnitSerializer.Save(data);
        }

        /// <summary>
        /// ES: Devuelve los datos almacenados del arreglo 'Measure' del programa.
        /// </summary>
        /// <returns>ES: Datos que existen durante la ejecución del programa.</returns>
        public SDA_Core.Business.Arrays.MeasureArray GetMeasures()
        {
            SDA_Core.Business.Arrays.MeasureArray result = new SDA_Core.Business.Arrays.MeasureArray();
            List<SDA_Core.Business.Measure> binary = Data.MeasuresDataList;
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        /// <summary>
        /// ES: Devuelve los datos almacenados del arreglo 'Unit' del programa.
        /// </summary>
        /// <returns>ES: Datos que existen durante la ejecución del programa.</returns>
        public SDA_Core.Business.Arrays.UnitArray GetUnits()
        {
            SDA_Core.Business.Arrays.UnitArray result = new SDA_Core.Business.Arrays.UnitArray();
            List<SDA_Core.Business.Unit> binary = Data.UnitsDataList;
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }
    }
}