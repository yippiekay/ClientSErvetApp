using AutoMapper;
using ClientApp.ApiService;
using ClientApp.AutoMapper;
using ClientApp.Models;
using ClientApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClientApp
{
    class AppViewModel : INotifyPropertyChanged
    {
        private readonly IMapper mapper;
        private readonly ServerApi server;
        private CardModel selectedCard;
        private bool isReverseOrder;
        private string sortButtonContent;

        public AppViewModel()
        {
            mapper = new Mapper(Configuration.GetConfiguration());
            server = new ServerApi();
            selectedCard = new CardModel();
            isReverseOrder = false;
            sortButtonContent = "Sort ↓";

            Cards = mapper.Map<List<Card>, BindingList<CardModel>>(server.GetCards());

            CreateCommand = new RelayCommand(obj =>
            {
                try
                {
                    NullValueValidation();
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
                    NullValueValidation();
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

            SortCommand = new RelayCommand(obj =>
            {
                if (isReverseOrder == false) 
                {
                    GetSortedList(Cards.OrderBy(card => card.Description).ToList());
                    SortButtonContent = "Sort ↑";
                    isReverseOrder = true;
                }
                else 
                {
                    GetSortedList(Cards.OrderByDescending(card => card.Description).ToList());
                    SortButtonContent = "Sort ↓";
                    isReverseOrder = false;
                }
                SelectedCard = new CardModel();
            });
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

        public string SortButtonContent
        {
            get => sortButtonContent;

            set
            {
                sortButtonContent = value;
                OnPropertyChanged(nameof(SortButtonContent));
            }
        }

        public RelayCommand CreateCommand { get; }

        public RelayCommand UpdateCommand { get; }

        public RelayCommand DeleteCommand { get; }

        public RelayCommand BrowseCommand { get; }

        public RelayCommand SortCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyNamed = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamed));
        }
    
        private void UpdateCardList()
        {
            Cards.Clear();
            var newCardsList = mapper.Map<List<Card>, BindingList<CardModel>>(server.GetCards());
           
            foreach (var card in newCardsList)
            {
                Cards.Add(card);
                SelectedCard = new CardModel();
            }
        }

        private BindingList<CardModel> GetSortedList(List<CardModel> sortedList)
        {
            Cards.Clear();
            foreach (var card in sortedList)
            {
                Cards.Add(card);
            }

            return Cards;
        }

        private void NullValueValidation()
        {
            if (SelectedCard.Description == "" || SelectedCard.Description == null)
            {
                throw new Exception("Add description");
            }
            if (SelectedCard.ImageSource == null)
            {
                throw new Exception("Add image");
            }
        }
    }
}
