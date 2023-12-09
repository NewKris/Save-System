using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VirtualDeviants.Saving.Snapshot;

namespace VirtualDeviants.Saving {
    public static class SaveSystem
    {
        private static HashSet<ISaveEndpoint> SaveEndpoints = new HashSet<ISaveEndpoint>();

        public static void RegisterEndpoint(ISaveEndpoint endpoint)
        {
            if (SaveEndpoints == null)
                SaveEndpoints = new HashSet<ISaveEndpoint>() { endpoint };
            else
                SaveEndpoints.Add(endpoint);
        }

        public static void UnregisterEndpoint(ISaveEndpoint endpoint)
        {
            if (SaveEndpoints.Contains(endpoint))
                SaveEndpoints.Remove(endpoint);
        }
        
        public static async Task<SnapshotTable> SaveGame(string id) {
            Save save = new Save();
            foreach (ISaveEndpoint saveEndpoint in SaveEndpoints) {
                saveEndpoint.WriteData(save);
            }

            string path = SaveSystemConfig.SaveFilePath + id;
            
            FileManager.CreateDirectory(SaveSystemConfig.SaveFilePath);
            await FileManager.SerializeFile(save, path);

            SnapshotTable snapshotTable = await FetchSnapshot();

            int i = snapshotTable.snapshots.FindIndex(snapshot => string.Equals(snapshot.id, id));
            if (i != -1) snapshotTable.snapshots.RemoveAt(i);
            
            SnapshotInfo newSnapshot = SnapshotFactory.CreateNewSnapshot(id);
            snapshotTable.snapshots.Insert(0, newSnapshot);
            
            await UpdateSnapshot(snapshotTable);

            return snapshotTable;
        }

        public static async Task LoadGame(string id) {
            string path = SaveSystemConfig.SaveFilePath + id;
            Save save = await FileManager.DeserializeFile<Save>(path);
            Save.AssignPendingSave(save);
            
            SceneManager.LoadScene(SaveSystemConfig.GameplayScene);
        }

        public static async Task<SnapshotTable> DeleteSaveFile(string id) {
            string path = SaveSystemConfig.SaveFilePath + id;
            FileManager.DeleteFile(path);

            SnapshotTable snapshotTable = await FetchSnapshot();
            int i = snapshotTable.snapshots.FindIndex(snapshot => string.Equals(snapshot.id, id));
            
            if (i != -1) snapshotTable.snapshots.RemoveAt(i);

            await UpdateSnapshot(snapshotTable);
            return snapshotTable;
        }

        public static async Task<SnapshotTable> FetchSnapshot() {
            SnapshotTable snapshotTable = await FileManager.DeserializeFile<SnapshotTable>(SaveSystemConfig.SaveFileSnapshotPath);

            if (snapshotTable != null) return snapshotTable;
            
            snapshotTable = new SnapshotTable();
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SaveFileSnapshotPath);

            return snapshotTable;
        }

        private static async Task UpdateSnapshot(SnapshotTable snapshotTable) {
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SaveFileSnapshotPath);
        }
    }
}
