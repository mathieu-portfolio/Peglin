using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScroll : MonoBehaviour
{
  [SerializeField] private Animator _animator;
  [SerializeField] private Sprite _openedScroll;
  
  private GameObject _ballCard;
  private Vector3 _center;

  public void Init()
  {
    _center = _openedScroll.bounds.center;
  }

  public IEnumerator ShowBall(BallCard ballCard)
  {
    Destroy(_ballCard);

    _animator.SetBool("Opened", false);
    yield return new WaitForSeconds(0.5f);

    _animator.SetBool("Opened", true);
    yield return new WaitForSeconds(0.5f);

    _ballCard = Instantiate(ballCard.gameObject, transform);
    _ballCard.transform.localPosition = _center;
  }
}
