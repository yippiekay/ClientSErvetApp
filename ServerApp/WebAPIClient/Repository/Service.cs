using System.Collections.Generic;
using WebAPIClient.ViewModel;

namespace WebAPIClient.Repository
{
    public class Service : IService
    {
        private readonly List<CardModel> list;
        private readonly FileContext context; 

        public Service (FileContext context)
        {
            list = context.LoadData();
            this.context = context;
        }

        public void CreateCard(CardModel card)
        {
            list.Add(card);
            context.SaveData(list);
        }

        public List<CardModel> GetAllCard()
        {
            return context.LoadData();
        }

        public void UpdateCard(int index, CardModel card)
        {
            list[index].Description = card.Description;
            list[index].ImageByteCode = card.ImageByteCode;
            context.SaveData(list);
        }

        public void DeleteCard (int id)
        {
            list.RemoveAt(id);
            context.SaveData(list);
        }
    }
}
