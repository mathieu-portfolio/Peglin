using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMouse : Aimer
{
  private Camera _cam;
  private LineRenderer _lr;

  private void Awake()
  {
    _lr = GetComponent<LineRenderer>();
  }

  private void OnEnable()
  {
    _lr.enabled = true;
    _cam = Camera.main;
  }

  private void Update()
  {
    if (Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.001f)
    {
      Aim();
    }
  }

  protected override void Simulate()
  {
    var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0;
    _dir = mousePosition - transform.position;

    _lr.positionCount = 0;

    BasicSimulatation();
  }

  protected override void RenderPoint(int i)
  {
    _lr.positionCount++;
    _lr.SetPosition(i, _ghostObj.transform.position);
  }
}
