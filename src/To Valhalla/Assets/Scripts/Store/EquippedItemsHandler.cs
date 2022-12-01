using System;
using Artifacts;
using Hammers;
using Services;
using UnityEngine;

namespace Store
{
    public class EquippedItemsHandler : BaseGameHandler<EquippedItemsHandler>
    {
        [SerializeField] private EquippedItemCell _equippedHammerCell;
        [SerializeField] private EquippedItemCell _firstEquippedArtifactCell;
        [SerializeField] private EquippedItemCell _secondEquippedArtifactCell;

        public static event Action<IStoreItem> ItemEquipped;
        public static event Action<IStoreItem> ArtifactUnequipped;

        public ScriptableArtifactData GetFirstArtifact() => _firstEquippedArtifactCell.Item as ScriptableArtifactData;
        public ScriptableArtifactData GetSecondArtifact() => _secondEquippedArtifactCell.Item as ScriptableArtifactData;
        public ScriptableHammerData GetHammer() => _equippedHammerCell.Item as ScriptableHammerData;
        
        public void EquipItem(IStoreItem item)
        {
            if(item is null) return;
            if (item.GetStoreItemType() is StoreItemType.Hammer)
            {
                EquipHammer(item);
            }

            if (item.GetStoreItemType() is StoreItemType.Skin)
            {
                EquipSkin(item);
            }
        }

        public void EquipArtifact(IStoreItem item, int cellIndex)
        {
            if(item is null) return;
            switch (cellIndex)
            {
                case 0: EquipFirstArtifact(item); break;
                case 1: EquipSecondArtifact(item); break;
                case -1: UnequipArtifact(item); break;
                default: throw new NotImplementedException();
            }
        }

        private void EquipHammer(IStoreItem hammer)
        {
            if(_equippedHammerCell.Item == hammer) return;
            _equippedHammerCell.Setup(hammer);
            ItemEquipped?.Invoke(hammer);
        }

        private void EquipFirstArtifact(IStoreItem artifact)
        {
            if(_firstEquippedArtifactCell.Item == artifact) return;
            if (_secondEquippedArtifactCell.Item == artifact)
            {
                _secondEquippedArtifactCell.SetupDefault();
            }
            
            _firstEquippedArtifactCell.Setup(artifact);
            ItemEquipped?.Invoke(artifact);
        }
        
        private void EquipSecondArtifact(IStoreItem artifact)
        {
            if(_secondEquippedArtifactCell.Item == artifact) return;
            if (_firstEquippedArtifactCell.Item == artifact)
            {
                _firstEquippedArtifactCell.SetupDefault();
            }
            
            _secondEquippedArtifactCell.Setup(artifact);
            ItemEquipped?.Invoke(artifact);
        }

        private void UnequipArtifact(IStoreItem artifact)
        {
            if(_firstEquippedArtifactCell.Item == artifact) _firstEquippedArtifactCell.SetupDefault();
            if(_secondEquippedArtifactCell.Item == artifact) _secondEquippedArtifactCell.SetupDefault();
            ArtifactUnequipped?.Invoke(artifact);
        }
        
        private void EquipSkin(IStoreItem skin)
        {
            
        }
    }
}