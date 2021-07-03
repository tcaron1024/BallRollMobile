/*****************************************************************************
// File Name :         ShopDatabase.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : A ShopDatabase holds all items that are available to buy / use from the shop.
*****************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item Database", menuName = "Shop Database")]
public class ShopDatabase : ScriptableObject
{
    [SerializeField] private string _databaseName;
    public string databaseName { get { return _databaseName; } }

    [SerializeField] private ShopItem[] _items;
    public ShopItem[] items { get { return _items; } }
}
