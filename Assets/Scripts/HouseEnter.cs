using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEnter : MonoBehaviour
{

    public Transform houseReturn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.instance.SetSpawnPoint(houseReturn.position, houseReturn.rotation);
            SceneManager.LoadScene(1);
        }
    }
}
