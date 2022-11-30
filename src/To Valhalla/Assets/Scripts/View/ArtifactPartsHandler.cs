using Level;
using TMPro;
using UnityEngine;

namespace View
{
    public class ArtifactPartsHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private int _count;

        private void OnEnable()
        {
            ArtifactPart.ArtifactPartCollected += OnArtifactPartCollected;
        }

        private void OnArtifactPartCollected()
        {
            _count++;
            _text.text = _count.ToString();
        }

        private void OnDisable()
        {
            ArtifactPart.ArtifactPartCollected -= OnArtifactPartCollected;
        }
    }
}
