using System.Collections.Generic;
using Assets.Scripts.Game.GameBoard;
using UnityEngine;
using System.Linq;
using System;

namespace Assets.Scripts.Game.Models
{
    public class PlayerModel
    {
        public string Name { get; private set; }
        public int Turns { get; private set; }

        public List<ChipHolder> TargetHolders { get; private set; }
        public List<IPlayerElement> PlayerChips { get; private set; }

        public PlayerModel(string name, List<ChipHolder> targetHolders, List<IPlayerElement> playerChips)
        {
            Name = name;
            Reset(targetHolders, playerChips);
        }

        public void Reset(List<ChipHolder> targetHolders, List<IPlayerElement> playerChips)
        {
            Turns = 0;
            TargetHolders = targetHolders;
            PlayerChips = playerChips;
        }

        public void MakeTurn() => Turns++;

        public bool IsPlayerWin() => TargetHolders.All(x => !x.IsEmpty && PlayerChips.Contains(x.GetPlayerElement));

        public bool IsPlayerChip(IPlayerElement playerChip)
        {
            return PlayerChips.Contains(playerChip);
        }
    }
}