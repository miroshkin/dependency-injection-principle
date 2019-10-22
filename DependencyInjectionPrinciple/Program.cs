using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, DIP!");
           
         
        }

        public class CustomerBusinessLogic
        {
            private ICustomerDataAccess _custDataAccess;

            public CustomerBusinessLogic()
            {
            }

            public string GetCustomerName(int id)
            {
                _custDataAccess = DataAccessFactory.GetDataAccessObj();

                return _custDataAccess.GetCustomerName(id);
            }
        }

        public class DataAccessFactory
        {
            public static ICustomerDataAccess GetDataAccessObj() 
            {
                return new CustomerDataAccess();
            }
        }

        public interface ICustomerDataAccess
        {
            string GetCustomerName(int id);
        }

        public class CustomerDataAccess: ICustomerDataAccess
        {
            public CustomerDataAccess()
            {
            }

            public string GetCustomerName(int id) {
                return "Dummy Customer Name";        
            }
        }
    }
}
