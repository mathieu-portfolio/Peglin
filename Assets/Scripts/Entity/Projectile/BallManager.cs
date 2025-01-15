using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
  [SerializeField] private Launcher _launcher;

  [SerializeField] private Inventory _inventory;
  [SerializeField] private BallSlider _slider;
  [SerializeField] private BallScroll _scroll;
  private List<BallCard> _projectiles;

  public BallCard GetBall() { return _projectiles.Count == 0 ? null : _projectiles[0]; }

  public IEnumerator Init()
  {
    _scroll.Init();
    yield return _launcher.Init();
    yield return ReloadBalls();
  }

  public IEnumerator Reset()
  {
    yield return new WaitUntil(() => _launcher.loaded);
    _launcher.gameObject.SetActive(true);
  }

  private IEnumerator ReloadBalls()
  {
    _projectiles = new List<BallCard>();

    var indices = new List<int>();
    for (int i = 0; i < _inventory.ballCards.Count; i++)
    {
      indices.Add(i);
    }

    for (int i = 0; i < _inventory.ballCards.Count; i++)
    {
      var randIdx = Random.Range(0, indices.Count);
      _projectiles.Add(_inventory.ballCards[indices[randIdx]]);
      indices.RemoveAt(randIdx);
    }
    
    _slider.Init(_projectiles);

    yield return LoadBall();
  }

  private IEnumerator LoadBall()
  {
    yield return _launcher.LoadBall(_projectiles[0].ballPrefab);
    StartCoroutine(_scroll.ShowBall(_projectiles[0]));
    yield return _slider.SlideUp();
  }

  public IEnumerator ReloadBall()
  {
    _projectiles.RemoveAt(0);

    if (_projectiles.Count == 0)
    {
      yield return ReloadBalls();
    }
    else
    {
      yield return LoadBall();
    }
  }

}
