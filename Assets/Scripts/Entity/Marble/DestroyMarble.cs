using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMarble : Destroyer
{

  protected override IEnumerator DestroyRoutine()
  {
    GetComponent<Collider2D>().enabled = false;
    yield return new WaitForEndOfFrame();
    GetComponent<Collider2D>().enabled = true;
    gameObject.SetActive(false);
  }
}
