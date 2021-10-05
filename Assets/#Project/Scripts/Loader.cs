using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager manager = FindObjectOfType<LevelManager>();
        if (manager.loaded)
        {
            manager.Load();
        }
        else
        {
            manager.Initialize();
        }
    }
}
