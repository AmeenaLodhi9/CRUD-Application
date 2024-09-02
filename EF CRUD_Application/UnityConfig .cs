using System;
using Unity;

namespace EF_CRUD_Application
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterComponents(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterComponents(IUnityContainer container)
        {
            // Register all your components with the container here
            // It is NOT necessary to register your controllers

            // Register ICrudOperations implementations with named mappings
            container.RegisterType<ICrudOperations, EFCrudOperations>("EF");
            container.RegisterType<ICrudOperations, AdoCrudOperations>("ADO");

            // Add any other type registrations needed
        }
    }
}
