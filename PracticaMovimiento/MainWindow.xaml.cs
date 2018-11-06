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
using System.Threading;
using System.Diagnostics;

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            //1. establecer instrucciones
            ThreadStart threadStart = new ThreadStart(moverEnemigos);

            Thread threadMoverEnemigos = new Thread(threadStart);

            threadMoverEnemigos.Start();
        }

        void moverEnemigos()
        {
            while (true)
            {
                Dispatcher.Invoke(
                () =>
                {
                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime = tiempoActual - tiempoAnterior;
                    double leftCarroActual = Canvas.GetLeft(imgCarro);
                    Canvas.SetLeft(imgCarro, leftCarroActual - (90 * deltaTime.TotalSeconds));
                    if(Canvas.GetLeft(imgCarro) <= - 100)
                    {
                        Canvas.SetLeft(imgCarro, 800);
                    }
                    tiempoAnterior = tiempoActual;
                }
                );
            }
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                double topFatherActual = Canvas.GetTop(imgFather);
                Canvas.SetTop(imgFather, topFatherActual - 15);
            }
            if (e.Key == Key.Left)
            {
                double leftFatherActual = Canvas.GetLeft(imgFather);
                Canvas.SetLeft(imgFather, leftFatherActual - 15);
            }
        }
    }
}
