using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebAPIClient.ViewModel;

namespace WebAPIClient.Repository
{
    public class FileContext
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\cards.json";

        public List<CardModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            if (!fileExist)
            {
                File.CreateText(PATH).Dispose();
            }
            
            using var reader = File.OpenText(PATH);
            var fileText = reader.ReadToEnd();
            
            if (fileText.Length == 0)
            {
                return new List<CardModel>();
            }

            return JsonConvert.DeserializeObject<List<CardModel>>(fileText);
        }

        public void SaveData(List<CardModel> cardDataList)
        {
            using StreamWriter writer = File.CreateText(PATH);
            string output = JsonConvert.SerializeObject(cardDataList);
            writer.Write(output);
        }
    }
}
