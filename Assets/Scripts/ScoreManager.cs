
using UnityEngine;
using TMPro;
using Unity.Netcode;

// Script pour gérer le score du jeu et gérer la fin de partie
public class ScoreManager : NetworkBehaviour // ne pas oublier component networkObject
{
   public static ScoreManager instance; // singleton
   [SerializeField] private TMP_Text scoreTxt; // Référence à la zone qui affiche le texte. À définir dans l'inspecteur
   [SerializeField] private int pointageCible; // Le pointage à atteindre pour gagner (servira plus tard)

   private NetworkVariable<int> scoreHote = new NetworkVariable<int>(); // Score de l'hôte (variable réseau)
   private NetworkVariable<int> scoreClient = new NetworkVariable<int>(); // Score du client (variable réseau)

   // Création du singleton
   private void Awake()
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

   /*Méthode appelée lors du spawn de l'objet réseau
   - Initialise les scores à 0 si c'est le serveur
   - S'abonne aux événements de changement de valeur des scores */
   public override void OnNetworkSpawn()
   {
       base.OnNetworkSpawn();
       
       if (IsServer)
       {
           scoreHote.Value = 0;
           scoreClient.Value = 0;
       }
      
       scoreHote.OnValueChanged += OnChangementPointageHote;
       scoreClient.OnValueChanged += OnChangementPointageClient;
   }

   /* Méthode appelée lors de la désactivation de l'objet réseau
   - Se désabonne des événements de changement de valeur des scores */
   public override void OnNetworkDespawn()
   {
       base.OnNetworkDespawn();
       
       scoreHote.OnValueChanged -= OnChangementPointageHote;
       scoreClient.OnValueChanged -= OnChangementPointageClient;
   }

   /* Fonction pour augmenter le score de l'hôte
    - On incrémente le score de l'hôte
    - On vérifie si la partie est terminée*/
   public void AugmenteHoteScore() // Fonction public. À implémenter La balle doit appeler cette fonction lorsqu'un but est compté par l'hôte
   {
       scoreHote.Value++;
   }

   /* Fonction pour augmenter le score du client
    - On incrémente le score du client
    - On vérifie si la partie est terminée*/
   public void AugmenteScoreClient() // Fonction public. À implémenter La balle doit appeler cette fonction lorsqu'un but est compté par le client
   {
       scoreClient.Value++;
   }

   // Méthode pour gérer le changement de valeur du score de l'hôte
   // Elle est appelée automatiquement à chaque fois que le score de l'hôte change
   // Elle met à jour le texte affiché avec les scores actuels
   private void OnChangementPointageHote(int ancienScoreHote, int nouveauScoreHote)
   {
       if (ancienScoreHote == nouveauScoreHote) return; // Évite de mettre à jour si le score n'a pas changé

       scoreTxt.text = scoreHote.Value + " - " + scoreClient.Value;
   }

   // Méthode pour gérer le changement de valeur du score du client
   // Elle est appelée automatiquement à chaque fois que le score du client change
   // Elle met à jour le texte affiché avec les scores actuels
   private void OnChangementPointageClient(int ancienScoreClient, int nouveauScoreClient)
   {
       if (ancienScoreClient == nouveauScoreClient) return; // Évite de mettre à jour si le score n'a pas changé
       scoreTxt.text = scoreHote.Value + " - " + scoreClient.Value;
   }
}
