
using UnityEngine;

public class AddCoins : Effect
{

  [SerializeField] private int _amount = 1;
  private MarblesManager _marblesManager;

  private void OnEnable()
  {
    _marblesManager = transform.parent.GetComponent<MarblesManager>();
  }

  public override void ApplyEffect(GameObject trigger)
  {
    transform.parent.GetComponent<MarblesManager>().coins += _amount;
  }
}
