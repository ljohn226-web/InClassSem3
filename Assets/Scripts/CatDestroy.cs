using UnityEngine;

public class CatDestroy : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }
}
