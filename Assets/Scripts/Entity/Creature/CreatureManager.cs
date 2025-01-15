using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
  [SerializeField] private Transform _creaturesContainer;
  [SerializeField] private Grid _grid;
  [SerializeField] private CreatureGrid _creatureGrid;
  [SerializeField] private Vector2Int _creatureGridOffset = new Vector2Int(-7, 5);
  [SerializeField] private Vector2Int _creatureGridCellSize = new Vector2Int(2, 2);
  [SerializeField] private Color _debugColor = new Color(1, 0, 0, 0.5f);

  [SerializeField] private int _spawnPlayerPosition = 0;
  [SerializeField] private int _spawnMonsterPosition = 7;

  [SerializeField] private Player _player;
  private List<Monster> _monsters;

  [SerializeField] private MonsterScroll _monsterScroll;
  [SerializeField] private List<Monster> _monstersToSpawn;

  public void Init()
  {
    _monsters = new List<Monster>();
    _monsterScroll.Init(_monstersToSpawn);
    SpawnPlayer();
  }

  public IEnumerator SpawnMonsters()
  {
    if (_monstersToSpawn.Count <= 0) yield break;
    var nextMonster = _monstersToSpawn[0];
    if (SpawnMonster(nextMonster))
    {
      _monstersToSpawn.RemoveAt(0);
      _monsterScroll.Remove(0);
      StartCoroutine(_monsterScroll.Play());
    }
  }

  public IEnumerator PlayTurn()
  {
    yield return _player.PlayTurn();

    var destroyedMonsters = new List<Monster>();
    foreach (var monster in _monsters)
    {
      if (monster)
      {
        yield return monster.PlayTurn();
        monster.transform.position = CreatureGridToWorld(monster.position);
      }
      else
      {
        destroyedMonsters.Add(monster);
      }
    }
    _monsters.RemoveAll(destroyedMonsters.Contains);
  }

  public Vector3 CreatureGridToWorld(Vector2Int creatureGridPos)
  {
    var gridPos = _creatureGridOffset + Vector2Int.Scale(
      new Vector2Int(creatureGridPos.x, creatureGridPos.y), _creatureGridCellSize);
    return _grid.CellToLocal((Vector3Int) gridPos);
  }

  public bool AllEnemyDefeated()
  {
    return _monsters.Count + _monstersToSpawn.Count <= 0;
  }

  private bool SpawnMonster(Monster monsterPrefab)
  {
    var spawnPos = new Vector2Int(_spawnMonsterPosition, monsterPrefab.posY);
    if (!_creatureGrid.CanPlaceCreature(spawnPos, monsterPrefab.size))
    {
      return false;
    }

    var monster = Instantiate(monsterPrefab, _creaturesContainer);
    _creatureGrid.AddCreature(spawnPos, monster);
    monster.transform.position = CreatureGridToWorld(spawnPos);
    _monsters.Add(monster);

    return true;
  }

  private void SpawnPlayer()
  {
    var spawnPos = new Vector2Int(_spawnPlayerPosition, _player.posY);
    _player.position = spawnPos;
    if (_creatureGrid.CanPlaceCreature(spawnPos, _player.size))
    {
      _creatureGrid.AddCreature(spawnPos, _player);
    }
    else
    {
      Debug.LogError("Player couldn't be added creature grid");
    }
    _player.transform.position = CreatureGridToWorld(spawnPos);
  }

  private void OnDrawGizmos()
  {
    _creatureGrid.DrawGizmos(_grid, _creatureGridCellSize, _creatureGridOffset, _debugColor);
  }
}
