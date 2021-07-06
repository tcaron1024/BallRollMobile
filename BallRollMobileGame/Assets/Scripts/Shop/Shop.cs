/*****************************************************************************
// File Name :         Shop.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : The shop that players can buy and select unlocked marbles from.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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



    [Header("Shop Balance Section")]
    [Tooltip("The text that displays the player's shop balance.")]
    [SerializeField] private TextMeshProUGUI balanceText;



    private void Start()
    {
        // Populate the catalog on start.
        PopulateCatalog();

        // Make sure the balance text displays the player's balance.
        UpdateBalanceText();
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
            ShopCatalogDisplay disp = Instantiate(catalogDisplayPrefab, content.transform);

            disp.Init(item.iconImg, item.unlocked);
            disp.AddBtnClickListener(() => DisplaySelection(item));
        }
    }

    /// <summary>
    /// Updates the shop balance text with the ShopBalance player pref.
    /// </summary>
    private void UpdateBalanceText()
    {
        balanceText.text = "X " + PlayerPrefs.GetInt("ShopBalance");
    }

    // TODO: Look into saving to binary file.
    // Need to make a file that contains all of the shop data the player owns.
    public void Buy(ShopItem item)
    {

    }

    /// <summary>
    /// Displays the currently selected catalog item in the larger display area.
    /// </summary>
    /// <param name="item">The ShopItem to display.</param>
    public void DisplaySelection(ShopItem item)
    {
        selectionDisplay.Init(item.iconImg, item.itemName, item.price, item.unlocked);
    }

    /// <summary>
    /// Unloads the Shop scene asynchronously.
    /// </summary>
    public void ExitShop()
    {
        SceneManager.UnloadSceneAsync("Shop");   
    }
}