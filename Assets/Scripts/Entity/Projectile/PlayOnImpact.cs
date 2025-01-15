using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnImpact : MonoBehaviour
{
  [SerializeField] private AudioSource _audioSource;
  [SerializeField] private AudioClip _impactClip;

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (_audioSource.enabled) _audioSource.PlayOneShot(_impactClip, 0.5f);
  }
}
