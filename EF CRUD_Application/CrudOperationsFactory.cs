using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EF_CRUD_Application.EFCrudOperations;

namespace EF_CRUD_Application
{
    public static class CrudOperationsFactory
    {
        public static ICrudOperations GetCrudOperations(string choice)
        {
            switch (choice)
            {
                case "1":
                    return new EFCrudOperations();
                case "2":
                    return new AdoCrudOperations();
                default:
                    throw new ArgumentException("Invalid choice");
            }
        }
    }

}
