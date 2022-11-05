using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hummers
{
    [CreateAssetMenu(fileName = "New Hummer Data", menuName = "ScriptableObjects/Hummers", order = 0)]
    public class ScriptableHummerData : ScriptableObject
    {
        #region SerializeFields
        [SerializeField] private string _hummerName;
        [SerializeField] private string _hummerDescription;
        [SerializeField] private Sprite _hummerSprite;
        [SerializeField] private float _weight;
        [SerializeField] private HummerType _hummerType;
        [SerializeField] private List<float> _cirleScalePartsInPercent = new List<float>();
        #endregion

        public string GetHummerName() => _hummerName;
        public string GetHummerDescription() => _hummerDescription;
        public Sprite GetHummerSprite() => _hummerSprite;
        public float GetHummerWeight() => _weight;
        public HummerType GetHummerType() => _hummerType;
        public IReadOnlyCollection<float> GetCirleScalePartsInPercent() => _cirleScalePartsInPercent;

    }

    public enum HummerType
    {
        Light,
        Medium,
        Heavy
    }
}