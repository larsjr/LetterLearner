using System;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace LetterLearner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        static Random rnd = new Random();

        private string[] _letters = 
        {
            "A", "B", "C", "D", "E", "F", "G", "H",
            "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R", "S", "T", "U", "V", "W", "X",
            "Y", "Z", "Æ", "Ø", "Å"
        };

        private string _helloWorld;
        public string HelloWorld
        {
            get { return _helloWorld; }
            set { Set(() => HelloWorld, ref _helloWorld, value); }
        }

        private string _currentLetter;
        public string CurrentLetter
        {
            get { return _currentLetter; }
            set {Set(() => CurrentLetter, ref _currentLetter, value); }
        }

        public MainViewModel()
        {
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";

            SelectLetter();
            Messenger.Default.Register<SendLetter>(
                this,
                (action) => ReceiveLetter(action));

        }

        private RelayCommand _showMessageCommand;
        public RelayCommand ShowMessageCommand =>
            _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessage));

        private RelayCommand _selectNewLetterCommand;

        public RelayCommand SelectNewLetterCommand =>
            _selectNewLetterCommand ?? (_selectNewLetterCommand = new RelayCommand(SelectLetter));

        private void ShowMessage()
        {
            var msg = new ShowMessageDialog {Message = "Hello World"};
            Messenger.Default.Send<ShowMessageDialog>(msg);
        }

        private void ReceiveLetter(SendLetter message)
        {
            if (message.Letter.Equals(CurrentLetter))
            {
                SelectLetter();
            }
        }

        private void SelectLetter()
        {
            int r = rnd.Next(_letters.Length);
            CurrentLetter =  _letters[r];
        }
    }
}