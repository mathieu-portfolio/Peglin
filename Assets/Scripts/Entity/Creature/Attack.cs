using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
  protected DealDamage _damageDealer;
  protected Creature _owner;
  protected Damageable _target;

  [SerializeField] private AudioSource _audioSource;
  [SerializeField] private AudioClip _impactClip;

  private void Start()
  {
    _owner = GetComponentInParent<Creature>();
    _damageDealer = _owner.GetComponentInChildren<DealDamage>();
  }

  public IEnumerator PlayAttack()
  {
    if (!CanAttack())
    {
      yield break;
    }
    yield return PlayAnimation();
    _audioSource.PlayOneShot(_impactClip, 0.8f);
    yield return new WaitForSeconds(_impactClip.length);
  }

  protected abstract bool CanAttack();
  protected abstract IEnumerator PlayAnimation();
}
