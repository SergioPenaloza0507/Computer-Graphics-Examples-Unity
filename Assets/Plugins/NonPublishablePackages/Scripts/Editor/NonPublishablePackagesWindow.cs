using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NonPublishablePackages.UI
{
    public class NonPublishablePackagesWindow : EditorWindow
    {
        private Vector2 scrollPosition;
        private static NonPublishablePackagesWindow currentWindow;
        
        [MenuItem("Window/Non Publishable Dependencies")]
        public static void ShowWindow()
        {
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
            
            currentWindow = GetWindow(typeof(NonPublishablePackagesWindow),true, "Extra packages") as NonPublishablePackagesWindow;
            currentWindow.maxSize = new Vector2(600, currentWindow.maxSize.y);
            currentWindow.minSize = new Vector2(600, currentWindow.minSize.y);
            NonPublishablePackagesObserver.CheckExtraPackagesIntegrity();
        }

        private void OnGUI()
        {
            string title =
                "Here you can check if you have all the necessary extra packages required\n for this project. have in mind that the project can still work without\n some packages";
            GUILayout.BeginArea(new Rect(10,10,position.width - 30, 100));
            var titleStyle = new GUIStyle {alignment = TextAnchor.UpperCenter, fontSize = 15};
            titleStyle.normal.textColor = Color.white;
            GUILayout.Label(title,titleStyle);
            GUILayout.Space(20);
            if (GUILayout.Button("Refresh"))
            {
                NonPublishablePackagesObserver.CheckExtraPackagesIntegrity();
            }
            GUILayout.EndArea();
            
            
            GUILayout.BeginArea(new Rect(10,130,position.width - 30, 100));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            var nameStyle = new GUIStyle(GUI.skin.label);
            foreach (KeyValuePair<string,bool> pair in NonPublishablePackagesObserver.RegistryState)
            {
                GUILayout.BeginHorizontal();
                if (!pair.Value)
                {
                    nameStyle.normal.textColor = Color.red;
                }
                else
                {
                    nameStyle.normal.textColor = Color.green;
                }
                GUILayout.Label(pair.Key, nameStyle);
                if (GUILayout.Button("Download", GUILayout.Width(300)))
                {
                    Application.OpenURL(NonPublishablePackagesObserver.LoadedRegistries[pair.Key].url);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}
