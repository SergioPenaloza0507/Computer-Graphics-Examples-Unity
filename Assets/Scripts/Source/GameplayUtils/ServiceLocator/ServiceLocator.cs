using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGameServices
{
    public class ServiceLocator : MonoBehaviour
    {
        #region Fields

        private static ServiceLocator instance;
        private Dictionary<Type, Component> registeredServices = new Dictionary<Type, Component>();
        
        #endregion
        
        #region Properties

        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject newLocator = new GameObject("Service Locator (GENERATED)");
                    instance = newLocator.AddComponent<ServiceLocator>();
                    instance.SendMessage("Awake");
                }

                return instance;
            }
        }
        #endregion

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded += (_, __) => CleanUp();
        }

        private void CleanUp()
        {
            registeredServices = registeredServices.Where(x => x.Value != null).ToDictionary(x=>x.Key, x => x.Value);
        }

        public TService Get<TService>() where TService : Behaviour
        {
            if (registeredServices.ContainsKey(typeof(TService)))
            {
                return (TService) registeredServices[typeof(TService)];
            }

            return null;
        }

        public bool Register<TService>(TService service, bool persistent = false) where TService : Behaviour
        {
            Type type = typeof(TService);
            if (registeredServices.TryGetValue(type, out Component component))
            {
                if (component == null)
                {
                    registeredServices[type] = service;
                    return true;
                }

                return false;
            }

            registeredServices.Add(type, service);
            if (persistent)
            {
                DontDestroyOnLoad(service.gameObject);
            }
            return true;
        }

        public TService GetUnsafe<TService>(ServiceLocatorUnsafeFallback creationFallbacks = (ServiceLocatorUnsafeFallback)(1<<1), bool persistent = false) where TService : Behaviour
        {
            TService ret = Get<TService>();

            if (ret != null) return ret;

            if (creationFallbacks.HasFlag(ServiceLocatorUnsafeFallback.FindInScene))
            {
                ret = FindObjectOfType<TService>();
                if (ret != null)
                {
                    Register(ret, persistent);
                }
            }
            
            if (creationFallbacks.HasFlag(ServiceLocatorUnsafeFallback.CreateNew))
            {
                GameObject newG = new GameObject($"{typeof(TService).Name} (GENERATED MANAGER)");
                ret = newG.AddComponent<TService>();
                Register(ret, persistent);
                ret.SendMessage("Awake");
            }

            return ret;
        }
    }
}
