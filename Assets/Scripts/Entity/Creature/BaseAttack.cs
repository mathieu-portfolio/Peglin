using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAttack : Attack
{
  [SerializeField] private Animator _animator;
  [SerializeField] private int _range = 1;

  protected override bool CanAttack()
  {
    if (!_owner.target) return false;
    var distance = Mathf.Abs(_owner.position.x - _owner.target.position.x);
    return distance <= _range;
  }

  protected override IEnumerator PlayAnimation()
  {
    _animator.SetTrigger("Base Attack");
    yield return new WaitForSeconds(0.5f);
    _damageDealer.DamageTarget(_owner.damage);
  }
}
