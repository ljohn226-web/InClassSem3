using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "NpcData")]
public class NpcData : ScriptableObject
{
    public string npcName;
    public string startNode;
    public enum dialoguePhase {  start, questDenied, questTaken, questComplete, questReturn}
    public dialoguePhase currentPhase;

    public void resetData()
    {
        currentPhase = dialoguePhase.start;
    }
}
