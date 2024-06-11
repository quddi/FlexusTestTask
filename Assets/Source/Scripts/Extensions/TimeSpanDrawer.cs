#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public class TimeSpanDrawer : OdinValueDrawer<TimeSpan>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            GUILayout.BeginHorizontal();
            {
                if (label != null)
                {
                    EditorGUI.PrefixLabel(GUIHelper.GetCurrentLayoutRect(), label);
                    GUILayout.Space(EditorGUIUtility.labelWidth - 8);
                }
                
                GUIHelper.PushLabelWidth(42f);
                GUIHelper.PushIndentLevel(0);
                
                GUILayout.BeginVertical();
                {
                    int days, hours, minutes, seconds, milliseconds;

                    EditorGUI.BeginChangeCheck();
                    {
                        GUILayout.BeginHorizontal();
                        {
                            days = SirenixEditorFields.IntField(GUIHelper.TempContent("Days"),
                                ValueEntry.SmartValue.Days);

                            hours = SirenixEditorFields.IntField(GUIHelper.TempContent("Hours"),
                                ValueEntry.SmartValue.Hours);
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        {
                            minutes = SirenixEditorFields.IntField(GUIHelper.TempContent("Mins"),
                                ValueEntry.SmartValue.Minutes);

                            seconds = SirenixEditorFields.IntField(GUIHelper.TempContent("Secs"),
                                ValueEntry.SmartValue.Seconds);
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        {
                            milliseconds = SirenixEditorFields.IntField(GUIHelper.TempContent("mSecs"),
                                ValueEntry.SmartValue.Milliseconds);
                        }
                        GUILayout.EndHorizontal();
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        ValueEntry.SmartValue = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }
                }
                GUILayout.EndVertical();
                
                GUIHelper.PopIndentLevel();
                GUIHelper.PopLabelWidth();
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif