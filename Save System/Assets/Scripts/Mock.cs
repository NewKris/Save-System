using System;
using System.Collections;
using UnityEngine;
using VirtualDeviants.Saving;

namespace VirtualDeviants {
    public class Mock : MonoBehaviour {
        public DefaultSave defaultSave;
        public ScoreDisplay display;

        private int _score;
        
        public int Score {
            get {
                return _score;
            }
            private set {
                _score = value;
                display.UpdateDisplay(_score);
            }
        }

        private void Start()
        {
            Save save = Save.PopPendingSave() ?? defaultSave.save;
            Score = save.score;
        }

        public void IncreaseScore() {
            Score++;
        }

        public void DecreaseScore() {
            Score--;
        }
    }
}