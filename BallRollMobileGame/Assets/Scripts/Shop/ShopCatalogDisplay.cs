/*****************************************************************************
// File Name :         ShopCatalogDisplay.cs
// Author :            Kyle Grenier
// Creation Date :     07/03/2021
//
// Brief Description : Controls the displaying of an item in the shop catalog.
*****************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCatalogDisplay : MonoBehaviour
{
    [Tooltip("The catalog item's image.")]
    [SerializeField] private Image iconImg;

    [Tooltip("The catalog item's text signifying if it's been unlocked.")]
    [SerializeField] private TextMeshProUGUI unlockedTxt;

    // The Button component that the player can click to get more information.
    private Button btn;

    private void Awake()
    {
        btn = GetComponentInChildren<Button>();
    }


    /// <summary>
    /// Initializes the display by setting the sprite icon.
    /// </summary>
    /// <param name="spr">The sprite to use for the icon.</param>
    /// <param name="unlocked">True if the item to display is unlocked (available for use).</param>
    public void Init(Sprite spr, bool unlocked)
    {
        iconImg.sprite = spr;
        unlockedTxt.text = (unlocked ? "UNLOCKED" : "LOCKED");
        // TODO: Add functionality for if unlocked or not.
    }

    /// <summary>
    /// Adds a listener to the button's onClick event.
    /// </summary>
    /// <param name="call">The callback function.</param>
    public void AddBtnClickListener(UnityEngine.Events.UnityAction call)
    {
        btn.onClick.AddListener(call);
    }
}