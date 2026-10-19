using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxTouch : MonoBehaviour
{
    public bool canInteract;
    public TextMeshProUGUI interactText;
    public NpcInteract npcI;
    public CatGrab catGrb;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            setText("Press E to search");
            canInteract = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            setText("");
            canInteract = false;
        }
    }

    public void setText(string txt)
    {
        interactText.text = txt;
    }

    public void Update()
    {
        if (canInteract)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                
                npcI.storyData.npcData.currentPhase = NpcData.dialoguePhase.questComplete;
                setText("You found a kitty!");
                catGrb.catGot();
                canInteract = false;
                Debug.Log("stop touching");
               // if (GameController.currentCatz == 8)
                {

                }
            }
        }
    }
}
