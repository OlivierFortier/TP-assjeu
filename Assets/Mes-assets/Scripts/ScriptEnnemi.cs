using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Gère les fonctionnalités des ennemis
/// </summary>
public class ScriptEnnemi : MonoBehaviour
{
    #region propriétés
    //référence au gameobject du personnage du joueur
    public GameObject cibleJoueur;

    public bool toucheJoueur = false;

    // la vie actuelle de l'entitée, qui change selon si elle est attaquée ou affectée
    [Header("vie de l'ennemi")]
    public float vieActuelle;

    // la vie maximale de l'entitée, c'est avec ce niveau de vie que l'entitée commence
    public float vieMaximum;

    //booléen pour connaitre l'état de vie de l'entitée
    private bool estMort;

    //référence au navmeshagent du personnage pour pouvoir le faire bouger par lui même
    private NavMeshAgent agentEnnemi;

    //référence à l'animator du personnage
    private Animator animEnnemi;

    //ajuster dans l'inspecteur la valeur des dommages de l'ennemi
    [Header("ajuster les attaques")]
    public float dommagesAttaque;
    

    //paramétrer le délai avant que le ennemi puisse attaquer
    [Header("Temps entre les attaques")]
    public float configDelai;
    public float delaiAttaque = 0;  

    //obtenir la vélocité du navmeshagent (de l'ennemi) pour effectuer plusieurs vérifications
    [Header("vitesse")]
    public Vector3 velociteEnnemi;

    //enregistrer l'état du mouvement de l'ennemi pour gérer les animations
    public bool enMouvement = false;

    //créer une variable pour tenir la distance entre 2 entitées pour effectuer des calculs
    public float distanceJoueur; 

    //gérer la distance minimum avant que l'ennemi agresse le joueur
    public float distanceAgression = 10;

    //pour détecter si l'ennemi est en "aggro" avec le joueur pour qu'il se mette à le poursuivre
    public bool agresseJoueur = false;

    #endregion

    private void Awake()
    {
        //assigner le composant navmeshagent pour la navigation
        agentEnnemi = gameObject.GetComponent<NavMeshAgent>();

        //assigner le composant animator pour gérer les animations
        animEnnemi = gameObject.GetComponent<Animator>();

        //initialiser la vie actuelle
        vieActuelle = vieMaximum;
        
    }

    
    void Update()
    {
        

        DetectionEtatVie();

        if(!estMort) {

            //obtenir la vélocité de l'ennemi
            velociteEnnemi = agentEnnemi.velocity;

            //calcul de la distance entre l'ennemi et le joueur
            distanceJoueur = DistanceAvecCible(cibleJoueur);

            if(distanceJoueur < distanceAgression) {
                agresseJoueur = true;
            }

            TimerAttaque();

            Bouger();

            if(toucheJoueur && delaiAttaque <= 0) {
                Attaquer(cibleJoueur);
            }

        }
    
        
    }

    private void OnCollisionEnter(Collision autreObjet) {
        
        if(autreObjet.gameObject.name == "joueur"){
            toucheJoueur = true;
        }
    }

    private void OnCollisionExit(Collision autreObjet) {
        
        if(autreObjet.gameObject.name == "joueur"){
            toucheJoueur = false;
        }
    }

    //méthode pour bouger l'ennemi
    public void Bouger()
    {
        if(agresseJoueur)
        {//si la distance est de 4 unités ou plus, bouger l'ennemi vers le personnage
        if (distanceJoueur >= 2)
        {
            //positionner l'ennemi vers la position du joueur
            agentEnnemi.SetDestination(cibleJoueur.transform.position);
        }

        //regarder vers le joueur
        transform.LookAt(cibleJoueur.transform);

        if(velociteEnnemi.magnitude > 0) {
            animEnnemi.SetFloat("enMarche", velociteEnnemi.magnitude);
        }}
        
    }

    //retourne la distance entre le ennemi et la cible
    public float DistanceAvecCible(GameObject cible) {
        return Vector3.Distance(transform.position, cible.transform.position);
    }

    //permet au ennemi d'attaquer une cible
    public void Attaquer(GameObject cible)
    {

        //jouer l'Animation d'attaque
        animEnnemi.SetTrigger("isAttaque");
        //le personnage regarde dans la direction de l'attaque
        transform.LookAt(cible.transform);

        //TODO : UTILISER UNE COROUTINE POUR LE TEMPS DE L'ANIMATION
        //si l'animation d'attaque est finie
        if (!animEnnemi.GetCurrentAnimatorStateInfo(0).IsTag("attaque"))
        {
            //causer des dommages à la cible
            cible.GetComponent<ControlePerso>().viePersonnage -= dommagesAttaque;

            //délai de 1.25 secondes entre chaque attaque
            delaiAttaque = configDelai;

        }
    }

    //gère le délai entre chaque attaque
    public void TimerAttaque() {
        //tant que le décompte n'a pas atteint 0 secondes
            if (delaiAttaque >= 0)
            {
                //enlever 1 seconde au décompte
                delaiAttaque -= Time.deltaTime;
            }
    }

    //méthode pour détruire l'entitée
    public void DetruireEntite()
    {
        //détruire l'entitée
        Destroy(gameObject);
    }

//si la vie actuelle de l'entitée atteint 0, il meurt
    public void DetectionEtatVie() {
        
        if (vieActuelle <= 0)
        {
            Mourir();
        }
    }

    //gestion de la mort du ennemi
    public void Mourir() {
        
            //l'entitée est morte
            estMort = true;

            //désactiver le navmesh agent
            agentEnnemi.enabled = false;

            //désactiver les autres animations
            animEnnemi.SetFloat("enMarche", 0);

            //déclencher l'animation de mort
            animEnnemi.SetTrigger("isMort");

            //désactiver le survol de la souris pour ne pas que le texte reste à l'écran
            Destroy(gameObject.GetComponent<survolSouris>().instanceTexte);

            Destroy(gameObject.GetComponent<survolSouris>().instanceBarreVie);

            //si l'animation de mort est terminée
            if (!animEnnemi.GetCurrentAnimatorStateInfo(0).IsTag("mourir"))
            {
                //détruire l'entitée
                Invoke("DetruireEntite", 2f);
            }
        
    }
}
