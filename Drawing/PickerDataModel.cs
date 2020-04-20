using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Drawing
{
    class NamedValue<A>
    {
        public string Name { get; set; }

        public A Value { get; set; }

        public NamedValue(string name, A value)
        {
            Name = name;
            Value = value;
        }
    }

    class PickerDataModel<T> : UIPickerViewModel
    {
        public event EventHandler<EventArgs> valueChanged;
        int selectedIndex;
        public List<NamedValue<T>> Items { private set; get; }

        public PickerDataModel()
        {
            Items = new List<NamedValue<T>>();
        }

        public NamedValue<T> SelectedItem
        {
            get
            {
                //Wenn Liste "items nicht leer, 
                //und der selectedIndex größer gleich null
                //und der selectedIndex kleiner als die Anzahl der Elemente in der Liste
                //dann gebe das ausgewählte Item zurück, ansonsten gebe null zurück
                return Items != null && selectedIndex >= 0 && selectedIndex < Items.Count ? Items[selectedIndex] : null;
            }
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            //Wenn Liste befüllt ist, gebe die Anzahl der Elemente zurück, sonst 0
            return Items != null ? Items.Count : 0;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Items.Count > row ? Items[(int)row].Name : null;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int) row;
            valueChanged?.Invoke(this, new EventArgs());
        }







    }
}