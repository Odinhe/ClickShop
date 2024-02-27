using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static playerClick;
using TMPro;
using GameRestart;
using AttackTri;
using HPTri;

//creat struct before the class
public struct PlayerStats
{
    public int health;
    public int attackDamage;
    public int deathTime;
    public int playerHP;
    public int monsterDamage;
}
public class playerClick : MonoBehaviour
{
    //call the stats
    public PlayerStats Stats;
    public int moneyAmount;
    //set up the timer for monster to attack
    public bool takeDamage = true;
    public float damageTime = 10f;
    //set up the timer when monster die
    public bool canAttack = true;
    public float attackTime = 3f;
    public List<Sprite> spr;
    //creat the text that will be changed
    public TextMeshProUGUI MonsterHP;
    public TextMeshProUGUI PlayHP;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI CanAttack;

    // Start is called before the first frame update
    void Start()
    {
        //set the stats into different intergers
        Stats.health = 100;
        Stats.attackDamage = 10;
        Stats.deathTime = 1;
        Stats.playerHP = 200;
        Stats.monsterDamage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //get the amount of money player hold right now
        moneyAmount = MoneyManager.Instance.Money;
        //set the text to the correct number
        MonsterHP.text = "Monster HP: " + Stats.health;
        PlayHP.text = "Player HP: " + Stats.playerHP;
        Money.text = "Money: " + moneyAmount;
        
        //when left button was clicked, deal damage to the monster
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            //run the program in the namespace that show player can attack
            AttackTri.AttackTriger restTriger = new AttackTri.AttackTriger();
            restTriger.attackTriger();
            //monster lose health
            Stats.health -= Stats.attackDamage;
            Debug.Log(Stats.health);
        }
        //when monster died
        if (Stats.health <= 0)
        {
            //game gets harder
            Stats.deathTime++;
            //add 100 dollars to player
            MoneyManager.Instance.AddMoney(100);
            //the next monster will have more hp
            Stats.health = 100 * Stats.deathTime;
            //start the death cool down and monster attack cool down
            StartCoroutine(DeathCool());
            StartCoroutine(TakeDamageCool());
        }
        //when player's hp is higher than 0
        if (takeDamage && Stats.playerHP > 0)
        {
            //run the program in namespace to display player where taking damage
            HPTri.HPTriger hpT = new HPTri.HPTriger();
            hpT.hpTriger();
            //player lose hp
            Stats.playerHP -= Stats.monsterDamage * Stats.deathTime;
            Debug.Log(Stats.playerHP);
            //start the monster attack cool down
            StartCoroutine(TakeDamageCool());
        }
        //if player died
        if(Stats.playerHP <= 0)
        {
            //run the reset game function
            ResetGame();
        }
    }
    //cool down systems
    System.Collections.IEnumerator TakeDamageCool()
    {
        takeDamage = false;

       yield return new WaitForSeconds(damageTime);
       
        takeDamage = true;

    }
    System.Collections.IEnumerator DeathCool()
    {
        canAttack = false;
        //GetComponent<SpriteRenderer>().sprite = spr[0];
        CanAttack.text = "Can't Attack!";
        yield return new WaitForSeconds(attackTime);

        canAttack = true;
        CanAttack.text = "Can Attack!";
        //GetComponent<SpriteRenderer>().sprite = spr[1];
    }

    //when the check out object meets the potions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the potion is fire potion and player had more than equals 100 money
        if (collision.gameObject.CompareTag("Fire") && moneyAmount >= 100)
        {
            FirePotion fireP = new FirePotion();
            //player's attack increased
            Stats.attackDamage += 10;
            //player's money reduced
            MoneyManager.Instance.ReduceMoney(100);
            //shows what the potion do by using inherited child class from a parent base class 
            fireP.AttackUp();
            //destory the potion
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Health") && moneyAmount >= 50)
        {
            HPPotion hPPotion = new HPPotion();
            //player's hp increase 100
            Stats.playerHP += 100;
            // player's money reduced
            MoneyManager.Instance.ReduceMoney(50);
            //shows what the potion do by using inherited child class from a parent base class 
            hPPotion.HPUp();
            //destory the potion
            Destroy(collision.gameObject);
        }
    }
    public void ResetGame()
    {
        //use namespace to call the reset game function so the game start when player don't have hp.
        GameRestart.RestartGame restartGame = new GameRestart.RestartGame();
        restartGame.RestartScene();
    }
}
