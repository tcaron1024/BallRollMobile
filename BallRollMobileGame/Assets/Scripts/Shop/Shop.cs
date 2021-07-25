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
    [SerializeField] private ShopDatabase defaultDatabase;

    /// <summary>
    /// The database currently in use.
    /// </summary>
    private ShopDatabase currentDatabase;


    [Header("Catalog Section")]
    [Tooltip("The child game object that holds all of the store's content.")]
    [SerializeField] private GameObject content;


    [Header("Selection Display Section")]

    [Tooltip("The game object that displays the currently selected item.")]
    [SerializeField] private ShopSelectionDisplay selectionDisplay;

    [Tooltip("The button the player presses to purchase a locked item.")]
    [SerializeField] private Button purchaseBtn;

    [Tooltip("The button the player presses to select the ball for use in game.")]
    [SerializeField] private Button selectBallBtn;


    [Header("Debug")]

    [Tooltip("True if player should have infinite money.")]
    [SerializeField] private bool infiniteMoney;



    [Header("Shop Balance Section")]

    [Tooltip("The text that displays the player's shop balance.")]
    [SerializeField] private TextMeshProUGUI balanceText;

    // The amount of shop currency the player owns.
    private int playerShopBalance;


    private void Start()
    {
        purchaseBtn.gameObject.SetActive(false);
        selectBallBtn.gameObject.SetActive(false);

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
        PlayerData data = SaveSystem.LoadPlayer();

        // If the player has purchased an item,
        // there should be data saved, so we'll take it from there
        // so the shop is updated appropriately.
        if (data != null)
            currentDatabase = ShopDatabase.CreateFromSave(data, defaultDatabase);
        // If we don't have a saved database, we'll create a new one using the
        // default database.
        // We have to do this or else we'll be modifying the default shop list
        // directly, and we want to keep that as a base.
        else
        {
            PlayerData newData = new PlayerData(defaultDatabase);
            currentDatabase = ShopDatabase.CreateFromSave(newData, defaultDatabase);
        }

        // If there is no data to load from, use the default database.
    }

    private void SaveShopData()
    {
        SaveSystem.SavePlayerData(currentDatabase);
    }

    /// <summary>
    /// Populates the store catalog with items from the database.
    /// </summary>
    private void PopulateCatalog()
    {
        print("Populating shop database " + currentDatabase.name);

        for (int i = 0; i < currentDatabase.items.Length; ++i)
        {
            ShopItem item = currentDatabase.items[i];

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
        playerShopBalance = PlayerPrefs.GetInt("ShopBalance");
        balanceText.text = "X " + playerShopBalance;
    }

    /// <summary>
    /// Unlocks the item and displays it.
    /// </summary>
    /// <param name="item">The item to purchase.</param>
    public void Buy(ShopItem item)
    {
        if (infiniteMoney)
        {
            item.Purchase();
            DisplaySelection(item);
            SaveSystem.SavePlayerData(currentDatabase);
        }
        else if (playerShopBalance >= item.price)
        {
            item.Purchase();
            DisplaySelection(item);
            SaveSystem.SavePlayerData(currentDatabase);

            // Update player shop balance.
            playerShopBalance -= item.price;
            PlayerPrefs.SetInt("ShopBalance", playerShopBalance);
            UpdateBalanceText();

            // TODO: play coin collect sound to signify purchase
        }
        else
        {
            // TODO: Play sound, UI to alert player they CANNOT purchase, etc.
        }
    }

    /// <summary>
    /// Displays the currently selected catalog item in the larger display area.
    /// </summary>
    /// <param name="item">The ShopItem to display.</param>
    public void DisplaySelection(ShopItem item)
    {
        selectionDisplay.UpdateDisplay(item.iconImg, item.itemName, item.price, item.unlocked);

        if (item.unlocked)
        {
            selectBallBtn.onClick.RemoveAllListeners();
            selectBallBtn.onClick.AddListener(() => SelectBall(item));

            purchaseBtn.gameObject.SetActive(false);
            selectBallBtn.gameObject.SetActive(true);

            // TODO: add listeners to selectBallBtn
        }
        else
        {
            purchaseBtn.onClick.RemoveAllListeners();
            purchaseBtn.onClick.AddListener(() => Buy(item));

            selectBallBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
        }
    }

    private void SelectBall(ShopItem item)
    {
        GameObject ballPrefab = defaultDatabase.GetItem(item.itemName).gameObject;

        if (ballPrefab == null)
            Debug.LogWarning("Could not load ball prefab with name '" + item.itemName + "'. Is it in the default shop database?");
        else
        {
            RuntimePlayerData.selectedBall = ballPrefab;
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