using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class LightTriggerController : MonoBehaviour
{
    public Light[] greenLights; // Başlangıçta yeşil olan ışıklar
    public Light[] redLights;   // Başlangıçta kırmızı olan ışıklar
    public GameObject[] greenSpheres; // Yeşil ışıklarla ilişkili Sphere'lar
    public GameObject[] redSpheres;   // Kırmızı ışıklarla ilişkili Sphere'lar
    public bool gameStarted = false;

    public float finalWaitTime = 3f; // Son turdan sonra bekleme süresi
    private PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // PlayerHealth scriptini bul

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameStarted)
        {
            gameStarted = true;
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f); // 2 saniye bekle
        Debug.Log("Oyun başladı!");

        for (int i = 0; i < 3; i++) // 3 tur boyunca ışıkları değiştir
        {
            SetLightsActive(true);  // Işıkları aç
            yield return new WaitForSeconds(4f); // 3 saniye açık tut

            SwapLightColors(); // Yeşilleri kırmızı, kırmızıları yeşil yap
        }
        yield return new WaitForSeconds(finalWaitTime);
        SetLightsActive(false);

        // Can kaybını durdur
        if (playerHealth != null)
        {
            playerHealth.StopHealthDecay();
        }
    }

    void SetLightsActive(bool state)
    {
        foreach (Light light in greenLights)
            light.enabled = state; // Yeşil ışıkları aç/kapat

        foreach (Light light in redLights)
            light.enabled = state; // Kırmızı ışıkları aç/kapat
    }

    void SwapLightColors()
    {
        // 1️⃣ **Işıkların rengini değiştir**
        foreach (Light light in greenLights)
            light.color = Color.red; // Yeşil ışıkları kırmızı yap

        foreach (Light light in redLights)
            light.color = Color.green; // Kırmızı ışıkları yeşil yap

        // 2️⃣ **Sphere objelerinin tag'lerini güncelle**
        foreach (GameObject sphere in greenSpheres)
            sphere.tag = "RedLight"; // Önceden yeşil olanlar kırmızı oldu

        foreach (GameObject sphere in redSpheres)
            sphere.tag = "GreenLight"; // Önceden kırmızı olanlar yeşil oldu

        // 3️⃣ **Işık ve Sphere dizilerini yer değiştir**
        Light[] tempLights = greenLights;
        greenLights = redLights;
        redLights = tempLights;

        GameObject[] tempSpheres = greenSpheres;
        greenSpheres = redSpheres;
        redSpheres = tempSpheres;
    }
}
