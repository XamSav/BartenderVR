using UnityEngine;

public class Botle : MonoBehaviour
{
    [SerializeField][Range(20, 180)] private float tiltThreshold = 60f;
    public bool isPouring = false;
    private GameObject _liquidParticles;

    private void Start()
    {
        if (_liquidParticles == null)
        {
            //Find child object with name "LiquidParticles"
            _liquidParticles = transform.Find("LiquidParticle").gameObject;
        }
    }
    void Update()
    {
        float xTilt = transform.eulerAngles.x;
        float zTilt = transform.eulerAngles.z;
        if (zTilt > tiltThreshold && zTilt < 360 - tiltThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }
        }
        else if (xTilt > tiltThreshold && xTilt < 360 - tiltThreshold)
        {
            if (!isPouring)
            {
                StartPouring();
            }
        }
        else
        {
            if (isPouring)
            {
                StopPouring();
            }
        }
    }

    void StartPouring()
    {
        isPouring = true;
        _liquidParticles.SetActive(true);
    }

    void StopPouring()
    {
        isPouring = false;
        Debug.Log("Botella en posición normal, deteniendo vertido.");
        _liquidParticles.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            if(transform.GetComponent<GlassContent>() != null)
            {
                transform.GetComponent<GlassContent>().ClearContents();
            }
            gameObject.SetActive(false);
        }
    }
}
