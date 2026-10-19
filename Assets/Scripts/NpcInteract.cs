using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Yarn.Unity;

public enum iType {  npc, item }

public class NpcInteract : MonoBehaviour
{
    //DONE A check if player in trigger radius
    //DONE B check if player in trigger when they press interaction key 
    //DONE C activate yarn spinner -- our dialogue runner
    //DONE E check pController for movement
    //DONE F make sure player cant move PARALYSE THEMMMMM
    //DONE G free mouse controller so you can select dialogue options

    //TODO D can only have the converstation once
    //TODO limit (and evench control) camera movement

    [Tooltip("Set the type of interaction //npc/item// to load the correct variables")]
    public iType type;

    //all variables have been restuctured using STRUCTS to make inspector more visually clear, and add in extra editor functionality
    [System.Serializable]
    public struct TextData
    {
        public TextMeshProUGUI interactTxt;
        public string interaction;
    }

    [Header("Text Display Variables")]
    public TextData txtData;
    public bool canInteract;

    [System.Serializable]
    public struct CamData
    {
        public NpcCamControl camController;
        public GameObject npcCamPrefab;
    }

    [Header("Camera Variables")]
    public CamData camData;

    [System.Serializable]
    public struct DialogueData
    {
        public DialogueRunner dialogue;
        public NpcData npcData;    

    }

    [Header("Dialogue and NPC data Variables")]
    public DialogueData storyData;
    public void Start()
    {
        //camController = transform.GetComponentInChildren<NpcCamControl>();
    }
    //---------------- START EDITOR FUNCTIONS----------------------//

            //create methods here that are called in Interact Editor, and displayed in inspector
    public void setTextVars()
    {
        txtData.interactTxt = GameObject.FindGameObjectWithTag("Itxt").GetComponent<TextMeshProUGUI>();
    }

    public void loadNpcCamPrefab()
    {
        Instantiate(camData.npcCamPrefab, transform);
    }

    public void setNpcCam()
    {
        camData.camController = transform.GetComponentInChildren<NpcCamControl>();
    }

    public void setYarnSpinner()
    {
        storyData.dialogue = GameObject.FindGameObjectWithTag("runner").GetComponent<DialogueRunner>();
    }

    //--------------------END EDITOR FUNCTIONS--------------------//

    public void OnTriggerEnter(Collider other) //A
    {
        if (other.gameObject.tag == "Player")
        {
            setText(txtData.interaction);
            canInteract = true; //B
        }
    }

    public void OnTriggerExit(Collider other)//A
    {
        if (other.gameObject.tag == "Player")
        {
            setText("");
            canInteract = false; //B
        }
    }

    //setter method for interaction
    public void setText(string txt)
    {
        txtData.interactTxt.text = txt;
    }

    public void Update()
    {
        if (canInteract)
        {
            //Cursor.lockState = CursorLockMode.None;
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                setText("");
                camData.camController.movePlayer();
                storyData.dialogue.StartDialogue(storyData.npcData.startNode);
                canInteract = false;
                Debug.Log("Aqquainted with meows");
            }

        }
    }

    [YarnCommand("dialoguePhaseSetter")]
    public void SetYarnPhase()
    {
        Debug.Log("setter is called");
        InMemoryVariableStorage vData = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        vData.SetValue("$dialoguePhase", storyData.npcData.currentPhase.ToString());        
    }

    [YarnCommand("dialoguePhaseGetter")]

    public void GetYarnPhase(string phase)
    {
        Debug.Log("getter is called");
        if (phase == "questTaken")
        {
            storyData.npcData.currentPhase = NpcData.dialoguePhase.questTaken;
        }
        if (phase == "questDenied")
        {
            storyData.npcData.currentPhase = NpcData.dialoguePhase.questDenied;
        }
        if (phase == "questComplete")
        {
            storyData.npcData.currentPhase = NpcData.dialoguePhase.questComplete;
        }
        if (phase == "questReturn")
        {
            storyData.npcData.currentPhase = NpcData.dialoguePhase.questReturn;
        }
    }

    public void OnApplicationQuit()
    {
        storyData.npcData.resetData();
    }
}
