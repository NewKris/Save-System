using System.Collections.Generic;

namespace VirtualDeviants.Slot_Saving.Saving {
    public static class SaveFactory {
        private static readonly HashSet<ISaveDataProvider> Providers = new HashSet<ISaveDataProvider>();

        public static void RegisterProvider(ISaveDataProvider provider) {
            Providers.Add(provider);
        }

        public static void UnRegisterProvider(ISaveDataProvider provider) {
            Providers.Remove(provider);
        }
        
        public static Save CreateSave() {
            Save save = Save.activeSave ?? new Save();

            foreach (ISaveDataProvider saveDataProvider in Providers) {
                saveDataProvider.ProvideSaveData(save);
            }
            
            return save;
        }
    }
}