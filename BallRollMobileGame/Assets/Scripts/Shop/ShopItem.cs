/*****************************************************************************
// File Name :         ShopItem.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Holds all of the shop information for a shop item.
*****************************************************************************/
using UnityEngine;

[System.Serializable]
public class ShopItemSave
{
    public string itemName;
    public bool unlocked;

    public ShopItemSave(string itemName, bool unlocked)
    {
        this.itemName = itemName;
        this.unlocked = unlocked;
    }
}

public class ShopItem : MonoBehaviour
{
    [Tooltip("The item's catalog icon.")]
    [SerializeField] private Sprite _iconImg;
    public Sprite iconImg { get { return _iconImg; } set { _iconImg = value; } }

    [Tooltip("The item's shop price.")]
    [SerializeField] private int _price;
    public int price { get { return _price; } set { _price = value; } }

    [Tooltip("Should this item be available to select and use?")]
    [SerializeField] private bool _unlocked;
    public bool unlocked { get { return _unlocked; } set { _unlocked = value; } }

    [Tooltip("The item's name")]
    [SerializeField] private string _itemName;
    public string itemName { get { return _itemName; } set { _itemName = value; } }

    /// <summary>
    /// Purchases the item by unlocking it.
    /// </summary>
    public void Purchase()
    {
        _unlocked = true;
    }
}
