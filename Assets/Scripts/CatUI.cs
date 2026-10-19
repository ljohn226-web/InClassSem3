using TMPro;
using UnityEngine;

public class CatUI : MonoBehaviour
{
    public TextMeshProUGUI catCountText;
    
    public void Awake()
    {
        GameController.instance.setUI(this);
        AddCatz(GameController.instance.currentCatz);
    }

    public void AddCatz(int cat)
    {
        catCountText.text = cat.ToString();
    }

}
