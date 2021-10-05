using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class AshBehavior : MonoBehaviour
{

    public Material[] materials;
    private NavMeshAgent agent;
    private CubeBehavior cubeDestination;
    public int colourIndex;
    public bool loaded = false;

    // Start is called before the first frame update
    public void Initialize()
    {
        if(materials == null || materials.Length < 4)
        {
            Debug.LogError("This component needs 4 materials.", gameObject);
        }
        else
        {
            if(!loaded)
            {
                colourIndex = Random.Range(0,3);
            }
            GetComponent<Renderer>().material = materials[colourIndex];
        }
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(CubeBehavior cube)
    {
        agent.SetDestination(cube.transform.position);
        cubeDestination = cube;
    }

    private void ChangeColour()
    {
        int exchange = colourIndex;
        colourIndex = cubeDestination.colourIndex;
        cubeDestination.colourIndex = exchange;

        GetComponent<Renderer>().material = materials[colourIndex];
        cubeDestination.GetComponent<Renderer>().material = materials[cubeDestination.colourIndex];
        // Material mat = GetComponent<Renderer>().material;
        // GetComponent<Renderer>().material = cubeDestination.GetComponent<Renderer>().material;
        // cubeDestination.GetComponent<Renderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        if(cubeDestination != null) 
        {
            if(Vector3.Distance(cubeDestination.transform.position, transform.position) < 0.5f)
            {
                ChangeColour();
                cubeDestination = null;
            }
        }
    }
}
