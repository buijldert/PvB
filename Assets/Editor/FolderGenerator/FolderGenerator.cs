using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Generators
{
    public static class FolderGenerator
    {
        [MenuItem("Tools/Generate Folders")]
        public static void Generate()
        {
            FolderList folderList = Resources.Load<FolderList>("FolderList");

            foreach (string path in folderList.folderPaths)
            {
                if (!AssetDatabase.IsValidFolder(path))
                {
                    Directory.CreateDirectory(path);
                }
                else
                {
                    Debug.LogWarning("FolderPath: " + path + ", does already exist.");
                }
            }

            AssetDatabase.Refresh();
        }
    }
}