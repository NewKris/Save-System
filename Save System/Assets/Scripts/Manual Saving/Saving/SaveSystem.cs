using System.Threading.Tasks;
using VirtualDeviants.Manual_Saving.Saving.Snapshot;

namespace VirtualDeviants.Manual_Saving.Saving {
    public static class SaveSystem {
        public static async Task<SnapshotTable> SaveGame(Save save, string id) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + id;
            
            FileManager.CreateDirectory(SaveSystemConfig.SAVE_FILE_PATH);
            await FileManager.SerializeFile(save, path);

            SnapshotTable snapshotTable = await FetchSnapshots();

            int i = snapshotTable.GetIndex(id);
            if (i != -1) snapshotTable.snapshots.RemoveAt(i);
            
            SnapshotInfo newSnapshot = SnapshotFactory.CreateNewSnapshot(id);
            snapshotTable.snapshots.Insert(0, newSnapshot);
            
            await UpdateSnapshot(snapshotTable);

            return snapshotTable;
        }

        public static async Task<T> LoadGame<T>(string id) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + id;
            return await FileManager.DeserializeFile<T>(path);
        }

        public static async Task<SnapshotTable> DeleteSaveFile(string id) {
            string path = SaveSystemConfig.SAVE_FILE_PATH + id;
            FileManager.DeleteFile(path);

            SnapshotTable snapshotTable = await FetchSnapshots();
            int i = snapshotTable.GetIndex(id);
            
            if (i != -1) snapshotTable.snapshots.RemoveAt(i);

            await UpdateSnapshot(snapshotTable);
            
            return snapshotTable;
        }

        public static async Task<SnapshotTable> FetchSnapshots() {
            SnapshotTable snapshotTable = await FileManager.DeserializeFile<SnapshotTable>(SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);

            if (snapshotTable != null) return snapshotTable;
            
            snapshotTable = new SnapshotTable();
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);

            return snapshotTable;
        }

        private static async Task UpdateSnapshot(SnapshotTable snapshotTable) {
            await FileManager.SerializeFile(snapshotTable, SaveSystemConfig.SAVE_FILE_SNAPSHOT_PATH);
        }
    }
}
