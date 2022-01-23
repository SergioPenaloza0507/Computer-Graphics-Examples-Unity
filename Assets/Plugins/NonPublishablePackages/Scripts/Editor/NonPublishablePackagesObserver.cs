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
        private const string DEPENDENCY_REGISTRY_PATH = "NonPublishablePackagesDependencies";

        private static Dictionary<string, NonPublishableregistry> loadedRegistries;

        private static Dictionary<string, bool> registryState;

        public static Dictionary<string, bool> RegistryState => registryState;
        public static Dictionary<string, NonPublishableregistry> LoadedRegistries => loadedRegistries;
        static NonPublishablePackagesObserver()
        {
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
            string json = Resources.Load<TextAsset>(DEPENDENCY_REGISTRY_PATH).text;
            loadedRegistries = JsonConvert.DeserializeObject<Dictionary<string, NonPublishableregistry>>(json);
            //Debug.Log($"Registry Loaded: {JsonConvert.SerializeObject(loadedRegistries, Formatting.Indented)}");
        }
    }
}
