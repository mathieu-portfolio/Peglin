using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRange : StartLevelEffect
{
  public override void Apply()
  {
    var aimer = FindObjectOfType<Aimer>();
    aimer.maxBounces++;
  }
}
