using System;

namespace VirtualDeviants.Manual_Saving.Saving.Snapshot {
    internal static class SnapshotFactory {
        public static SnapshotInfo CreateNewSnapshot(string id) {
            return new SnapshotInfo() {
                id = id, 
                savedDate = DateTime.Now.ToFileTimeUtc()
            };
        }
    }
}