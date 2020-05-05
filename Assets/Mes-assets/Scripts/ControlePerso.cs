using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/**
 * Author: Olivier Fortier
 * Description: Classe qui assure le controle du personnage et de ses interractions diverses
 * */

public class ControlePerso : MonoBehaviour
{

    //référence au système de particules qu'on active pour donner une rétroaction visuelle au joueur lorsqu'il clique pour bouger
    public ParticleSystem particuleSouris;

    [Header("information sur la vie")]

    //référence a l'objet UI de la vie du joueur
    public GameObject uiVieJoueur;

    //la vie actuelle du personnage
    public float viePersonnage;

    //le nombre maximal de vie du personnage
    public float viePersonnageMax;

    //référence au navmeshagent du personnage pour pouvoir le faire bouger par clic de souris comme dans diablo
    private NavMeshAgent agentPerso;

    //référence à l'animator du personnage
    private Animator animPerso;

    //spécifier dans l'inspecteur une valeur pour le calcul des dégats aux ennemis
    [Header("les dommages de chaque attaque")]
    public float dommagesAttaque;

    [Header("configuration du délai entre attaques")]
    public float configDelai;
    //enregistrer le délai entre 2 attaques du personnage
    public float delaiAttaque = 0;

    //enregistrer l'état d'attaque du joueur
    public bool enAttaque = false;

    [Header("état du mouvement du joueur")]
    //enregistrer l'état de mouvement du joueur
    public bool enMouvement = false;

    //calcul de distance entre le personnage et l'ennemi
    public float distance;

    //obtenir la vélocité du navmeshagent (du personnage) pour effectuer plusieurs vérifications si besoin
    public Vector3 velocitePerso;

    //enregistrer l'état de vie ou de mort du personnage
    private bool estMort = false;


    private void Start()
    {
        //obtenir le composant navmesh et l'associer à la variable pour travailler avec
        agentPerso = GetComponent<NavMeshAgent>();

        //obtenir le composant animator afin de pouvoir modifier et commencer des animations sur le personnage
        animPerso = GetComponent<Animator>();
    }

    private void Update()
    {
        //mettre à jour la barre de vie du joueur à chaque frame
        AfficherVie();
        
        if (!estMort)
        {
            //calculer la distance entre la cible de la souris et le joueur
            distance = Vector3.Distance(transform.position, SelectCible() != null ? SelectCible().transform.position : Vector3.zero);

            //obtenir la vélocité du navmeshagent (du personnage) pour effectuer plusieurs vérifications si besoin
            velocitePerso = agentPerso.velocity;

            TimerAttaque();

            clicDroit();

            clicGauche();

            //activer l'animation de marche si le personnage est en mouvement
            if (velocitePerso.magnitude > 0)
            {
                enMouvement = true;
            }
            else
            {
                enMouvement = false;
            }

            //activer/désactiver l'animation de mouvement
            animPerso.SetFloat("enMouvement", velocitePerso.magnitude);
        }
    }

    //méthode pour mettre à jour la barre de vie du joueur
    private void AfficherVie() {
       uiVieJoueur.GetComponent<Image>().fillAmount = ((viePersonnage * 1) / viePersonnageMax);
    }

    //méthode pour sélectionner un objet,une cible, ou un ennemi
    private GameObject SelectCible()
    {
        //obtenir l'endroit ou on clique avec la souris dans l'écran
        Ray rayonSouris = Camera.main.ScreenPointToRay(Input.mousePosition);

        //initialiser la variable à retourner à null
        GameObject cibleSelect = null;

        //si on clique sur l'écran
        if (Physics.Raycast(rayonSouris, out RaycastHit clic, Mathf.Infinity))
        {
            //enregistrer l'objet/ennemi/cible dans la variable si c'est quelque chose avec lequel on peut interragir
            if (clic.transform.CompareTag("ennemi")) //|| clic.transform.CompareTag("objet") )
            {
                //assigner la cible du clic à la variable qu'on va retourner
                cibleSelect = clic.transform.gameObject;
            }
        }

        //retourner la cible sélectionner pour utiliser ailleurs
        return cibleSelect;
    }

