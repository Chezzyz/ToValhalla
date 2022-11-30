using System;
using System.Collections.Generic;
using System.Linq;
using Store.View;
using UnityEngine;

namespace Store
{
    public class StoreFiller : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _items;
        [SerializeField] private List<StoreItemCell> _cells;

        public StoreItemType CurrentSection { get; private set; }
        
        private void OnEnable()
        {
            StoreHandler.StoreSectionButtonPressed += OnStoreSectionButtonPressed;
        }

        private void OnStoreSectionButtonPressed(StoreItemType type)
        {
            CurrentSection = type;
            FillStoreWithItems(_items
                .Select(item => item as IStoreItem)
                .Where(item => item.GetStoreItemType() == type)
                .ToList());
        }

        private void FillStoreWithItems(List<IStoreItem> items)
        {
            _cells.ForEach(cell => cell.Clear());
            for (int i = 0; i < items.Count; i++)
            {
                _cells[i].Fill(items[i]);
            }
        }

        private void OnDisable()
        {
            StoreHandler.StoreSectionButtonPressed -= OnStoreSectionButtonPressed;
        }
    }
}