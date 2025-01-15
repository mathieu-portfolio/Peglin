using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexAttack : Attack
{
  [SerializeField] private int _range = 2;
  [SerializeField] private Renderer _animationRenderer;
  [SerializeField] private float _animationHeight = 3;
  [SerializeField] private float _animationDuration = 1;

  protected override bool CanAttack()
  {
    if (!_owner.target) return false;

    var distance = Mathf.Abs(_owner.position.x - _owner.target.position.x);
    return distance <= _range && distance > 1;
  }

  protected override IEnumerator PlayAnimation()
  {
    var distance = Mathf.Abs(_owner.position.x - _owner.target.position.x);
    if (distance <= _range && distance > 1)
    {
      _animationRenderer.enabled = true;
      yield return MathsUtils.ConvexTrajectory(
        _animationRenderer.gameObject,
        _owner.transform.position + 0.5f * Vector3.up,
        _owner.target.transform.position,
        _animationHeight,
        0,
        0,
        _animationDuration);
      _animationRenderer.enabled = false;

      _damageDealer.DamageTarget(_owner.damage);
    }

    yield return null;
  }
}
