using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;
    public float normalDecayRate = 1f;  
    public float redDecayRate = 5f;     
    public float greenDecayRate = 0f;
    private float decayRate;
    private bool healthDecayStopped = false; // Can kaybý durduruldu mu?
    private bool isInTriggerZone = false; // Oyuncu TriggerZone içinde mi?


    void Start()
    {
        currentHealth = maxHealth;
        decayRate = 0f; // Baþlangýçta can azalmamalý

    }
    void Update()
    {
        if (!healthDecayStopped&& isInTriggerZone) // Eðer can kaybý durdurulmadýysa azalmaya devam etsin
        {
            currentHealth -= decayRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 0 ile max arasýnda sýnýrla
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player öldü!");
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            isInTriggerZone = true; // Oyuncu TriggerZone'a girdi
            decayRate = normalDecayRate; // Varsayýlan azalma hýzý
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GreenLight"))
        {
            decayRate = greenDecayRate; // Yeþil ýþýk: Can azalmýyor
        }
        else if (other.CompareTag("RedLight"))
        {
            decayRate = redDecayRate; // Kýrmýzý ýþýk: Hýzlý can kaybý
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GreenLight") || other.CompareTag("RedLight"))
        {
            decayRate = normalDecayRate; // Iþýktan çýkýnca tekrar yavaþ azalma
        }
        if (other.CompareTag("TriggerZone"))
        {
            isInTriggerZone = false; // Oyuncu TriggerZone'dan çýktý
            decayRate = 0f; // Can azalmasýný tamamen durdur
        }
    }
    public void StopHealthDecay()
    {
        healthDecayStopped = true; // Can kaybýný tamamen durdur
        Debug.Log("Can kaybý durduruldu!");
    }
}
