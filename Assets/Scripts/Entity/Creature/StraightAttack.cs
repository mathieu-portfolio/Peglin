using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : Attack
{
  [SerializeField] private Renderer _animation;
  [SerializeField] private float _animationSpeed = 5;
  private CreatureGrid _creatureGrid;

  private void OnEnable()
  {
    _creatureGrid = FindObjectOfType<CreatureGrid>();
  }

  protected override bool CanAttack()
  {
    Creature creature;
    if (!_creatureGrid.FindNearestCreature(_owner.position, Vector2Int.right, 15, out creature))
    {
      return false;
    }

    _target = creature.GetComponent<Damageable>();
    return _target != null;
  }

  protected override IEnumerator PlayAnimation()
  {
    yield return MoveAnimation();
    _damageDealer.DamageTarget(_owner.damage, _target);
  }

  private IEnumerator MoveAnimation()
  {
    _animation.enabled = true;

    var start = _owner.transform.position + 0.5f * Vector3.up;
    var end = _target.transform.position + 0.5f * Vector3.up;
    float startTime = Time.time;
    float journeyLength = Vector3.Distance(start, end);

    while (Vector3.Dot(end - _animation.transform.position, end - start) > 0)
    {
      float distCovered = (Time.time - startTime) * _animationSpeed;
      float fracJourney = distCovered / journeyLength;
      _animation.transform.position = Vector3.Lerp(start, end, fracJourney);
      yield return null;
    }

    Destroy(_animation);
  }
}
