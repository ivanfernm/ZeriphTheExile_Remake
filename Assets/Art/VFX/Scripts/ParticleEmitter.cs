using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter
{

}

public class DirtParticle
{
    public DirtParticle(Vector3 PositionToSpawn,_DirtParticle SizeParticle) 
    {

    }
    
    public void init() 
    {

    }

    public ParticleSystem DesireParticle(_DirtParticle particle)
    {
        var result = new ParticleSystem();

        switch (particle)
        {
            case _DirtParticle.tiny:
            
                break;
       
            case _DirtParticle.large:
       
                break;
            
            case _DirtParticle.medium:
               
                break;
            
        }

        return result;
    }
}

public enum _DirtParticle
{
    tiny,
    medium,
    large,
}

public class DustParticle
{
    Vector3 _PositionToSpawn;
    _DustType _Dustsize;
  
    public DustParticle(Vector3 PositionToSpawn,_DustType _DustType) 
    {

    }
    public void init() 
    {
        var a = new ParticleSystem();

        a = UnityEngine.GameObject.Instantiate(DesireParticle(_Dustsize),_PositionToSpawn,Quaternion.identity);

  
    }
    
    public ParticleSystem DesireParticle(_DustType particle)
    {
        var result = new ParticleSystem();

        switch (particle)
        {
            case _DustType.tiny:

                result = Resources.Load<ParticleSystem>("Particles/Dust/DustExplosionTiny");

                break;

            case _DustType.large:
                result = Resources.Load<ParticleSystem>("Particles/Dust/DustExplosionLarge");
                break;

            case _DustType.medium:
                result = Resources.Load<ParticleSystem>("Particles/Dust/DustExplosionMedium");
                break;

        }

        return result;
    }

}

public enum _DustType
{
    tiny,
    medium,
    large,
}

public class DamageParticle
{
    Vector3 _PositionToSpawn;
    private _DamagePType _type;

    public DamageParticle(Vector3 positionToSpawn, _DamagePType ParticleType)
    {
        _PositionToSpawn = positionToSpawn;
        _type = ParticleType;
    }

    public void Simulate()
    {
        var a = new ParticleSystem();

        a = UnityEngine.GameObject.Instantiate(DesireParticle(_type),_PositionToSpawn,Quaternion.identity);
    }

    public ParticleSystem DesireParticle(_DamagePType particle)
    {
        var result = new ParticleSystem();
        switch (particle)
        {
            case _DamagePType.slash:
                result = Resources.Load<ParticleSystem>("Particles/Damage/SlashDamageParticle");
                break;
            
        }

        return result;
    }
}

public enum _DamagePType
{
    slash,
}