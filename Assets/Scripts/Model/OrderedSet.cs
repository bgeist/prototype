using System.Collections.ObjectModel;
using System;

public class OrderedSet<T> : Collection<T> {
  protected override void InsertItem(int idx, T item) {
    ThrowIfDuplicate(item);
    base.InsertItem(idx, item);
  }

  protected override void SetItem(int idx, T item) {
    ThrowIfDuplicate(item);
    base.SetItem(idx, item);
  }

  void ThrowIfDuplicate(T item) {
    if (Contains(item)) {
      throw new ArgumentException("Duplicate item=" + item);
    }
  }
}