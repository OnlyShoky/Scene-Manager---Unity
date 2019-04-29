using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Info 
{
  
  private static string choosenOption;

  public static string getChoosenOption{
      get{
          return choosenOption;
      }
      set{
          choosenOption = value;
      }
  }




}
