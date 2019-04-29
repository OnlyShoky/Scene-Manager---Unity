using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;




public class ContentController : MonoBehaviour
{

    public GameObject controllerObject ;
    public List<GameObject> gameObjectList;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public InputField commandText;
    private List<string> contentDataList;



    //Add a command to the list , the text in the input field will be the command , and the number is the last number plus one
    public void AddObjectToList(){

        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        int index = gameObjectList.Count -1;
            
        Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
        gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
        components[0].text = gameObjectList.Count.ToString();
        components[1].text = commandText.text;

        commandText.text = "";

    }

    public void AddObjectToList(string name){

        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        int index = gameObjectList.Count -1;
            
        Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
        gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
        components[0].text = gameObjectList.Count.ToString();
        components[1].text = name;


    }

    //Remove the last object of the list
    public void RemoveLastObject(){
        Destroy(gameObjectList[gameObjectList.Count-1]);
        gameObjectList.Remove(gameObjectList[gameObjectList.Count-1]);

    }

    //Save list to json
    public void SaveList(){
        
        
        CommandList commandList = new CommandList();
        List<string> contentDataList = new List<string>();
        
        foreach (var item in gameObjectList)
        {
            Text[] components = item.GetComponentsInChildren<Text>();
            Controller controller = controllerObject.GetComponentInChildren<Controller>();
            contentDataList.Add(components[1].text);
        }

        commandList.contentDataList = contentDataList ;
        string json = JsonUtility.ToJson(commandList);
        File.WriteAllText(Application.dataPath+"/DATA/saveFile.json",json);
        print(Application.dataPath);
        print(json);
        

 
    }

    public void LoadList(){

        string json = File.ReadAllText(Application.dataPath+"/DATA/saveFile.json");
        CommandList loadedGameObjectList = JsonUtility.FromJson<CommandList>(json);
        int nbElements = gameObjectList.Count;

        for(int i = 0; i<nbElements ; i++){
            RemoveLastObject();
        }
        foreach(var item in loadedGameObjectList.contentDataList){
            AddObjectToList(item);
        }

    }

    //Excute all the commands from the list
    public void PlayAllCommands(){
        foreach (var item in gameObjectList)
        {
            Text[] components = item.GetComponentsInChildren<Text>();
            Controller controller = controllerObject.GetComponentInChildren<Controller>();
            print(controller.name + ": "+ components[1].text);   
            controller.WriteToArduino(components[1].text);

        }

    }

    [System.Serializable]
    public class CommandList{
        public List<string> contentDataList;

    }




 
}
