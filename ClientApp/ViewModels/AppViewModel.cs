using AutoMapper;
using ClientApp.ApiService;
using ClientApp.AutoMapper;
using ClientApp.Models;
using ClientApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClientApp
{
    class AppViewModel : INotifyPropertyChanged
    {
        private readonly IMapper mapper = new Mapper(Configuration.GetConfiguration());
        private readonly ServerApi server = new ServerApi();
        private CardModel selectedCard;

        public AppViewModel()
        {
            Cards = mapper.Map<List<Card>, BindingList<CardModel>>(server.GetCards());

            CreateCommand = new RelayCommand(obj =>
            {
                try
                {
                    if(SelectedCard.Description == "")
                    {
                        throw new Exception("Add description");
                    }
                    server.CreateCard(mapper.Map<Card>(selectedCard));
                    UpdateCardList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            });

            UpdateCommand = new RelayCommand(obj =>
            {
                try
                {
                    if (SelectedCard.Description == "")
                    {
                        throw new Exception("Add description");
                    }
                    server.UpdateCard(mapper.Map<Card>(SelectedCard), Index);
                    UpdateCardList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            });

            DeleteCommand = new RelayCommand(obj => {
                server.DeleteCard(Index);
                UpdateCardList();
            });

            BrowseCommand = new RelayCommand(obj =>
            {
                try
                {
                    var ofdPicture = new OpenFileDialog() { Filter = "Image files|*.jpg;*.png", FilterIndex = 1 };

                    if (ofdPicture.ShowDialog() == true)
                    {
                        SelectedCard.ImageSource = new BitmapImage(new Uri(ofdPicture.FileName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            });
           
            SelectedCard = new CardModel();
        }

        public BindingList<CardModel> Cards { get; set; }
       
        public int Index { get; set; }
       
        public CardModel SelectedCard
        {
            get => selectedCard;
            
            set
            {
                selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
            }
        }

        public RelayCommand CreateCommand { get; }

        public RelayCommand UpdateCommand { get; }

        public RelayCommand DeleteCommand { get; }

        public RelayCommand BrowseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyNamed = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamed));
        }
    
        void UpdateCardList()
        {
            Cards.Clear();
            var newCardsList = mapper.Map<List<Card>, BindingList<CardModel>>(server.GetCards());
           
            foreach (var card in newCardsList)
            {
                Cards.Add(card);
                SelectedCard = new CardModel();
            }
        }
    }
}
