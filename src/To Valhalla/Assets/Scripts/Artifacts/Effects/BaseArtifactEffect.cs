using System;

namespace Artifacts.Effects
{
    public abstract class BaseArtifactEffect
    { 
        public abstract Action<ArtifactEffectApplier> GetEffect();
    }
}
