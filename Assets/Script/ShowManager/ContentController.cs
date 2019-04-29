using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ContentController : MonoBehaviour
{

    public List<GameObject> gameObjectList;
    
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;


    public void AddObjectToList(){

        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        int index = gameObjectList.Count -1;
        
        print(index) ;
    
        Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
        gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
        components[0].text = gameObjectList.Count.ToString();
        components[1].text = "show"+ gameObjectList.Count.ToString();


    }

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        /* // Instantiate at position (0, 0, 0) and zero rotation.

        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));


        print(gameObjectList[0].name);
        Text[] components = gameObjectList[0].GetComponentsInChildren<Text>();

        components[0].text = "18";
        print(components[0].name);
        components[1].text = "show3";
        print(components[1].name);

        */

    }
}
