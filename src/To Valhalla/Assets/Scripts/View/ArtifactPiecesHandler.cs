using Services;
using Services.SaveLoad;
using Store;
using TMPro;
using UnityEngine;

namespace View
{
    public class ArtifactPiecesHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            CurrencyHandler.ArtifactPiecesCountChanged += OnArtifactPiecesCountChanged;
            SaveLoadSystem.SaveLoaded += OnSaveLoaded;
        }

        private void OnSaveLoaded()
        {
            _text.text = CurrencyHandler.Instance.ArtifactPiecesCount.ToString();
        }

        private void OnArtifactPiecesCountChanged(int count)
        {
            _text.text = count.ToString();
        }

        private void OnDisable()
        {
            CurrencyHandler.ArtifactPiecesCountChanged -= OnArtifactPiecesCountChanged;
            SaveLoadSystem.SaveLoaded -= OnSaveLoaded;
        }
    }
}