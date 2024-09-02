using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CRUD_Application
{
    //Client Class
    public interface ICrudOperations
    {
        void CreateProduct();
        void ReadProducts();
        void UpdateProduct();
        void DeleteProduct();
    }

}
