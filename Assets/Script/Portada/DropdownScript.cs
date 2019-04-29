using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class DropdownScript : MonoBehaviour
{
      //Create a List of new Dropdown options
    List<string> m_DropOptions = new List<string>();
    //This is the Dropdown
    public Dropdown m_Dropdown;

    private int value = 0 ;


    public string actualOption;


    //Get the list of ports available and update the dropdown with that list
    private void getPortNames(){
        string[] ports = SerialPort.GetPortNames();

        m_DropOptions.Clear();

        foreach(string port in ports)
        {
            m_DropOptions.Add(port);
        }

    }

  

     
    private void Start() {
        //Fetch the Dropdown GameObject the script is attached to
        //Clear the old options of the Dropdown menu
         m_Dropdown.ClearOptions();
        //Add the options created in the List above
        getPortNames();
        m_Dropdown.AddOptions(m_DropOptions);
     
     

    }


    

    private void Update() {

        value = m_Dropdown.value;
        actualOption = m_Dropdown.options[value].text;
        Info.getChoosenOption = actualOption;


        
    }
}
