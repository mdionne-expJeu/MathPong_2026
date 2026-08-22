using UnityEngine;
using Unity.Netcode; // namespace pour utiliser Netcode
using UnityEngine.SceneManagement; // namespace pour la gestion des scènes

public class GameManager : NetworkBehaviour //pour un network object
{
    public static GameManager instance;// Singleton pour parler au GameManager de n'importe où

    // Création du singleton si nécessaire
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fonction appelée pour le bouton qui permet de se connecter comme hôte
    public void LanceCommeHote() // Public pour être appeler de l'extérieur (par le bouton Hôte)
    {
        NetworkManager.Singleton.StartHost(); // Fonction du NetworkManager pour démarrer une partie comme hôte
    }

    // Fonction appelée pour le bouton qui permet de se connecter comme client
    public void LanceCommeClient() // Public pour être appeler de l'extérieur (par le bouton Client)
    {
        NetworkManager.Singleton.StartClient(); // Fonction du NetworkManager pour démarrer une partie comme client
    }
}
