using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Damageable
{
  [SerializeField] private Animator _animator;

  public override void TakeDamage(int amount)
  {
    base.TakeDamage(amount);
    _animator.SetBool("Lit", _pv <= 1);
  }
}
