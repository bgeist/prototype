using UnityEngine;

public class FlagNavMeshAgent : MonoBehaviour {
  FlagCollider flagCollider;
  NavMeshAgent nav;

  void Awake() {
    flagCollider = GetComponentInChildren<FlagCollider>();
    nav = GetComponent<NavMeshAgent>();
  }

  void Update() {
    if (flagCollider.collided.Count > 0) {
      nav.SetDestination(flagCollider.collided[0].transform.position);
    }
  }
}