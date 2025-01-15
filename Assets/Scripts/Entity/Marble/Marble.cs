using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
  public void ApplyEffects(GameObject trigger)
  {
    var effects = GetComponents<Effect>();
    foreach (var effect in effects)
    {
      if (effect.enabled) effect.ApplyEffect(trigger);
    }
  }
}
