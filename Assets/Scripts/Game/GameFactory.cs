using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Core;
using Assets.Scripts.Game.GameBoard;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameFactory : BaseFactory
    {
        private const string resourcesPath = "Game/";
        private const string chipPath = "PlayerChip";
        private const string boardChipHolderPath = "ChipHolder";

        private readonly List<PlayerChip> spawnedChips = new List<PlayerChip>();
        private readonly List<ChipHolder> spawnedChipHolders = new List<ChipHolder>();

        public PlayerChip SpawnChip(Transform holder)
        {
            var path = Path.Combine(resourcesPath, chipPath);

            var chip = Spawn<PlayerChip>(path, holder);
            spawnedChips.Add(chip);
            return chip;
        }

        public ChipHolder SpawnChipHolder(Transform holder)
        {
            var path = Path.Combine(resourcesPath, boardChipHolderPath);

            var chipHolder = Spawn<ChipHolder>(path, holder);
            spawnedChipHolders.Add(chipHolder);
            return chipHolder;
        }

        public void ReleaseAllSpawned()
        {
            ReleaseChips();
            ReleaseChipHolders();
        }

        private void ReleaseChips()
        {
            for (int i = 0; i < spawnedChips.Count; i++)
            {
                var chip = spawnedChips[i];
                spawnedChips.Remove(chip);
                Destroy(chip.gameObject);
            }
            spawnedChips.Clear();
        }

        private void ReleaseChipHolders()
        {
            for (int i = 0; i < spawnedChipHolders.Count; i++)
            {
                var chipHolder = spawnedChipHolders[i];
                spawnedChipHolders.Remove(chipHolder);
                Destroy(chipHolder.gameObject);
            }
            spawnedChipHolders.Clear();
        }

    }
}