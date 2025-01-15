using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathsUtils
{
  public static int WeightedRandomSelection(List<int> weights, int totalWeight)
  {
    int randomValue = Random.Range(0, totalWeight);

    for (int i = 0; i < weights.Count; i++)
    {
      if (randomValue < weights[i])
      {
        return i;
      }
      else
      {
        randomValue -= weights[i];
      }
    }

    return -1;
  }

  public static int WeightedRandomSelection(List<int> weights)
  {
    int totalWeight = 0;
    foreach (int weight in weights)
    {
      totalWeight += weight;
    }
    return WeightedRandomSelection(weights, totalWeight);
  }

  public static IEnumerator ConvexTrajectory(GameObject o,
                                          Vector3 startPos,
                                          Vector3 endPos,
                                          float maxHeight,
                                          float startAngle,
                                          float endAngle,
                                          float duration)
  {
    float progress = 0f;
    while (progress < 1f)
    {
      float height = Mathf.Sin(Mathf.PI * progress) * maxHeight;
      Vector3 pos = Vector3.Lerp(startPos, endPos, progress);
      pos.y += height;

      Vector3 angle = Vector3.Lerp(Vector3.forward * startAngle, Vector3.forward * endAngle, progress);

      o.transform.position = pos;
      o.transform.localEulerAngles = angle;

      progress += Time.deltaTime / duration;

      yield return null;
    }
  }

  public static IEnumerator ConvexTrajectory(GameObject o,
                                          Vector3 startPos,
                                          Vector3 endPos,
                                          float maxHeight,
                                          float duration,
                                          bool clockwise = true)
  {
    yield return ConvexTrajectory(o, startPos, endPos, maxHeight, 0, (clockwise ? 1 : -1) * 360, duration);
  }
}
