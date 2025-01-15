using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    var marble = collision.gameObject.GetComponent<Marble>();
    if (marble) marble.ApplyEffects(gameObject);
    GetComponent<Ball>().bounces++;
  }
}
