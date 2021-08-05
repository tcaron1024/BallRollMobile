/*****************************************************************************
// File Name :         MenuAudioListener.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioListener : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += Disable;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= Disable;
    }

    private void Disable(Scene currentScene, Scene newScene)
    {
        print("CURRENT SCENE: " + currentScene.name);
        print("NEW SCENE: " + newScene.name);

        if (newScene.name == "Menu")
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
