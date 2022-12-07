using System;
using UnityEngine;

namespace Artifacts.Effects
{
    public class GreaterMagnetEffect : BaseArtifactEffect
    {
        public override Action<ArtifactEffectApplier> GetEffect()
        {
            return applier =>
            {
                applier.SetCoinMagnetRadiusMultiplier(2.5f);
                applier.SetArtifactPieceMagnetRaduisMultiplier(2.5f);
            };
        }
    }
}