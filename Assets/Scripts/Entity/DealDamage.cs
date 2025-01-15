using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
  private Damageable _target;

  public void SetTarget(Damageable target)
  {
    _target = target;
  }

  public void DamageTarget(int damage)
  {
    DamageTarget(damage, _target);
  }

  public void DamageTarget(int damage, Damageable target)
  {
    target.TakeDamage(damage);
  }
}
