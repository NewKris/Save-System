using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VirtualDeviants.Saving {
    internal static class FileManager {
        private const string FILE_TYPE = ".json";
        
        private static string GamePath {
            get {
#if UNITY_EDITOR
                return Application.dataPath;
#else
                return Application.persistentDataPath;
#endif
            }
        }

        public static void CreateDirectory(string directory) {
            Directory.CreateDirectory(GamePath + directory);
            UpdateFileSystem();
        }
        
        public static async Task SerializeFile<T>(T data, string localPath) {
            string absolutePath = GamePath + localPath + FILE_TYPE;
            string json = JsonUtility.ToJson(data, true);
            await File.WriteAllTextAsync(absolutePath, json);
            UpdateFileSystem();
        }

        public static async Task<T> DeserializeFile<T>(string localPath) {
            string absolutePath = GamePath + localPath + FILE_TYPE;

            if (!File.Exists(absolutePath)) {
                return JsonUtility.FromJson<T>("");
            }
            
            string json = await File.ReadAllTextAsync(absolutePath);
            return JsonUtility.FromJson<T>(json);
        }

        public static void DeleteFile(string localPath) {
            string absolutePath = GamePath + localPath + FILE_TYPE;

            if (File.Exists(absolutePath)) {
                File.Delete(absolutePath);
            }
            
            UpdateFileSystem();
        }

        private static void UpdateFileSystem() {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
