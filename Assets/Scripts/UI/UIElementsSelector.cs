using System;
using System.Collections;
using System.Collections.Generic;
using Accord.Math;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIElementsSelector : MonoBehaviour
{

    [Header("Selection Settings")] [SerializeField]
    private List<UIElementManager> selectedElements;


    [SerializeField] private Text selectedCountText;
    private int _selectedItemsCount;


    public UnityEvent onFirstItemSelected;
    public UnityEvent onItemsDeselected;
    
    public void AddItemToSelectedList(UIElementManager element)
    {
        if (selectedElements.Count == 0)
            onFirstItemSelected?.Invoke();
        _selectedItemsCount++;
        selectedCountText.text = _selectedItemsCount.ToString();
        selectedElements.Add(element);
    }

    public void RemoveItemFromSelectedList(UIElementManager element)
    {
        selectedElements.Remove(element);
        _selectedItemsCount--;
        selectedCountText.text = _selectedItemsCount.ToString();
        if (selectedElements.Count == 0)
            onItemsDeselected?.Invoke();
    }

    private void Awake()
    {
        selectedElements = new List<UIElementManager>();
    }
}
