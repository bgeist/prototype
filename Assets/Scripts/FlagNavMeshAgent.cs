using UnityEngine;

public class FlagNavMeshAgent : MonoBehaviour {
  public float range = 2;
  [HideInInspector] public Collider dest;
  FlagCollider flagCollider;
  NavMeshAgent nav;

  void Awake() {
    flagCollider = GetComponentInChildren<FlagCollider>();
    nav = GetComponent<NavMeshAgent>();
  }

  void Update() {
    dest = flagCollider.collided.Count > 0 ? flagCollider.collided[0] : null;
    if (dest != null) {
      if (inRange(dest)) {
        nav.enabled = false;
      } else {
        nav.SetDestination(dest.transform.position);
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