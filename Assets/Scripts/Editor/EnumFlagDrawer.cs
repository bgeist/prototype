using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// http://wiki.unity3d.com/index.php/EnumFlagPropertyDrawer
// http://www.sharkbombs.com/2015/02/17/unity-editor-enum-flags-as-toggle-buttons/
[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer {
  int rows = 1;
  GUIStyle buttonStyle;

  public EnumFlagDrawer() {
    buttonStyle = EditorStyles.miniButton;
  }

  public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {
    EditorGUI.BeginProperty(pos, label, prop);
    UpdateFieldLabel(pos, label);
    rows = UpdateValueButtons(pos, prop, label);
    EditorGUI.EndProperty();

    Repaint(prop);
  }

  public override float GetPropertyHeight (SerializedProperty prop, GUIContent label) {
    return base.GetPropertyHeight (prop, label) * rows;
  }

  void UpdateFieldLabel(Rect position, GUIContent label) {
    EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), label);
  }

  int UpdateValueButtons(Rect position, SerializedProperty property, GUIContent label) {
    float originX = position.x + EditorGUIUtility.labelWidth;
    float width = position.width - EditorGUIUtility.labelWidth;
    float rowHeight = base.GetPropertyHeight(property, label);

    float x = originX;
    float y = position.y;
    int val = property.intValue;

    EditorGUI.BeginChangeCheck();

    int rows = 1;
    for (int i = 0; i < property.enumNames.Length; ++i) {
      GUIContent text = ButtonText(property, i);
      float buttonWidth = ButtonMinWidth(text);

      if (x + buttonWidth > originX + width) {
        x = originX;
        y += rowHeight;
        ++rows;
      }

      int mask = EnumVal(property, i);
      bool pressed = (val & mask) == mask && (mask != 0 || val == 0);

      Rect buttonPosition = new Rect(x, y, buttonWidth, rowHeight);
      pressed = GUI.Toggle(buttonPosition, pressed, text, buttonStyle);

      if (pressed) {
        val = (mask == 0) ? 0 : (val | mask);
      } else {
        val &= ~mask;
      }

      x += buttonWidth;
    }

    if (EditorGUI.EndChangeCheck()) {
      property.intValue = val;
    }

    return rows;
  }

  float ButtonMinWidth(GUIContent content) {
    float minWidth;
    float maxWidth;  
    buttonStyle.CalcMinMaxWidth(content, out minWidth, out maxWidth);
    return minWidth;
  }

  GUIContent ButtonText(SerializedProperty prop, int index) {
    return new GUIContent(prop.enumDisplayNames[index]);
  }

  int EnumVal(SerializedProperty prop, int index) {
    Enum enumValue = GetBaseProperty<Enum>(prop);
    FieldInfo enumField = enumValue.GetType().GetField(prop.enumNames[index]);
    return (int) enumField.GetValue(enumValue);
  }

  void Repaint(SerializedProperty property) {
    EditorUtility.SetDirty(property.serializedObject.targetObject);
  }

  static T GetBaseProperty<T>(SerializedProperty prop) {
    // Separate the steps it takes to get to this property
    string[] separatedPaths = prop.propertyPath.Split('.');

    // Go down to the root of this serialized property
    System.Object reflectionTarget = prop.serializedObject.targetObject as object;
    // Walk down the path to get the target object
    foreach (var path in separatedPaths) {
      FieldInfo fieldInfo = reflectionTarget.GetType().GetField(path);
      reflectionTarget = fieldInfo.GetValue(reflectionTarget);
    }
    return (T) reflectionTarget;
  }
}