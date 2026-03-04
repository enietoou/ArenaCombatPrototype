using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image heathFill;
    [SerializeField] private Image damageDelayFill;

    [SerializeField] private float delay = 0.3f;
    [SerializeField] private float smoothSpeed = 2f;
    
    private Coroutine _smoothCoroutine;

    private void Start()
    {
        playerHealth.OnHealthChange += UpdateHealthBar;
    }
    
    private void OnDisable()
    {
        playerHealth.OnHealthChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        float value = (float)current / max;
        
        heathFill.fillAmount = value;
        
        if (_smoothCoroutine != null) StopCoroutine(_smoothCoroutine);

        _smoothCoroutine = StartCoroutine(SmoothDamageBar(value));
    }

    private IEnumerator SmoothDamageBar(float value)
    {
        yield return new WaitForSecondsRealtime(delay);

        while (damageDelayFill.fillAmount > value)
        {
            damageDelayFill.fillAmount -= smoothSpeed * Time.unscaledDeltaTime;
            yield return null;
        }
        
        damageDelayFill.fillAmount = value;
    }
}
