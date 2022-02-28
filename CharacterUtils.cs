using System;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class CharacterUtils
    {
        public static void PrintAllCharacters(List<Character> characterList){
            foreach(Character character in characterList){
                Console.WriteLine("\n" + character.ToString());
            }  
            Console.WriteLine("\n");
        }        

    }
}