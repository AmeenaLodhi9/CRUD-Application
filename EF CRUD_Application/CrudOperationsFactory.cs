using System;
using static EF_CRUD_Application.EFCrudOperations;

namespace EF_CRUD_Application
{
    public static class CrudOperationsFactory
    {
        public static ICrudOperations GetCrudOperations(string choice)
        {
            try
            {
                ICrudOperations crudOperations;
                switch (choice)
                {
                    case "1":
                        crudOperations = new EFCrudOperations();
                        break;
                    case "2":
                        crudOperations = new AdoCrudOperations();
                        break;
                    default:
                        throw new ArgumentException("Invalid choice");
                }

                Logger.Log("CRUD operation selected successfully", string.Empty);
                return crudOperations;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ex.ToString());
                throw; // Re-throw the exception to let it propagate
            }
        }
    }
}
