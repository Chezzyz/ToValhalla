using Level;
using Services;
using Store;
using UnityEngine;

namespace Artifacts
{
    public class ArtifactEffectApplier : MonoBehaviour
    {
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

        #endregion



    }
}