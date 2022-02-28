using System;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class PlayerUtils
    {
        public static void PrintAllPlayers(List<Player> playerList){
            foreach(Player player in playerList){
                Console.WriteLine("\n" + player.ToString());
            }  
            Console.WriteLine("\n");
        }        

    }
}