/*****************************************************************************
// File Name :         ShopSelectionDisplay.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Controls the displaying of the currently selected shop item.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelectionDisplay : MonoBehaviour
{
    [SerializeField] private Image selectionImg;
    [SerializeField] private TextMeshProUGUI selectionNameText;
    [SerializeField] private TextMeshProUGUI selectionPriceText;

    private void Start()
    {
        selectionNameText.text = string.Empty;
        selectionPriceText.text = string.Empty;
        selectionImg.enabled = false;
    }

    public void UpdateDisplay(Sprite selectionSpr, string name, int price, bool unlocked)
    {
        selectionImg.enabled = true;

        selectionImg.sprite = selectionSpr;
        selectionNameText.text = name;

        if (unlocked)
            selectionPriceText.text = "UNLOCKED";
        else
            selectionPriceText.text = price + " coins";
    }
}