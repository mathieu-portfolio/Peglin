using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BallSlider : MonoBehaviour
{
  [SerializeField] private Tilemap _foreground;
  [SerializeField] private Vector3Int _position;
  [SerializeField] private TileBase _wood;
  [SerializeField] private GameObject _ballTilePrefab;
  [SerializeField] private SpriteRenderer _currentBallRenderer;

  [SerializeField] private AnimationCurve _scrollCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
  [SerializeField] private int _scrollFrames = 60;

  private List<GameObject> _incomingBalls;

  public void Init(List<BallCard> incomingBallPrefabs)
  {
    _incomingBalls = new List<GameObject>();
    for (int i = 0; i < incomingBallPrefabs.Count + 2; i++)
    {
      var tilePos = _position + i * Vector3Int.down;
      _foreground.SetTile(tilePos, _wood);

      if (i == 0 || i == incomingBallPrefabs.Count + 1) { continue; }

      var ballTile = Instantiate(_ballTilePrefab, transform);
      ballTile.transform.localPosition = _foreground.CellToLocal(tilePos) + 0.5f * _foreground.cellSize;

      var ballSprite = ballTile.transform.GetChild(0).GetComponent<SpriteRenderer>();
      ballSprite.sprite = incomingBallPrefabs[i - 1].ballSprite;
      ballSprite.sortingOrder = ballTile.GetComponent<SpriteRenderer>().sortingOrder + 1;
      
      _incomingBalls.Add(ballTile);
    }
  }

  public IEnumerator SlideUp()
  {
    var dist = _foreground.cellSize.y;
    var startingPos = new List<Vector3>();
    foreach (var ball in _incomingBalls)
    {
      startingPos.Add(ball.transform.localPosition);
    }

    for (int frame = 0; frame < _scrollFrames; frame++)
    {
      var t = _scrollCurve.Evaluate(frame / (float) _scrollFrames);
      for (int i = 0; i < _incomingBalls.Count; i++)
      {
        var ball = _incomingBalls[i];
        ball.transform.localPosition = startingPos[i] + Vector3.up * dist * t;
      }

      yield return new WaitForEndOfFrame();
    }

    var ballTile = _incomingBalls[0];
    _currentBallRenderer.sprite = ballTile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    Destroy(ballTile);
    _incomingBalls.RemoveAt(0);
  }
}
