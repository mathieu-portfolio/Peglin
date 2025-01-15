using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTrigger : MonoBehaviour
{
  [SerializeField] private GameObject _ball;
  [SerializeField] private List<LayerMask> _excludeLayers;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var marble = collision.GetComponent<Marble>();
    if (!marble) return;

    foreach (var layer in _excludeLayers)
    {
      if (1 << marble.gameObject.layer == layer)
      {
        return;
      }
    }

    marble.ApplyEffects(_ball);
  }
}
