using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : DestroyMarble
{
  [SerializeField] private float _strength = 10;
  [SerializeField] private float _radius = 1;
  [SerializeField] private LayerMask[] _layerMasks;
  [SerializeField] private BombAttack _bombAttackPrefab;

  [SerializeField] private AudioSource _audioSource;
  [SerializeField] private AudioClip _audioClip;

  protected override IEnumerator DestroyRoutine()
  {
    bool simulated = gameObject.scene.name.Equals("Simulation");
    var projectilesContainer = GameObject.Find((simulated ? "Simulated Projectiles" : "Projectile Container")).transform;
     
    var projectiles = projectilesContainer.GetComponentsInChildren<Ball>();
    if (projectiles.Length == 0) Debug.Log("no projectiles");
    foreach (var p in projectiles)
    {
      var rb = p.GetComponent<Rigidbody2D>();
      var seg = rb.transform.position - transform.position;
      if (seg.magnitude < _radius)
      {
        rb.AddForce(seg.normalized * (1 - seg.magnitude / _radius) * _strength, ForceMode2D.Impulse);
      }
    }

    if (simulated)
    {
      yield return base.DestroyRoutine();
    }
    else
    {
      GetComponent<Collider2D>().enabled = false;
      GetComponent<Renderer>().enabled = false;

      var player = FindObjectOfType<Player>();
      var attack = Instantiate(_bombAttackPrefab, player.transform);
      attack.transform.SetAsFirstSibling();

      _audioSource.PlayOneShot(_audioClip, 0.2f);
      yield return new WaitForSeconds(_audioClip.length * 0.7f);

      GetComponent<Collider2D>().enabled = true;
      GetComponent<Renderer>().enabled = true;
      gameObject.SetActive(false);
    }
  }
}
