using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : Aimer
{
  [SerializeField] private float _step = 5;

  protected override void Simulate()
  {
    float angle;
    float bestAngle = 0;

    int score;
    int bestScore = -1;

    angle = 0;
    while (angle < 360)
    {
      _dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
      BasicSimulatation();
      score = _ghostMarblesContainer.GetComponent<MarblesManager>().score;
      if (score > bestScore)
      {
        bestScore = score;
        bestAngle = angle;
      }
      angle += _step;
    }

    _dir = Quaternion.Euler(0, 0, bestAngle) * Vector2.right;
  }
}
