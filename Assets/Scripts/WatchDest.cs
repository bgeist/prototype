using UnityEngine;

public class WatchDest : MonoBehaviour {
  public Transform eye;
  Quaternion eyeInitialRotation;
  FlagNavMeshAgent nav;

  void Awake() {
    nav = GetComponent<FlagNavMeshAgent>();
    eyeInitialRotation = eye.localRotation;
  }

  void Update() {
    if (nav.dest != null) {
      eye.LookAt(nav.dest.transform);
      eye.localRotation *= eyeInitialRotation;
    }
  }
}