using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SamuraiWpfUI
{
    public partial class MainWindow : Window
    {
        private readonly ConnectedData _data = new ConnectedData();
        private Samurai _currentSamurai;
        private bool _isListChanging;
        private bool _isLoading;
        private ObjectDataProvider _samuraiViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoading = true;
            samuraiListBox.ItemsSource = _data.SamuraisListInMemory();
            _samuraiViewSource = (ObjectDataProvider)FindResource("samuraiViewSource");
            _isLoading = false;
            samuraiListBox.SelectedIndex = 0;
        }
         
        private void samuraiListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!_isLoading)
            {
                _isListChanging = true;
                _currentSamurai = _data.LoadSamuraiGraph((int)samuraiListBox.SelectedValue);
                _samuraiViewSource.ObjectInstance = _currentSamurai;
                _isListChanging = false;
            }
        }

      

        private void NewSamuraiButton_Click(object sender, RoutedEventArgs e)
        {
            _currentSamurai = _data.CreateNewSamurai();
            _samuraiViewSource.ObjectInstance = _currentSamurai;
            samuraiListBox.SelectedItem = _currentSamurai;
        }

        private void realNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!_isLoading && !_isListChanging)
            {
                if (_currentSamurai.SecretIdentity == null)
                {
                    _currentSamurai.SecretIdentity = new SecretIdentity();
                }
                _currentSamurai.SecretIdentity.RealName = ((TextBox)sender).Text;
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            samuraiListBox.Items.Refresh();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _data.SaveChanges();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_data.ChangesNeedToBeSaved())
            {
                e.Cancel = PromptSaveChanges();
            }
        }

        private bool PromptSaveChanges()
        {
            string messageBoxText = "There are unsaved changes. Do you want to save them?";
            var result = MessageBox.Show(messageBoxText,"Samurai", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            bool cancelOperation = false;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    _data.SaveChanges();
                    break;
                case MessageBoxResult.No:
                    break;
                 case MessageBoxResult.Cancel:
                    cancelOperation = true;
                    break;
            }
            return cancelOperation;
        }
        
   
    }
}