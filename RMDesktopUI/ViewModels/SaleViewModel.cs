using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Helpers;
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
        IConfigHelper _configHelper;



        public  SaleViewModel(IProductEndpoint productEndPoint, IConfigHelper configHelper)
        {
            _productEndpoint = productEndPoint;
            _configHelper = configHelper;

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


        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);

            }
        }


        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);

            }
        }


        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
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
                
                
                return CalculatedSubTotal().ToString("c");
            }
            
        }
        private decimal CalculatedSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }
            return subTotal;
        }

        public string Tax
        {
            get
            {
                
                return CalculatedTax().ToString("c");
            }

        }
        private decimal CalculatedTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxtRate();

            taxAmount = Cart
                        .Where(x => x.Product.IsTaxable)
                        .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            //foreach (var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmount += item.Product.RetailPrice * item.QuantityInCart * taxRate/100;
            //    }
            //}

            return taxAmount;

        }

        public string Total
        {
            get
            {
                decimal total = CalculatedTax() + CalculatedSubTotal();
                return total.ToString("c");
            }

        }



        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                if(ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }


                return output;
            }

        }

        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if(existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                //Find better solution
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);

            }

            

            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);

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

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
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
