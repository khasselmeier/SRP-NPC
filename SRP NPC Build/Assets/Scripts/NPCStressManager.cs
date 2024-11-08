using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NPCStressManager : MonoBehaviour
{
    public Slider stressBar;
    private float stress = 0f; // Current stress level
    public float stressIncreaseRate = 5f; // amt of stress added per second

    private void Start()
    {
        StartCoroutine(IncreaseStressOverTime());
    }

    private IEnumerator IncreaseStressOverTime()
    {
        while (stress < 100f)
        {
            IncreaseStress(stressIncreaseRate); // Add stress every second
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
    }

    public void IncreaseStress(float amount)
    {
        stress += amount;
        stress = Mathf.Clamp(stress, 0f, 100f);
        UpdateStressBar();

        if (stress >= 100f)
        {
            HandleDeathByStress();
        }
    }

    private void UpdateStressBar()
    {
        if (stressBar != null)
        {
            stressBar.value = stress;
        }
    }

    private void HandleDeathByStress()
    {
        Debug.Log("You've died from shock");

        StartCoroutine(DestroyNPCAfterDelay(2f));
    }

    private IEnumerator DestroyNPCAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

        SceneManager.LoadScene("end");
    }
}