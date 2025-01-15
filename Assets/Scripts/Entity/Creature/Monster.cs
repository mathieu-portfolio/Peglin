using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
  [SerializeField] private int _speed = 1;
  [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
  [SerializeField] private int _animationFrames = 60;
  [SerializeField] private DealDamage _damageDealer;
  private CreatureManager _manager;

  [SerializeField] private HealthBar _healthBarPrefab;
  private HealthBar _healthBar;

  protected override void Start()
  {
    base.Start();
    target = FindObjectOfType<Player>();
    _damageDealer.SetTarget(target.GetComponent<Damageable>());
    _manager = FindObjectOfType<CreatureManager>();

    var panel = FindObjectOfType<Canvas>().transform.GetChild(0);
    _healthBar = Instantiate(_healthBarPrefab, panel);
    _healthBar.target = this;

    var followMonster = _healthBar.GetComponent<FollowWorldObj>();
    followMonster.target = transform;
  }

  public override IEnumerator PlayTurn()
  {
    yield return Attack();
    yield return Move();
  }

  protected virtual IEnumerator Move()
  {
    var initialPos = _manager.CreatureGridToWorld(position);
    grid.MoveCreature(this, _speed);
    var targetPos = _manager.CreatureGridToWorld(position);
    var delta = targetPos - initialPos;
    
    for (int frame = 0; frame < _animationFrames; frame++)
    {
      float t = _animationCurve.Evaluate(frame / (float)_animationFrames);
      transform.position = initialPos + delta * t;
      yield return new WaitForEndOfFrame();
    }
  }

  protected virtual IEnumerator Attack()
  {
    var attacks = GetComponents<Attack>();
    foreach (var attack in attacks)
    {
      yield return attack.PlayAttack();
    }

    yield return null;
  }

  private void OnDestroy()
  {
    grid.RemoveCreature(this);
    if (_healthBar) Destroy(_healthBar.gameObject);
  }
}
