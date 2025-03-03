using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("Tiempo")]
    [SerializeField] private TMP_Text _clock;
    private void Update()
    {
        float time = GameManager.instance.GetTime();
        _clock.text = string.Format("{00:F2} : {01:F2}", time);
    }
}
