using UnityEngine;

public class CatGrab : MonoBehaviour
{
    public string catID;

    public void Awake()
    {
        catID = this.name + "_" + transform.position.ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided w " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            catGot();
        }
    }

    public void catGot()
    {
        GameController.instance.catCollect(catID);
        //this.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;   
    }
    
        
}
