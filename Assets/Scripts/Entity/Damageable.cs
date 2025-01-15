using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
  [SerializeField] private int _maxPv = 1;
  [SerializeField] protected int _pv;

  [SerializeField] private Destroyer _destroyer;

  protected virtual void Start()
  {
    _pv = _maxPv;
  }

  public virtual void TakeDamage(int amount)
  {
    _pv -= amount;
    if (_pv <= 0)
    {
      _destroyer.Destroy();
    }
  }

  public int GetMaxPV()
  {
    return _maxPv;
  }

  public int GetPV()
  {
    return _pv;
  }
}
