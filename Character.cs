using System;
using System.Collections.Generic;

namespace MIS_321_PA_1
{
    public class Character:ICharacter
    {
        public string CharacterName {get; set;}
        public int MaxPower {get; set;}
        public double Health {get; set;}
        public int AttackStrength {get; set;}
        public int DefensivePower {get; set;}
        public string CharacterAttackType {get; set;}


        public string ToFile(){
            return this.CharacterName + "#" + this.MaxPower + "#" + this.Health + "#" + this.AttackStrength + "#" + this.DefensivePower + "#" + this.CharacterAttackType;
        }

        public override string ToString(){
            return "\nCharacter Name: " + this.CharacterName + "\nMax Power: " + this.MaxPower + "\nHealth: " + this.Health + "\nAttack Strength: " + this.AttackStrength + "\nDefensive Power: " + this.DefensivePower + "\nCharacter Move: " + this.CharacterAttackType;
        }
    }
}