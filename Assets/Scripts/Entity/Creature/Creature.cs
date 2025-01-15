using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Damageable
{
  public Vector2Int position { get; set; }
  public Vector2Int size = Vector2Int.one;
  public int posY = 0;
  public int damage = 1;
  public CreatureGrid grid { get; set; }
  public Creature target { get; protected set; }

  public abstract IEnumerator PlayTurn();
}
