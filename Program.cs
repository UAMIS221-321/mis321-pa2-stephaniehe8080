using System;
using System.Collections.Generic;
using System.IO;

namespace MIS_321_PA_1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Default characters created
            // CreateCharacter("Jack Sparrow", "Distract opponent");
            // CreateCharacter("Will Turner", "Sword");
            // CreateCharacter("Davy Jones", "Cannon fire");

            int currentPlayer = 1;
            bool userSelectionForCharacterStatsNotValid = true;
            string playerOne = "E";
            string playerTwo = "A";
            
            // while(currentPlayer < 3){
            //     string userSelection = GetCreateCharacterOrNotSelection(currentPlayer).ToUpper();

            //     if (userSelection == "SELECT CHARACTER"){
            //         userSelectionForCharacterStatsNotValid = true;
            //         while(userSelectionForCharacterStatsNotValid){

            //             string userSelectionForCharacterStats = GetSelectionForCharacterStats().ToUpper();

            //             if(userSelectionForCharacterStats == "Y"){
            //                 ShowSingleCharacterOriginalStats();
            //             }
            //             else if(userSelectionForCharacterStats == "N"){
            //                 string playerName = AddPlayerCharacter();
            //                     switch(currentPlayer)
            //                     {
            //                         case 1:
            //                             playerOne = playerName;
            //                         break;

            //                         case 2:
            //                             playerTwo = playerName;
            //                         break;

            //                         default:
            //                         break;
            //                     }
            //                 userSelectionForCharacterStatsNotValid = false;
            //                 currentPlayer ++;
            //             }
            //             else{
            //                 EnterValidSelection();
            //             }
            //         }
            //     }
            //     else if (userSelection == "CREATE CHARACTER"){
            //         //Check if character exists using List.Exist()
            //         string characterName = GetCharacterName();
            //         string characterAttackType = GetCharacterAttackType();
            //         CreateCharacter(characterName, characterAttackType);
            //     }
            //     else if (userSelection == "SEE CHARACTER STATS"){
            //         userSelectionForCharacterStatsNotValid = true;
            //         while(userSelectionForCharacterStatsNotValid){

            //             string userSelectionForCharacterStats1 = GetSelectionForCharacterStats().ToUpper();

            //             if(userSelectionForCharacterStats1 == "Y"){
            //                 ShowSingleCharacterOriginalStats();
            //             }
            //             else if(userSelectionForCharacterStats1 == "N"){
            //                 userSelectionForCharacterStatsNotValid = false;
            //             }
            //             else{
            //                 EnterValidSelection();
            //             }
            //         }
            //     }
            //     else if (userSelection == "EXIT"){
            //         currentPlayer = 3;
            //         Console.WriteLine("Bye!");
            //         //Add in method that deletes all player date from players.txt if player so chooses and default characters from characters.txt?
            //     }
            //     else{
            //         EnterValidSelection();
            //     }

            // }
            
            bool playerOneFirstOrNot = PlayerOneFirstOrNot();
            double damageDone = 0;

            int playerOneAttackStrength = SearchAttackStrengthByPlayerName(playerOne);
            int playerTwoAttackStrength = SearchAttackStrengthByPlayerName(playerTwo);
            int playerOneDefensivePower = SearchDefensivePowerByPlayerName (playerOne);
            int playerTwoDefensivePower = SearchDefensivePowerByPlayerName(playerTwo);
            double playerOneHealth = SearchHealthByPlayerName(playerOne);
            double playerTwoHealth = SearchHealthByPlayerName(playerTwo);
            string playerOneAttackType = SearchAttackTypeByPlayerName(playerOne);
            string playerTwoAttackType = SearchAttackTypeByPlayerName(playerTwo);
            string playerAttackingAttackType = null;
            string playerDefendingAttackType = null;

            while(playerOneHealth > 0 && playerTwoHealth > 0){
                switch(playerOneFirstOrNot)
                {
                    case true:
                        playerAttackingAttackType = SearchAttackTypeByPlayerName(playerOne);
                        playerDefendingAttackType = SearchAttackTypeByPlayerName (playerTwo);
                        
                        if(playerOneAttackStrength <= playerTwoDefensivePower){
                            damageDone = 0;
                        }
                        else{
                            damageDone = DamageDone(playerAttackingAttackType, playerDefendingAttackType, playerOneAttackStrength, playerTwoDefensivePower);
                            playerTwoHealth = playerTwoHealth - damageDone;
                            UpdateDefensivePlayerHealth(playerTwo, playerTwoHealth);
                        }

                        ShowPowerandDamageDone(playerOne, damageDone);
                        ShowAttackedPlayerStats(playerTwo);
                        playerOneFirstOrNot = false;

                        PressEnterToContinue();
                    break;

                    case false:
                        playerAttackingAttackType = SearchAttackTypeByPlayerName(playerTwo);
                        playerDefendingAttackType = SearchAttackTypeByPlayerName (playerOne);

                        if(playerTwoAttackStrength <= playerOneDefensivePower){
                            damageDone = 0;
                        }
                        else{
                            damageDone = DamageDone(playerAttackingAttackType, playerDefendingAttackType, playerTwoAttackStrength, playerOneDefensivePower);
                            playerOneHealth = playerOneHealth - damageDone;
                            UpdateDefensivePlayerHealth(playerOne, playerOneHealth);
                        }

                        ShowPowerandDamageDone(playerTwo, damageDone);
                        ShowAttackedPlayerStats(playerOne);
                        playerOneFirstOrNot = true;

                        PressEnterToContinue();
                    break;

                    default:
                    break;
                }   
            }
            Console.WriteLine("Congrats! Someone won.");
        }

        static void PressEnterToContinue(){
            Console.WriteLine("-----------------------------------------------------");
            Console.Write("Press <Enter> to continue... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) {}
        }
        static void CreateCharacter(string characterName, string characterAttackType){
            Random maxPower = new Random();
            int maxPowerCharacter = maxPower.Next(100) + 1;
            int attackStrengthCharacter = maxPower.Next(maxPowerCharacter - 1) + 1;
            int defensivePowerCharacter = maxPower.Next(maxPowerCharacter - 1) + 1;

            Character character = new Character{CharacterName = characterName, MaxPower = maxPowerCharacter, Health = 100, AttackStrength = attackStrengthCharacter, DefensivePower = defensivePowerCharacter, CharacterAttackType = characterAttackType};
            
            List<Character> characterList = CharacterFile.GetCharacters();
            characterList.Add(character);

            StreamWriter outFile = new StreamWriter("characters.txt", true);

            outFile.WriteLine(character.ToFile()); 

            outFile.Close();       
        }
        static string GetCreateCharacterOrNotSelection(int playerNumber){
            Console.WriteLine("Player " + playerNumber + ". Welcome to Battle of Calypso's maelstrom. Select character to fight with or quit! Your options are: \nSelect Character\nCreate Character\nSee Character Stats\nExit");
            return Console.ReadLine();
        }
        static void ShowCharactersAvailable(){
            List<Character> characterList = CharacterFile.GetCharacters();
            
            foreach(Character character in characterList){
                Console.WriteLine(character.CharacterName);
            }
            
        }
        static string GetCharacterSelection(){
            Console.WriteLine("Select your character");
            ShowCharactersAvailable();
            return Console.ReadLine();
        }
        static string GetSelectionForCharacterStats(){
            Console.WriteLine("Would you like to view a character's stats?\nY\nN");
            return Console.ReadLine();
        }

        static void PrintUserSelection(string userSelection){
            Console.WriteLine("Your user selection is " + userSelection);
        }

        static int GetCharacterID(){ //Gets greatest ID in characters.txt assuming 0, 1, 2, etc ID system
        //Override IDs using foreach(Console.WriteLine(count+Character.Name+...)....see ShowOriginal in PA 1
            int count = 0;

            StreamReader inFile = null;
            try{
                inFile = new StreamReader("characters.txt");
            }
            catch (FileNotFoundException e){
                Console.WriteLine("Something went wrong {0}", e);
            }

            string line = inFile.ReadLine();
            while(line !=null){
                string[] temp = line.Split('#');

                string playerID = temp[0];
                int maxPower = int.Parse(temp[2]);
                int health = int.Parse(temp[3]);
                int attackStrength = int.Parse(temp[4]);
                int defensivePower = int.Parse(temp[5]);
                count++;
                line = inFile.ReadLine();
            }
            inFile.Close();
            return count;
            
        }

        static string GetPlayerName(){
            Console.WriteLine("Enter in name for player");
            return Console.ReadLine();
        }
        static string GetCharacterName(){
            Console.WriteLine("Enter in name for character");
            return Console.ReadLine();
        }
        static string GetCharacterAttackType(){
            Console.WriteLine("Enter in name for the attack of your character");
            return Console.ReadLine();
        }
        static void EnterValidSelection(){
            Console.WriteLine("Please enter valid selection");
        }
        static bool PlayerOneFirstOrNot(){
            bool playerOneFirstOrNot = true;
            
            Random whoFirstValue = new Random();
            int playerOne = whoFirstValue.Next(100) + 1;   
            int playerTwo = whoFirstValue.Next(100) + 1;         

            if(playerOne < playerTwo){
                playerOneFirstOrNot = false;
            }

            return playerOneFirstOrNot;
        }
        static int SearchAttackStrengthByPlayerName(string playerName){
            Player player = SearchForPlayer(playerName);

            int playerAttackStrength = player.AttackStrength;
            
            return playerAttackStrength;
        }
        static int SearchDefensivePowerByPlayerName(string playerName){
            Player player = SearchForPlayer(playerName);

            int playerDefensivePower = player.DefensivePower;
            
            return playerDefensivePower;
        }
        static double SearchHealthByPlayerName(string playerName){
            Player player = SearchForPlayer(playerName);

            double playerHealth = player.Health;
            
            return playerHealth;
        }
        static string SearchAttackTypeByPlayerName(string playerName){
            Player player = SearchForPlayer(playerName);

            string playerAttackType = player.CharacterAttackType;
            
            return playerAttackType;
        }
        static Player SearchForPlayer(string playerName){
            List<Player> playerList = PlayerFile.GetPlayers();

            Player player = playerList.Find(x => x.PlayerName.Contains(playerName)); 

            return player;
        }
        static Character SearchForCharacter(){
            List<Character> characterList = CharacterFile.GetCharacters();

            Character character = null;
            bool characterNotFound = true;

            //3rd iteration of invalid input crashes but when not put in method, it's fine??
            //Error Handling or else after 3rd iteration of entering in character name that does not exist, will receive error
            do{                
                Console.WriteLine("Please make a valid selection. Check spelling, capitalization, etc."); //why is this iterating 3 times before printing again if entering invalid character name??
                character = characterList.Find(x => x.CharacterName.Contains(GetCharacterSelection())); 
                //Console.WriteLine("hi"); /why is this iterating 3 times before printing again if entering invalid character name??
                characterNotFound = false;
            }while(characterNotFound);

            return character;
        }
        static string AddPlayerCharacter(){
            string playerName = GetPlayerName();
            
            Character character = SearchForCharacter();

            string characterName = character.CharacterName;
            int maxPower = character.MaxPower;
            double health = character.Health;
            int attackStrength = character.AttackStrength;
            int defensivePower = character.DefensivePower;
            string attackType = character.CharacterAttackType;
        
            Player myPlayer = new Player{PlayerName = playerName, CharacterName = characterName, MaxPower = maxPower, Health = health, AttackStrength = attackStrength, DefensivePower = defensivePower, CharacterAttackType = attackType};
            
            List<Player> playerList = PlayerFile.GetPlayers();
            playerList.Add(myPlayer);

            StreamWriter outFile = new StreamWriter("players.txt", true);

            outFile.WriteLine(myPlayer.ToFile()); 

            outFile.Close();

            return playerName;
        }
        static void ShowSingleCharacterOriginalStats(){
            List<Character> characterList = CharacterFile.GetCharacters(); 

            Character character = SearchForCharacter();

            Console.WriteLine(character.ToString());
        }     
        static void ShowAllCharacterOriginalStats(){
            List<Character> characterList = CharacterFile.GetCharacters(); 

            CharacterUtils.PrintAllCharacters(characterList);
        }
        static void ShowAttackedPlayerStats(string playerName){
            List<Player> playerList = PlayerFile.GetPlayers(); 

            Player player = SearchForPlayer(playerName);

            Console.WriteLine("Attacked player: " + player.ToString());
        }   
        static void ShowAllCurrentPlayerStats(){
            List<Player> playerList = PlayerFile.GetPlayers(); 

            PlayerUtils.PrintAllPlayers(playerList);
        }    
        static void ShowPowerandDamageDone(string playerName, double damageDone){
            List<Player> playerList = PlayerFile.GetPlayers(); 

            Player player = SearchForPlayer(playerName);

            Console.WriteLine("Your character's attack power: " + player.AttackStrength + "\nDamage done: " + damageDone);
        }
        static void RemovePlayer(string playerName){
            List<Player> playerList = PlayerFile.GetPlayers();
            Console.ReadKey();
            Console.WriteLine("1");
            Player player = playerList.Find(x => x.PlayerName.Contains(playerName)); 
            Console.ReadKey();
            Console.WriteLine("2");
            playerList.Remove(SearchForPlayer(playerName));
            Console.ReadKey();
            Console.WriteLine("3");
            StreamWriter outFile = new StreamWriter("players.txt");
            Console.ReadKey();
            Console.WriteLine("4");
            foreach(Player aPlayer in playerList){
                Console.ReadKey();
                Console.WriteLine("5");
                outFile.WriteLine(player.ToFile()); 
                Console.ReadKey();
                Console.WriteLine("6"); 
            }
            Console.ReadKey();
            Console.WriteLine("7");
            outFile.Close();
            Console.ReadKey();
            Console.WriteLine("8");
        }     
        static double DamageDone(string playerAttackingAttackType, string playerDefendingAttackType, int attackerAttackStrength, int defenderDefensePower){
            double damageDone = 0;
            double typeBonus = 0;

            if(playerAttackingAttackType == "Distract opponent" && playerDefendingAttackType == "Sword" || playerAttackingAttackType == "Sword" && playerDefendingAttackType =="Cannon fire" || playerAttackingAttackType == "Cannon fire" && playerDefendingAttackType == "Distract opponent"){
                typeBonus = 1.0;
                damageDone = (attackerAttackStrength - defenderDefensePower) * typeBonus;
            }
            else{
                typeBonus = 1.0;
                damageDone = (attackerAttackStrength - defenderDefensePower) * typeBonus;
            }

            return damageDone;
        }
        static void UpdateDefensivePlayerHealth(string playerName, double playerHealth){
            Player defensivePlayer = SearchForPlayer(playerName);

            //string playerNam = defensivePlayer.PlayerName;
            string characterName = defensivePlayer.CharacterName;
            int maxPower = defensivePlayer.MaxPower;
            double health = playerHealth;
            int attackStrength = defensivePlayer.AttackStrength;
            int defensivePower = defensivePlayer.DefensivePower;
            string attackType = defensivePlayer.CharacterAttackType;
        
            Player myPlayer = new Player{PlayerName = playerName, CharacterName = characterName, MaxPower = maxPower, Health = health, AttackStrength = attackStrength, DefensivePower = defensivePower, CharacterAttackType = attackType};
            
            RemovePlayer(playerName);

            List<Player> playerList = PlayerFile.GetPlayers();
            playerList.Add(myPlayer);

            StreamWriter outFile = new StreamWriter("players.txt", true);

            outFile.WriteLine(myPlayer.ToFile()); 

            outFile.Close();
        }
    }
}
