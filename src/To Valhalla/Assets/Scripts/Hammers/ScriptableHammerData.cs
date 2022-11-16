using System.Collections.Generic;
using UnityEngine;

namespace Hammers
{
    [CreateAssetMenu(fileName = "HummerData", menuName = "ScriptableObjects/Hummers", order = 0)]
    public class ScriptableHammerData : ScriptableObject
    {
        [Header("View")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        [Header("Model")]
        [SerializeField] private float _weight;
        [SerializeField] private int _cost;
        [SerializeField] private HammerType _hammerType;
        [SerializeField] private List<float> _scalePartsPercents = new List<float>();

        public string GetName() => _name;
        public string GetDescription() => _description;
        public Sprite GetSprite() => _sprite;
        public int GetCost() => _cost;
        public float GetWeight() => _weight;
        public HammerType GetHammerType() => _hammerType;
        public IReadOnlyCollection<float> GetScalePartsInPercent() => _scalePartsPercents;

    }

    public enum HammerType
    {
        Light,
        Medium,
        Heavy
    }
}