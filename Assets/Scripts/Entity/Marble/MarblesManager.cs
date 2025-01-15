using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarblesManager : MonoBehaviour
{
  [SerializeField] private Transform _marbleContainer;
  public Sprite _marbleSprite;
  public Sprite _critMarbleSprite;
  public Sprite _refreshMarbleSprite;

  public List<System.Type> specialMarbles { get; set; }
  public Ball ball { private get; set; }
  public int baseAttack { get; set; }
  public int score { get; set; }
  public bool crit { get; private set; }
  public int coins { get; set; }

  public void Init()
  {
    specialMarbles = new List<System.Type>();
    Reset();
  }

  public void Reset()
  {
    baseAttack = 0;
    score = 0;
    coins = 0;
    SetCrit(false);
  }

  public int GetAttack()
  {
    return baseAttack + score * (crit ? ball._critDamage : ball._damage);
  }

  public void SetCrit(bool crit_)
  {
    var color = crit_ ? Color.red : Color.white;
    var marbles = _marbleContainer.GetComponentsInChildren<Marble>(true).Where(m => m.CompareTag("Untagged"));
    foreach (var marble in marbles)
    {
      marble.GetComponent<SpriteRenderer>().color = color;
    }

    crit = crit_;
  }

  public void Refresh()
  {
    var marbles = _marbleContainer.GetComponentsInChildren<Marble>(true).Where(x => x.CompareTag("Untagged")).ToArray();

    for (int i = 0; i < marbles.Length; i++)
    {
      var marble = marbles[i];
      marble.gameObject.SetActive(true);

      bool onlySpecialRemain = i >= marbles.Length - specialMarbles.Count - 1;
      if (specialMarbles.Count > 0 && (Random.Range(0, marbles.Length) == 0 || onlySpecialRemain))
      {
        var type = specialMarbles[0];
        marble.gameObject.AddComponent(type);
        specialMarbles.RemoveAt(0);
      }
    }
  }
}
