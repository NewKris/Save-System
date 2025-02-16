using UnityEngine;
using VirtualDeviants.Manual_Saving.Saving;

namespace VirtualDeviants.Manual_Saving {
    public class Mock : MonoBehaviour, ISaveDataEndpoint {
        public DefaultSave defaultSave;
        public ScoreDisplay display;

        private int _score;
        
        private int Score {
            get => _score;
            set {
                _score = value;
                display.UpdateDisplay(_score);
            }
        }
        
        public void WriteDataToSave(Save save) {
            save.score = Score;
        }
        
        public void IncreaseScore() {
            Score++;
        }

        public void DecreaseScore() {
            Score--;
        }

        private void Start() {
            Save save = Save.activeSave ?? defaultSave.save;
            Score = save.score;
            SaveFactory.RegisterEndpoint(this);
        }

        private void OnDestroy() {
            SaveFactory.UnRegisterEndpoint(this);
        }
    }
}