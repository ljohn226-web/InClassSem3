using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (NpcInteract))]
public class NpcInteractEditor : Editor

{
    public override void OnInspectorGUI()
    {
        //displays on screen the default inspector of type of class, NPC interact here

        //-------------------------------------------------------------------
        //DrawDefaultInspector();
        //EditorGUILayout.HelpBox("Reminder: Load in the Npc Cam Prefab first BEFORE trying to generate and assign variable", MessageType.Info);
        //-------------------------------------------------------------------

        //loading in serializedProp tmp variable
        //and loading in type propertery from npcInteract
        SerializedProperty typeProp = serializedObject.FindProperty("type");
        EditorGUILayout.PropertyField(typeProp);

        iType currentType = (iType)typeProp.enumValueIndex;

        if (currentType == iType.npc)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("txtData"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("camData"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("storyData"));
        }

        //loading the class we want to edit/change
        //so then we can call the methods from that class in our editor layout (HUH)
        NpcInteract npcI = (NpcInteract)target;


        //if statements in editor scripts generate GUI layout elements rather than checking to see if they exist
        //this is not a condition check, its fullfilling the condition as true automatically
        //each statement block creates a new interactable segment in inspector
        if (GUILayout.Button("Set Interact Text Object"))
        {
            //made a button to set interaction text based on tag in the inspectir 
            npcI.setTextVars();
        }
        else if (GUILayout.Button("Load Npc Cam Prefab "))
        {
            npcI.loadNpcCamPrefab();
        }
        else if (GUILayout.Button("Set Npc Cam Variable"))
        {
            npcI.setNpcCam();
        }
        else if (GUILayout.Button("Load Dialogue Runner"))
        {
            npcI.setYarnSpinner();
        }

        serializedObject.ApplyModifiedProperties();

    }

    
}
