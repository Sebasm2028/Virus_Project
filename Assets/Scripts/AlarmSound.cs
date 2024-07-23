using UnityEngine;
using System.Collections;

public class AlarmSound : MonoBehaviour
{
    public AudioClip alarmClip;
    public float alarmInterval = 10f; // Intervalo de 20 segundos

    private Coroutine alarmCoroutine;

    private void Start()
    {
        alarmCoroutine = StartCoroutine(PlayAlarmSoundRepeatedly());
    }

    private void OnDisable()
    {
        if (alarmCoroutine != null)
        {
            StopCoroutine(alarmCoroutine);
        }
    }

    private IEnumerator PlayAlarmSoundRepeatedly()
    {
        while (true)
        {
            AudioManager.Instance.PlayAlarmSound(alarmClip);
            yield return new WaitForSeconds(alarmInterval); // Espera el intervalo antes de repetir
            yield return new WaitForSeconds(1f); // Espera un segundo antes de iniciar el siguiente sonido
        }
    }
}
