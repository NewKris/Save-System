using System;
using System.Collections.Generic;

namespace VirtualDeviants.Saving.Snapshot {
    [Serializable]
    public class SnapshotTable {
        public List<SnapshotInfo> snapshots;

        public SnapshotTable() {
            snapshots = new List<SnapshotInfo>();
        }
    }
}