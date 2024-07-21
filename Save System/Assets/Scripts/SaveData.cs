using System;
using VirtualDeviants.Saving;

namespace VirtualDeviants {
    [Serializable]
    public class SaveData : Save {
        public static SaveData activeSave;

        public int score;
    }
}