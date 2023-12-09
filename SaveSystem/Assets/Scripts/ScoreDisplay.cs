using System;
using TMPro;
using UnityEngine;

namespace VirtualDeviants {
    public class ScoreDisplay : MonoBehaviour {
        private TextMeshProUGUI _text;

        private void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateDisplay(int score) {
            _text.text = score.ToString();
        }
    }
}