using System;
using System.Collections.Generic;

namespace VirtualDeviants.Slot_Saving.Saving.Snapshot {
    [Serializable]
    public class SnapshotTable {
        public List<SnapshotInfo> snapshots;

        public SnapshotInfo this[int i] {
            get => snapshots[i];
            set => snapshots[i] = value;
        }

        public int Count => snapshots.Count;

        public SnapshotTable(int saveSlotCount) {
            snapshots = new List<SnapshotInfo>();
            
            for (int i = 0; i < saveSlotCount; i++) {
                snapshots.Add(SnapshotFactory.CreateEmptySnapshot());
            }
        }
    }
}