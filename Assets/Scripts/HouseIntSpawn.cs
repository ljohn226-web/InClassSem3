using UnityEngine;

public class HouseIntSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController.instance.SpawnCharacter(this.transform.position, this.transform.rotation);

    }

   
}
