using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VirtualDeviants {
    public class SnapshotView : MonoBehaviour {
        [Header("Text")]
        public TextMeshProUGUI idText;
        public TextMeshProUGUI dateText;
        
        [Header("Buttons")]
        public Button saveButton;
        public Button loadButton;
        public Button deleteButton;
        
        public void Initialize(SnapshotViewConfig config) {
            idText.text = config.id;
            dateText.text = DateTime.FromFileTimeUtc(config.date).ToString("F");
            
            saveButton.onClick.AddListener(() => config.onSaveCallback(config.id));
            loadButton.onClick.AddListener(() => config.onLoadCallback(config.id));
            deleteButton.onClick.AddListener(() => config.onDeleteCallback(config.id));
        }
    }

    public struct SnapshotViewConfig {
        public string id;
        public long date;
        public Action<string> onLoadCallback;
        public Action<string> onSaveCallback;
        public Action<string> onDeleteCallback;
    }
}