using System;
using System.Collections.Generic;
using System.Linq;
using Store.View;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreFiller : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _items;
        [SerializeField] private List<StoreItemCell> _cells;
        [SerializeField] private int _cellsOnPage;
        [SerializeField] private Button _previousPageButton;
        [SerializeField] private Button _nextPageButton;
        [SerializeField] private int _pagesCount;
        [SerializeField] private List<Image> _pageDots;
        [SerializeField] private Sprite _defaultDot;
        [SerializeField] private Sprite _activeDot;

        public StoreItemType CurrentSection { get; private set; }

        private int _currentPage;

        private void OnEnable()
        {
            StoreHandler.StoreSectionButtonPressed += OnStoreSectionButtonPressed;
        }

        private void Start()
        {
            _previousPageButton.onClick.AddListener(() => ChangePage(_currentPage - 1));
            _nextPageButton.onClick.AddListener(() => ChangePage(_currentPage + 1));
        }

        private void ChangePage(int index)
        {
            if (index < 0)
            {
                SetCurrentPage(0);
                FillStoreWithItems(GetItemsOfType(CurrentSection), 0);
                return;
            }

            if (index > _pagesCount - 1)
            {
                return;
            }

            SetCurrentPage(index);
            FillStoreWithItems(GetItemsOfType(CurrentSection), _currentPage);
        }

        private void SetCurrentPage(int index)
        {
            _currentPage = index;
            SetActivePageDot(index);
        }

        private void SetActivePageDot(int index)
        {
            _pageDots.ForEach(image => image.sprite = _defaultDot);
            _pageDots[index].sprite = _activeDot;
        }

        private void OnStoreSectionButtonPressed(StoreItemType type)
        {
            CurrentSection = type;
            ChangePage(0);
            FillStoreWithItems(GetItemsOfType(CurrentSection), _currentPage);
        }

        private List<IStoreItem> GetItemsOfType(StoreItemType type)
        {
            return _items
                .Select(item => item as IStoreItem)
                .Where(item => item.GetStoreItemType() == type)
                .ToList();
        }

        private void FillStoreWithItems(List<IStoreItem> items, int page)
        {
            _cells.ForEach(cell => cell.Clear());
            int offset = page * _cellsOnPage;
            for (int i = 0; i < items.Count; i++)
            {
                if(i + offset > items.Count - 1) continue;
                _cells[i].Fill(items[i + offset]);
            }
        }

        private void OnDisable()
        {
            StoreHandler.StoreSectionButtonPressed -= OnStoreSectionButtonPressed;
        }
    }
}