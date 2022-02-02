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

        private readonly List<IPlayerElement> spawnedPlayerElements = new List<IPlayerElement>();
        private readonly List<ChipHolder> spawnedChipHolders = new List<ChipHolder>();

        public IPlayerElement SpawnPlayerElement(Transform holder)
        {
            var path = Path.Combine(resourcesPath, chipPath);

            var playerElement = Spawn<IPlayerElement>(path, holder);
            spawnedPlayerElements.Add(playerElement);
            return playerElement;
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
            for (int i = 0; i < spawnedPlayerElements.Count; i++)
            {
                var chip = spawnedPlayerElements[i];
                spawnedPlayerElements.Remove(chip);
                Destroy(chip.GetTransform.gameObject);
            }
            spawnedPlayerElements.Clear();
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