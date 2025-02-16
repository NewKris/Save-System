using System.Collections.Generic;

namespace VirtualDeviants.Manual_Saving.Saving {
    public static class SaveFactory {
        private static readonly HashSet<ISaveDataProvider> Endpoints = new HashSet<ISaveDataProvider>();

        public static void RegisterEndpoint(ISaveDataProvider provider) {
            Endpoints.Add(provider);
        }

        public static void UnRegisterEndpoint(ISaveDataProvider provider) {
            Endpoints.Remove(provider);
        }
        
        public static Save CreateSave() {
            Save newSave = new Save();

            foreach (ISaveDataProvider saveDataEndpoint in Endpoints) {
                saveDataEndpoint.WriteDataToSave(newSave);
            }
            
            return newSave;
        }
    }
}