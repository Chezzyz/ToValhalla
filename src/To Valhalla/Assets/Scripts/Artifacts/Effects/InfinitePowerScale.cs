using System;

namespace Artifacts.Effects
{
    public class InfinitePowerScale : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return (applier) => applier.SetInfinitePowerScale(true);
        }
    }
}