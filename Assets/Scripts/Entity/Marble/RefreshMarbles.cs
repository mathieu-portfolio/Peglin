using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshMarbles : Effect
{
  private MarblesManager _marblesManager;
  private SpriteRenderer _spriteRenderer;
  private bool disableEffectOn = true;

  private void OnEnable()
  {
    var addScore = GetComponent<AddScore>();
    if (addScore) addScore.enabled = false;

    _marblesManager = FindObjectOfType<MarblesManager>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _spriteRenderer.sprite = _marblesManager._refreshMarbleSprite;

    gameObject.tag = "Refresh Marble";
    gameObject.layer = LayerMask.NameToLayer("Special Marbles");
  }

  private void OnDisable()
  {
    if (!disableEffectOn) return;

    var addScore = GetComponent<AddScore>();
    if (addScore) addScore.enabled = true;

    _spriteRenderer.sprite = _marblesManager._marbleSprite;

    gameObject.tag = "Untagged";
    gameObject.layer = LayerMask.NameToLayer("Marbles");

    _marblesManager.specialMarbles.Add(GetType());
    _marblesManager.Refresh();
  }

  public override void ApplyEffect(GameObject trigger)
  {
    Destroy(this);
  }

  public void HardDestroy()
  {
    disableEffectOn = false;
    Destroy(this);
  }
}
