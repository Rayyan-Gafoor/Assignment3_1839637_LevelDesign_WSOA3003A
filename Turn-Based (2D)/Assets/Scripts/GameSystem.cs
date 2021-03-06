using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Win,
    Lose
}

public class GameSystem : MonoBehaviour
{
   
    public GameStates State;
    public GameObject Player;
    public GameObject Enemy;

    public Transform PlayerBattleStation;
    public Transform EnemyBattleStation;

    CharacterData PlayerData;
    CharacterData EnemyData;

    public Text dialogueText;
    public Text BuffText;
    public Text BuffApplied;
    public string Buff;

    public Text PlayerCharacterName;
    public Text PlayerCharacterStats;
    public Text PlayerCharacterInfo;

    public Text EnemyCharacterName;
    public Text EnemyCharacterStats;
    public Text EnemyCharacterInfo;

    public ScreenShake Camera;
    public float Duration, Strength;

    public float TutFlag = 0;
    public Text HintText;
    public GameObject StunB;
    public GameObject HealB;
    public GameObject SpecialB;
    public GameObject BuffB;
    public GameObject BuffStats;

    public float RoundFlag = 0;
    float action = 0;



    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene 1")
        {
            RoundFlag = 0;
        }
        if (scene.name == "SampleScene")
        {
            RoundFlag = 1;
        }
        State = GameStates.Start;
        StartCoroutine(BattleSetUp());

    }
    void Update()
    {
        //This is a simple way of adding color
        
        //This is a more specific way of adding color
        
    }

    //game set up 
    IEnumerator BattleSetUp()
    {
        yield return new WaitForSeconds(2f);
        GameObject PlayerGameObject = Instantiate(Player, PlayerBattleStation);
        PlayerData = PlayerGameObject.GetComponent<CharacterData>();

        GameObject EnemyGameObject = Instantiate(Enemy, EnemyBattleStation);
        EnemyData = EnemyGameObject.GetComponent<CharacterData>();

        dialogueText.text = "A wild " + EnemyData.Name + " approaches";
        BuffText.text = "No Buff Active";
        BuffApplied.text = "No Buff Applied";
        //player stats bar
        PlayerCharacterName.text = "Player: " + PlayerData.Name;
        PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG +" INT:" +PlayerData.INT;
        PlayerCharacterInfo.text = "Character Info: " + PlayerData.STRS_WEAK;
        //enemy stats bar
        EnemyCharacterName.text = "Enemy: " + EnemyData.Name;
        EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " INT:" + EnemyData.INT;
        EnemyCharacterInfo.text = "Character Info: " + EnemyData.STRS_WEAK;

        yield return new WaitForSeconds(2f);

        State = GameStates.PlayerTurn;
        PlayerTurn();
    }

    //Attack Function ...deals damage to enemy
    IEnumerator PlayerAttack()
    {
        bool isDead = EnemyData.PlayerAttacks(PlayerData.Attack);


        if (Buff == "Aether Buff")
        {
            BuffApplied.text = "Life Gain Has Been Used";
            Debug.Log("Aether buff has been used");
            PlayerData.ParticleEffect_Aether();
            PlayerData.CurrentHealth = PlayerData.CurrentHealth + EnemyData.CurrentHealth * (0.3f);
        }
        if (Buff == "Necro Buff")
        {
            BuffApplied.text = "Life Steal has been used";
            Debug.Log("Necro buff activated- Life Steal has been used");
            EnemyData.ParticleEffect_Necro();
            PlayerData.CurrentHealth = PlayerData.CurrentHealth + 5f;
            EnemyData.CurrentHealth = EnemyData.CurrentHealth - 5f;
        }
        if (Buff == "Aero Buff")
        {
            BuffApplied.text = "Energy Recharge";
            Debug.Log("Aero buff activated- Energy Recharge");
            PlayerData.ParticleEffect_Aero();
            PlayerData.current_ENG = PlayerData.current_ENG + 1f;
            
        }
        if (Buff == "Pyro Buff")
        {
            BuffApplied.text = "Vaporized is used";
            Debug.Log("Pyro buff activated- Vapourized is used");
            EnemyData.ParticleEffect_Pyro();
            EnemyData.CurrentHealth = EnemyData.CurrentHealth - EnemyData.current_ENG * (0.3f);
        }
        if (Buff == "Hydro Buff")
        {
            BuffApplied.text = "Heal has been used";
            Debug.Log("Hydro buff activated- Heal has been used");
            PlayerData.ParticleEffect_Hydro();
            PlayerData.CurrentHealth = PlayerData.CurrentHealth + 5f;
        }
        if (Buff == "Cryo Buff")
        {
            BuffApplied.text = "Freeze has been used";
            Debug.Log("Cryo buff activated- Freeze has been used");
            EnemyData.ParticleEffect_Cryo();
            EnemyData.CurrentHealth = EnemyData.CurrentHealth - 2f;
            StartCoroutine(PlayerStun());
        }
        if (Buff == "Electro Buff")
        {
            BuffApplied.text = "Shock has been used";
            Debug.Log("Electro buff activated- Shock has been used");
            EnemyData.ParticleEffect_Electro();
            EnemyData.CurrentHealth = EnemyData.CurrentHealth - 6f;
            StartCoroutine(PlayerStun());
        }
        if (Buff == "Geo Buff")
        {
            BuffApplied.text = "Stun has been used";
            Debug.Log("Geo buff activated- Stun has been used");
            EnemyData.ParticleEffect_Geo();
            PlayerData.Defence = PlayerData.Defence + PlayerData.Defence * (0.1f);
            StartCoroutine(PlayerStun());
        }
        if (Buff == "Dendro Buff")
        {
            BuffApplied.text = "Heal has been used";
            Debug.Log("Dendro buff activated- Heal has been used");
            PlayerData.ParticleEffect_Dendro();
            PlayerData.CurrentHealth = PlayerData.CurrentHealth + 2f;
        }

        PlayerData.Projectile();
        yield return new WaitForSeconds(1.5f);
        EnemyData.ParticleEffect_EnemyDeath();
        StartCoroutine (Camera.ShakeScreen(Duration, Strength));

        EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " INT:" + EnemyData.INT;
        PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
        dialogueText.text = PlayerData.Name + " Attacked " + EnemyData.Name;
        Debug.Log("player attacked");
       // Debug.Log(Buff);

        yield return new WaitForSeconds(3f);

        if (isDead)
        {
            TutFlag = TutFlag + 1;
            //Destroy(Enemy);
            EnemyData.ParticleEffect_EnemyDeath();
            State = GameStates.Win;
            EndGame();
        }
        else
        {
            TutFlag = TutFlag + 1;
            State = GameStates.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator PlayerSpecial()
    {

        bool isDead = EnemyData.SpecialAttack(PlayerData.Attack, PlayerData.Element, EnemyData.Element, PlayerData.Elemental_Damage, PlayerData.current_ENG);
        PlayerData.Projectile();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Camera.ShakeScreen(Duration, Strength));
        PlayerData.current_ENG = PlayerData.current_ENG - 1f;
        EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " INT:" + EnemyData.INT;
        PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
        dialogueText.text = PlayerData.Name + " used Special Attack on " + EnemyData.Name;
        Debug.Log("player special attacked");

        yield return new WaitForSeconds(2f);
        TutFlag = TutFlag + 1;
        if (isDead)
        {
            State = GameStates.Win;
            EndGame();
        }
        else
        {
            State = GameStates.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        PlayerData.HealCharacter(PlayerData.INT);
        PlayerData.ParticleEffect_Hydro();
        PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
        dialogueText.text = PlayerData.Name + "Used Heal... ";
        Debug.Log("player Heal");

        yield return new WaitForSeconds(2f);

        TutFlag = TutFlag + 1;
        State = GameStates.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerStun()
    {
        bool isStunned = EnemyData.Stunned(PlayerData.INT, EnemyData.INT);
        EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " INT:" + EnemyData.INT;

        dialogueText.text = PlayerData.Name + " tries to stun " + EnemyData.Name;
        Debug.Log("player Stun");

        yield return new WaitForSeconds(2f);

        if (isStunned)
        {
            dialogueText.text = EnemyData.Name + " is Stunned ";
            EnemyData.ParticleEffect_Geo();
            yield return new WaitForSeconds(3f);
            TutFlag = TutFlag + 1;
            State = GameStates.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            dialogueText.text = EnemyData.Name + " was not Stunned ";
            yield return new WaitForSeconds(3f);
            TutFlag = TutFlag + 1;
            StartCoroutine(EnemyTurn());
        }
    }

    //Player Turn State
    void PlayerTurn()
    {

        if (RoundFlag==0)
        {
            if (TutFlag == 0)
            {
                dialogueText.text = " Choose An Action (There is only one option, so I am hoping you can do that much ._. ): ";
                HintText.text = "Use the attack button to deal damage to an opponent";
            }
            if (TutFlag == 1 || TutFlag == 2)
            {
                StunB.SetActive(true);
                HealB.SetActive(true);
                dialogueText.text = " Choose An Action(Oh Come On... Try the Heal or Stun, I'm just trying to help you learn): ";
                HintText.text = "Using the Heal or Stun actions,is determined your slimes intelligence (INT) level. " +
                    "The high your INT the more health you regain, " +
                    "With stun the greater the difference between your INT and an opponents INT, the greater the chance of a stun occuring.";
            }
            if (TutFlag == 3)
            {
                SpecialB.SetActive(true);
                dialogueText.text = "Choose An Action (pshh...Go ahead... try the special, you know you want to XD):";
                HintText.text = "The character information seen under the slime stats is applied when a special attack is used ";
            }
            if (TutFlag == 4)
            {
                BuffB.SetActive(true);
                BuffStats.SetActive(true);
                dialogueText.text = "Choose An Action (Feel like your slime sucks, need something to add 'pazaaz' to your moves, well look no further the buffs are here. Go ahead, try them ):";
                HintText.text = "Buff are used to boost normal attacks. Various buff have various results. " +
                    "The results depend on the elemental reaction created between your slimes element and the element of the buff ";
            }
            if (TutFlag >= 5)
            {
                dialogueText.text = " Choose An Action (Go ahead, you have freedom of choice you know, stop relying on me to tell you what to do ";
                HintText.text = "Defeat the opponent!";
            }
        }

        if (RoundFlag ==1)
        {
            HintText.text = "Use What You Have Learned To Defeat The Cyro Slime";
            dialogueText.text = "Choose Your Action";
        }

    }

    //Attack Button... Call Attack Function
    public void OnAttack()
    {
        if (State != GameStates.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }
    public void OnSpecial()
    {
        if (State != GameStates.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerSpecial());
    }
    //Heal Button ... Call Heal Function
    public void OnHeal()
    {
        if (State != GameStates.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
    //Stun Button... Call Stun Function
    public void OnStun()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerStun());
    }



    //Enemy Turn State...
    IEnumerator EnemyTurn()
    {
        if (RoundFlag == 0)
        {
            dialogueText.text = EnemyData.Name + " Attacks " + PlayerData.Name;
            EnemyData.Projectile();
            yield return new WaitForSeconds(1.5f);
            PlayerData.ParticleEffect_PlayerDeath();
            StartCoroutine(Camera.ShakeScreen(Duration, Strength));
            bool isDead = PlayerData.EnemyAttacks(EnemyData.Attack);
            PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                State = GameStates.Lose;
                EndGame();
            }
            else
            {
                State = GameStates.PlayerTurn;
                PlayerTurn();
            }
        }
        if (RoundFlag == 1)
        {
           
            if (action == 0)
            {
                // USe Normal Attack
                action =action+ 1;
                Debug.Log("Attack 1 from Cryo");
                dialogueText.text = EnemyData.Name + " Attacks " + PlayerData.Name;
                EnemyData.Projectile();
                yield return new WaitForSeconds(1.5f);
                PlayerData.ParticleEffect_PlayerDeath();
                StartCoroutine(Camera.ShakeScreen(Duration, Strength));
                bool isDead = PlayerData.EnemyAttacks(EnemyData.Attack);
                PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    State = GameStates.Lose;
                    EndGame();
                }
                else
                {
                    State = GameStates.PlayerTurn;
                    PlayerTurn();
                }

                
            }
            else if (action == 1)
            {
                // Use Normal Attack
                action = action + 1;
                Debug.Log("Attack 2 from Cryo");
                dialogueText.text = EnemyData.Name + " Attacks " + PlayerData.Name;
                EnemyData.Projectile();
                yield return new WaitForSeconds(1.5f);
                PlayerData.ParticleEffect_PlayerDeath();
                StartCoroutine(Camera.ShakeScreen(Duration, Strength));
                bool isDead = PlayerData.EnemyAttacks(EnemyData.Attack);
                PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    State = GameStates.Lose;
                    EndGame();
                }
                else
                {
                    State = GameStates.PlayerTurn;
                    PlayerTurn();
                }
            }
            else if (action == 2)
            {
                // Use Stun
                action += 1;

                bool isStunned = PlayerData.StunnedEnemy(EnemyData.INT, PlayerData.INT);
                PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;

                dialogueText.text = EnemyData.Name + " tries to stun " + PlayerData.Name;
                Debug.Log("enemy Stun");

                yield return new WaitForSeconds(2f);

                if (isStunned)
                {
                    dialogueText.text = PlayerData.Name + " is Stunned ";
                    PlayerData.ParticleEffect_Geo();
                    yield return new WaitForSeconds(3f);
                   // TutFlag = TutFlag + 1;
                    State = GameStates.EnemyTurn;
                    StartCoroutine(EnemyTurn());
                    
                }
                else
                {
                    dialogueText.text = EnemyData.Name + " was not Stunned ";
                    yield return new WaitForSeconds(3f);
                    // TutFlag = TutFlag + 1;
                    State = GameStates.PlayerTurn;
                    PlayerTurn();
                }
            }
            else if (action == 3)
            {
                //use Heal
                action += 1;

                EnemyData.HealCharacter(EnemyData.INT);
                EnemyData.ParticleEffect_Hydro();
                EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " ENG:" + EnemyData.current_ENG + "/" + EnemyData.ENG + " INT:" + EnemyData.INT;
                dialogueText.text = EnemyData.Name + "Used Heal... ";
                Debug.Log("Enemy Heal");

                yield return new WaitForSeconds(2f);

                //TutFlag = TutFlag + 1;
                State = GameStates.PlayerTurn;
                PlayerTurn();

            }
            else if (action == 4)
            {
                //Use Sepcial
                action =0;
                bool isDead = PlayerData.SpecialAttackEnemy(EnemyData.Attack, EnemyData.Element, PlayerData.Element, EnemyData.Elemental_Damage, EnemyData.current_ENG);
                EnemyData.Projectile();
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(Camera.ShakeScreen(Duration, Strength));
                EnemyData.current_ENG = EnemyData.current_ENG - 1f;
                PlayerCharacterStats.text = "ATT:" + PlayerData.Attack + "  DEF:" + PlayerData.Defence + "  Health:" + PlayerData.CurrentHealth + "/" + PlayerData.Health + " ENG:" + PlayerData.current_ENG + "/" + PlayerData.ENG + " INT:" + PlayerData.INT;
                EnemyCharacterStats.text = "ATT:" + EnemyData.Attack + "  DEF:" + EnemyData.Defence + "  Health:" + EnemyData.CurrentHealth + "/" + EnemyData.Health + " ENG:" + EnemyData.current_ENG + "/" + EnemyData.ENG + " INT:" + EnemyData.INT;
                dialogueText.text = EnemyData.Name + " used Special Attack on " + PlayerData.Name;
                Debug.Log("enemy used special attacked");

                yield return new WaitForSeconds(2f);
                //TutFlag = TutFlag + 1;
                if (isDead)
                {
                    State = GameStates.Lose;
                    EndGame();
                }
                else
                {
                    State = GameStates.PlayerTurn;
                    PlayerTurn();
                }
            }

        }
       
    }

    //End Game Function
     void EndGame()
    {
        float elapse=0;
        if (State == GameStates.Win)
        {
           
            dialogueText.text = "You Won! You shoud be proud, at least you can do that much... ";
            BuffText.text = "a new Game will begin in shortly";
            while(elapse< 3f)
            {
                elapse += Time.deltaTime;
            }
            SceneManager.LoadScene(1);


        }
        else if (State == GameStates.Lose)
        {
            dialogueText.text = "OH wow, would you look at that, you LOST :(";
            BuffText.text = "a new Game will begin in shortly";
            while (elapse < 3f)
            {
                elapse += Time.deltaTime;
            }
            
            StartCoroutine(BattleSetUp());
        }
        
    }

    public void OnAetherBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Aether Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnNecroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Necro Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnAeroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Aero Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnPyroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Pyro Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnHydroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Hydro Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnElectroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Electro Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());

    }
    public void OnCryoBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Cryo Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnDendroBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Dendro Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
    public void OnGeoBuff()
    {

        if (State != GameStates.PlayerTurn)
        {
            return;
        }
        Buff = "Geo Buff";
        BuffText.text = Buff + " Is Active";
        // StartCoroutine(PlayerStun());
    }
   
}
