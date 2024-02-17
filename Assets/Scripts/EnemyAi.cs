using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public float attackRange = 5f;
    public int damage = 10;
    private Vector2 targetPosition;
    private Vector2 spawnPosition;
    private Animator animator;
    private GameObject player;
    private float minMoveDistance = 0.5f;

    void Start()
    {
        spawnPosition = transform.position;
        targetPosition = GetRandomPositionAroundSpawn();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
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
    }
}
