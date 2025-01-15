using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : Destroyer
{
  protected override IEnumerator DestroyRoutine()
  {
    Debug.Log("Game Over");
    yield return null;
  }
}
