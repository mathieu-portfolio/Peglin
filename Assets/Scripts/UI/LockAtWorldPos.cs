using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockAtWorldPos : MonoBehaviour
{
  [SerializeField] private Vector3 _lockedPos;
  [SerializeField] private RectTransform _transform;
  [SerializeField] private RectTransform _canvas;

  private void Update()
  {
    SetPos();
  }

  private void OnValidate()
  {
    SetPos();
  }

  private void SetPos()
  {
    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _lockedPos);

    _transform.anchoredPosition = screenPoint - _canvas.sizeDelta / 2f;
  }
}
