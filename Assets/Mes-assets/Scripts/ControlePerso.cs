using System.Collections;
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

    [Header("Référence a l'arme")]
    //référence au gameobject contenant le rigidbody de l'arme
    public GameObject refArme;

    //spécifier dans l'inspecteur une valeur pour le calcul des dégats aux ennemis
    [Header("les dommages de chaque attaque")]
    public float dommagesAttaque;

    [Header("configuration du délai entre attaques")]
    //la durée d'une attaque
    public float dureeAttaque = 1.20f;
    public float configDelai = 1.25f;
    //enregistrer le délai entre 2 attaques du personnage
    [HideInInspector] public float delaiAttaque = 0f;

    //enregistrer l'état d'attaque du joueur
    [HideInInspector] public bool enAttaque = false;

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

        refArme.GetComponent<BoxCollider>().enabled = false;
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

            //activer/désactiver l'animation de mouvement
            animPerso.SetFloat("enMouvement", velocitePerso.magnitude);
        }
    }

    //méthode pour mettre à jour la barre de vie du joueur
    private void AfficherVie()
    {
        uiVieJoueur.GetComponent<Image>().fillAmount = ((viePersonnage * 1) / viePersonnageMax);
    }

    //méthode pour sélectionner un objet,une cible, ou un ennemi
    public GameObject SelectCible()
    {
        //obtenir l'endroit ou on clique avec la souris dans l'écran
        Ray rayonSouris = Camera.main.ScreenPointToRay(Input.mousePosition);

        //initialiser la variable à retourner à null
        GameObject cibleSelect = null;

        //si on clique sur l'écran
        if (Physics.Raycast(rayonSouris, out RaycastHit clic, Mathf.Infinity))
        {
            //enregistrer l'objet/ennemi/cible dans la variable si c'est quelque chose avec lequel on peut interragir
            if (clic.transform.CompareTag("ennemi") || ListeTagObjetsPersonnages.liste.Contains(clic.transform.tag))
            {
                //assigner la cible du clic à la variable qu'on va retourner
                cibleSelect = clic.transform.gameObject;
            }
            else
            {
                cibleSelect = null;
            }
        }
        else
        {
            cibleSelect = null;
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
            //jouer l'effet de clic de souris
            EffetSouris();

            

            //si c'est un personnage qu'on sélectionne en meme temps de cliquer
            if (SelectCible() != null && ListeTagObjetsPersonnages.liste.Contains(SelectCible().tag))
            {
                //si le joueur est loin du personnage, s'approcher de lui
                if (distance > 5) agentPerso.SetDestination(SelectCible().transform.position);

                //si le joueur est suffisament proche du personnage, déclencher l'événement de clic
                if (distance <= 5)
                {
                    //vérifier qu'on a bien un composant de déclencheur d'événement
                    if (SelectCible().gameObject.TryGetComponent(out DeclencheurEvenement evenement))
                    {
                        //déclencher un événement de type click
                        evenement.EvenementCliquer();
                    }
                }

            }// sinon, attaquer
            else if (delaiAttaque <= 0)
            {
                //arreter de bouger, et attaquer
                agentPerso.ResetPath();
                agentPerso.isStopped = true;
                AttaqueBasique();

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
        if (SelectCible() != null && SelectCible().gameObject.CompareTag("ennemi"))
        {
            transform.LookAt(SelectCible().transform);
        }
        //si on est en état d'attaque
        if (enAttaque)
        {   //regarder dans la direction du clic de la souris
            transform.LookAt(GameObject.Find("particulesClicSouris").transform);
            //commencer la gestion de l'attaque qui se fait par activation du collider de l'épée
            StartCoroutine(PrepareAttaque());

        }
    }

    //enumerateur pour attendre qu'une partie de l'animation de l'attaque finisse pour activer le collider de l'arme
    public IEnumerator PrepareAttaque()
    {

        //délai de 1.25 secondes entre chaque attaque
        delaiAttaque = configDelai;

        //on attent que cette partie de l'animation finisse
        yield return new WaitForSeconds(0.70f);

        //on active le collider après
        refArme.GetComponent<BoxCollider>().enabled = true;


        //on commence la prochaine séquence d'attaque
        StartCoroutine(TempsAttaque());

    }

    //enumerateur pour attendre qu'une partie de l'animation de l'attaque finisse pour désactiver le collider de l'arme
    public IEnumerator TempsAttaque()
    {

        //on attends la durée de l'attaque
        yield return new WaitForSeconds(dureeAttaque);

        //on sort de l'Attaque
        enAttaque = false;

        //on désactive le collider de l'arme
        refArme.GetComponent<BoxCollider>().enabled = false;

        //on peut recommencer a bouger
        agentPerso.isStopped = false;

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