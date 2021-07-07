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
using UnityEngine.UI;

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

    [Tooltip("The button the player presses to purchase a locked item.")]
    [SerializeField] private Button purchaseBtn;



    [Header("Shop Balance Section")]
    [Tooltip("The text that displays the player's shop balance.")]
    [SerializeField] private TextMeshProUGUI balanceText;


    private void Start()
    {
        purchaseBtn.gameObject.SetActive(false);

        // Loads any pre-existing shop data (i.e. from previous play throughs).
        // If no shop data is found, the default shop database is used.
        LoadShopData();

        // Populate the catalog on after database retrieval.
        PopulateCatalog();

        // Make sure the balance text displays the player's balance.
        UpdateBalanceText();
    }


    private void LoadShopData()
    {
        SaveSystem.ClearData();
        PlayerData data = SaveSystem.LoadPlayer();

        // If the player has purchased an item,
        // there should be data saved, so we'll take it from there
        // so the shop is updated appropriately.
        if (data != null)
            database = ShopDatabase.CreateFromSave(data, database);

        // If there is no data to load from, use the default database.
    }

    private void SaveShopData()
    {
        SaveSystem.SavePlayerData(database);
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

            // Display the first item in the catalog so we're not displaying
            // empty space.
            if (i == 0)
                DisplaySelection(item);
        }
    }

    /// <summary>
    /// Updates the shop balance text with the ShopBalance player pref.
    /// </summary>
    private void UpdateBalanceText()
    {
        balanceText.text = "X " + PlayerPrefs.GetInt("ShopBalance");
    }

    /// <summary>
    /// Unlocks the item and displays it.
    /// </summary>
    /// <param name="item">The item to purchase.</param>
    public void Buy(ShopItem item)
    {
        item.Purchase();
        DisplaySelection(item);
        SaveSystem.SavePlayerData(database);
    }

    /// <summary>
    /// Displays the currently selected catalog item in the larger display area.
    /// </summary>
    /// <param name="item">The ShopItem to display.</param>
    public void DisplaySelection(ShopItem item)
    {
        selectionDisplay.UpdateDisplay(item.iconImg, item.itemName, item.price, item.unlocked);

        if (item.unlocked)
            purchaseBtn.gameObject.SetActive(false);
        else
        {
            purchaseBtn.onClick.RemoveAllListeners();
            purchaseBtn.onClick.AddListener(() => Buy(item));

            purchaseBtn.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Unloads the Shop scene asynchronously.
    /// </summary>
    public void ExitShop()
    {
        SceneManager.UnloadSceneAsync("Shop");   
    }
}