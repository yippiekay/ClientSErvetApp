using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace ClientApp.Models
{
    public class CardModel : INotifyPropertyChanged
    {
        private string description;
       
        public string Description 
        { 
            get => description;
            
            set
            {
                description = value;
                OnPropertyChanged("Description");
            } 
        }

        public BitmapImage ImageSource { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyNamed = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamed));
        }
    }
}
