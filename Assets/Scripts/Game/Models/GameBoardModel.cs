using System.Collections.Generic;
using Assets.Scripts.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters.Math;
using UnityEngine;

namespace Assets.Scripts.Game.Models
{

    public class BoardSaveModel
    {
        [JsonConverter(typeof(Vector2IntConverter))]
        public Vector2Int boardSize;


        public readonly List<Vector2Int> firstPlayerStartChipPositions = new List<Vector2Int>();


        public readonly List<Vector2Int> secondPlayerStartChipPositions = new List<Vector2Int>();
    }
}