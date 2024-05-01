using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public float attackRange = 5f;
    public int damage = 2;
    private Vector2 targetPosition;
    private Vector2 spawnPosition;
    private Animator animator;
    private GameObject player;
    private float minMoveDistance = 0.5f;
    private GameObject castle;
    private CastleHealth castleHealth;
    
    

    void Start()
    {
        spawnPosition = transform.position;
        targetPosition = GetRandomPositionAroundSpawn();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        castle = GameObject.FindGameObjectWithTag("Castle");
        castleHealth = castle.GetComponent<CastleHealth>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        float distanceToCastle = Vector2.Distance(transform.position, castle.transform.position);
        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToCastle > attackRange)
        {
            MoveToCastle();
        }
        else
        {
            animator.ResetTrigger("attack");
            animator.SetBool("isRunning", true);
            MoveRandomly();
        }
    }

    Vector2 GetRandomPositionAroundSpawn()
    {
        return spawnPosition + new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    }

    void MoveRandomly()
    {
        if (Vector2.Distance(transform.position, targetPosition) > minMoveDistance)
        {
            Vector2 direction = targetPosition - (Vector2)transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);

            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            targetPosition = GetRandomPositionAroundSpawn();
        }
    }

    void MoveToCastle()
    {
        Vector2 castleLocation = castle.transform.position - transform.position;
        transform.Translate(castleLocation.normalized * speed * Time.deltaTime);
        animator.SetBool("isRunning", true);

        if (castleLocation.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (castleLocation.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    void AttackPlayer()
    {
        animator.SetBool("isRunning", false);
        animator.SetTrigger("attack");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Avoid")
        {
            targetPosition = GetRandomPositionAroundSpawn();
        }
        else if (collision.gameObject.tag == "Castle")
        {
             if (castle != null)
             {
                castleHealth.TakeDamage(damage);
 
             }
             Destroy(gameObject);
        }
        
    }
}
