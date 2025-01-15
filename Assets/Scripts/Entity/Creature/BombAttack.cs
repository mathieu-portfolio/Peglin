using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombAttack : Attack
{
  [SerializeField] private int _damage = 50;
  [SerializeField] private float _throwDistance = 10;
  [SerializeField] private Renderer _animationRenderer;
  [SerializeField] private float _animationDuration = 1;
  [SerializeField] private float _animationHeight = 2;
  private Vector3 _animationStart;
  private Vector3 _animationEnd;

  protected override bool CanAttack()
  {
    return true;
  }

  protected override IEnumerator PlayAnimation()
  {
    _animationStart = _owner.transform.position + 0.5f * Vector3.up;
    _animationEnd = _animationStart + Vector3.right * _throwDistance;

    yield return MoveBombInArc();

    var monsters = FindObjectsOfType<Monster>().Where(m => m.GetComponent<Damageable>() != null);
    foreach (var monster in monsters)
    {
      _damageDealer.DamageTarget(_damage, monster.GetComponent<Damageable>());
    }

    yield return null;
  }

  private IEnumerator MoveBombInArc()
  {
    _animationRenderer.enabled = true;

    yield return MathsUtils.ConvexTrajectory(
      _animationRenderer.gameObject, _animationStart, _animationEnd, _animationHeight, _animationDuration, false);

    Destroy(_animationRenderer.gameObject);
  }
}
