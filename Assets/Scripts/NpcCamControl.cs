using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;

public class NpcCamControl : MonoBehaviour
{
    //TODO switch from TPC cam to NPC cam 
    //TODO create methods fro switching position of NPC cam
    //TODO get switchinh to 'talk' to yarn spinner

    public Transform npcLoc;
    public Transform pcLoc;
    public Transform pcMoveLoc;
    public CinemachineCamera npcCinCam;
    public int pValue;
    
    [YarnCommand("camMove")]
    public void camMove(string focus)
    {
        npcCinCam.Priority = pValue;
        if (focus == "npc")
        {
            npcCinCam.gameObject.transform.position = npcLoc.position;
        }
        else if (focus == "pc")
        {
            npcCinCam.gameObject.transform.position = pcLoc.position;
        }
        else if (focus == "return")
        {
            npcCinCam.Priority = 0;
        }
    }

    public void movePlayer()
    {
        GameController.instance.moveForDialogue(pcMoveLoc);
    }

}
