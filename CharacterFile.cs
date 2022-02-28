using System;
using System.IO;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class CharacterFile
    {
        public static List<Character> GetCharacters(){
            

            List<Character> characterList = new List<Character>();

            StreamReader inFile = null;
            try{
                inFile = new StreamReader("characters.txt");
            }
            catch (FileNotFoundException e){
                Console.WriteLine("Something went wrong {0}", e);
                return characterList;
            }
            string line = inFile.ReadLine();
            while(line !=null){
                string[] temp = line.Split('#');


                int maxPower = int.Parse(temp[1]);
                int health = int.Parse(temp[2]);
                int attackStrength = int.Parse(temp[3]);
                int defensivePower = int.Parse(temp[4]);
                characterList.Add(new Character{CharacterName = temp[0], MaxPower = maxPower, Health = health, AttackStrength = attackStrength, DefensivePower = defensivePower, CharacterAttackType = temp[5]});
                line = inFile.ReadLine();
            }
            inFile.Close();

            return characterList;
        }
    }
}