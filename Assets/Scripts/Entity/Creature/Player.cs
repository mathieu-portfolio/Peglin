using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
  private MarblesManager _marblesManager;

  protected override void Start()
  {
    base.Start();
    _marblesManager = FindObjectOfType<MarblesManager>();
  }

  public override IEnumerator PlayTurn()
  {
    damage = _marblesManager.GetAttack();
    var attacks = GetComponentsInChildren<Attack>();
    foreach (var attack in attacks)
    {
      yield return attack.PlayAttack();
      Destroy(attack.gameObject);
    }

    yield return null;
  }
}
