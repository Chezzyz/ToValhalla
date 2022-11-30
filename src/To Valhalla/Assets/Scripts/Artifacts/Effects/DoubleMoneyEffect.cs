using System;

namespace Artifacts
{
    public class DoubleMoneyEffect : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return applier => applier.SetCoinValueMultiplier(applier.GetCoinValueMultiplier() * 2);
        }
    }
}
