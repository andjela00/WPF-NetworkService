using CG4_T7_G1_P2.Common;
using System;
using CG4_T7_G1_P2.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using CG4_T7_G1_P2.Model;

namespace CG4_T7_G1_P2.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private NetworkDataViewModel networkDataViewModel = new NetworkDataViewModel();
        private NetworkViewViewModel networkViewViewModel = new NetworkViewViewModel();
        private DataChartViewModel dataChartViewModel = new DataChartViewModel();
        private BindableBase currentViewModel;
        public int monitor = 0;
        private string path;
        private int id;
        private double value;
        private bool file;

        public MainWindowViewModel()
        {
            path = Environment.CurrentDirectory + @"\Log.txt";
            File.WriteAllText(path, String.Empty);
            createListener(); //Povezivanje sa serverskom aplikacijom
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = networkDataViewModel;
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            int c = ViewModel.NetworkViewViewModel.monitor;
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(DataBase.MerniInstrumenti.Count().ToString());
                            stream.Write(data, 0, data.Length);
                            file = false;
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Objekat_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            string[] split = incomming.Split('_', ':');
                            id = Int32.Parse(split[1]);
                            value = Double.Parse(split[2]);
                            if (DataBase.MerniInstrumenti.Count() > 0 && (DataBase.MerniInstrumenti.Count() > id))
                            {
                                DataBase.MerniInstrumenti[id].Value = value;
                                WriteLog(split, DataBase.MerniInstrumenti[id].Id);
                            }
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        private void WriteLog(string[] split, int id)
        {
            if (!file)
            {
                StreamWriter writer;
                File.AppendAllText(@"Log.txt", $"Source: {id}\t|Value: {int.Parse(split[2])}\t|Time: {DateTime.Now}" + Environment.NewLine);
                file = true;
            }
            else
            {
                StreamWriter writer;
                File.AppendAllText(@"Log.txt", $"Source:{id}\t|Value: {int.Parse(split[2])}\t|Time: {DateTime.Now}" + Environment.NewLine);
            }
            file = true;
        }
    

    public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "NetworkData":
                    CurrentViewModel = networkDataViewModel;
                    break;
                case "NetworkView":
                    CurrentViewModel = networkViewViewModel;
                    break;

                case "DataChart":
                    CurrentViewModel = dataChartViewModel;
                    break;
            }
        }
    }
}
