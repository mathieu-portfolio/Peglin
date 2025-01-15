using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
  public void LoadGame()
  {
    SceneManager.LoadScene("Level 1.1");
  }
}
