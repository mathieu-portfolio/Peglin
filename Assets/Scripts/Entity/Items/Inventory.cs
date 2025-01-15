using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public List<BallCard> ballCards;
  public List<GameObject> relics;

  [SerializeField] private Vector2Int _dimensions;
  [SerializeField] private float _step;

  public void InstantiateRelics()
  {
    for (int i = 0; i < relics.Count; i++)
    {
      var relic = Instantiate(relics[i], transform);

      var x = i / _dimensions.y * _step;
      var y = i % _dimensions.y * _step;
      relic.transform.localPosition = new Vector3(x, y, 0);
    }
  }

  public IEnumerator ApplyStartLevelEffects()
  {
    var startLevelEffects = GetComponentsInChildren<StartLevelEffect>();
    foreach (var effect in startLevelEffects)
    {
      effect.Apply();
    }

    yield return null;
  }
}
