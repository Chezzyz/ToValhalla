using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/Skin", order = 0)]
public class PlayerScriptableData : ScriptableObject
{
    [SerializeField] private string _skinName;
    [Header("View")]
    [ShowAssetPreview]
    [SerializeField] private Sprite _idleSprite;
    [ShowAssetPreview]
    [SerializeField] private Sprite _flyingSprite;

    public Sprite GetIdleSprite() => _idleSprite; 
    public Sprite GetFlyingSprite() => _flyingSprite; 
}