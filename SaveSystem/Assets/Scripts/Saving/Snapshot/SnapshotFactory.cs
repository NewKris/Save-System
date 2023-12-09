using System;
using System.Globalization;

namespace VirtualDeviants.Saving.Snapshot {
    public static class SnapshotFactory {
        public static SnapshotInfo CreateNewSnapshot(string id) {
            return new SnapshotInfo() { id = id, savedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture) };
        }
    }
}