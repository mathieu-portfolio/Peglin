using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScroll : MonoBehaviour
{
  [SerializeField] private Animator _animator;
  [SerializeField] private int _monstersPerLine = 4;
  [SerializeField] private float _monstersScale = 0.5f;
  [SerializeField] private Vector2 _monstersOffset = 1.5f * new Vector2(1, -1);
  [SerializeField] private List<SpriteRenderer> _incomingMonsters;

  public void Init(List<Monster> incomingMonsters)
  {
    for (int i = 0; i < incomingMonsters.Count; i++)
    {
      var incomingMonster = incomingMonsters[i];
      var monster = new GameObject(incomingMonster.name);
      monster.transform.SetParent(transform, false);
      monster.transform.localScale *= _monstersScale;

      var renderer = monster.AddComponent<SpriteRenderer>();
      var incomingRenderer = incomingMonster.GetComponentInChildren<SpriteRenderer>();
      renderer.sprite = incomingRenderer.sprite;
      renderer.enabled = false;
      renderer.sortingOrder = incomingRenderer.sortingOrder + 1;

      _incomingMonsters.Add(renderer);
    }
  }

  public void Remove(int idx)
  {
    Destroy(_incomingMonsters[idx].gameObject);
    _incomingMonsters.RemoveAt(idx);
  }

  public IEnumerator Play()
  {
    foreach (var monster in _incomingMonsters)
    {
      monster.enabled = false;
    }
    _animator.SetBool("Opened", false);
    yield return new WaitForSeconds(0.5f);

    if (_incomingMonsters.Count <= 0) yield break;
    for (int i = 0; i < _incomingMonsters.Count; i++)
    {
      var position = new Vector2(i % _monstersPerLine, -i / _monstersPerLine);
      _incomingMonsters[i].transform.localPosition = _monstersOffset + position;
    }

    _animator.SetBool("Opened", true);
    yield return new WaitForSeconds(0.5f);
    foreach (var monster in _incomingMonsters)
    {
      monster.enabled = true;
    }
  }
}
