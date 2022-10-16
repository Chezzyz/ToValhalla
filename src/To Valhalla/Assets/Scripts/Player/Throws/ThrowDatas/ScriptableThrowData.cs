using UnityEngine;

namespace Player.Throws.ThrowDatas
{
    [CreateAssetMenu(fileName = "ThrowData", menuName = "ScriptableObjects/Throws/ThrowData", order = 0)]
    public class ScriptableThrowData : ScriptableObject
    {
        [SerializeField]
        protected ThrowData _throwData;

        public ThrowData GetThrowData()
        {
            return _throwData;
        }
    }

    [System.Serializable]
    public struct CalculatedThrowData
    {
        public float Height;
        public float Length;
        public float Duration;
        public AnimationCurve Curve;

        public CalculatedThrowData(float height, float length, float duration, AnimationCurve curve)
        {
            Height = height;
            Length = length;
            Duration = duration;
            Curve = curve;
        }
    }
    
    [System.Serializable]
    public struct ThrowData
    {
        public float Weight;
        public AnimationCurve Curve;

        public ThrowData(float weight,
            float minDuration, float maxDuration,
            AnimationCurve curve)
        {
            Weight = weight;
            Curve = curve;
        }
    }
}