using System;

namespace Artifacts.Effects
{
    public class CoinForObstacleEffect : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return (applier) => applier.AddCoinForObstacle();
        }
    }
}