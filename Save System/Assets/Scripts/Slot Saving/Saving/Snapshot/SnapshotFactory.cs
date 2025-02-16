using System;

namespace VirtualDeviants.Slot_Saving.Saving.Snapshot {
    internal static class SnapshotFactory {
        public static SnapshotInfo CreateNewSnapshot() {
            return new SnapshotInfo() {
                savedDate = DateTime.Now.ToFileTimeUtc()
            };
        }

        public static SnapshotInfo CreateEmptySnapshot() {
            return new SnapshotInfo();
        }
    }
}