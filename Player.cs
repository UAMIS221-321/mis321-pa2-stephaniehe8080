using System;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class Player : Character
    {
        public string PlayerName {get; set;}

        public string ToFile(){
            return this.PlayerName + '#' + this.CharacterName + "#" + this.MaxPower + "#" + this.Health + "#" + this.AttackStrength + "#" + this.DefensivePower + "#" + this.CharacterAttackType;
        }

        public override string ToString(){
            return this.PlayerName + "\nCharacter Name: " + this.CharacterName + "\nMax Power: " + this.MaxPower + "\nHealth: " + this.Health + "\nAttack Strength: " + this.AttackStrength + "\nDefensive Power: " + this.DefensivePower + "\nCharacter Move: " + this.CharacterAttackType;
        }
    }
}