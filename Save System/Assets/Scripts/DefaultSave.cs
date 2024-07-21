using UnityEngine;
using VirtualDeviants.Saving;

namespace VirtualDeviants {
    
    [CreateAssetMenu(menuName = "Default Save")]
    public class DefaultSave : ScriptableObject {
        public SaveData save;
    }
}