using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Launcher : MonoBehaviour
{
  [SerializeField] private float _strength = 10;
  [SerializeField] private Aimer _aimer;

  [SerializeField] private GameObject _marblesContainer;
  private GameObject _marbleContainerStripped;

  [SerializeField] private GameObject _projectilesContainer;

  private Rigidbody2D _ballRb;
  public bool loaded { get; private set; }

  private void Update()
  {
    if (Input.GetMouseButtonDown(1))
    {
      Shoot();
      gameObject.SetActive(false);
    }
  }

  public float GetStrength() { return _strength; }
  public Rigidbody2D GetBall() { return _ballRb; }

  public IEnumerator Init()
  {
    loaded = false;
    yield return StripMarbles();
    _aimer.CreatePhysicsScene();
  }

  public IEnumerator LoadBall(Ball ballPrefab)
  {
    if (!ballPrefab)
    {
      yield break;
    }

    var ball = Instantiate(ballPrefab, _projectilesContainer.transform);
    ball.transform.position = transform.position;
    _ballRb = ball.GetComponent<Rigidbody2D>();
    _ballRb.simulated = false;

    yield return StripMarbles();
    _aimer.Aim();

    loaded = true;
  }

  private IEnumerator StripMarbles()
  {
    Destroy(_marbleContainerStripped);
    _marbleContainerStripped = Instantiate(_marblesContainer);

    var renderers = _marbleContainerStripped.GetComponentsInChildren<Renderer>();
    foreach (var renderer in renderers)
    {
      Destroy(renderer);
    }
    var audios = _marbleContainerStripped.GetComponentsInChildren<AudioSource>();
    foreach (var audio in audios)
    {
      Destroy(audio);
    }
    var crits = _marbleContainerStripped.GetComponentsInChildren<ApplyCrit>();
    foreach (var crit in crits)
    {
      crit.HardDestroy();
    }
    var refreshes = _marbleContainerStripped.GetComponentsInChildren<RefreshMarbles>();
    foreach (var refresh in refreshes)
    {
      refresh.HardDestroy();
    }
    var addScores = _marbleContainerStripped.GetComponentsInChildren<AddScore>();
    foreach (var addScore in addScores)
    {
      Destroy(addScore);
    }
    var addCoins = _marbleContainerStripped.GetComponentsInChildren<AddCoins>();
    foreach (var addCoin in addCoins)
    {
      Destroy(addCoin);
    }

    _aimer.marblesContainerStripped = _marbleContainerStripped;
    _marbleContainerStripped.SetActive(false);

    yield return null;
  }

  private void Shoot()
  {
    _ballRb.simulated = true;
    _ballRb.AddForce(_aimer.GetDirection().normalized * _strength, ForceMode2D.Impulse);

    _ballRb.transform.parent = _projectilesContainer.transform;

    loaded = false;
  }
}
