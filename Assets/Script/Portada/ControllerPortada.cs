using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO.Ports;



 


public class ControllerPortada : MonoBehaviour
{
    
     
    

    public void ChangeScene(string name){
        
        print("Cambiando a la escena "+ name);
        SceneManager.LoadScene(name);

        
    }
    

    public void Salir(){

        print("Saliendo del juego");
        Application.Quit();
    }

   

}
