/*****************************************************************************
// File Name :         ShopSelectionDisplay.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Controls the displaying of the currently selected shop item.
*****************************************************************************/
using UnityEngine;
using TMPro;

public class ShopSelectionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectDisplayParent;
    [SerializeField] private TextMeshProUGUI selectionNameText;
    [SerializeField] private TextMeshProUGUI selectionPriceText;

    private GameObject currentlySelectedObject;


    public void UpdateDisplay(GameObject selectionGameObject, string name, int price, bool unlocked)
    {
        if (currentlySelectedObject != null)
            currentlySelectedObject.SetActive(false);

        currentlySelectedObject = selectionGameObject;

        selectionGameObject.transform.SetParent(gameObjectDisplayParent.transform,false);
        selectionGameObject.transform.localPosition = Vector3.zero;
        selectionGameObject.SetActive(true);


        selectionNameText.text = name;

        if (unlocked)
            selectionPriceText.text = "UNLOCKED";
        else
            selectionPriceText.text = price + " coins";
    }
}