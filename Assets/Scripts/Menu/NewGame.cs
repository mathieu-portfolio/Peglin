using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
  [SerializeField] private GameObject _newGamePanel;

  [SerializeField] private CanvasGroup _canvas;
  [SerializeField] private float _fadeDuration = 1;
  [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.Linear(0, 0, 1, 1);

  [SerializeField] private Camera _cam;
  [SerializeField] private float _targetHeight = 20;
  [SerializeField] private float _scrollDuration = 3;
  [SerializeField] private AnimationCurve _scrollCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

  private bool _scrolling = false;

  public void ScrollUp()
  {
    StartCoroutine(ScrollRoutine(true));
  }

  public void ScrollDown()
  {
    StartCoroutine(ScrollRoutine(false));
  }

  public void Play()
  {
    if (_scrolling) return;

    SceneManager.LoadScene("Level 1.1");
  }

  private IEnumerator ScrollRoutine(bool up)
  {
    _scrolling = true;

    yield return FadeRoutine(false);
    yield return new WaitForSeconds(0.5f);

    var startPos = _cam.transform.position;

    float progress = 0f;
    while (progress < 1f)
    {
      float height = _scrollCurve.Evaluate(progress) * _targetHeight;
      _cam.transform.position = startPos + (up ? 1 : -1) * Vector3.up * height;

      progress += Time.deltaTime / _scrollDuration;

      yield return null;
    }

    _newGamePanel.SetActive(up);

    yield return new WaitForSeconds(0.5f);
    yield return FadeRoutine(true);

    _scrolling = false;
  }

  private IEnumerator FadeRoutine(bool renderCanvas)
  {
    float startAlpha = renderCanvas ? 0 : 1;
    float endAlpha = 1 - startAlpha;

    float progress = 0f;
    while (progress < 1f)
    {
      float t = _fadeCurve.Evaluate(progress);
      _canvas.alpha = (1 - t) * startAlpha + t * endAlpha;

      progress += Time.deltaTime / _fadeDuration;

      yield return null;
    }
  }
}
