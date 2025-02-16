using System.Threading.Tasks;
using UnityEngine;
using VirtualDeviants.Slot_Saving.Saving.Snapshot;

namespace VirtualDeviants.Slot_Saving.Saving {
    public static class SaveSystem {
        private const int SAVE_SLOT_COUNT = 4;
        
        public static async Task<SnapshotTable> SaveGame(Save save) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + save.saveSlotIndex;
            Debug.Log($"Saving game to {path}");
            
            FileManager.CreateDirectory(SaveSystemConfig.SAVE_FILE_PATH);
            await FileManager.SerializeFile(save, path);

            SnapshotTable snapshotTable = await FetchSnapshot();
            snapshotTable[save.saveSlotIndex] = SnapshotFactory.CreateNewSnapshot();
            
            await UpdateSnapshot(snapshotTable);

            return snapshotTable;
        }

        public static async Task<T> LoadGame<T>(int id) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + id;
            Debug.Log($"Loading game from {path}");
            return await FileManager.DeserializeFile<T>(path);
        }

        public static async Task<SnapshotTable> DeleteSaveFile(int id) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + id;
            Debug.Log($"Deleting save at {path}");
            
            FileManager.DeleteFile(path);

            SnapshotTable snapshotTable = await FetchSnapshot();
            snapshotTable[id] = SnapshotFactory.CreateEmptySnapshot();

            await UpdateSnapshot(snapshotTable);
            
            return snapshotTable;
        }

        public static async Task<SnapshotTable> FetchSnapshot() {
            SnapshotTable snapshotTable = await FileManager.DeserializeFile<SnapshotTable>(SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);

            if (snapshotTable != null) return snapshotTable;
            
            snapshotTable = new SnapshotTable(SAVE_SLOT_COUNT);
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);

            return snapshotTable;
        }

        private static async Task UpdateSnapshot(SnapshotTable snapshotTable) {
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);
        }
    }
}
