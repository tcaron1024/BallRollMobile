using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySetup : MonoBehaviour
{
    //public static int scenerySettings;
    public Material cloudSceneMat;
    public Material desertSceneMat;
    Renderer r;


    // Start is called before the first frame update
    void Start()
    {
        r = gameObject.GetComponent<Renderer>();
        SetSceneSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
