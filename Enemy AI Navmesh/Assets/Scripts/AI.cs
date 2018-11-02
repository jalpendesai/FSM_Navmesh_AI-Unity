using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private float maxDistanceToCheck = 15.0f;
    private float currentDistance;
    private Vector3 checkDirection;
    private Vector3 enemyVision;
    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;
    private Ray ray;
    private RaycastHit hit;
    private  int _amountOfAmmo = 10;
    private bool playerinSight = false;

    // Patrol state variables
    public float _angle;
    public float _fieldOfVision = 45;
    public Transform pointA;
    public Transform pointB;
    public Transform bullet;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public GameObject head;

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
        ////////////////////////////////////////////////////////////////////////////////
        enemyVision = transform.TransformDirection(Vector3.forward);

        _angle = Vector3.Angle(checkDirection, enemyVision);
        if(_angle < _fieldOfVision)
        {
            if(Physics.Raycast(transform.position + transform.up, checkDirection.normalized,out hit, maxDistanceToCheck))
            {
                if(hit.collider.gameObject == player)
                {
                    animator.SetBool("isPlayerVisible", true);
                }
                else
                {
                    animator.SetBool("isPlayerVisible", false);
                }
            }
        }

        //ray = new Ray(transform.position, checkDirection);
        //if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        //{

        //    if (hit.collider.gameObject == player)
        //    {
        //        animator.SetBool("isPlayerVisible", true);
        //    }
        //    else
        //    {
        //        animator.SetBool("isPlayerVisible", false);
        //    }

        //}

        //else
        //{
        //    animator.SetBool("isPlayerVisible", false);
        //}

        if (_amountOfAmmo <= 0)
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
        // Vector3 fwd = transform.TransformDirection(Vector3.forward);
        // Vector3 fwd = ray.transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position + transform.up, enemyVision * maxDistanceToCheck, Color.red);


      //  Debug.DrawRay(transform.position, , Color.blue);

        animator.SetInteger("amountOfAmmo",_amountOfAmmo);

        // Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        // Vector3 frontRayPoint = transform.position + (transform.forward * maxDistanceToCheck);
        //Vector3 frontRayPoint = transform.position + (enemyVision * maxDistanceToCheck);

        ////Approximate perspective visualization
        //Vector3 leftRayPoint = frontRayPoint;
        //leftRayPoint.x += 25 * 0.5f;

        //Vector3 rightRayPoint = frontRayPoint;
        //rightRayPoint.x -= 25 * 0.5f;

        //Debug.DrawLine(transform.position + transform.up, frontRayPoint, Color.red);
        //Debug.DrawLine(transform.position + transform.up, leftRayPoint, Color.green);
        //Debug.DrawLine(transform.position + transform.up, rightRayPoint, Color.green);
    }

    private void OnDrawGizmos()
    {
        // Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        // Vector3 frontRayPoint = transform.position + (transform.forward * maxDistanceToCheck);
        Vector3 frontRayPoint = transform.position + (enemyVision * maxDistanceToCheck);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += 25 * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= 25 * 0.5f;

        Debug.DrawLine(transform.position + transform.up, frontRayPoint, Color.red);
        Debug.DrawLine(transform.position + transform.up, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position + transform.up, rightRayPoint, Color.green);

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            // Rotate head left- right
            animator.SetTrigger("door");
            Debug.Log("Door Triggered");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Door")
    //    {
    //        // Rotate head left- right
    //        animator.SetTrigger("door");
    //        Debug.Log("Door Triggered");
    //    }
    //}
}
