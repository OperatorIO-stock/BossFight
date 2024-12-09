namespace BossFight;

class Program
{
    static void Main(string[] args)
    {
        const string CommandBaseAttack = "BA";
        const string CommandAttackFireBall = "FB";
        const string CommandAttackBlast = "B";
        const string CommandHelingPlayer = "H";

        //Характеристики игрока
        int playerHealth = 100;
        int playerMana = 100;
        int playerMaxHealth = 100;
        int playerMinHealth = 0;
        int playerMaxMana = 100;
        int playerDamageBaseAttack = 15;
        int playerDamageFireBall = 35;
        int playerDamageBlast = 80;
        int healingAttempts = 3;

        //Лечение и пополнения маны
        int healingHealth = 50;
        int healingMana = 25;

        //Стоимость использования способностей 
        int costUseManaFireBall = 15;
        int costUseManaBlast = 25;

        //Характеристики босса
        int bossHealth = 500;
        int bossMaxHealth = 500;
        int bossBaseDamageLower = 10;
        int bossBaseDamageUpper = 25;

        bool isUsingUltimate = false;
        bool isPlayerMadeHit = false;

        Console.WriteLine("В мрачном свете заброшенной арены, наполненной шепотом древних легенд, на ваших глазах поднимается величественная фигура.");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("Увенчанный черным доспехом, взглянул на вас с презрением, его глаза светятся загадочным красным светом.\n" + 
                            "Его шаги еле слышны, но земля под ним трясётся, как будто сама природа боится его могущества.");
        Thread.Sleep(5000);
        Console.Clear();
        Console.WriteLine("Глубокий голос разносится по арене:\n -Ты осмелился прийти сюда, но знаешь ли ты, с кем имеешь дело?");
        Thread.Sleep(3000);
        Console.Clear();
        Console.WriteLine("Игрок атакует первым!");

        Console.WriteLine($"Доступные действия:\nБазовая атака - {CommandBaseAttack}\nОгненный шар - {CommandAttackFireBall}\n" + 
                                $"Ультимейт - {CommandAttackBlast}\nИсцеление - {CommandHelingPlayer}");

        while (playerHealth > 0 || bossHealth > 0)
        {
            Random random = new Random();
            int bossBaseAttack = random.Next(bossBaseDamageLower, bossBaseDamageUpper);

            string dialogBox = Console.ReadLine();

            switch(dialogBox)
            {
                case CommandBaseAttack:
                    bossHealth -= playerDamageBaseAttack;

                    isUsingUltimate = false;
                    isPlayerMadeHit = true;
                    break;

                case CommandAttackFireBall:
                    playerMana -= costUseManaFireBall;

                    if (playerMana < 0)
                    {
                        Console.WriteLine("Закончилась мана!");
                        playerMana += costUseManaFireBall;
                        break;
                    }

                    bossHealth -= playerDamageFireBall;

                    isUsingUltimate = true;
                    isPlayerMadeHit = true;
                    break;

                case CommandAttackBlast:
                    if (!isUsingUltimate)
                    {
                        Console.WriteLine("\nСначала ты должен использовать навык <<AttackFireBall>>");
                        break;
                    }

                    playerMana -= costUseManaBlast;

                    if (playerMana < 0)
                    {
                        Console.WriteLine("Закончилась мана!");
                        playerMana += costUseManaBlast;
                        break;
                    }

                    bossHealth -= playerDamageBlast;

                    isPlayerMadeHit = true;
                    isUsingUltimate = false;
                    break;

                case CommandHelingPlayer:
                    if (healingAttempts > 0)
                    {
                        for (int i = 0; i <= healingAttempts;)
                        {
                            playerHealth += healingHealth;

                            if (playerHealth > playerMaxHealth)
                                playerHealth = playerMaxHealth;

                            playerMana += healingMana;

                            if(playerMana > playerMaxMana)
                                playerMana = playerMaxMana;

                            healingAttempts--;

                            Console.WriteLine($"Вы использовали исцеления!\nОсталось попыток: {healingAttempts}");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nУ вас закончились попытки исцеления.");
                    }

                    isUsingUltimate = false;
                    break;

                default:
                    Console.WriteLine("\nВам не удалось ранить противника");
                    Console.WriteLine("Теперь удар делает Душа Пепла");
                    Thread.Sleep(3000);
                    
                    playerHealth -= bossBaseAttack;

                    if (playerHealth <= 0)
                        playerHealth = playerMinHealth;

                    isPlayerMadeHit = false;
                    break;
            }

            if (isPlayerMadeHit)
            {
                Console.WriteLine("\nТеперь удар делает Душа Пепла");
                Thread.Sleep(3000);

                playerHealth -= bossBaseAttack;

                if (playerHealth <= 0)
                    playerHealth = playerMinHealth;
            }

            if (playerHealth <= 0 && bossHealth > 0)
            {
                Console.WriteLine("\nВы проиграли эту битву!");
                Console.WriteLine($"\nPlayer Health and Mana: {playerHealth}/{playerMaxHealth} || {playerMana}/{playerMaxMana}\n" +
                                $"Boss Health: {bossHealth}/{bossMaxHealth}");
                break;
            }
            else if (bossHealth <= 0 && playerHealth > 0)
            {
                Console.WriteLine("\nВы оказались сильнее и победили эту битву!");
                Console.WriteLine($"\nPlayer Health and Mana: {playerHealth}/{playerMaxHealth} || {playerMana}/{playerMaxMana}\n" +
                                $"Boss Health: {bossHealth}/{bossMaxHealth}");
                break;
            }
            else if (bossHealth <= 0 && playerHealth <= 0)
            {
                Console.WriteLine("\nНичья!");
                Console.WriteLine($"\nPlayer Health and Mana: {playerHealth}/{playerMaxHealth} || {playerMana}/{playerMaxMana}\n" +
                                $"Boss Health: {bossHealth}/{bossMaxHealth}");
                break;
            }

            Console.WriteLine($"\nPlayer Health and Mana: {playerHealth}/{playerMaxHealth} || {playerMana}/{playerMaxMana}\n" +
                                $"Boss Health: {bossHealth}/{bossMaxHealth}");
            Console.WriteLine("\nСледующий удар за вами!\n" + $"Доступные действия:\nБазовая атака - {CommandBaseAttack}\nОгненный шар - {CommandAttackFireBall}\n" + 
                                $"Ультимейт - {CommandAttackBlast}\nИсцеление - {CommandHelingPlayer}");
        }
    }
}
