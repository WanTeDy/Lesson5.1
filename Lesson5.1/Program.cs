using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lesson5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Check ch0 = new Check("Иванова");            
            Check ch1 = new Check("Петрова");
            Check.ShopInfo = "СИЛЬПО";
            ch1.AddProduct("Pepsi", 2, 15.7, 10);
            ch1.AddProduct("Живчик", 5, 12.3, 5);
            Console.WriteLine(ch1);
            Console.WriteLine(ch0);
        }
    }

    struct Check
    {
        private static long checkNumber;
        private static string shopInfo;

        private long currentNumber;
        private List<Product> products;
        private double totalCost;
        private string cashierName;
        private DateTime checkTime;
        
        public static string ShopInfo
        {
            get
            {
                return shopInfo;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                shopInfo = value;
            }
        }
        
        static Check()
        {
            ShopInfo = "";
            checkNumber = 1;
        }
        public Check(string name)
        {            
            
            if (name == null)
                throw new ArgumentNullException();            
            cashierName = name;
            products = new List<Product>();
            totalCost = 0;
            checkTime = DateTime.Now;
            currentNumber = checkNumber++;
        } 
        private double TotalCheckSum()
        {
            double sum = 0;
            foreach(var p in products)
            {
                sum += p.Price * p.Quantity * (1 - p.Discount/100);
            }
            return sum;
        }
        public void AddProduct(string name, double quantity, double price, double discount)
        {
            products.Add(new Product(name, quantity, price, discount));
        }

        public override string ToString()
        {
            string productsList = "";
            foreach(var p in products)
            {
                productsList+=p.ToString();
            }
            return String.Format("{0}\nЧек №: {1}\nДата:{2}\n\n{3}\nИтого к оплате:{4}\n\nВас обслуживал кассир: {5}\nСпасибо за покупку!",
                ShopInfo, currentNumber, checkTime, productsList, TotalCheckSum(), cashierName);
        }
    }

    class Product
    {
        private string name;
        private double quantity;
        private double price;
        private double discount;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                name = value;
            }
        }
        public double Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                quantity = value;
            }
        }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                price = value;
            }
        }
        public double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                discount = value;
            }
        }
        public Product(string n, double q, double p, double d)
        {
            Name = n;
            Quantity = q;
            Price = p;
            Discount = d;
        }
        public override string ToString()
        {
            return String.Format("{0}  {1} x {2} = {3}\nСкидка {4}%, итого: {5}\n",
                Name, Price, Quantity, Price * Quantity, Discount, Price * Quantity * (1 - Discount / 100));
        }
    }
}
