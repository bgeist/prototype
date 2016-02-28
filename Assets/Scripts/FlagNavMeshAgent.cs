using UnityEngine;

public class FlagNavMeshAgent : MonoBehaviour {
  FlagCollider flagCollider;
  NavMeshAgent nav;
  public float range = 2;

  void Awake() {
    flagCollider = GetComponentInChildren<FlagCollider>();
    nav = GetComponent<NavMeshAgent>();
  }

  void Update() {
    if (flagCollider.collided.Count > 0) {
      Collider other = flagCollider.collided[0];
      if (inRange(other)) {
        nav.Stop();
      } else {
        nav.Resume();
        nav.SetDestination(other.transform.position);
      }
    }
  }

  bool inRange(Collider other) {
    return distance(other) < range;
  }

  float distance(Collider other) {
    return Vector3.Distance(transform.position, other.transform.position);
  }
}