using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Effect
{
  [SerializeField] private Damageable _body;

  public override void ApplyEffect(GameObject trigger)
  {
    _body.TakeDamage(1);
  }
}
