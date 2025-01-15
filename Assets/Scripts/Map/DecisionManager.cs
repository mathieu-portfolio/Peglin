using System;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
  public event Action<int> OnDecisionMade;

  // Call this method when a decision is made
  public void MakeDecision(int decisionIdentifier)
  {
    OnDecisionMade?.Invoke(decisionIdentifier);
  }
  void Start()
  {
    OnDecisionMade += HandleDecision;
  }

  private void HandleDecision(int holeIdentifier)
  {
    switch (holeIdentifier)
    {
      case 0:
        Debug.Log("Going through hole 0");
        // Load one path or trigger one event
        break;
      case 1:
        Debug.Log("Going through hole 1");
        // Load another path or trigger another event
        break;
      // Add more cases as needed
      default:
        Debug.LogError("Unknown hole identifier: " + holeIdentifier);
        break;
    }
  }

}
