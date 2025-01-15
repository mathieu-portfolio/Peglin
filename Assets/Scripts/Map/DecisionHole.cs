using UnityEngine;

public class DecisionHole : MonoBehaviour
{
  private DecisionManager _decisionManager;
  public int _holeIdentifier;

  private void Start()
  {
    _decisionManager = GetComponentInParent<DecisionManager>();
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    var ball = collider.GetComponent<Ball>();
    if (ball)
    {
      _decisionManager.MakeDecision(_holeIdentifier);
    }
  }
}
