using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Models
{

    public class BoardSaveModel
    {
        public Vector2Int boardSize;


        public readonly List<Vector2Int> firstPlayerStartPositions = new List<Vector2Int>();
        public readonly List<Vector2Int> secondPlayerStartPositions = new List<Vector2Int>();
    }
}