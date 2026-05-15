using UnityEngine;
using UnityEngine.UI;

public class BarraConfianzaRuta : MonoBehaviour
{
    public Slider slider;
    public int npcIDAsociado;
    // se le asigna una barra de confianza a cada NPC segun su ID
    void Update()
    {
        if (RelacionesManager.instancia != null)
        {
            slider.value = RelacionesManager.instancia.GetConfianza(npcIDAsociado);
        }
    }
}
