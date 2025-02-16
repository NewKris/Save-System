using System.Collections.Generic;

namespace VirtualDeviants.Manual_Saving.Saving {
    public static class SaveFactory {
        private static readonly HashSet<ISaveDataEndpoint> Endpoints = new HashSet<ISaveDataEndpoint>();

        public static void RegisterEndpoint(ISaveDataEndpoint endpoint) {
            Endpoints.Add(endpoint);
        }

        public static void UnRegisterEndpoint(ISaveDataEndpoint endpoint) {
            Endpoints.Remove(endpoint);
        }
        
        public static Save CreateSave() {
            Save newSave = new Save();

            foreach (ISaveDataEndpoint saveDataEndpoint in Endpoints) {
                saveDataEndpoint.WriteDataToSave(newSave);
            }
            
            return newSave;
        }
    }
}