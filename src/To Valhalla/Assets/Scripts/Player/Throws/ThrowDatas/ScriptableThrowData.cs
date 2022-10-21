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
        public Vector2[] Points;
        public float DeltaTime;

        public CalculatedThrowData(Vector2[] points, float deltaTime)
        {
            Points = points;
            DeltaTime = deltaTime;
        }
    }
    
    [System.Serializable]
    public struct ThrowData
    {
        public float Weight;

        public ThrowData(float weight,
            AnimationCurve curve)
        {
            Weight = weight;
        }
    }
}