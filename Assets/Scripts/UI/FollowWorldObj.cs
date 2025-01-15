using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldObj : MonoBehaviour
{
  public Transform target;
  [SerializeField] private Vector3 _offset;
  [SerializeField] private RectTransform _transform;
  private RectTransform _canvas;

  private void Start()
  {
    _canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
  }

  private void Update()
  {
    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position + _offset);

    _transform.anchoredPosition = screenPoint - _canvas.sizeDelta / 2f;
  }
}
