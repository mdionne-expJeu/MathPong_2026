using System.Collections;
using UnityEngine;

public class CamShaker : MonoBehaviour
{
    bool shakingCam; // pour savoir si la caméra est en train de trembler

    // Fonction publique pour démarrer le shake de la caméra
    // Paramètres : durée, amplitude et intensité
    // - durée : combien de temps le shake doit durer
    public void Shake(float duration, float amount, float intensity)
    {
        if (!shakingCam)
            StartCoroutine(ShakeCam(duration, amount, intensity));
    }

    // Coroutine pour gérer le shake de la caméra
    IEnumerator ShakeCam(float dur, float amount, float intensity)
    {
        float t = dur;
        Vector3 originalPos = Camera.main.transform.localPosition;
        Vector3 targetPos = Vector3.zero;
        shakingCam = true;

        // Boucle jusqu'à ce que le temps soit écoulé
        // On utilise Random.insideUnitSphere pour obtenir un vecteur aléatoire dans une sphère
        // On déplace la caméra vers cette position aléatoire
        // On utilise Vector3.Lerp pour interpoler entre la position actuelle et la position cible
        // On vérifie si la distance entre la position actuelle et la cible est inférieure
        // à un seuil pour réinitialiser la cible à zéro
        // On réduit le temps restant à chaque frame
        while (t > 0.0f)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = originalPos + (Random.insideUnitSphere * amount);
            }

            Camera.main.transform.localPosition =
                Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime);

            if (Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;
        }
        // Une fois le shake terminé, on remet la caméra à sa position originale
        // et on met shakingCam à false pour indiquer que le shake est terminé
        Camera.main.transform.localPosition = originalPos;
        shakingCam = false;
    }
}
