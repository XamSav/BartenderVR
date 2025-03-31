using UnityEngine;

public class Strainer : MonoBehaviour
{
    private GlassContent glassContent;

    private void Start()
    {
        glassContent = GetComponentInParent<GlassContent>(); // Busca en el vaso de mezcla
        if (glassContent == null)
        {
            Debug.LogError("Strainer: No se encontró GlassContent en el padre.");
        }
    }

    public void EquipStrainer(bool state)
    {
        if (glassContent != null)
        {
            glassContent.SetStrainer(state);
        }
    }
}
