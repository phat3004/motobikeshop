using _21_11_2021.Areas.admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }

        public CartItem()
        {
        }

        public CartItem(SanPham product)
        {
            ProductId = product.MaSanPham;
            ProductName = product.TenSanPham;
            Price = product.GiaDaKhuyenMai;
            Quantity = 1;
            Image = product.HinhAnh;
        }
    }

}
