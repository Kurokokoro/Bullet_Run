using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacle)), CanEditMultipleObjects]
public class Obstacle_Editor : Editor
{
    private SerializedProperty obstacleType;
    private SerializedProperty rigidbody;
    private SerializedProperty parentObstacle;
    private SerializedProperty velocityModifier;
    private SerializedProperty pointsAmount;
    private SerializedProperty onDestroyAudio;
    private SerializedProperty onDestroyVFX;

    protected void OnEnable()
    {
        obstacleType = serializedObject.FindProperty("type");
        rigidbody = serializedObject.FindProperty("rb");
        parentObstacle = serializedObject.FindProperty("parentObstacle");
        velocityModifier = serializedObject.FindProperty("velocityModifier");
        pointsAmount = serializedObject.FindProperty("pointsAmount");
        onDestroyAudio = serializedObject.FindProperty("onDestroyAudio");
        onDestroyVFX = serializedObject.FindProperty("onDestroyEffect");
    }

    public override void OnInspectorGUI()
    {
        DrawComponents();
        serializedObject.ApplyModifiedProperties();
    }

    protected void DrawComponents()
    {
        EditorGUILayout.PropertyField(obstacleType);
        switch ((ObstacleType)obstacleType.enumValueIndex)
        {
            case ObstacleType.Inherited:
                EditorGUILayout.PropertyField(parentObstacle);
                break;
            case ObstacleType.Destructable:
            case ObstacleType.NonDestructable:
                EditorGUILayout.PropertyField(rigidbody);
                EditorGUILayout.PropertyField(velocityModifier);
                EditorGUILayout.PropertyField(pointsAmount);
                EditorGUILayout.PropertyField(onDestroyAudio);
                EditorGUILayout.PropertyField(onDestroyVFX);
                break;
            case ObstacleType.None:
            default:
                break;
        }
    }
}
