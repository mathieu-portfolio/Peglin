using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMonster : Destroyer
{
  protected override IEnumerator DestroyRoutine()
  {
    Destroy(gameObject);
    yield return null;
  }
}
