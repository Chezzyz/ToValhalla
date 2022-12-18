using System;

namespace Artifacts.Effects
{
    public class AdditionalVelocityForDuration : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return (applier) => applier.AddAdditionalVelocity();
        }
    }
}