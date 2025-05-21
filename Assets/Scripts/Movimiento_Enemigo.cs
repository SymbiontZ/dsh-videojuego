using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Movimiento_Enemigo : LivingEntity
{
    public Vector3 patrolAreaCenter;
    public Vector3 patrolAreaSize;
    private Transform player;
    public Transform raycastOrigin;
    public float detectionRange = 10f;
    public float fieldOfView = 120f;
    public float chaseTime = 3f;
    public float cooldownAtaque = 2f;
    public float damage = 1;
    public float attackRange = 5f;

    private NavMeshAgent agent;
    private bool isChasing = false;
    private float lastSeenTime = Mathf.NegativeInfinity;
    private bool puedeAtacar = true;
    private bool fin = false;
    private AudioSource audioSource;
    public static int enemigosEliminados = 0;

    // Preparación para el JumpScare
    private Camera playerCamera; 
    public Camera alternativeCamera;

    public Animator animator;


    protected override void Start()
    {
        base.Start();
        Camera cam = Camera.main;
        if (cam != null)
        {
            GameObject cameraObj = cam.gameObject;
            playerCamera = cam;
        }
        else{
            Debug.Log("No se encontró la cámara del jugador START.");
        }
        GameObject player_ = Jugador.Instance.gameObject;
        if (player != null)
        {
            player = player_.transform;
        }
        else
        {
            Debug.Log("No se encontró el objeto del jugador START.");
        }
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponents<AudioSource>()[0];

    }

    void Update()
    {
        // Por si acaso se elimina el jugador, hay que recuperar el transform Y la camara
        if (player == null)
        {
            GameObject player_ = Jugador.Instance.gameObject;
            if (player_ != null)
            {
                player = player_.transform;
            }
            else
            {
                Debug.Log("No se encontró el objeto del jugador.");
            }
            Camera cam = Camera.main;
            if (cam != null)
            {
                GameObject cameraObj = cam.gameObject;
                playerCamera = cam;
            }
            else
            {
                Debug.Log("No se encontró la cámara del jugador UPDATE CON PLAYER NULL.");
            }
        }
        else
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                GameObject cameraObj = cam.gameObject;
                playerCamera = cam;
            }
            else
            {
                Debug.Log("No se encontró la cámara del jugador UPDATE.");
            }
        }

        if (CanSeePlayer())
        {
            lastSeenTime = Time.time;

            // Si no estaba persiguiendo al jugador, inicia la persecución
            if (!isChasing)
            {
                isChasing = true;
                PlayChaseSound();
            }

            agent.SetDestination(player.position);

            // Si el enemigo está cerca del jugador y no está atacando, inicia el ataque
            if (Vector3.Distance(transform.position, player.position) <= attackRange && puedeAtacar)
            {
                puedeAtacar = false;
                StartCoroutine(Atacar());
            }
        }
        else if (isChasing)
        {
            // Si ha pasado el tiempo máximo sin ver al jugador, vuelve a patrullar
            if (Time.time - lastSeenTime > chaseTime)
            {
                StopChasingAndPatrol();
            }
            else
            {
                // Sigue moviéndose hacia la última posición conocida del jugador
                agent.SetDestination(player.position);
            }
        }
        else if (!agent.pathPending && agent.remainingDistance - agent.stoppingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    private void StopChasingAndPatrol()
    {
        isChasing = false;
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        GoToNextWaypoint();
    }

    private void PlayChaseSound()
    {
        if (audioSource != null && audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - raycastOrigin.position;
        directionToPlayer.y = 0;

        float angleToPlayer = Vector3.Angle(raycastOrigin.forward, directionToPlayer);

        if (angleToPlayer < fieldOfView * 0.5f)
        {
            Debug.DrawRay(raycastOrigin.position, directionToPlayer, Color.red);
            if (directionToPlayer.magnitude <= detectionRange)
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin.position, directionToPlayer, out hit, detectionRange))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void GoToNextWaypoint()
    {
        if (!isChasing)
        {
            StartCoroutine(WaitAndFindPoint());
        }
    }

    IEnumerator WaitAndFindPoint()
    {
        float waitTime = 2f;
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(FindReachableRandomPoint());
    }

    IEnumerator FindReachableRandomPoint()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 randomPosition = GetRandomPointInArea();
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, 2f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetDestination(hit.position);
                    yield break;
                }
            }
            yield return null;
        }

        Debug.LogWarning("No se encontró una posición alcanzable tras varios intentos.");
    }

    Vector3 GetRandomPointInArea()
    {
        Vector3 point = Vector3.zero;
        float maxDistance = 0;

        for (int i = 0; i < 10; i++)
        {
            float randomX = Random.Range(-patrolAreaSize.x / 2f, patrolAreaSize.x / 2f);
            float randomZ = Random.Range(-patrolAreaSize.z / 2f, patrolAreaSize.z / 2f);
            Vector3 candidate = patrolAreaCenter + new Vector3(randomX, 0, randomZ);

            float distance = Vector3.Distance(transform.position, candidate);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                point = candidate;
            }
        }

        return point;
    }

    private IEnumerator Atacar()
    {
        Debug.Log("Atacando al jugador");
        puedeAtacar = false;
        agent.isStopped = true;

        // Desactiva al enemigo y cambia la cámara
        Debug.Log("Cambiando cámara");
        if (alternativeCamera == null)
        {
            Debug.Log("No se encontró la cámara alternativa.");
        }
        if (playerCamera == null)
        {
            Debug.Log("No se encontró la cámara del jugador ESTOY EN ATACAR.");
        }
        if (playerCamera != null && alternativeCamera != null)
        {
            alternativeCamera.gameObject.SetActive(true); // Activa la cámara alternativa
            playerCamera.gameObject.SetActive(false);
            animator.SetBool("Muerto", true);
            //gameObject.SetActive(false);                 // Desactiva al enemigo
        }

        yield return new WaitForSeconds(10f);
        alternativeCamera.gameObject.SetActive(false); // Desactiva la cámara alternativa
        playerCamera.gameObject.SetActive(true);
        animator.SetBool("Muerto", false);
        Jugador.Instance.GameOver();
    }
}
