using System.Windows;
using System.Windows.Controls;

namespace ClientApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            newCardForm.Visibility = newCardForm.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            createButton.IsEnabled = true;
            updateButton.IsEnabled = false;
        }
        
        private void UpdateCardButton_Click(object sender, RoutedEventArgs e)
        {
            newCardForm.Visibility = Visibility.Visible;
            createButton.IsEnabled = false;
            updateButton.IsEnabled = true;
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            newCardForm.Visibility = Visibility.Collapsed;
        }

        private void CardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updateCardButton != null)
            {
                updateCardButton.Visibility = cardList.SelectedItem == null ? Visibility.Collapsed : Visibility.Visible;
                deleteCardButton.Visibility = cardList.SelectedItem == null ? Visibility.Collapsed : Visibility.Visible;
                newCardForm.Visibility = Visibility.Collapsed;
            }
        }
    }
}
