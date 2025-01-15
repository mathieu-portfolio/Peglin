using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  [SerializeField] private MarblesManager _marblesManager;
  [SerializeField] private BallManager _ballManager;
  [SerializeField] private CreatureManager _creatureManager;
  [SerializeField] private Inventory _inventory;
  [SerializeField] private Player _player;

  [SerializeField] private GameObject _holes;

  private int _coins;

  private void Start()
  {
    StartCoroutine(InitRoutine());
  }

  private IEnumerator InitRoutine()
  {
    _inventory.InstantiateRelics();
    _marblesManager.Init();
    _creatureManager.Init();
    yield return _ballManager.Init();
    yield return _inventory.ApplyStartLevelEffects();
    yield return TurnRoutine();
  }

  public void EndTurn()
  {
    StartCoroutine(EndTurnRoutine());
  }

  private IEnumerator TurnRoutine()
  {
    _marblesManager.Reset();
    yield return _ballManager.Reset();

    var ball = _ballManager.GetBall().ballPrefab;
    _marblesManager.ball = ball;
    Instantiate(ball._attackPrefab, _player.transform);

    yield return _creatureManager.SpawnMonsters();
  }

  private IEnumerator EndTurnRoutine()
  {
    _coins += _marblesManager.coins;

    StartCoroutine(_ballManager.ReloadBall());
    yield return _creatureManager.PlayTurn();

    if (_creatureManager.AllEnemyDefeated())
    {
      yield return Win();
      yield break;
    }

    if (_player.GetPV() <= 0)
    {
      yield return Lose();
      yield break;
    }

    yield return TurnRoutine();

    // Debug.Log("coins: " + _coins);
  }

  private IEnumerator Win()
  {
    _holes.SetActive(true);

    _marblesManager.Reset();
    yield return _ballManager.Reset();

    var ball = _ballManager.GetBall().ballPrefab;
    _marblesManager.ball = ball;
  }

  private IEnumerator Lose()
  {
    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    SceneManager.LoadScene("Menu");
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      SceneManager.LoadScene("Menu");
    }
  }
}
