using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Advancing,
        Attacking,
        Dead
    }

    [SerializeField] private float lineOfSight;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private float timer;
    private Vector3 patrolPoint;

    public Transform playerCheck;
    public GameObject attackSphere;
    public EnemyState currentState;
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        attackSphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        UpdateState();   
    }

    void CheckState()
    {
        switch (currentState) {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Advancing:
                MoveTowardsPlayer();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Dead:
                Die();
                break;
            default :
                Debug.Log("*Visible Confusion*");
                break;
        }
    }

    void UpdateState()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= lineOfSight)
        {
            currentState = EnemyState.Advancing;
        }
        else
        {
            currentState = EnemyState.Patrolling;
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint, speed * Time.deltaTime);
        LookAtPoint(patrolPoint);

        // After 10 seconds or player has reached patrol point, pick a new patrol point.
        if ((timer % 10 == 0 && timer > 0) || Vector3.Distance(transform.position, patrolPoint) <= 0.1f)
        {
            float newX = Random.Range(-100, 100);
            float newY = Random.Range(-100, 100);
            float newZ = Random.Range(-100, 100);

            patrolPoint = new Vector3(newX, newY, newZ);

            timer = 0;
        }
    }

    void Attack()
    {
        attackSphere.SetActive(true);
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        LookAtPoint(player.transform.position);
    }
    void Die()
    {
        Destroy(gameObject, 0);
        // TODO spawn die from enemy & special effects
    }

    private void LookAtPoint(Vector3 target)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = patrolPoint - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
