using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{

    public AshBehavior ash;
    public int colourIndex;
    public bool loaded = false;

    // Start is called before the first frame update
    public void Initialize()
    {
        if(!loaded)
        colourIndex = Random.Range(0,4);
        if(ash == null)
        {
            ash = FindObjectOfType<AshBehavior>();
        }
        Material mat = ash.materials[colourIndex]; // 3e manière de le faire où Unity va lui-meme chercher Ash
        // 1 Material mat = FindObjectOfType<AshBehavior>().materials[index];
        // 2 Material mat : ash.materials[index]; autre manière de le faire, mais il faut spécifier dans Unity que la sphère "Ash" est Ash du script.
        GetComponent<Renderer>().material = mat;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        ash.SetDestination(this);
    }
}
