using System;
using Unity;

namespace EF_CRUD_Application
{
    public static class CrudOperationsFactory
    {
        public static ICrudOperations GetCrudOperations(string choice, IUnityContainer container)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        Logger.Instance.Log("Entity Framework CRUD operation selected successfully", string.Empty);
                        return container.Resolve<ICrudOperations>("EF");
                    case "2":
                        Logger.Instance.Log("ADO.NET CRUD operation selected successfully", string.Empty);
                        return container.Resolve<ICrudOperations>("ADO");
                    default:
                        throw new ArgumentException($"Invalid choice: {choice}", nameof(choice));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Error resolving CRUD operations", ex.ToString());
                throw; // Re-throw the exception to let it propagate
            }
        }
    }
}
