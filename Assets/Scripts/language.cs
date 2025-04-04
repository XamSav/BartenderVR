using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class language : MonoBehaviour
{
    public void CambiarIdiomaPorBool(bool siguiente)
    {
        StartCoroutine(CambiarIdiomaCoroutine(siguiente));
    }

    private IEnumerator CambiarIdiomaCoroutine(bool siguiente)
    {
        yield return LocalizationSettings.InitializationOperation;

        var locales = LocalizationSettings.AvailableLocales.Locales;
        int actualIndex = locales.IndexOf(LocalizationSettings.SelectedLocale);

        if (actualIndex == -1)
        {
            Debug.LogWarning("No se encontr� el idioma actual en la lista.");
            yield break;
        }

        int nuevoIndex;
        if (siguiente)
        {
            // Si estamos en el �ltimo idioma, volvemos al primero (circular)
            nuevoIndex = (actualIndex + 1) % locales.Count;
        }
        else
        {
            // Si estamos en el primero, vamos al �ltimo (circular)
            nuevoIndex = (actualIndex - 1 + locales.Count) % locales.Count;
        }

        LocalizationSettings.SelectedLocale = locales[nuevoIndex];
    }
}
