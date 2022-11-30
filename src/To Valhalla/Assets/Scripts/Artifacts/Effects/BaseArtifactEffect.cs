using System;

public abstract class BaseArtifactEffect
{ 
    public abstract Action<ArtifactEffectApplier> GetEffect();
}
