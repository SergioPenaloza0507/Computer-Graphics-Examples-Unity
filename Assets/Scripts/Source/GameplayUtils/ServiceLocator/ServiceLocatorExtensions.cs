using UnityEngine;

namespace InGameServices
{
    public static class ServiceLocatorExtensions
    {
        public static TService FindService<TService>(this Behaviour component) where TService : Behaviour
        {
            return ServiceLocator.Instance.Get<TService>();
        }
        
        public static TService FindServiceUnsafe<TService>(this Behaviour component, ServiceLocatorUnsafeFallback creationFallbacks = (ServiceLocatorUnsafeFallback)(1<<1), bool persistent = false) where TService : Behaviour
        {
            return ServiceLocator.Instance.GetUnsafe<TService>();
        }

        public static void RegisterSelf<TService>(this IService<TService> newService, bool persistent = false) where TService : Behaviour
        {
            Behaviour casted;
            try
            {
                casted = (Behaviour) newService;
            }
            catch
            {
                return;
            }
            ServiceLocator.Instance.Register(casted, persistent);
        }
    }
}
