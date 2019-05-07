using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;




public class ContentController : MonoBehaviour
{

    public GameObject controllerObject ;
    public List<GameObject> gameObjectList;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public InputField commandText,startTime,durationTime;

    public Text timeText ;

    private List<string> contentDataList;

    private float executionTime = 0 ;

    private bool playP = false ;


    private bool VerifyCommand(string command){


        string[] fileText = File.ReadAllLines(Application.dataPath+"/DATA/CommandReference.txt");

        foreach(var line in fileText){
            // Line to catch the command name between "" 
            string commandReference = line.Split(',')[0].Substring(1,line.Split(',')[0].Length-2);

            if(command.Equals(commandReference))
                return true;

        }

        return false;

    }

    //Verify that the current element that we will add , has a startTime bigger that the last element
    private bool VerifyOrderLogic(string startTime){

        int index = gameObjectList.Count -1;

        if(index >=0){

            if(startTime =="")
                return false ;
                
            Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();        

            // Calculate the total duration  
            string[] startSeparadoPrevious =  components[2].text.Split(':');
            string[] startSeparado =  startTime.Split(':');



            long value = TimeToFloat(startTime);
            long valuePrevious = int.Parse(startSeparadoPrevious[0])*3600 + int.Parse(startSeparadoPrevious[1])*60 + int.Parse(startSeparadoPrevious[2]) ;

            if(value >= valuePrevious)
                return true;
            else
                return false;
        }else        
            return true;
        
        

    }

    // Transform a string on the format HH:MM:SS to an int corresponding of the number of seconds
    private long TimeToFloat(string timeInFormat){
        string[] startSeparado =  timeInFormat.Split(':');

        int value =0 ;
        if(startSeparado.Length ==1)
            value = int.Parse(startSeparado[0]);
        else 
            if(startSeparado.Length ==2)
                value =  int.Parse(startSeparado[0])*60 + int.Parse(startSeparado[1]) ;
            else
                value = int.Parse(startSeparado[0])*3600 + int.Parse(startSeparado[1])*60 + int.Parse(startSeparado[2]) ;

        return value;
                
    }

