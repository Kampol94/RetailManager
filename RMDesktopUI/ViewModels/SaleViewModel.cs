using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
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
        IProductEndpoint _productEndpoint;



        public  SaleViewModel(IProductEndpoint productEndPoint)
        {
            _productEndpoint = productEndPoint;

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            List<ProductModel> productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }


        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity); 
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

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;


                return output;
            }

        }

        public void RemoveFromCart()
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
