using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private RectTransform _transform;
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private RectTransform _bar;
  private float _baseWidth;

  public Damageable target;

  private void Start()
  {
    float pixelSize = _transform.rect.height / 7;

    _baseWidth = _transform.rect.width - 6 * pixelSize;
    _bar.localPosition = Vector2.left * _baseWidth / 2;
    _bar.sizeDelta = new Vector2(_baseWidth, pixelSize);

    _text.rectTransform.position += Vector3.up * 4 * pixelSize;
  }

  private void LateUpdate()
  {
    if (!target) return;

    float proportion = target.GetPV() / (float) target.GetMaxPV();
    _bar.sizeDelta = new Vector2(_baseWidth * proportion, _bar.sizeDelta.y);
    _text.text = target.GetPV() + "/" + target.GetMaxPV();
  }
}
