using System;
using System.Collections.Generic;

namespace VirtualDeviants.Manual_Saving.Saving.Snapshot {
    [Serializable]
    public class SnapshotTable {
        public List<SnapshotInfo> snapshots = new List<SnapshotInfo>();

        public void ForEach(Action<SnapshotInfo> callback) {
            snapshots.ForEach(callback);
        }
        
        public int GetIndex(string key) {
            return snapshots.FindIndex(x => string.Equals(x.id, key));
        }
    }
}