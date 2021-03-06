﻿using SDA_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SDA_Program.Interface
{
    public class UnitInterface
    {
        private SDA_Core.Business.Arrays.UnitArray unitArray;

        // Objeto de una clase 'funcional' para hacer transformaciones.
        private SDA_Core.Functional.Data dataManager;

        public UnitInterface()
        {
            unitArray = new SDA_Core.Business.Arrays.UnitArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        public void UpdateTable(DataGrid DG_Units) => DG_Units.ItemsSource = dataManager.UnitListToDataTable(unitArray).AsDataView();

        public void NewUnit(DataGrid DG_Units, TextBox TB_Unit)
        {
            if (TB_Unit.Text == "")
            {
                MessageBox.Show("Empty fields.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.Unit newUnit = new SDA_Core.Business.Unit(TB_Unit.Text);
            if (unitArray.List.Exists(newUnit))
            {
                MessageBox.Show("Value alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }
            unitArray.List.Add(newUnit);

            TB_Unit.Text = "";
            UpdateTable(DG_Units);
        }

        public void DeleteBaudRate(DataGrid DG_Units)
        {
            if (DG_Units.SelectedItem == null) return;
            int selected = DG_Units.SelectedIndex;
            unitArray.List.Remove(selected);
            UpdateTable(DG_Units);
        }

        public void LoadDataGrid(DataGrid DG_Units, SDA_Core.Business.Arrays.UnitArray data)
        {
            unitArray = data;
            UpdateTable(DG_Units);
        }

        public SDA_Core.Business.Arrays.UnitArray GetData() => unitArray;
    }
}