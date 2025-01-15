using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
  [SerializeField] LevelManager _manager;

  private void OnTriggerExit2D(Collider2D collider)
  {
    var ball = collider.GetComponent<Ball>();
    if (ball)
    {
      Destroy(collider.gameObject);
      if (FindObjectsOfType<Ball>().Length == 1)
      {
        _manager.EndTurn();
      }
    }
  }
}
