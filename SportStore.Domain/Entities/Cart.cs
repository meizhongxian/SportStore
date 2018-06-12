using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity) {

            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// 删除已添加物品
        /// </summary>
        /// <param name="product"></param>
        public void RemoveLine(Product product) {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// 计算总价
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue() {
            return lineCollection.Sum(e=>e.Product.Price*e.Quantity);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public void Clear() {
            lineCollection.Clear();
        }

        /// <summary>
        /// 查看购物车内商品
        /// </summary>
        public IEnumerable<CartLine> Lines {
            get { return lineCollection; }
        }
    }

    public class CartLine {
        /// <summary>
        /// 购买产品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
