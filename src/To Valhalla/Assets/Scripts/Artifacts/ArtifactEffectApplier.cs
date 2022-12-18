using System;
using Input;
using Level;
using Player;
using Services;
using Store;
using UnityEngine;

namespace Artifacts
{
    public class ArtifactEffectApplier : MonoBehaviour
    {
        private bool _isAddVelocityForDuration;
        private bool _isAddCoinForObstacle;
        
        private void OnEnable()
        {
            Obstacle.ObstacleTouched += OnObstacleTouched;
        }

        private void OnDisable()
        {
            Obstacle.ObstacleTouched -= OnObstacleTouched;
        }

        public void ApplyEffects(ScriptableArtifactData storeItem1, ScriptableArtifactData storeItem2)
        {
            if (storeItem1 != null)
            {
                storeItem1.GetBaseArtifactEffect().GetEffect().Invoke(this);
            }

            if (storeItem2 != null)
            {
                storeItem2.GetBaseArtifactEffect().GetEffect().Invoke(this);
            }
        }

        #region ApplierInterface

        public void SetCoinValueMultiplier(int value) => CurrencyHandler.Instance.SetCoinValueMultiplier(value);
        public int GetCoinValueMultiplier() => CurrencyHandler.Instance.CoinValueMultiplier;
        public void SetCoinMagnetRadiusMultiplier(float value) => Coin.SetColliderRadiusMultiplier(value);

        public void SetArtifactPieceMagnetRaduisMultiplier(float value) =>
            ArtifactPiece.SetColliderRadiusMultiplier(value);

        public void AddCoinForObstacle() => _isAddCoinForObstacle = true;

        public void SetInfinitePowerScale(bool state) =>
            FindObjectOfType<ThrowScalesController>().IsInfinitePowerScale = state;

        public void AddAdditionalVelocity() => _isAddVelocityForDuration = true;

        #endregion

        private void OnObstacleTouched()
        {
            if (_isAddVelocityForDuration)
            {
                Obstacle.AdditionalVelocity = FlightDataHandler.DurationSinceLastObstacle;
                FlightDataHandler.DurationSinceLastObstacle = 0;
            }

            if (_isAddCoinForObstacle)
            {
                Coin.InvokeCoinCollected();
            }
        }

    }
}