using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void Change(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeWithLoad(string name)
    {
        FindObjectOfType<LevelManager>().loaded = true;
        Change(name);
    }
}
