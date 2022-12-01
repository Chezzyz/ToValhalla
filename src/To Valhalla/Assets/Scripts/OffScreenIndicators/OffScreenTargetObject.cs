using OffScreenIndicators;
using UnityEngine;
using NaughtyAttributes;

public class OffScreenTargetObject : MonoBehaviour
{
    [ShowAssetPreview]
    [SerializeField] private Sprite _spriteToShow;

    private OffScreenIndicatorsHandler offScreenIndicatorHandler = null;

    private void Awake()
    {
        if(offScreenIndicatorHandler == null)
        {
            offScreenIndicatorHandler = FindObjectOfType<OffScreenIndicatorsHandler>();
        }

        if (offScreenIndicatorHandler == null)
        {
            Debug.LogError("No IndicatorsController component found");
        }

        ShowIndicator();
    }

    public Sprite GetSpriteToShow() => _spriteToShow;

    public void ShowIndicator()
    { 
        offScreenIndicatorHandler.AddTargetIndicator(this);
    }

    public void HideIndicator()
    {
        offScreenIndicatorHandler.RemoveTargetIndicator(this);
    }
}