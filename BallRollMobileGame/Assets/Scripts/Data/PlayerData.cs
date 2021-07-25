/*****************************************************************************
// File Name :         PlayerData.cs
// Author :            Kyle Grenier
// Creation Date :     07/06/2021
//
// Brief Description : Holds all of the player's data.
*****************************************************************************/

[System.Serializable]
public class PlayerData
{
    // Right now, we're just storing shop items.
    // It would be wise to move coins and shop balance to here as well!
    public ShopDatabaseSave shopDatabase;

    public PlayerData(ShopDatabase database)
    {
        this.shopDatabase = new ShopDatabaseSave(database);
    }
}