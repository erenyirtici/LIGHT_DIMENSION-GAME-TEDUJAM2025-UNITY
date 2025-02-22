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
    private bool healthDecayStopped = false; // Can kayb� durduruldu mu?
    private bool isInTriggerZone = false; // Oyuncu TriggerZone i�inde mi?


    void Start()
    {
        currentHealth = maxHealth;
        decayRate = 0f; // Ba�lang��ta can azalmamal�

    }
    void Update()
    {
        if (!healthDecayStopped&& isInTriggerZone) // E�er can kayb� durdurulmad�ysa azalmaya devam etsin
        {
            currentHealth -= decayRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 0 ile max aras�nda s�n�rla
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player �ld�!");
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            isInTriggerZone = true; // Oyuncu TriggerZone'a girdi
            decayRate = normalDecayRate; // Varsay�lan azalma h�z�
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GreenLight"))
        {
            decayRate = greenDecayRate; // Ye�il ���k: Can azalm�yor
        }
        else if (other.CompareTag("RedLight"))
        {
            decayRate = redDecayRate; // K�rm�z� ���k: H�zl� can kayb�
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GreenLight") || other.CompareTag("RedLight"))
        {
            decayRate = normalDecayRate; // I��ktan ��k�nca tekrar yava� azalma
        }
        if (other.CompareTag("TriggerZone"))
        {
            isInTriggerZone = false; // Oyuncu TriggerZone'dan ��kt�
            decayRate = 0f; // Can azalmas�n� tamamen durdur
        }
    }
    public void StopHealthDecay()
    {
        healthDecayStopped = true; // Can kayb�n� tamamen durdur
        Debug.Log("Can kayb� durduruldu!");
    }
}
