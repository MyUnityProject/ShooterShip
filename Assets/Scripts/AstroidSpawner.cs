using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour {
    /// <summary>
    /// This script is used to spawn astroids from the center of the sides of the screen
    /// </summary>

    [SerializeField]
    GameObject astroid;

    //List to store (x,y) of spawn points

    Vector3[] direction = new Vector3[4];


    // Use this for initialization
    void Start () {
        ScreenUtils.Initialize();
        SetPoints();
        SpawnAstroids();
        
    }

    // Update is called once per frame
    void Update () {
		
	}

    void SetPoints()
    {

        //Set up the 4 points to spawn
        direction[0] = new Vector3(ScreenUtils.ScreenLeft, (ScreenUtils.ScreenTop + ScreenUtils.ScreenBottom) / 2, 0);
        direction[1] = new Vector3((ScreenUtils.ScreenLeft + ScreenUtils.ScreenRight) / 2, ScreenUtils.ScreenBottom, 0);
        direction[2] = new Vector3(ScreenUtils.ScreenRight, (ScreenUtils.ScreenTop + ScreenUtils.ScreenBottom) / 2, 0);
        direction[3] = new Vector3((ScreenUtils.ScreenLeft + ScreenUtils.ScreenRight) / 2, ScreenUtils.ScreenTop, 0);



    }


    void SpawnAstroids()
    {
        
        for (int i = 0;i < 4;i++)
        {
            
            
            GameObject thisAstroid = Instantiate(astroid, direction[i], transform.rotation);
            thisAstroid.GetComponent<Astroid>().Initialize(i*90,2);

        }
    }


}
