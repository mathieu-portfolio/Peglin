using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
  public void Quit()
  {
    #if UNITY_EDITOR
      EditorApplication.ExitPlaymode();
    #else
      Application.Quit();
    #endif
    Debug.Log("Application Quit / Play Mode Exited");
  }
}
