using System;
using System.IO;
using Assets.Scripts.Game.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class JsonWrapper
    {
        public static string DevicePath
        {
            get
            {
#if UNITY_EDITOR
                return $"{Application.dataPath}/Resources";
#else
                return Application.persistentDataPath;
#endif
            }
        }

        public static BoardSaveModel GetGameBoardModel()
        {
            var path = Path.Combine(DevicePath, "BoardSaveModel.JSON");
            return LoadJsonFile<BoardSaveModel>(path);
        }

        public static void SaveGameBoardModel(BoardSaveModel model)
        {
            var path = Path.Combine(DevicePath, "BoardSaveModel.JSON");
            SaveJsonFile(path, model);
        }

        public static T LoadJsonFile<T>(string path) where T : class
        {
            if (!File.Exists(path))
            {
                Debug.Log($"Path: {path} {Environment.NewLine}File doesn't exist");
                return null;
            }

            object deserialized = null;

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = GetJsonSerializer();

                deserialized = (T)serializer.Deserialize(file, typeof(T));
            }
            return deserialized as T;
        }

        public static void SaveJsonFile<T>(string path, T data, Formatting formatting = Formatting.Indented) where T : class
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = GetJsonSerializer(formatting);

                serializer.Serialize(file, data);
            }
        }

        private static JsonSerializer GetJsonSerializer(Formatting formatting = Formatting.Indented)
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = formatting
            };
            serializer.Converters.Add(new Newtonsoft.Json.UnityConverters.Math.Vector2IntConverter());
            
            return serializer;
        }
    }
}