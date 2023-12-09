using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VirtualDeviants.Saving;
using VirtualDeviants.Saving.Snapshot;

namespace VirtualDeviants {
    public class SaveWindow : MonoBehaviour {
        public GameObject loadingSpinner;
        public GameObject createNewSaveButton;
        public GameObject snapshotContainer;
        public GameObject snapshotPrefab;

        public void CreateNewSave() {
            string id = Guid.NewGuid().ToString();
            StartCoroutine(SaveGameAsync(id));
        }
        
        private void OnEnable() {
            StartCoroutine(LoadSnapshot());
        }
        
        private IEnumerator LoadSnapshot() {
            SetLoading(true);
            
            Task<SnapshotTable> snapShotTableTask = SaveSystem.FetchSnapshot();
            while (!snapShotTableTask.IsCompleted) yield return null;

            yield return InstantiateSnapshotRows(snapShotTableTask.Result);
            
            SetLoading(false);
        }

        private IEnumerator InstantiateSnapshotRows(SnapshotTable snapshotTable) {
            foreach (Transform child in snapshotContainer.transform) {
                Destroy(child.gameObject);
            }
            
            foreach (SnapshotInfo snapshot in snapshotTable.snapshots) {
                SnapshotView snapshotView = Instantiate(snapshotPrefab, snapshotContainer.transform)
                    .GetComponent<SnapshotView>();
                
                snapshotView.Initialize(new SnapshotViewConfig() {
                    id = snapshot.id,
                    date = snapshot.savedDate,
                    onSaveCallback = SaveGame,
                    onLoadCallback = LoadGame,
                    onDeleteCallback = DeleteSave
                });
                
                yield return null;
            }
        }

        private void SaveGame(string id) {
            StartCoroutine(SaveGameAsync(id));
        }

        private IEnumerator SaveGameAsync(string id) {
            SetLoading(true);

            Task<SnapshotTable> updatedSnapshotTable = SaveSystem.SaveGame(id);
            while (!updatedSnapshotTable.IsCompleted) yield return null;

            yield return InstantiateSnapshotRows(updatedSnapshotTable.Result);
            
            SetLoading(false);
        }

        private void LoadGame(string id)
        {
            StartCoroutine(LoadGameAsync(id));
        }

        private IEnumerator LoadGameAsync(string id)
        {
            SetLoading(true);
            
            yield return SaveSystem.LoadGame(id);
            
            SetLoading(false);
        }

        private void DeleteSave(string id) {
            StartCoroutine(DeleteSaveAsync(id));
        }

        private IEnumerator DeleteSaveAsync(string id) {
            SetLoading(true);

            Task<SnapshotTable> updatedSnapshotTable = SaveSystem.DeleteSaveFile(id);
            while (!updatedSnapshotTable.IsCompleted) yield return null;

            yield return InstantiateSnapshotRows(updatedSnapshotTable.Result);
            
            SetLoading(false);
        }

        private void SetLoading(bool loading) {
            snapshotContainer.SetActive(!loading);
            loadingSpinner.SetActive(loading);
        }
    }
}