/*****************************************************************************
// File Name :         Shop.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : The shop that players can buy and select unlocked marbles from.
*****************************************************************************/
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Prefabs")]

    [Tooltip("The prefab that displays an item in the catalog.")]
    [SerializeField] private ShopCatalogDisplay catalogDisplayPrefab;

    [Tooltip("The database of items to aggregate the store catalog.")]
    [SerializeField] private ShopDatabase database;


    [Header("Catalog Section")]

    [Tooltip("The child game object that holds all of the store's content.")]
    [SerializeField] private GameObject content;

    [Header("Selection Display Section")]
    [Tooltip("The game object that displays the currently selected item.")]
    [SerializeField] private ShopSelectionDisplay selectionDisplay;

    private void Start()
    {
        // Populate the catalog on start.
        PopulateCatalog();
    }

    /// <summary>
    /// Populates the store catalog with items from the database.
    /// </summary>
    private void PopulateCatalog()
    {
        print("Populating shop database " + database.name);

        for (int i = 0; i < database.items.Length; ++i)
        {
            ShopItem item = database.items[i];
            Instantiate(catalogDisplayPrefab, content.transform).Init(item.iconImg, item.unlocked);
        }
    }

    /// <summary>
    /// Displays the currently selected catalog item in the larger display area.
    /// </summary>
    /// <param name="item">The ShopItem to display.</param>
    public void DisplaySelection(ShopItem item)
    {
        selectionDisplay.Init(item.iconImg, item.itemName, item.price, item.unlocked);
    }
}