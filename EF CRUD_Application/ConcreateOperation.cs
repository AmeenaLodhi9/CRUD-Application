using EF_CRUD_Application.Data;
using EF_CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EF_CRUD_Application
{
    namespace EF_CRUD_Application
    {
        public class ConcreteOperations
        {
            private readonly ICrudOperations _crudOperations;

            public ConcreteOperations(ICrudOperations crudOperations)
            {
                _crudOperations = crudOperations;
            }

            public void CreateProduct()
            {
                _crudOperations.CreateProduct();
            }

            public void ReadProducts()
            {
                _crudOperations.ReadProducts();
            }

            public void UpdateProduct()
            {
                _crudOperations.UpdateProduct();
            }

            public void DeleteProduct()
            {
                _crudOperations.DeleteProduct();
            }
        }
    }


}
