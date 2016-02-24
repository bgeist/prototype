using UnityEngine;

public class FlagCollider : MonoBehaviour {
  [EnumFlag] public Flag flag;
  public OrderedSet<Collider> collided = new OrderedSet<Collider>();

  void OnTriggerStay(Collider other) {
    FlagCollider otherFlagCollider = other.gameObject.GetComponent<FlagCollider>();
	if (otherFlagCollider != null && (otherFlagCollider.flag & flag) != 0) {
      collided.Remove(other);
      collided.Add(other);
    }
  }

  void OnTriggerExit(Collider other) {
    collided.Remove(other);
  }
}
