/*****************************************************************************
// File Name :         ShopDatabaseSave.cs
// Author :            Kyle Grenier
// Creation Date :     07/06/2021
//
// Brief Description : A shop database that can be saved to disk.
*****************************************************************************/

[System.Serializable]
public class ShopDatabaseSave
{
    private string _databaseName;
    public string databaseName { get { return _databaseName; } }

    private ShopItemSave[] _items;
    public ShopItemSave[] items { get { return _items; } }
    
    /// <summary>
    /// Copies a Scriptable Object ShopDatabase into this saveable class.
    /// </summary>
    /// <param name="so_database">The scriptable object ShopDatabase.</param>
    public ShopDatabaseSave(ShopDatabase so_database)
    {
        this._databaseName = so_database.databaseName;

        _items = new ShopItemSave[so_database.items.Length];

        for (int i = 0; i < _items.Length; ++i)
        {
            _items[i] = new ShopItemSave(so_database.items[i].itemName, so_database.items[i].unlocked);
        }
    }
}