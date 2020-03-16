using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LetterLearner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindowOnKeyDown;
            Messenger.Default.Register<ShowMessageDialog>(
                this, 
                (action) => ReceiveMessage(action));
        }

        private async void ReceiveMessage(ShowMessageDialog action)
        {
            DialogService dialogService = new DialogService();
            await dialogService.ShowMessage(action.Message, "Sample Universal App");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream =
                await synth.SynthesizeTextToStreamAsync("Hello, World!");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private void CoreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            var letter = new SendLetter {Letter = VirtualKeyToLetterOnNorwegianKeyboard(args.VirtualKey)};
            Messenger.Default.Send<SendLetter>(letter);
        }

        private string VirtualKeyToLetterOnNorwegianKeyboard(VirtualKey key)
        {
            switch (key.ToString())
            {
                case "222":
                    return "Æ";
                case "186":
                    return "Ø";
                case "192":
                    return "Ø";
                case "219":
                    return "Å";
                case "221":
                    return "Å";
                default:
                    return key.ToString();
            }
        }


    }
}
