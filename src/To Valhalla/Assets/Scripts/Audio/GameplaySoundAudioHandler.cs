using Level;
using Player.Throws;
using Services.SaveLoad;
using UnityEngine;

namespace Audio
{
    public class GameplaySoundAudioHandler : BaseAudioHandler<GameplaySoundAudioSource>
    {
        [Header("Artifact Piece Collected Sound")]
        [SerializeField] private AudioClip _artifactPieceCollectedSound;
        [SerializeField] [Range(0f,1f)] private float _artifactPieceCollectedVolumeScale;
        [Header("Cloud Interaction Sound")]
        [SerializeField] private AudioClip _cloudInteractionSound;
        [SerializeField] [Range(0f,1f)] private float _cloudInteractionVolumeScale;
        [Header("Throw Completed Sound")]
        [SerializeField] private AudioClip _throwCompletedSound;
        [SerializeField] [Range(0f,1f)] private float _throwCompletedVolumeScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            ArtifactPiece.ArtifactPieceCollected += OnArtifactPieceCollected;
            Obstacle.ObstacleTouched += OnObstacleTouched;
            BaseThrow.ThrowCompleted += OnThrowCompleted;
        }

        private void OnThrowCompleted()
        {
            PlayOneShot(_throwCompletedSound, volumeScale:_throwCompletedVolumeScale);
        }

        private void OnObstacleTouched()
        {
            PlayOneShot(_cloudInteractionSound, Random.Range(0.85f,1.15f), _cloudInteractionVolumeScale);
        }

        private void OnArtifactPieceCollected()
        {
            PlayOneShot(_artifactPieceCollectedSound, volumeScale:_artifactPieceCollectedVolumeScale);
        }
    }
}