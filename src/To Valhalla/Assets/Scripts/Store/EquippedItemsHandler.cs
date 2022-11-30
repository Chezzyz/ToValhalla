using System;
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

        public IStoreItem GetFirstArtifact() => _firstEquippedArtifactCell.Item;
        public IStoreItem GetSecondArtifact() => _secondEquippedArtifactCell.Item;
        public IStoreItem GetHammer() => _equippedHammerCell.Item;
        
        public void EquipItem(IStoreItem item)
        {
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
            _equippedHammerCell.Setup(hammer);
            ItemEquipped?.Invoke(hammer);
        }

        private void EquipFirstArtifact(IStoreItem artifact)
        {
            _firstEquippedArtifactCell.Setup(artifact);
            ItemEquipped?.Invoke(artifact);
        }
        
        private void EquipSecondArtifact(IStoreItem artifact)
        {
            _secondEquippedArtifactCell.Setup(artifact);
            ItemEquipped?.Invoke(artifact);
        }

        private void UnequipArtifact(IStoreItem artifact)
        {
            if(_firstEquippedArtifactCell.Item == artifact) _firstEquippedArtifactCell.SetupDefault();
            if(_secondEquippedArtifactCell.Item == artifact) _secondEquippedArtifactCell.SetupDefault();
        }
        
        private void EquipSkin(IStoreItem skin)
        {
            
        }
    }
}