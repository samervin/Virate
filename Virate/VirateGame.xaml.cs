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
using VirateEngine;

namespace Virate
{
    public partial class VirateGame : Window
    {
        private World world;
        private Virus virus;

        private Country currentCountry;

        public VirateGame()
        {
            InitializeComponent();
            virus = new Virus("testing!");
            world = new World(virus);
            loadComboBox();
            updateLabels();
        }

        private void updateLabels()
        {
            labelVirusDNA.Content = virus.DNA;
            labelSicknessLevel.Content = String.Format("{0:0.00}", virus.getSicknessLevel());
            labelInfectiousness.Content = String.Format("{0:0.00}", virus.getInfectivityLevel());
            labelTreatmentResistance.Content = String.Format("{0:0.00}", virus.getTreatmentResistance());
            labelEvolutionRate.Content = virus.getEvolutionRate() + " days";
            labelCurrentDay.Content = world.Day;

            if (currentCountry != null)
            {
                labelHealthyPopulation.Content = currentCountry.HealthyPopulation;
                labelSickPopulation.Content = currentCountry.SickPopulation;
                labelBirthRate.Content = String.Format("{0:#.#########}", currentCountry.BirthRate);
                labelDeathRate.Content = String.Format("{0:#.#########}", currentCountry.DeathRate);
                labelGDP.Content = currentCountry.GDP;
            }

            if (!virus.canEvolve(world.Day))
            {
                buttonSickness.Visibility = Visibility.Hidden;
                buttonInfectivity.Visibility = Visibility.Hidden;
                buttonEvolutionRate.Visibility = Visibility.Hidden;
                buttonTreatmentResistance.Visibility = Visibility.Hidden;
                buttonAdvanceDay.Visibility = Visibility.Visible;
                buttonAdvanceYears.Visibility = Visibility.Visible;
            }
            else
            {
                buttonSickness.Visibility = Visibility.Visible;
                buttonInfectivity.Visibility = Visibility.Visible;
                buttonEvolutionRate.Visibility = Visibility.Visible;
                buttonTreatmentResistance.Visibility = Visibility.Visible;
                buttonAdvanceDay.Visibility = Visibility.Hidden;
                buttonAdvanceYears.Visibility = Visibility.Hidden;
            }

            this.UpdateLayout();
        }        

        private void loadComboBox()
        {
            foreach (Country c in world.countries)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = c.Name;
                comboCountries.Items.Add(item);
            }
        }

        private void buttonAdvanceDay_Click(object sender, RoutedEventArgs e)
        {
            world.advanceDay();
            updateLabels();
        }

        private void comboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem newCountry = (ComboBoxItem)comboCountries.SelectedItem;
            currentCountry = world.countries.Find(c => c.Name == newCountry.Content.ToString());
            updateLabels();
        }

        private void buttonSickness_Click(object sender, RoutedEventArgs e)
        {
            virus.updateDNAWithBase('A');
            world.advanceDay();
            updateLabels();
        }

        private void buttonInfectivity_Click(object sender, RoutedEventArgs e)
        {
            virus.updateDNAWithBase('T');
            world.advanceDay();
            updateLabels();
        }

        private void buttonEvolutionRate_Click(object sender, RoutedEventArgs e)
        {
            virus.updateDNAWithBase('G');
            world.advanceDay();
            updateLabels();
        }

        private void buttonTreatmentResistance_Click(object sender, RoutedEventArgs e)
        {
            virus.updateDNAWithBase('C');
            world.advanceDay();
            updateLabels();
        }

        private void buttonAdvanceYears_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3650; i++)
            {
                world.advanceDay();
            }
            updateLabels();
        }
    }
}
