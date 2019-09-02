using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SaleViewModel : Screen
    {
        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private string _itemQuantity;

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity); //?
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

       

        public string SubTotal
        {
            get
            {
                //TODO - obliczenia
                return "0";
            }
            
        }
        public string Tax
        {
            get
            {
                //TODO - obliczenia
                return "0";
            }

        }
        public string Total
        {
            get
            {
                //TODO - obliczenia
                return "0";
            }

        }



        public bool CanAddToCart
        {
            get
            {
                bool output = false;


                return output;
            }

        }

        public void AddToCart()
        {

        }

        public bool CanRemoveToCart
        {
            get
            {
                bool output = false;


                return output;
            }

        }

        public void RemoveToCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;


                return output;
            }

        }

        public void CheckOut()
        {

        }
    }
}
