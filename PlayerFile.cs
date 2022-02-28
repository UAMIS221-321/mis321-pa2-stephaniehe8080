using System;
using System.IO;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class PlayerFile
    {
        public static List<Player> GetPlayers(){
            

            List<Player> playerList = new List<Player>();

            StreamReader inFile = null;
            try{
                inFile = new StreamReader("players.txt");
            }
            catch (FileNotFoundException e){
                Console.WriteLine("Something went wrong {0}", e);
                return playerList;
            }
            string line = inFile.ReadLine();
            while(line !=null){
                string[] temp = line.Split('#');

                int maxPower = int.Parse(temp[2]);
                int health = int.Parse(temp[3]);
                int attackStrength = int.Parse(temp[4]);
                int defensivePower = int.Parse(temp[5]);
                playerList.Add(new Player{PlayerName = temp[0], CharacterName = temp[1], MaxPower = maxPower, Health = health, AttackStrength = attackStrength, DefensivePower = defensivePower, CharacterAttackType = temp[6]});
                line = inFile.ReadLine();
            }
            inFile.Close();

            return playerList;
        }
    }
}