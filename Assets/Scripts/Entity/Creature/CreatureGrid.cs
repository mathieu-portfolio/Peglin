using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGrid : MonoBehaviour
{
  private Dictionary<Vector2Int, Creature> _grid;

  public CreatureGrid()
  {
    _grid = new Dictionary<Vector2Int, Creature>();
  }

  public void AddCreature(Vector2Int position, Creature creature)
  {
    for (int i = 0; i < creature.size.x; i++)
    {
      for (int j = 0; j < creature.size.y; j++)
      {
        Vector2Int pos = position + new Vector2Int(i, j);
        _grid.Add(pos, creature);
      }
    }

    creature.position = position;
    creature.grid = this;
  }

  public bool CanPlaceCreature(Vector2Int position, Vector2Int size)
  {
    if (position.x < 0) return false;

    for (int i = 0; i < size.x; i++)
    {
      for (int j = 0; j < size.y; j++)
      {
        Vector2Int pos = position + new Vector2Int(i, j);
        if (_grid.ContainsKey(pos))
        {
          return false;
        }
      }
    }
    return true;
  }

  public void MoveCreature(Creature creature, int offset)
  {
    Vector2Int targetPosition = creature.position + offset * Vector2Int.left;
    Vector2Int newPosition;
    if (!FindNearestAvailablePosition(creature.position, targetPosition, creature.size, out newPosition))
      return;

    RemoveCreature(creature);
    AddCreature(newPosition, creature);

    creature.position = newPosition;
  }

  public bool FindNearestCreature(Vector2Int position, Vector2Int direction, int range, out Creature creature)
  {
    if (_grid.Count == 0)
    {
      creature = null;
      return false;
    }

    for (int i = 1; i <= range; i++)
    {
      var target = position + i * direction;
      if (_grid.ContainsKey(target))
      {
        creature = _grid[target];
        return true;
      }
      target += Vector2Int.up;
      if (_grid.ContainsKey(target))
      {
        creature = _grid[target];
        return true;
      }
    }

    creature = null;
    return false;
  }

  private bool FindNearestAvailablePosition(Vector2Int creaturePosition, Vector2Int targetPosition, Vector2Int size, out Vector2Int position)
  {
    int distance = creaturePosition.x - targetPosition.x;
    for (int x = 0; x < distance; x++)
    {
      position = targetPosition + x * Vector2Int.right;
      if (CanPlaceCreature(position, size))
      {
        return true;
      }
    }

    position = creaturePosition;
    return false;
  }

  public void RemoveCreature(Creature creature)
  {
    for (int i = 0; i < creature.size.x; i++)
    {
      for (int j = 0; j < creature.size.y; j++)
      {
        Vector2Int pos = creature.position + new Vector2Int(i, j);
        _grid.Remove(pos);
      }
    }
  }

  public void DrawGizmos(Grid grid, Vector2Int size, Vector2Int offset, Color color)
  {
    var cellSize = grid.cellSize;
    cellSize.x *= size.x;
    cellSize.y *= size.y;
    var min = grid.CellToLocal((Vector3Int) offset);

    Gizmos.color = color;

    foreach (var pos in _grid.Keys)
    {
      var worldPos = Vector3.Scale(cellSize, (Vector3Int)pos) + min;
      Gizmos.DrawCube(worldPos + 0.5f * Vector3.up, cellSize);
    }
  }
}
