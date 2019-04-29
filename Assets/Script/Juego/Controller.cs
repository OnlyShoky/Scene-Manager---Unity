using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO.Ports;

 
public class Controller : MonoBehaviour
{

    public string port;

    SerialPort stream ;

    private void Start() {
        //Choose the port choosed in the dropdown in Portada Scene
        print(Info.getChoosenOption);
        port = Info.getChoosenOption;
        stream = new SerialPort(port,9600);
     
        //Open the port
        stream.Open();
        print("Puerto Abierto Satisfactoriamente");

    }
    
    // Write in the serial port of arduino
    public void WriteToArduino(string message) {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    //Changing the scene
    public void ChangeScene(string name){
        
        print("Cambiando a la escena "+ name);
        SceneManager.LoadScene(name);
        stream.Close();
        
    }

    

    public void Salir(){

        print("Saliendo del juego");
        Application.Quit();
    }


    //Read the serial port from arduino
    public string ReadFromArduino (int timeout = 0) {
        stream.ReadTimeout = timeout;  
        
        try {
            return stream.ReadLine();
        }
        catch (System.Exception) {
            return null;
        }
    }

    // Print all messages geted from arduino
    private void Update() {
        string message = ReadFromArduino(50);
        if(message != null){
            print(message);
        }

    }



}
