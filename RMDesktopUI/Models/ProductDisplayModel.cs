﻿using System.ComponentModel;

namespace RMDesktopUI.Models
{
    public class ProductDisplayModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Descripton { get; set; }

        public decimal RetailPrice { get; set; }

        private int _quantityInStock;

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                _quantityInStock = value;
                CallPropertyChange(nameof(QuantityInStock));
            }
        }


        public bool IsTaxable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CallPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
