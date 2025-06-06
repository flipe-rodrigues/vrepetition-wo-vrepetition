using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    public GameObject spawner;   // Objeto que deve APARECER
    public GameObject image;   // Objeto que deve DESAPARECER

    public void OnButtonClick()
    {
        if (spawner != null)
            spawner.SetActive(true);  // Ativa o objeto

        if (image != null)
            image.SetActive(false); // Desativa o objeto
    }
}