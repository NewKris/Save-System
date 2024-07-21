using System;
using UnityEngine;
using VirtualDeviants.Saving;

namespace VirtualDeviants {
    public class ScoreEndpoint: MonoBehaviour, ISaveEndpoint
    {
        public Mock mock;

        private void Awake()
        {
            SaveSystem.RegisterEndpoint(this);
        }

        private void OnDestroy()
        {
            SaveSystem.UnregisterEndpoint(this);
        }

        public void WriteData(Save save)
        {
            save.score = mock.Score;
        }
    }
}