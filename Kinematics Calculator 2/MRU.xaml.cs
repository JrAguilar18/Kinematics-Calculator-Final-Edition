using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kinematics_Calculator_2
{
    /// <summary>
    /// Interaction logic for MRU.xaml
    /// </summary>
    public partial class MRU : Window
    {
        public MRU()
        {
            InitializeComponent();
            FunctionSelector.SelectedIndex = 0; // Selección predeterminada
            UpdateMissingParametersText();
        }

        private void FunctionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMissingParametersText();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(VelocityInput.Text, out double v) &&
                double.TryParse(TimeInput.Text, out double t))
            {
                string selectedFunction = ((ComboBoxItem)FunctionSelector.SelectedItem).Content.ToString();

                var plt = PlotArea.Plot;
                plt.Clear();

                if (selectedFunction == "d(t) = v·t")
                {
                    double[] time = GenerateRange(0, t, 100);
                    double[] distance = new double[time.Length];
                    for (int i = 0; i < time.Length; i++)
                        distance[i] = v * time[i];

                    var scatter = plt.Add.Scatter(time, distance);
                    scatter.LineWidth = 3;
                    scatter.Color = ScottPlot.Colors.MediumBlue;

                    plt.Title("Gráfico de d(t) = v·t", size: 20);
                    plt.XLabel("Tiempo (s)", size: 16);
                    plt.YLabel("Distancia (m)", size: 16);
                }
                else if (selectedFunction == "v(t) = v")
                {
                    double[] time = GenerateRange(0, t, 100);
                    double[] velocity = new double[time.Length];
                    for (int i = 0; i < time.Length; i++)
                        velocity[i] = v;

                    var scatter = plt.Add.Scatter(time, velocity);
                    scatter.LineWidth = 3;
                    scatter.Color = ScottPlot.Colors.DarkGreen;

                    plt.Title("Gráfico de v(t) = v", size: 20);
                    plt.XLabel("Tiempo (s)", size: 16);
                    plt.YLabel("Velocidad (m/s)", size: 16);
                }

                PlotArea.Refresh();
                MissingParametersText.Text = "Todos los parámetros están completos.";
            }
            else
            {
                MessageBox.Show("Por favor, ingresa valores válidos para velocidad y tiempo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PlotArea.Plot.Clear();
            PlotArea.Refresh();
            VelocityInput.Clear();
            TimeInput.Clear();
            UpdateMissingParametersText();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Volviendo al menú principal...", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ingresa velocidad (m/s) y tiempo (s), elige una función, y presiona 'Calcular'.\n\nPuedes limpiar la gráfica o volver al menú con los botones.", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateMissingParametersText()
        {
            int missing = 0;
            if (string.IsNullOrWhiteSpace(VelocityInput.Text)) missing++;
            if (string.IsNullOrWhiteSpace(TimeInput.Text)) missing++;

            MissingParametersText.Text = missing == 0
                ? "Todos los parámetros están completos."
                : $"Faltan {missing} parámetro(s) por completar.";
        }

        private double[] GenerateRange(double start, double end, int points)
        {
            double[] range = new double[points];
            double step = (end - start) / (points - 1);
            for (int i = 0; i < points; i++)
                range[i] = start + i * step;

            return range;
        }
    }
}
