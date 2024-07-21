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
            get => _score;
            private set {
                _score = value;
                display.UpdateDisplay(_score);
            }
        }
        
        public void IncreaseScore() {
            Score++;
        }

        public void DecreaseScore() {
            Score--;
        }

        private void Start() {
            SaveData save = SaveData.activeSave ?? defaultSave.save;
            Score = save.score;
        }
    }
}