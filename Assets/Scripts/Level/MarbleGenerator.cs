using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleGenerator : MonoBehaviour
{
  [SerializeField] private List<GameObject> _marblePrefabs;
  [SerializeField] private List<int> _weights;
  [SerializeField] private Transform _marbleContainer;
  [SerializeField] private Vector2Int _size;
  [SerializeField] private float _step;

  public void Generate()
  {
    // Calcul de la position de départ pour centrer les marbres
    var startPos = -new Vector2(_size.x * _step * 0.5f, _size.y * _step * 0.5f);

    // Poids total utilisé dans la sélcetion du préfab à instancier
    int totalWeights = 0;
    foreach (var w in _weights)
    {
      totalWeights += w;
    }

    for (int x = 0; x < _size.x; x++)
    {
      for (int y = 0; y < _size.y; y++)
      {

        var index = MathsUtils.WeightedRandomSelection(_weights, totalWeights);
        var marble = Instantiate(_marblePrefabs[index], _marbleContainer);
        marble.transform.localPosition = new Vector2(startPos.x + x * _step, startPos.y + y * _step);
      }
    }
  }
}
