using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public string boolName = "Open";
    public List<GameObject> lights; // 4 ışık nesnesini buraya ekle
    private bool isAnimating = false; // Animasyonun çalıştığını takip eder

    // Kapıyı açma veya kapama metodu
    private IEnumerator ToggleDoorOpen(bool state)
    {
        isAnimating = true; // Animasyonun çalıştığını işaretle
        animator.SetBool(boolName, state); // Kapıyı aç/kapat

        yield return new WaitForSeconds(1.5f); // 1.5 saniye bekle (animasyon süresine göre ayarla)

        isAnimating = false; // Animasyon tamamlandı
    }

    // Işıkların belirli kombinasyonunu sürekli kontrol eden metot
    private bool CheckSpecificLightsCondition()
    {
        if (lights.Count < 4)
        {
            Debug.LogError("Light list does not have 4 elements!");
            return false; // Eğer liste eksikse kapıyı açma.
        }

        // Işıkların sahnede aktif olup olmadığını kontrol et
        return (lights[0].activeSelf && // 1. ışık sahnede aktif
                lights[1].activeSelf && // 2. ışık sahnede aktif
                lights[2].activeSelf && // 3. ışık sahnede aktif
                !lights[3].activeSelf); // 4. ışık sahnede aktif değil (kapalı)
    }

    // Sürekli ışıkları kontrol et
    private void Update()
    {
        bool isOpen = animator.GetBool(boolName); // Kapının mevcut durumu
        bool shouldBeOpen = CheckSpecificLightsCondition(); // Işık kombinasyonu doğru mu?

        // Eğer kapının durumu değişmesi gerekiyorsa VE animasyon çalışmıyorsa güncelle
        if (isOpen != shouldBeOpen && !isAnimating)
        {
            Debug.LogError("girdi");
            StartCoroutine(ToggleDoorOpen(shouldBeOpen));
        }
    }
}
