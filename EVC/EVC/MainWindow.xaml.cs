using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace EVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EVCViewModel EVCViewModel;
        string IPPattern;
        string NumericPattern;
        MessageAssembler MessageAssembler;
        EVCServer Server;

        public bool[] StartFlag = { false, false, false };

        public MainWindow()
        {
            InitializeComponent();
            EVCViewModel = (EVCViewModel)Application.Current.TryFindResource("EVCViewModel");
            IPPattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
            NumericPattern = "@0|[1-9][0-9]*";
            MessageAssembler = new MessageAssembler();

        }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        private void Load_SM(object sender, RoutedEventArgs e)
        {
            bool? result = EVCViewModel.SMdlg.ShowDialog();

            if (result == true)
            {
                SMDataBox.Text = System.IO.File.ReadAllText(EVCViewModel.SMdlg.FileName);
                EVCViewModel.SMData = System.IO.File.ReadAllLines(EVCViewModel.SMdlg.FileName);
                EVCViewModel.SMDataAssembled = MessageAssembler.Assemble(EVCViewModel.SMData, true);

                SMDataByteBox.Text = EVCViewModel.PrintAssembledData(true);
                StartFlag[0] = true;

                if (StartFlag[0] == true && StartFlag[1] == true && StartFlag[2] == true)
                    StartButton.IsEnabled = true;
            }
        }

        private void Load_PA(object sender, RoutedEventArgs e)
        {
            bool? result = EVCViewModel.PAdlg.ShowDialog();

            if (result == true)
            {
                PADataBox.Text = System.IO.File.ReadAllText(EVCViewModel.PAdlg.FileName);
                EVCViewModel.PAData = System.IO.File.ReadAllLines(EVCViewModel.PAdlg.FileName);
                EVCViewModel.PADataAssembled = MessageAssembler.Assemble(EVCViewModel.PAData, false);

                PADataByteBox.Text = EVCViewModel.PrintAssembledData(false);
                StartFlag[1] = true;

                if (StartFlag[0] == true && StartFlag[1] == true && StartFlag[2] == true)
                    StartButton.IsEnabled = true;
            }
        }

        private void Set_Connection(object sender, RoutedEventArgs e)
        {
            string IPAdress = IPAdressBox.Text.ToString();
            string PortNumber = PortNumberBox.Text.ToString();
            int PortNumberInt;
            int.TryParse(PortNumber, out PortNumberInt);

            if (Regex.Match(IPAdress, IPPattern).Success && Regex.Match(PortNumber, NumericPattern).Success && PortNumberInt < 65536)
            {
                EVCViewModel.IPAdress = IPAdress;
                EVCViewModel.PortNumber = PortNumberInt;
                StartFlag[2] = true;

                if (StartFlag[0] == true && StartFlag[1] == true && StartFlag[2] == true)
                    StartButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("IP address or port number not valid!", "Value Error");
            }
        }

        private void Start_Scenario(object sender, RoutedEventArgs e)
        {
            AllocConsole();
            Server = new EVCServer(EVCViewModel.PortNumber, EVCViewModel.IPAdress);
            Server.Start(EVCViewModel.SMDataAssembled, EVCViewModel.PADataAssembled);
            FreeConsole();
        }
    }
}
