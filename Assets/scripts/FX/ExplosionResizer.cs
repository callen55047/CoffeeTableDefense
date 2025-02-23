using UnityEngine;

[ExecuteAlways]
public class ExplosionResizer : MonoBehaviour
{
    // Desired parameters for the explosion effect.
    public float targetStartSize = 0.5f;    // New start size for particles.
    public float targetStartLifetime = 0.5f; // New lifetime for particles.
    public float targetDuration = 0.5f;     // New duration of the effect.

    void Start()
    {
        AdjustExplosion();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        AdjustExplosion();
    }
#endif

    void AdjustExplosion()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var main = ps.main;
            main.startSize = targetStartSize;
            main.startLifetime = targetStartLifetime;
            main.duration = targetDuration;

            Debug.Log("ExplosionResizer: Adjusted explosion parameters. " +
                      "Start Size: " + targetStartSize +
                      ", Lifetime: " + targetStartLifetime +
                      ", Duration: " + targetDuration);
        }
        else
        {
            Debug.LogWarning("ExplosionResizer: No ParticleSystem found on " + gameObject.name);
        }
    }
}