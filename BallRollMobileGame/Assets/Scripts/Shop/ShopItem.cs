/*****************************************************************************
// File Name :         ShopItem.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Holds all of the shop information for a shop item.
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;


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


    private const string SHOP_SCENE_NAME = "Shop";

    private void Awake()
    {
        // If the active scene is the Menu, remove all non-ShopItem related components 
        // so the marble won't fall to the ground, be controllable, etc.
        if (SceneManager.GetActiveScene().name == SHOP_SCENE_NAME)
        {
            Destroy(GetComponent<BallController>());
            Destroy(GetComponent<IBallMovementBehaviour>());
            Destroy(GetComponent<Health>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

            ChangeLayer(transform);
        }
    }

    private void ChangeLayer(Transform parent)
    {
        for (int i = 0; i < parent.childCount; ++i)
            ChangeLayer(parent.GetChild(i));

        parent.gameObject.layer = LayerMask.NameToLayer("UI");
    }

    /// <summary>
    /// Purchases the item by unlocking it.
    /// </summary>
    public void Purchase()
    {
        _unlocked = true;
    }
}
