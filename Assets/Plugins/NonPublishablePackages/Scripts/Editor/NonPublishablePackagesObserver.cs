using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NonPublishablePackages.UI;
using UnityEditor;
using UnityEngine;

namespace NonPublishablePackages
{
    [InitializeOnLoad]
    public class NonPublishablePackagesObserver
    {
        private const string DEPENDENCY_REGISTRY_PATH = "Resources/NonPublishablePackagesDependencies.json";

        private static Dictionary<string, NonPublishableregistry> loadedRegistries;

        private static Dictionary<string, bool> registryState;

        public static Dictionary<string, bool> RegistryState => registryState;
        public static Dictionary<string, NonPublishableregistry> LoadedRegistries => loadedRegistries;

        private static bool initialized = false;
        
        static NonPublishablePackagesObserver()
        {
            EditorApplication.update += OneTimeInitialize;
        }

        private static void OneTimeInitialize()
        {
            if(initialized) return;
            initialized = true;
            if (!CheckExtraPackagesIntegrity())
            {
                NonPublishablePackagesWindow.ShowWindow();
            }
        }
        
        public static bool CheckExtraPackagesIntegrity()
        {
            LoadDependencyRegistry();
            registryState = new Dictionary<string, bool>();
            bool ret = true;
            foreach (KeyValuePair<string,NonPublishableregistry> record in loadedRegistries)
            {
                bool result = CheckExtraPackageIntegrity(record.Value.projectRootPath);
                if (!result)
                {
                    ret = result;
                }

                registryState[record.Key] = result;
            }

            return ret;
        } 

        private static bool CheckExtraPackageIntegrity(string path)
        {
            string p = Application.dataPath + "/" + path;
            return Directory.Exists(p);
        }
        
        private static void LoadDependencyRegistry()
        {
            using StreamReader stream = new StreamReader($"{Application.dataPath}/{DEPENDENCY_REGISTRY_PATH}");
            string json = stream.ReadToEnd();
            loadedRegistries = JsonConvert.DeserializeObject<Dictionary<string, NonPublishableregistry>>(json);
        }
    }
}
