using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProfessionalEx2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;
        DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Conect_Click(object sender, RoutedEventArgs e)
        {
            ConectToServerAsync();
            Timer();
            
        }

        private async void ConectToServerAsync()
        {
            await Task.Delay(2000);
            Task task = new Task(ConectToServer);
            task.Start();
        }

        private void ConectToServer()
        {
            string conectionStringSql = @"Data Source=.;Initial Catalog=mobileappdb;Integrated Security=True";

            using (sqlConnection = new SqlConnection(conectionStringSql))
            {

                string text1 = "«Підключено до бази даних»";
                text.Dispatcher.Invoke(() => { text.Text = text1; });
            }
        }

        private void DisconectToServer(SqlConnection sqlConnection)
        {
            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Close();
                    text.Text = "Disconect to database";
                }
                catch (Exception ex)
                {
                    text.Text= "Slq conection didn`t created. First create conection. Exception: "+ex.Message;
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            text.Dispatcher.Invoke(() => { text.Text += "\nДані отримані"; });
        }

        public void Timer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,3);
            dispatcherTimer.Start();
        }

        private void Disconect_Click(object sender, RoutedEventArgs e)
        {
            DisconectToServer(sqlConnection);
            dispatcherTimer.Stop();
        }
    }
}
