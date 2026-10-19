using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class GameController : MonoBehaviour
{
   public static GameController instance { get; private set;  }

    public Transform startLoc;
    public Vector3 loadLoc;
    public Quaternion loadRot;
    public GameObject ThirdPersonRig;

    //cat collection
    public int currentCatz = 0;
    public List<string> collectedCatID;
    public CatUI catDisplay;
    public CatAdd catUpdate;
    public NpcInteract NpcI;
    public void Awake()
    {
        if (instance != null && instance != this) //if there is an existing instance 
        {                                         //and its not THIS instance
            Destroy(this.gameObject);             //destroy this instance
        }
        else
        {
            instance = this;                      //otherwise this is the instance
            DontDestroyOnLoad(this);              //and stay persistent across scenes

            Debug.Log("Hello GameController");    //working
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (loadLoc != Vector3.zero)
            {
                Debug.Log("new spawn point");
                SpawnCharacter(loadLoc, loadRot);
            }
            else
            {
                Debug.Log("OG spawn point");
                SpawnCharacter(startLoc.position, startLoc.rotation);
            }

            //loadCatz(); eventually 
        }

        else if (scene.buildIndex == 1)
        {
            DialogueRunner runner = GameObject.FindAnyObjectByType<DialogueRunner>();
            runner.onDialogueStart.AddListener(() => setPlayerMovement(true));
            runner.onDialogueComplete.AddListener(() => setPlayerMovement(false));
            Debug.Log("Loading Freezing Events");
        }
        
            //loadCatz(); eventually
       
    }

    public void SpawnCharacter(Vector3 loc, Quaternion rot)
    {
        Instantiate(ThirdPersonRig, loc, rot);
    }

    public void SetSpawnPoint(Vector3 loc, Quaternion rot)
    {
        loadLoc = loc;
        loadRot = rot;
    }


    public void catCollect(string catID)
    {
        Debug.Log("found car");
        collectedCatID.Add(catID);
        currentCatz++;
        catUpdate.AddListener(catDisplay.AddCatz);
        catUpdate.Invoke(currentCatz);
    }

    public void loadCatz()
    {
        CatGrab[] allCatzInScene = FindObjectsByType<CatGrab>();
        if (collectedCatID != null)
        {
            foreach (CatGrab cat in allCatzInScene) //for each cat in the scene
            {
                if (collectedCatID.Contains(cat.catID))
                {
                    //NOT HAPPENING YET
                    Destroy(cat.gameObject);
                }
            }
        }
    }

    public void Update()
    {
        if (currentCatz == 8)
        {
            NpcI.storyData.npcData.currentPhase = NpcData.dialoguePhase.questComplete;
        }
    }

    //setter method for UI
    public void setUI(CatUI UI)
    {
        catDisplay = UI;
    }
    public void setPlayerMovement(bool b)
    {
        //Debug.Log("GAH FREEEZE");
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //basic rig prefab does not have the class we need to access
        ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();
        Debug.Log("TPC: " + tpc.ToString());


        if (b)
        {
            tpc.enabled = false;
            Debug.Log("u should be frozen"); //notHappening yet
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            tpc.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            //Debug.Log("unfrozen");
        }
    }

    public void moveForDialogue(Transform movePos)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = movePos.position;
    }
}

[System.Serializable]

public class CatAdd : UnityEvent<int>
{

}
