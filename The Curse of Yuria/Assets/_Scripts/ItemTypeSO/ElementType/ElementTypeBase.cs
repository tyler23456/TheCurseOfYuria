using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementTypeBase : ElementTypeSO
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] AudioClip clip;

    public override int weaknessIndex => 0;

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        int weakness = target.getStats.GetWeakness(weaknessIndex);

        if (particleSystem != null)
            Destroy(Instantiate(particleSystem, user.obj.transform), 5f);

        if (clip != null)
            target.obj.GetComponent<AudioSource>()?.PlayOneShot(clip);

        return weakness >= 0? accumulator * (IStats.weaknessSensitivity / (IStats.weaknessSensitivity + weakness)) :
                              accumulator * ((-weakness + IStats.weaknessSensitivity) / IStats.weaknessSensitivity);
    }
}
