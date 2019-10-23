using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace DependencyInjectionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICar, BMW>();
            container.RegisterType<ICar, Audi>("LuxuryCar");
            
            Console.WriteLine("Hello, DIP!");

            container.RegisterType<Driver>("LuxuryCarDriver", 
                new InjectionConstructor(container.Resolve<ICar>("LuxuryCar")));

            Driver driver1 = container.Resolve<Driver>();// injects BMW
            driver1.RunCar();

            Driver driver2 = container.Resolve<Driver>("LuxuryCarDriver");// injects Audi
            driver2.RunCar();
         
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

        public interface ICar
        {
            int Run();
        }

        public class BMW : ICar
        {
            private int _miles = 0;

            public int Run()
            {
                return ++_miles;
            }
        }

        public class Ford : ICar
        {
            private int _miles = 0;

            public int Run()
            {
                return ++_miles;
            }
        }

        public class Audi : ICar
        {
            private int _miles = 0;

            public int Run()
            {
                return ++_miles;
            }

        }
        public class Driver
        {
            private ICar _car = null;

            public Driver(ICar car)
            {
                _car = car;
            }

            public void RunCar()
            {
                Console.WriteLine("Running {0} - {1} mile ", _car.GetType().Name, _car.Run());
            }
        }
    }
}
