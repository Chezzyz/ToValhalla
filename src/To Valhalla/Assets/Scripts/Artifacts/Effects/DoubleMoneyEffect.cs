using System;

namespace Artifacts.Effects
{
    public class DoubleMoneyEffect : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return applier => applier.SetCoinValueMultiplier(applier.GetCoinValueMultiplier() * 2);
        }
    }
}
