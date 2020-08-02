using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIClient.ViewModel;

namespace WebAPIClient.Repository
{
    public interface IService
    {
        public void CreateCard(CardModel card);

        public List<CardModel> GetAllCard();

        public void UpdateCard(int index, CardModel card);

        public void DeleteCard(int id);
    }
}
