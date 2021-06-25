using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySetup : MonoBehaviour
{
    //public static int scenerySettings;
    public Material cloudSceneMat;
    public Material desertSceneMat;
    public Material arcticSceneMat;
    Renderer r;


    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Renderer>();
        SetSceneSettings();
    }

    void SetSceneSettings()
    {
        if(GameController.scenerySettings == 1)
        {
            r.material = cloudSceneMat;
        }
        else if(GameController.scenerySettings == 2)
        {
            r.material = desertSceneMat;
        }
        else if(GameController.scenerySettings == 3)
        {
            r.material = arcticSceneMat;
        }
    }
}
