using System;
using UnityEngine;

namespace VirtualDeviants.Saving {
    
    [Serializable]
    public class Save
    {
        private static Save PendingSave;
        
        public int score;

        public static void AssignPendingSave(Save save)
        {
            PendingSave = save;
        }

        public static Save PopPendingSave()
        {
            Save pendingSave = PendingSave;
            PendingSave = null;

            return pendingSave;
        }
    }
}
