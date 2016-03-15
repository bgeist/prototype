using UnityEngine;
using System;

public class TwoBoneInverseKinematics : MonoBehaviour {
  public Transform bone0;
  public Transform bone1;
  public Transform tip;

  // TODO: pass this in from something else
  public Transform point;

  Vector3 target;

  void Awake() {
    target = tip.position;
  }

  void Update() {
    target = Vector3.Slerp(target, point.position, 4 * Time.deltaTime);
    Target(target);
  }

  void Target(Vector3 pt) {
    // TODO: this math is pretty crufty. Clean it up.
    Vector3 p = pt - bone0.position;
    double theta1 = ((p.z > 0 && p.y < 0 || p.z < 0 && p.y > 0) ? 1 : -1) * Math.Acos((Sq(p.magnitude) - Sq(BoneLen0()) - Sq(BoneLen1())) / (2 * BoneLen0() * BoneLen1()));
    if (Double.IsNaN(theta1)) {
      return;
    }

    double theta0 = (p.y < 0 ? -1 : 1) * Math.Acos ((BoneLen0() * p.z + BoneLen1() * (p.z * Math.Cos(theta1) +p.y * Math.Sin(theta1))) / Sq(p.magnitude));
    if (Double.IsNaN(theta0)) {
      return;
    }

    bone1.localRotation = Quaternion.AngleAxis(Degrees((float) theta1), Vector3.right);
    bone0.localRotation = Quaternion.AngleAxis(Degrees((float) theta0), Vector3.right);
  }

  float BoneLen1() {
    Vector3 v = tip.position - bone1.position;
    return v.magnitude;
  }

  float BoneLen0() {
    Vector3 v = bone1.position - bone0.position;
    return v.magnitude;
  }

  float Sq(float val) {
    return val * val;
  }

  float Degrees(float rads) {
    return rads * 180 / (float) Math.PI;
  }
}