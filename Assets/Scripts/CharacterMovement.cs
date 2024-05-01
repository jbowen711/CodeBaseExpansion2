using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostedSpeed = 10f; // Shift tuşuna basıldığında kullanılacak hız
    public float boostDuration = 3f; // Boost'un süresi (saniye cinsinden)
    public float boostCooldown = 5f; // Boost'un soğuma süresi (saniye cinsinden)
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private float originalSpeed; // Orijinal hızı saklamak için
    private bool canBoost = true; // Boost'un kullanılabilir olup olmadığını kontrol etmek için
    public Score score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalSpeed = moveSpeed; // Orijinal hızı sakla
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Shift tuşuna basıldığında ve boost kullanılabilir olduğunda hızı ve animasyon hızını artır
        if (Input.GetKeyDown(KeyCode.LeftShift) && canBoost)
        {
            canBoost = false; // Boost'u devre dışı bırak
            moveSpeed = boostedSpeed;
            animator.speed = 2f; // Animasyon hızını artır
            StartCoroutine(ResetSpeedAfterBoost());
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Boost süresi dolduktan sonra hızı ve animasyon hızını sıfırla ve boost'un soğuma süresini başlat
    IEnumerator ResetSpeedAfterBoost()
    {
        yield return new WaitForSeconds(boostDuration);
        moveSpeed = originalSpeed;
        animator.speed = 1f; // Animasyon hızını sıfırla
        yield return new WaitForSeconds(boostCooldown);
        canBoost = true; // Boost'u tekrar kullanılabilir hale getir
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            score.IncreaseScore();

        }

    }
}
