/*****************************************************************************
// File Name :         ShopItem.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Holds all of the shop information for a shop item.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Tooltip("The item's catalog icon.")]
    [SerializeField] private Sprite _iconImg;
    public Sprite iconImg { get { return _iconImg; } }

    [Tooltip("The item's shop price.")]
    [SerializeField] private int _price;
    public int price { get { return _price; } }

    [Tooltip("Should this item be available to select and use?")]
    [SerializeField] private bool _unlocked;
    public bool unlocked { get { return _unlocked; } }

    [Tooltip("The item's name")]
    [SerializeField] private string _itemName;
    public string itemName { get { return _itemName; } }
}
