using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyer : MonoBehaviour
{
  public void Destroy()
  {
    StartCoroutine(DestroyRoutine());
  }

  protected abstract IEnumerator DestroyRoutine();
}
