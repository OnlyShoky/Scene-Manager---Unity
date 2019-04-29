using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class ContentController : MonoBehaviour
{

    public GameObject controllerObject ;



    public List<GameObject> gameObjectList;
    
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public InputField commandText;
    


    public void AddObjectToList(string name){


        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        int index = gameObjectList.Count -1;
        
        print(index) ;
    
        Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
        gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
        components[0].text = gameObjectList.Count.ToString();
        components[1].text = commandText.text;

        commandText.text = "";

    }

    public void RemoveLastObject(){
        print("Nombre de gameobject antes de elminacion : " + gameObjectList.Count);
        Destroy(gameObjectList[gameObjectList.Count-1]);
        gameObjectList.Remove(gameObjectList[gameObjectList.Count-1]);
        print("Nombre de gameobject despues de elminacion : " + gameObjectList.Count);

    }

    public void SaveList(){
        string json = JsonUtility.ToJson(gameObjectList);
        Debug.Log(json);
    }

    public void PlayAllCommands(){
        foreach (var item in gameObjectList)
        {
            Text[] components = item.GetComponentsInChildren<Text>();
            Controller controller = controllerObject.GetComponentInChildren<Controller>();
            print(controller.name + ": "+ components[1].text);   
            controller.WriteToArduino(components[1].text);

              

        }

    }


 
}
