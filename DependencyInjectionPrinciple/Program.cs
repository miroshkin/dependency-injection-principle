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

        interface IDataAccessDependency
        {
            void SetDependency(ICustomerDataAccess customerDataAccess);
        }

        public class CustomerBusinessLogic : IDataAccessDependency
        {
            private ICustomerDataAccess _dataAccess;

            //Property Injection
            public ICustomerDataAccess DataAccess { get; set; }

            //Constructor Injection
            public CustomerBusinessLogic(ICustomerDataAccess custDataAccess)
            {
                _dataAccess = custDataAccess;
            }

            //Method Injection
            public void SetDependency(ICustomerDataAccess customerDataAccess)
            {
                _dataAccess = customerDataAccess;
            }

            public CustomerBusinessLogic()
            {
                _dataAccess = new CustomerDataAccess();
            }

            public string GetCustomerName(int id)
            {

                return _dataAccess.GetCustomerName(id);
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

        public class CustomerService
        {
            CustomerBusinessLogic _customerBL;

            public CustomerService()
            {
                //Contstructor injection
                _customerBL = new CustomerBusinessLogic(new CustomerDataAccess());

                //Property Injection
                _customerBL.DataAccess = new CustomerDataAccess();

                //Method Injection
                _customerBL.SetDependency(new CustomerDataAccess());
            }

            public string GetCustomerName(int id) {
                return _customerBL.GetCustomerName(id);
            }
        }
    }
}
