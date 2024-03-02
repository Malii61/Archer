using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[Serializable]
public class ParticleArgs
{
    public ParticleType particleType;
    public ParticleSystem particle;
}

public enum ParticleType
{
    Blood,
}

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    [SerializeField] private List<ParticleArgs> particleArgs = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Play(ParticleType particleType, Vector3 pos, Quaternion rot = default, float releaseAfterDelay = 0f)
    {
        if (rot == default)
            rot = Quaternion.identity;

        ParticleSystem particle = GetParticle(particleType);
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.Play();
        if (releaseAfterDelay > 0f)
        {
            PoolHandler.Instance.Release(particle.transform, GetPoolType(particleType), releaseAfterDelay);
        }
    }

    #region private
    private PoolType GetPoolType(ParticleType particleType)
    {
        PoolType poolType = default;
        switch (particleType)
        {
            case ParticleType.Blood:
                poolType = PoolType.Blood;
                break;
        }
        return poolType;
    }

    private ParticleSystem GetParticle(ParticleType particleType)
    {
        PoolType poolType = GetPoolType(particleType);
        var particle = PoolHandler.Instance.Get(poolType);
        if (particle == null)
        {
            PoolHandler.Instance.Create(
                particleArgs.FirstOrDefault(x => x.particleType == particleType)?.particle.transform, poolType);
            particle = PoolHandler.Instance.Get(poolType);
        }
        return particle.GetComponent<ParticleSystem>();
    }
    #endregion
}