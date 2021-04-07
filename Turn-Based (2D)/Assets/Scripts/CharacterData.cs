using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public string Name;
    public int Level;

    public float Attack;
    public float Defence;
    public float Health;
    public float CurrentHealth;
    
    public float SPD;
    public float ENG;
    public float current_ENG;
    public float INT;
    public float LUCK;
    public float Elemental_Damage;

    public GameObject projectile;
    public Transform startPoint;
    public float rotation;

    public string STRS_WEAK;

    public GameObject particle_Effect_Aether;
    public GameObject particle_Effect_Necro;
    public GameObject particle_Effect_Pyro;
    public GameObject particle_Effect_Hydro;
    public GameObject particle_Effect_Aero;
    public GameObject particle_Effect_Electro;
    public GameObject particle_Effect_Dendro;
    public GameObject particle_Effect_Cryo;
    public GameObject particle_Effect_Geo;
    public GameObject EnemyDeath;
    public GameObject PlayerDeath;
    public enum Elemental_Type 
    {
        Pyro,
        Hydro,
        Aero,
        Geo,
        Dendro,
        Cyro,
        Electro,
        Aether,
        Necro
    }

    public Elemental_Type Element;
    
    public bool PlayerAttacks(float ATT)
    {
        float Damage = ATT-Defence;
        if(Damage>0)
        {
            CurrentHealth = CurrentHealth - Damage;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }
        else 
        {
            CurrentHealth = CurrentHealth - 0;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       

        
    }
    public bool EnemyAttacks(float ATT)
    {
        float Damage = ATT - Defence;
        if (Damage > 0)
        {
            CurrentHealth = CurrentHealth - Damage;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            CurrentHealth = CurrentHealth - 0;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }

    public bool SpecialAttack(float ATT, Elemental_Type elemental_Type1, Elemental_Type elemental_Type2, float ElementalBoost, float CurrentENG)
    {
        
        

        if (CurrentENG > 0)
        {
            Debug.Log(CurrentENG);
            CurrentENG = CurrentENG- 1;
            Debug.Log(CurrentENG);
           

            if ((elemental_Type1 == Elemental_Type.Hydro) && (elemental_Type2 == Elemental_Type.Pyro ))
            {
                float AttackBoost = ATT * (ElementalBoost / 100);
                Debug.Log(AttackBoost);
                Debug.Log("STRS_WEAK used");
                //CurrentHealth = CurrentHealth - ATT;
                CurrentHealth = CurrentHealth -ATT- AttackBoost;
                if (CurrentHealth <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.Log("STRS_WEAK not used ");
                //CurrentHealth = CurrentHealth - ATT;
                CurrentHealth = CurrentHealth -ATT- (ATT * (5 / 100));
                if (CurrentHealth <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            

        }
        else
        {
            Debug.Log("no energy for special");
            CurrentHealth = CurrentHealth - ATT;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

       
    }
    public bool SpecialAttackEnemy(float ATT, Elemental_Type elemental_Type1, Elemental_Type elemental_Type2, float ElementalBoost, float CurrentENG)
    {



        if (CurrentENG > 0)
        {
            Debug.Log(CurrentENG);
            CurrentENG = CurrentENG - 1;
            Debug.Log(CurrentENG);


            if ((elemental_Type1 == Elemental_Type.Cyro) && (elemental_Type2 == Elemental_Type.Hydro))
            {
                float AttackBoost = ATT * (ElementalBoost / 100);
                Debug.Log(AttackBoost);
                Debug.Log("Enemy STRS_WEAK used");
                //CurrentHealth = CurrentHealth - ATT;
                CurrentHealth = CurrentHealth - ATT - AttackBoost;
                if (CurrentHealth <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.Log("STRS_WEAK not used ");
                //CurrentHealth = CurrentHealth - ATT;
                CurrentHealth = CurrentHealth - ATT - (ATT * (5 / 100));
                if (CurrentHealth <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        else
        {
            Debug.Log("no energy for special");
            CurrentHealth = CurrentHealth - ATT;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }

    public bool Stunned(float PlayerINT, float EnemyINT)
    {
        float StunChance = Random.value+(PlayerINT / 100);
        float StunBlock = Random.value + (EnemyINT/100);

        if (StunBlock<StunChance)
        {
            CurrentHealth = CurrentHealth - (PlayerINT-(EnemyINT/2));
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool StunnedEnemy( float EnemyINT, float PlayerINT)
    {
        float StunChance = Random.value + (EnemyINT / 100);
        float StunBlock = Random.value + (PlayerINT / 100);

        if (StunBlock < StunChance)
        {
            CurrentHealth = CurrentHealth - (EnemyINT - (PlayerINT / 2));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HealCharacter(float INT)
    {
        if(CurrentHealth >= Health)
        {
            return;
        }
        else if(CurrentHealth< CurrentHealth + (INT/2))
        {
            CurrentHealth = CurrentHealth + (INT/2);
            return;
        }
    }
    public void HealEnemyCharacter(float INT)
    {
        if (CurrentHealth >= Health)
        {
            return;
        }
        else if (CurrentHealth < CurrentHealth + (INT / 2))
        {
            CurrentHealth = CurrentHealth + (INT / 2);
            return;
        }
    }

    public void ParticleEffect_Aether()
    {
        Instantiate(particle_Effect_Aether, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Necro()
    {
        Instantiate(particle_Effect_Necro, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Pyro()
    {
        Instantiate(particle_Effect_Pyro, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Hydro()
    {
        Instantiate(particle_Effect_Hydro, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Aero()
    {
        Instantiate(particle_Effect_Aero, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Electro()
    {
        Instantiate(particle_Effect_Electro, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Dendro()
    {
        Instantiate(particle_Effect_Dendro, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Cryo()
    {
        Instantiate(particle_Effect_Cryo, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_Geo()
    {
        Instantiate(particle_Effect_Geo, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_EnemyDeath()
    {
        Instantiate(EnemyDeath, transform.position, Quaternion.identity);
    }
    public void ParticleEffect_PlayerDeath()
    {
        Instantiate(PlayerDeath, transform.position, Quaternion.identity);
    }
    public void Projectile()
    {
        Instantiate(projectile, startPoint.position, transform.rotation);

    }
    
}
