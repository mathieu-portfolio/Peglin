using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Aimer : MonoBehaviour
{
  protected Vector2 _dir;
  [SerializeField] private Launcher _launcher;

  [SerializeField] private List<CompositeCollider2D> _staticColliders;
  [SerializeField] private List<GameObject> _otherColliders;

  public GameObject marblesContainerStripped { get; set; }
  protected GameObject _ghostMarblesContainer;

  [SerializeField] private GameObject _projectilesContainer;
  private GameObject _ghostProjectilesContainer;

  [SerializeField] protected int _maxPhysicsFrameIterations = 100;
  private Scene _simulationScene;
  private PhysicsScene2D _physicsScene;
  protected GameObject _ghostObj;

  public int maxBounces { get; set; } = 1;

  public Vector2 GetDirection() { return _dir; }

  public void Aim()
  {
    Physics2D.simulationMode = SimulationMode2D.Script;
    Simulate();
    Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
  }

  public void CreatePhysicsScene()
  {
    _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
    _physicsScene = _simulationScene.GetPhysicsScene2D();

    // Copy all the static colliders to the simulated scene.
    foreach (var staticCollider in _staticColliders)
    {
      AddObjectToSimulation(staticCollider.gameObject);
    }

    // Copy other colliders
    foreach (var otherCollider in _otherColliders)
    {
      AddObjectToSimulation(otherCollider);
    }

    // Copy all the dynamic colliders to the simulated scene. These are stored under a parent Game Object that
    // is attached in the editor
    AddObjectToSimulation(marblesContainerStripped, true);
    _ghostMarblesContainer = _ghostObj;
    AddObjectToSimulation(_projectilesContainer);
    _ghostProjectilesContainer = _ghostObj;
    _ghostProjectilesContainer.name = "Simulated Projectiles";
  }

  private void AddObjectToSimulation(GameObject o, bool stripped = false)
  {
    _ghostObj = Instantiate(o, o.transform.position, o.transform.rotation);
    _ghostObj.SetActive(true);

    if (!stripped)
    {
      var renderers = _ghostObj.GetComponentsInChildren<Renderer>(true);
      foreach (var renderer in renderers)
      {
        renderer.enabled = false;
      }
      var ghostAudios = _ghostObj.GetComponentsInChildren<AudioSource>();
      foreach (var audio in ghostAudios)
      {
        audio.enabled = false;
      }
    }

    SceneManager.MoveGameObjectToScene(_ghostObj, _simulationScene);
  }

  private void ResetMarbles()
  {
    Destroy(_ghostMarblesContainer);
    AddObjectToSimulation(marblesContainerStripped, true);
    _ghostMarblesContainer = _ghostObj;
  }

  protected abstract void Simulate();

  protected void BasicSimulatation()
  {
    var ballPrefab = _launcher.GetBall();
    if (!ballPrefab) return;

    ResetMarbles();

    // Instantiate the ball prefab for the simulation
    AddObjectToSimulation(ballPrefab.gameObject);
    _ghostObj.transform.parent = _ghostProjectilesContainer.transform;
    _ghostObj.transform.position = _launcher.transform.position;

    // Assign the ball's velocity
    var ghostRb = _ghostObj.GetComponent<Rigidbody2D>();
    ghostRb.simulated = true;
    ghostRb.AddForce(_dir.normalized * _launcher.GetStrength(), ForceMode2D.Impulse);

    var ghostBall = _ghostObj.GetComponent<Ball>();
    int frame = 0;
    while (frame < _maxPhysicsFrameIterations && ghostBall.bounces < maxBounces)
    {
      _physicsScene.Simulate(Time.fixedDeltaTime);
      RenderPoint(frame);
      frame++;
    }

    Destroy(_ghostObj);
  }

  protected virtual void RenderPoint(int i) { }
}
