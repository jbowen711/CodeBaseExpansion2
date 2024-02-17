using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    // Belirteç objesine referans
    public GameObject marker;

    // Update her karede bir kez çağrılır
    void Update()
    {
        // Eğer sol mouse tıklanırsa
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse'un dünya koordinatlarındaki pozisyonunu al
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Z pozisyonunu sıfır olarak ayarla (2D oyun için)
            targetPosition.z = 0;

            // Belirteci hedef pozisyona taşı
            marker.transform.position = targetPosition;
        }
    }
}
