using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectablePlus : Selectable
{

    private bool selected = false ;
    private bool pressed = false;

    public bool isSelected(){
        return selected;
    }

    

private void Update() {
    
    if(IsPressed()){
        pressed = true ;
              
    }

    if( (IsPressed()==false) && (pressed == true)){
        selected = !selected;
        pressed = false ;
    }

    if(selected)
        this.DoStateTransition(SelectionState.Pressed,true);
    else
        this.DoStateTransition(SelectionState.Normal,true); 


    
    
}
}
