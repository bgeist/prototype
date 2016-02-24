using System;

[Flags] public enum Flag {
  None = 0,
  Player = 1 << 0,
  Monster = 1 << 1,
  Desiderata = 1 << 2,
  Remnata = 1 << 3,
}