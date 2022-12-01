using Services;
using Store;
using UnityEngine;

namespace Artifacts
{
    public class ArtifactEffectApplier : MonoBehaviour
    {
        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            ApplyEffects(EquippedItemsHandler.Instance.GetFirstArtifact() as ScriptableArtifactData, 
                EquippedItemsHandler.Instance.GetSecondArtifact() as ScriptableArtifactData);
        }

        #region ApplierInterface
        public void SetCoinValueMultiplier(int value) => CurrencyHandler.Instance.SetCoinValueMultiplier(value);
        public int GetCoinValueMultiplier() => CurrencyHandler.Instance.CoinValueMultiplier;
        #endregion
        

        private void ApplyEffects(ScriptableArtifactData storeItem1, ScriptableArtifactData storeItem2)
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
    }
}
