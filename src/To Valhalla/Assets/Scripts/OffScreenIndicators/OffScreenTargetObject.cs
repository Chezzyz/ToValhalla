using UnityEngine;

public class OffScreenTargetObject : MonoBehaviour
{
    private OffScreenIndicatorsHandler ui = null;

    private void Awake()
    {
        if(ui == null)
        {
            ui = FindObjectOfType<OffScreenIndicatorsHandler>();
        }

        if (ui == null)
        {
            Debug.LogError("No IndicatorsController component found");
        }

        ShowIndicator();
    }

    public void ShowIndicator()
    { 
        ui.AddTargetIndicator(this);
    }

    public void HideIndicator()
    {
        ui.RemoveTargetIndicator(this);
    }
}