using UnityEngine;

public class FlagCollider : MonoBehaviour {
  [EnumFlag] public Flag flag;
  public OrderedSet<Collider> collided = new OrderedSet<Collider>();

  void OnTriggerEnter(Collider other) {
    FlagEmitter emitter = other.gameObject.GetComponent<FlagEmitter>();
    if (emitter != null && (emitter.flag & flag) != 0) {
      collided.Remove(other);
      collided.Add(other);
    }
  }

  void OnTriggerExit(Collider other) {
    collided.Remove(other);
  }
}