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
    // The name of the shop database.
    public string databaseName;

    // The items this database contains.
    public ShopItem[] items;

    public ShopItem GetItem(string name)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            if (items[i].itemName == name)
                return items[i];
        }

        return null;
    }

    /// <summary>
    /// Returns a ShopDatabase with data populated from a saved shop database.
    /// </summary>
    /// <param name="savedData">The saved PlayerData.</param>
    /// <param name="defaultDatabase">The default database.</param>
    /// <returns>A ShopDatabase with data populated from a saved shop database.</returns>
    public static ShopDatabase CreateFromSave(PlayerData savedData, ShopDatabase defaultDatabase)
    {
        ShopDatabase db = CreateInstance<ShopDatabase>();
        // TODO: Check to see if the items are empty or if they're dupped.
        db.databaseName = savedData.shopDatabase.databaseName;
        db.items = new ShopItem[defaultDatabase.items.Length];

        // Instantiating clones of the shop items so we don't update the
        // actual prefabs.
        for (int i = 0; i < db.items.Length; ++i)
        {
            ShopItem itemClone = Instantiate(defaultDatabase.items[i]);
            itemClone.gameObject.SetActive(false);
            db.items[i] = itemClone;
        }

        foreach(ShopItemSave savedItem in savedData.shopDatabase.items)
        {
            foreach(ShopItem item in db.items)
            {
                if (item.itemName == savedItem.itemName)
                    item.unlocked = savedItem.unlocked;
            }
        }

        return db;
    }
}