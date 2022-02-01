#if UNITY_EDITOR
using System;
using Assets.Scripts.Game;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.EditorExtensions
{
    [CustomEditor(typeof(GameSettingsSO))]
    public class GameSettingsSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var gameSettingsSO = (GameSettingsSO)target;
            base.OnInspectorGUI();

            GUILayout.Space(100);
            GUILayout.Label("Save new json board settings", EditorStyles.boldLabel);
            if (GUILayout.Button("Save"))
            {
                gameSettingsSO.PrepareJsonBoardModel();
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif
