using UnityEngine;

public class AddScore : Effect
{

  [SerializeField] private int _amount = 1;
  private MarblesManager _marblesManager;

  private void OnEnable()
  {
    var parent = transform.parent;
    while (parent)
    {
      _marblesManager = FindObjectOfType<MarblesManager>();
      if (_marblesManager) break;
      parent = parent.parent;
    }
  }

  public override void ApplyEffect(GameObject trigger)
  {
    _marblesManager.score += _amount;
  }
}
