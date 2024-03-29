﻿using System.Collections.Generic;
using Persistent;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class UIElementsSelector : MonoBehaviour
    {

        [Header("Selection Settings")]
        public List<UIElementManager> selectedElements;


        [SerializeField] private Text selectedCountText;
        private int _selectedItemsCount;


        public UnityEvent onFirstItemSelected;
        public UnityEvent onItemsDeselected;
    
        public void AddItemToSelectedList(UIElementManager element)
        {
            if (selectedElements.Count == 0)
            {
                onFirstItemSelected?.Invoke();
                SettingsManager.GetInstance.isSelectingMode = true;
            }
            Vibration.Vibrate(200);
            _selectedItemsCount++;
            selectedCountText.text = _selectedItemsCount.ToString();
            selectedElements.Add(element);
        }


        public void DeleteSelectedElements()
        {
            foreach (var element in selectedElements)
            {
                element.DeleteElement();
            }
        }



        public void RemoveItemFromSelectedList(UIElementManager element)
        {
            selectedElements.Remove(element);
            _selectedItemsCount--;
            selectedCountText.text = _selectedItemsCount.ToString();
            if (selectedElements.Count == 0)
            {
                SettingsManager.GetInstance.isSelectingMode = false;
                onItemsDeselected?.Invoke();
            }
            
        }

        public void RemoveAllItemsFromSelectedList()
        {
            foreach (var item in selectedElements)
            {
                item.DeselectAutomatically();
            }
            onItemsDeselected?.Invoke();
            _selectedItemsCount = 0;
            selectedCountText.text = "0";
            selectedElements.Clear();
            SettingsManager.GetInstance.isSelectingMode = false;

        }
    

        private void Awake()
        {
            selectedElements = new List<UIElementManager>();
            onFirstItemSelected.AddListener(() => SettingsManager.GetInstance.longPressTime = 0f);
            onItemsDeselected.AddListener(() => SettingsManager.GetInstance.longPressTime = .5f);
            SettingsManager.GetInstance.Selector = this;
        }
    }
}