    //Add a command to the list , the text in the input field will be the command , and the number is the last number plus one
    public void AddObjectToList(){

        if(VerifyCommand(commandText.text)){
            if(VerifyOrderLogic(startTime.text)){
                gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
                int index = gameObjectList.Count -1;
                    
                Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
                gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
                components[0].text = gameObjectList.Count.ToString();
                components[1].text = commandText.text;
                
                if(startTime.text !="")
                    components[2].text = startTime.text ;
                else              
                    components[2].text = "0";
                    
                if(durationTime.text !="")
                    components[3].text = durationTime.text;
                else              
                    components[3].text = "1";
            


                // Calculate the total duration
                string[] startSeparado =  components[2].text.Split(':');
                string[] durationSeparado =  components[3].text.Split(':');

                components[4].text= "" ;
                components[2].text= "" ;
                components[3].text= "" ;

            
                // Number of parameters to define hours , minutes , seconds , in this case 3 because we use the system hh:mm:ss
                int totalParams = 3 ;

                // Convert the Starttime and durationtime on the format HH:MM:SS and sum this two times to create end duration , with the same format
                for(int i =0 ; i < 3 ; i++){
                    int value = 0;

                    if(totalParams-i <= startSeparado.Length){
                        value = value + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]) ;
                        if(startSeparado.Length -(totalParams-i) < startSeparado.Length-1)
                            components[2].text = components[2].text + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]).ToString("00") +":";
                        else
                            components[2].text = components[2].text + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]).ToString("00");
                    }else
                        components[2].text = components[2].text + "00:";
            

                    if(totalParams-i <= durationSeparado.Length){
                        value = value + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]) ;
                        if(durationSeparado.Length -(totalParams-i) < durationSeparado.Length-1)
                            components[3].text = components[3].text + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]).ToString("00") +":";
                        else
                            components[3].text = components[3].text + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]).ToString("00") ;
                    }else
                        components[3].text = components[3].text + "00:";

                    if(i<totalParams-1)
                        components[4].text = components[4].text + value.ToString("00") + ":";
                    else
                        components[4].text = components[4].text + value.ToString("00") ;
                }


                //Erase the text in the input field
                commandText.text = "";
                durationTime.text= "";
                startTime.text="";

                //Keep the inputfield activated for continu to tap.
                commandText.ActivateInputField();
        }
            else
                EditorUtility.DisplayDialog("Error!","Start time bigger than the last command","OK");
        }else
           EditorUtility.DisplayDialog("Error!","Command not found on the file CommandReference.txt","OK");
        


    }

    //Overide method to add and object choosing the name
    public void AddObjectToList(string name,string startTime, string durationTime){

        gameObjectList.Add(Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity,gameObject.transform));
        int index = gameObjectList.Count -1;
            
        Text[] components = gameObjectList[index].GetComponentsInChildren<Text>();
        gameObjectList[index].name = "(" + gameObjectList.Count.ToString() + ")";
        components[0].text = gameObjectList.Count.ToString();
        components[1].text = name;

        components[2].text = startTime;
        components[3].text = durationTime;


        // Calculate the total duration
        string[] startSeparado =  components[2].text.Split(':');
        string[] durationSeparado =  components[3].text.Split(':');

        components[4].text= "" ;
        components[2].text= "" ;
        components[3].text= "" ;

    
        // Number of parameters to define hours , minutes , seconds , in this case 3 because we use the system hh:mm:ss
        int totalParams = 3 ;

        // Convert the Starttime and durationtime on the format HH:MM:SS and sum this two times to create end duration , with the same format
        for(int i =0 ; i < 3 ; i++){
            int value = 0;

            if(totalParams-i <= startSeparado.Length){
                value = value + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]) ;
                if(startSeparado.Length -(totalParams-i) < startSeparado.Length-1)
                    components[2].text = components[2].text + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]).ToString("00") +":";
                else
                    components[2].text = components[2].text + int.Parse(startSeparado[startSeparado.Length -(totalParams-i)]).ToString("00");
            }else
                components[2].text = components[2].text + "00:";
    

            if(totalParams-i <= durationSeparado.Length){
                value = value + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]) ;
                if(durationSeparado.Length -(totalParams-i) < durationSeparado.Length-1)
                    components[3].text = components[3].text + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]).ToString("00") +":";
                else
                    components[3].text = components[3].text + int.Parse(durationSeparado[durationSeparado.Length -(totalParams-i)]).ToString("00") ;
            }else
                components[3].text = components[3].text + "00:";

            if(i<totalParams-1)
                components[4].text = components[4].text + value.ToString("00") + ":";
            else
                components[4].text = components[4].text + value.ToString("00") ;
        }


    }

    //Remove the last object of the list
    public void RemoveLast(){
        Destroy(gameObjectList[gameObjectList.Count-1]);
        gameObjectList.Remove(gameObjectList[gameObjectList.Count-1]);

    }

    //Remove all the elements from the list
    public void RemoveAll(){
        int nbElements = gameObjectList.Count;
        for(int i = 0; i<nbElements ; i++){
                RemoveLast();
            }
    }

    //Remove the item that we pass in parameter
    private void removeItem(GameObject gameObject){
        Destroy(gameObject);
        gameObjectList.Remove(gameObject);

    }



    //Save list to json
    public void SaveList(){
        
        
        CommandList commandList = new CommandList();
        List<string> contentDataList = new List<string>();
        List<string> startTimeList= new List<string>();
        List<string> durationTimeList= new List<string>();
        
        // We save all our gameobjectlist in a contentDataList 
        //for save commands because we can't save instanciate GameObjects 
        //, so we just need to save commands and save 
        // them in the same order
        foreach (var item in gameObjectList)
        {
            Text[] components = item.GetComponentsInChildren<Text>();
            Controller controller = controllerObject.GetComponentInChildren<Controller>();
            contentDataList.Add(components[1].text);
            startTimeList.Add(components[2].text);
            durationTimeList.Add(components[3].text);

        }

        commandList.contentDataList = contentDataList ;
        commandList.startTimeList = startTimeList ;
        commandList.durationTimeList = durationTimeList ;

        string json = JsonUtility.ToJson(commandList);

        // Open file explorer to choose what json we want to save
        string path = EditorUtility.SaveFilePanel(
            "Save command list as Json",
            "",
            "commandSaveList"+ ".json",
            "json");

        if (path.Length != 0)
        {
            File.WriteAllText(path,json);

        }
 
        print("File Saved");
        

 
    }

    public void LoadList(){

        // Open file explorer to choose what json we want to load
        string path = EditorUtility.OpenFilePanel("Overwrite with json", Application.dataPath+"/DATA/", "json");
        if (path.Length != 0)
        {
            string json = File.ReadAllText(path);
            CommandList loadedGameObjectList = JsonUtility.FromJson<CommandList>(json);

            RemoveAll();


            for(int i=0 ; i< loadedGameObjectList.contentDataList.Count ;i++){
                AddObjectToList(loadedGameObjectList.contentDataList[i],loadedGameObjectList.startTimeList[i],
                loadedGameObjectList.durationTimeList[i]);
            }
        
        }
    
    

    }

    
    [System.Serializable]
    private class CommandList{
        public List<string> contentDataList;
        public List<string> startTimeList;
        public List<string> durationTimeList;

    }


    //Excute all the commands from the list
    private void PlayAllCommands(){
        
        if(playP){
            executionTime += Time.deltaTime % long.MaxValue;

            long maxEndTime= 0 ;

            //Update of timerText 
            int valueM = (int) executionTime /60 ;
            int valueS = (int) executionTime %60 ;
            int valueH = valueM /60 ;
            valueM = valueM % 60 ;

            timeText.text =valueH.ToString("00") +":" +valueM.ToString("00") +":" + valueS.ToString("00");

            //Command update , choose all the commands to send
            string totalCommand = "";
            Controller controller = controllerObject.GetComponentInChildren<Controller>();
        
            for(int i =0 ; i< gameObjectList.Count ; i++){
                

                // The \r indicates the end of a command
                Text[] components = gameObjectList[i].GetComponentsInChildren<Text>();
                long startTime = TimeToFloat(components[2].text);
                long endTime = TimeToFloat(components[4].text);
                long actualTime = (long) executionTime ;

                if(maxEndTime< endTime)
                    maxEndTime= endTime ;


                //Picking the commands when its their time and changing the color to known which is activated
                if(actualTime >= startTime && actualTime < endTime ){
                    totalCommand = totalCommand + components[1].text +"\r" ;
                    gameObjectList[i].GetComponent<Image>().color = Color.green;
                }else
                {
                    gameObjectList[i].GetComponent<Image>().color = Color.white;
                }
            }
            
            if(totalCommand != ""){
                controller.WriteToArduino(totalCommand);
            }

            //Stops when it's finish
            if(maxEndTime < (long)executionTime)
                StopPlayer();

        }


    }

    public void StartPlayer(){
        playP = true ;
        
    }

    public void StopPlayer(){
        playP = false; ;
        executionTime= 0 ;
        timeText.text = "00:00:00";

    }

    public void PausePlayer(){
        playP = false; 
    }


    //AddObjectToList if we detect "Enter" keytouch
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Return) ) {
            AddObjectToList();         
        }

        if (Input.GetKeyDown(KeyCode.Delete) ) {

            List<GameObject> objectsToDelte = new List<GameObject>();
            int decalage = 0 ;
            foreach(var item in gameObjectList){

                
                Text[] components = item.GetComponentsInChildren<Text>();
                int order = int.Parse(components[0].text) -decalage ;
                item.name = "(" + order.ToString() + ")";
                components[0].text = order.ToString();
                

           
                SelectablePlus selectable = item.GetComponent<SelectablePlus>();
                
                if(selectable.isSelected()){
                    objectsToDelte.Add(item);
                    decalage++;
                }

            }

            foreach(var item in objectsToDelte ){
                removeItem(item);
            }
        }

        PlayAllCommands();
    }



 
}
