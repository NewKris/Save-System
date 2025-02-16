using UnityEngine;
using VirtualDeviants.Manual_Saving.Saving;

namespace VirtualDeviants.Manual_Saving {
    
    [CreateAssetMenu(menuName = "Default Save")]
    public class DefaultSave : ScriptableObject {
        public Save save;
    }
}