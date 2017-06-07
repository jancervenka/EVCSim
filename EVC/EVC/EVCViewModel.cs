using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace EVC
{
    public class EVCViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Microsoft.Win32.OpenFileDialog SMdlg;
        public Microsoft.Win32.OpenFileDialog PAdlg;

        public string IPAdress;
        public int PortNumber;

        public string[] SMData;
        public string[] PAData;

        public List<MessageContainer> SMDataAssembled;
        public List<MessageContainer> PADataAssembled;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public EVCViewModel()
        {

            SMdlg = new Microsoft.Win32.OpenFileDialog();
            PAdlg = new Microsoft.Win32.OpenFileDialog();

            SMdlg.DefaultExt = ".txt";
            SMdlg.Filter = "Text files (*.txt)|*.txt";

            PAdlg.DefaultExt = ".txt";
            PAdlg.Filter = "Text files (*.txt)|*.txt";
        }

        public string PrintAssembledData(bool Flag)
        {
            List<string> DataPrint = new List<string>();

            if (Flag)
            {
                foreach (MessageContainer Container in SMDataAssembled)
                {
                    DataPrint.Add(BitConverter.ToString(Container.Msg.ToArray()));
                    DataPrint.Add("\n");                
                }
            }
            else
            {
                foreach (MessageContainer Container in PADataAssembled)
                {
                    DataPrint.Add(BitConverter.ToString(Container.Msg.ToArray()));
                    DataPrint.Add("\n");
                }
            }
            return string.Join(string.Empty, DataPrint.ToArray());
        }
    }
}
