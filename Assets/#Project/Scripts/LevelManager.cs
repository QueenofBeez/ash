using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // 1
using System.IO; // 2
using System.Runtime.Serialization.Formatters.Binary; // " --> Faut les ajouter pour le système de sauvegarde

[Serializable]

public class GameData
{
    public int AshColour;
    public int[] CubesColour;
    public float[] ashPosition = new float[3];
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool loaded = false;

    public void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat");
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            GameData data = new GameData();
            AshBehavior ash = FindObjectOfType<AshBehavior>();

            data.AshColour = ash.colourIndex;
            data.ashPosition[0] = ash.transform.position.x;
            data.ashPosition[1] = ash.transform.position.y;
            data.ashPosition[2] = ash.transform.position.z;

            CubeBehavior[] cubes = FindObjectsOfType<CubeBehavior>();
            data.CubesColour = new int[cubes.Length];
            for(int i = 0; i < cubes.Length; i++)
            {
                data.CubesColour[i] = cubes[i].colourIndex;
            }
            bf.Serialize(file,data);
        }
        finally
        {
            file.Close();
        }
    }

    public void Load()
    {
        GameData data = null; 

        if(File.Exists(Application.persistentDataPath + "/data.dat")) 
        {
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                data = bf.Deserialize(file) as GameData;
            }
            finally
            {
                file.Close();
            }
        }

        Initialize(data);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        Debug.Log("Space key was pressed.");
        {
            Save();
            Application.Quit();
        }
    }

    public void Initialize(GameData data = null)
    {
        // Si on ne passe pas d'arguments, il le considérera null
        

        AshBehavior ash = FindObjectOfType<AshBehavior>();
        CubeBehavior[] cubes = FindObjectsOfType<CubeBehavior>();

        if(data != null)
        {
            ash.colourIndex = data.AshColour;
            ash.loaded = true;
            Vector3 position = new Vector3(
                data.ashPosition[0],
                data.ashPosition[1],
                data.ashPosition[2]);

            ash.transform.position = position;
            for(int i = 0; i < cubes.Length; i++)
            {
                cubes[i].colourIndex = data.CubesColour[i];
                cubes[i].loaded = true;
            }
        }

        ash.Initialize();
        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].Initialize();
        }
    }
}
