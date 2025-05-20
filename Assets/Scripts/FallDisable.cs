using UnityEngine;

public class FallDisable : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            if (transform.GetComponent<GlassContent>() != null)
            {
                transform.GetComponent<GlassContent>().ClearContents();
            }
            gameObject.SetActive(false);
        }
    }
}
