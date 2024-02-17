using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    // Animator bileşenine referans
    public Animator animator;
    // Hareket hızı
    public float speed = 5f;

    // Diğer tavuk karakterlerine referans
    public GameObject[] otherChickens;
    // Minimum mesafe
    public float minDistance = 1f;

    // Hedef pozisyon
    private Vector3 targetPosition;

    // Düşmanla karşılaşma mesafesi
    public float encounterDistance = 2f;

    // Başlangıçta çağrılır
    void Start()
    {
        // Başlangıçta hedef pozisyonu karakterin mevcut pozisyonu olarak ayarla
        targetPosition = transform.position;
    }

    // Update her karede bir kez çağrılır
    void Update()
    {
        // Eğer sol mouse tıklanırsa
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse'un dünya koordinatlarındaki pozisyonunu al
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Z pozisyonunu sıfır olarak ayarla (2D oyun için)
            targetPosition.z = 0;

            // Yürüme animasyonunu başlat
            animator.SetBool("Walk", true);
        }

        // Karakteri hedef pozisyona doğru hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Eğer karakter hedef pozisyona ulaştıysa
        if (transform.position == targetPosition)
        {
            // Yürüme animasyonunu durdur
            animator.SetBool("Walk", false);
        }

        // Karakterin yönünü kontrol et
        if (targetPosition.x > transform.position.x)
        {
            // Sağa bak
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (targetPosition.x < transform.position.x)
        {
            // Sola bak
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Her bir diğer tavuk karakteri için
        foreach (GameObject otherChicken in otherChickens)
        {
            // Karakterler arasındaki mesafeyi kontrol et
            float distance = Vector3.Distance(transform.position, otherChicken.transform.position);

            // Eğer karakterler arasındaki mesafe minimum mesafeden küçükse
            if (distance < minDistance)
            {
                // Karakterleri birbirinden uzaklaştır
                Vector3 dir = transform.position - otherChicken.transform.position;
                dir.Normalize();
                transform.position = otherChicken.transform.position + dir * minDistance;
            }
        }

        // Düşmanla karşılaşma kontrolü
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, encounterDistance);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // Saldırı animasyonunu başlat
                animator.SetBool("Attack", true);
            }
            else
            {
                // Saldırı animasyonunu durdur
                animator.SetBool("Attack", false);
            }
        }
    }
}
