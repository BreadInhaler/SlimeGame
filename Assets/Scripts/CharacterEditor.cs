#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character), true)] // true makes it work for subclasses too
public class CharacterEditor : Editor{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI(); // draws default inspector fields
        
        Character character = (Character)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("── Runtime Debug ──", EditorStyles.boldLabel);
        
        // Stats
        EditorGUILayout.LabelField("HP", character.GetStats().hp + " / " + character.GetStats().maxHP);
        EditorGUILayout.LabelField("Defense", character.RefreshStats().defense.ToString());
        EditorGUILayout.LabelField("Speed", character.GetStats().speed.ToString());
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Status Effects", EditorStyles.boldLabel);
        
        if(character.GetStatusEffects().Count == 0){
            EditorGUILayout.LabelField("none");
        } else {
            foreach(StatusEffectInstance effect in character.GetStatusEffects()){
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField(effect.effectData.id, EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Stacks", effect.stacks.ToString());
                EditorGUILayout.LabelField("Time", $"{effect.elapsedTime:F1} / {effect.effectData.duration:F1}");
                EditorGUILayout.EndVertical();
            }
        }
        
        // forces inspector to keep refreshing while in play mode
        if(Application.isPlaying) Repaint();
    }
}
#endif