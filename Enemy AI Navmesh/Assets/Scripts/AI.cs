using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private float maxDistanceToCheck = 15.0f;
    private float currentDistance;
    private Vector3 checkDirection;
    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;
    private Ray ray;
    private RaycastHit hit;
    private  int _amountOfAmmo = 10;

    // Patrol state variables
    public Transform pointA;
    public Transform pointB;
    public Transform bullet;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public int AmountOfAmmo
    {
        get
        {
            return this._amountOfAmmo;
        }
        set
        {
            _amountOfAmmo = value;
        }      
    }

    public void Fire()
    {
        transform.LookAt(player.transform);
        Vector3 shootDirection = Vector3.Normalize(checkDirection);
        Instantiate(bullet, transform.position, Quaternion.LookRotation(shootDirection));
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        pointA = GameObject.Find("p1").transform;
        pointB = GameObject.Find("p2").transform;

        navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        waypoints = new Transform[2] {
            pointA,
            pointB
        };
        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }

    private void Update()
    {
        //First we check distance from the player 
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        //Then we check for visibility
        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
           
            if (hit.collider.gameObject == player)
            {
                animator.SetBool("isPlayerVisible", true);
            }
            else
            {
                animator.SetBool("isPlayerVisible", false);
            }
            
        }
        else
        {
            animator.SetBool("isPlayerVisible", false);
        }

        if(_amountOfAmmo <= 0)
        {
            animator.SetBool("IsOutOfAmmo", true);
        }
        else
        {
            animator.SetBool("IsOutOfAmmo", false);
        }
        //Lastly, we get the distance to the next waypoint target
        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
        
        // Draw the Raycast form the enemy
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //Vector3 fwd = ray.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * maxDistanceToCheck, Color.red);

        animator.SetInteger("amountOfAmmo",_amountOfAmmo);
    }
    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 0;
                break;

        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
    public void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
    public void StopsAndLook()
    {
        navMeshAgent.isStopped = true;
        transform.Rotate(Vector3.right * Time.deltaTime);
    }
}
