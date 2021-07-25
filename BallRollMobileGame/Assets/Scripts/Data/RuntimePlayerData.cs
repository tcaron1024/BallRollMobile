/*****************************************************************************
// File Name :         RuntimePlayerData.cs
// Author :            Kyle Grenier
// Creation Date :     07/25/2021
//
// Brief Description : Holds all of the player's data that is needed 
                       throughout the game, even between scenes.
*****************************************************************************/
using UnityEngine;

public static class RuntimePlayerData
{
    /// <summary>
    /// The ball that will be used in game.
    /// </summary>
    private static GameObject _selectedBall;
    public static GameObject selectedBall
    {
        get { return _selectedBall; }
        set
        {
            _selectedBall = value;
            Debug.Log("RUNTIME_PLAYER_DATA: Selected ball is now " + _selectedBall.name);
        }
    }
}