    //méthode pour créer l'effet au clic de la souris et le position à l'endroit cliqué dans l'écran comme dans Diablo
    private void EffetSouris()
    {
        //obtenir l'endroit ou on clique avec la souris dans l'écran
        Ray rayonSouris = Camera.main.ScreenPointToRay(Input.mousePosition);

        //créer un rayon à l'endroit ou on clique dans l'écran afin d'obtenir les coordonées du point
        Physics.Raycast(rayonSouris, out RaycastHit clic, Mathf.Infinity);

        //changer la position des particules de l'effet du clic de souris à l'emplacement du clic
        particuleSouris.transform.position = clic.point;

        //accéder au composante d'emission du systeme de particules
        var emissionParticules = particuleSouris.GetComponent<ParticleSystem>().emission;
        //activer l'émission des particules
        emissionParticules.enabled = true;

        //appeler la méthode pour terminer l'effet apres une demie seconde
        Invoke("TerminerEffet", 0.5f);
    }

    //méthode pour arrêter les effets de particule du clic de la souris
    private void TerminerEffet()
    {
        //accéder au composante d'emission du systeme de particules
        var emissionParticules = particuleSouris.GetComponent<ParticleSystem>().emission;
        //désactiver l'émission des particules
        emissionParticules.enabled = false;
    }

    //méthode pour permettre au personnage de se déplacer par clic de la souris comme Diablo
    public void BougerPerso()
    {
        //obtenir l'endroit ou on clique avec la souris dans l'écran
        Ray rayonSouris = Camera.main.ScreenPointToRay(Input.mousePosition);

        //si on clique sur l'écran
        if (Physics.Raycast(rayonSouris, out RaycastHit clic, Mathf.Infinity))
        {
            //faire bouger le personnage par son navmeshagent à l'endroit désigné par le clic de la souris
            agentPerso.SetDestination(clic.point);
        }
    }

    //mouvement du personnage lors du clic droit de la souris
    public void clicDroit()
    {

        if (Input.GetMouseButton(1))
        {
            //jouer l'effet de clic de souris
            EffetSouris();

            //faire bouger le personnage à l'endroit ou le joueur clic dans l'écran
            BougerPerso();
        }
    }
    
    //action du personnage lors du clic gauche de la souris sur un objet/cible/ennemi
    public void clicGauche()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //si la cible est un ennemi, bouger vers lui et attaquer
            if (SelectCible() != null && SelectCible().CompareTag("ennemi"))
            {
                //si le joueur n'est pas en distance d'attaque, le faire bouger plus pres
                if (distance >= 3 && !enAttaque)
                {
                    //déplacer le personnage jusqu'a la cible
                    BougerPerso();
                }
                else
                {
                    //si le personnage est pres et immobile, alors attaquer
                    if (!enMouvement && delaiAttaque <= 0 && !enAttaque) AttaqueBasique();
                }
            }
        }
    }

    //méthode pour effectuer une attaque de base avec le clic gauche
    public void AttaqueBasique()
    {
        //on entre en attaque
        enAttaque = true;

        //jouer l'Animation d'attaque
        animPerso.SetTrigger("siAttaque");
        //le personnage regarde dans la direction de l'attaque
        transform.LookAt(SelectCible().transform);

        //si l'animation d'attaque est finie
        if (!animPerso.GetCurrentAnimatorStateInfo(0).IsName("arthur_attack_01"))
        {
            //causer des dommages à l'ennemi
            SelectCible().GetComponent<ScriptEnnemi>().vieActuelle -= dommagesAttaque;

            //on sort de l'Attaque
            enAttaque = false;

            //délai de 0.75 secondes entre chaque attaque
            delaiAttaque = configDelai;
        }
    }

    //gère le délai entre chaque attaque
    public void TimerAttaque()
    {
        //tant que le décompte n'a pas atteint 0 secondes
        if (delaiAttaque >= 0)
        {
            //enlever 1 seconde au décompte
            delaiAttaque -= Time.deltaTime;
        }
    }
}