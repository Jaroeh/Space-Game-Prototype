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
using UniverseGeneration;

namespace Game_Design_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UniverseGeneration.Resources.InitializeResourceArray();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WorldGeneration worldGeneration = new WorldGeneration();
            Galaxy galaxy = worldGeneration.GenerateGalaxy();
            TreeViewItem starSystemCount = new TreeViewItem();
            TreeViewItem planetCount = new TreeViewItem();
            TreeViewItem moonCount = new TreeViewItem();
            TreeViewItem moonACount = new TreeViewItem();
            TreeViewItem moonBCount = new TreeViewItem();
            TreeViewItem moonCCount = new TreeViewItem();

            treeView.Items.Add(starSystemCount);
            treeView.Items.Add(planetCount);
            treeView.Items.Add(moonCount);
            moonCount.Items.Add(moonACount);
            moonCount.Items.Add(moonBCount);
            moonCount.Items.Add(moonCCount);

            foreach (Sector sector in galaxy.Sectors)
            {
                TreeViewItem sectorItem = new TreeViewItem {Header = string.Concat("Sector: ",sector.Id)};
                sectorItem.Items.Add(string.Concat("System Count: ", sector.StarSystemCount));
                sectorItem.Items.Add(string.Concat("Distance from core: ", sector.DistanceFromCore));
                treeView.Items.Add(sectorItem);
                foreach (StarSystem starSystem in sector.SectorGrid)
                {
                    if (starSystem != null)
                    {
                        TreeViewItem starSystemItem = new TreeViewItem {Header = string.Concat("System: ", starSystem.Id) };
                        TreeViewItem starItem = new TreeViewItem { Header = string.Concat("Star: ",starSystem.Star.Id + 1) };
                        sectorItem.Items.Add(starSystemItem);
                        starSystemItem.Items.Add(starItem);
                        
                        starItem.Items.Add(string.Concat("Luminosity Class: ",starSystem.Star.LuminosityClass));
                        starItem.Items.Add(string.Concat("Spectral Class: ", starSystem.Star.SpectralClass));
                        starItem.Items.Add(string.Concat("Luminosity: ", starSystem.Star.Luminosity, " Solar Luminosities"));
                        starItem.Items.Add(string.Concat("Magnitude: ", starSystem.Star.Magnitude));
                        starItem.Items.Add(string.Concat("Temperature: ", starSystem.Star.Temperature, " Kelvins"));
                        starItem.Items.Add(string.Concat("Radius: ", starSystem.Star.Radius, " Solar Radii"));
                        starItem.Items.Add(string.Concat("Mass: ", starSystem.Star.Mass, " Solar Masses"));
                        starItem.Items.Add(string.Concat("Inner Hab Zone Radius: ", starSystem.Star.HabitableZoneInnerRadius, " AU"));
                        starItem.Items.Add(string.Concat("Outer Hab Zone Radius: ", starSystem.Star.HabitableZoneOuterRadius, " AU"));

                        foreach (SolarBodies.Planet planet in starSystem.Planets)
                        {
                            TreeViewItem planetsItem = new TreeViewItem {Header = string.Concat("Planet: ", planet.Id)};
                            starSystemItem.Items.Add(planetsItem);

                            planetsItem.Items.Add(string.Concat("AU from Star: ", planet.AuFromStar, " AU"));
                            planetsItem.Items.Add(string.Concat("Radius: ", planet.Radius, " Km"));
                            planetsItem.Items.Add(string.Concat("Volume: ", planet.Volume, " Km^3"));
                            planetsItem.Items.Add(string.Concat("Mass: ", planet.Mass, " Kg"));
                            planetsItem.Items.Add(string.Concat("Gravity: ", planet.Gravity, " Meters/second"));
                            planetsItem.Items.Add(string.Concat("AxialTilt: ", planet.AxialTilt, " Degrees"));
                            planetsItem.Items.Add(string.Concat("Average Density: ", planet.AverageDensity, " grams/meter^3"));
                            planetsItem.Items.Add(string.Concat("Atmospheric Density: ", planet.AtmosphericDensity, " Random number between 1 and 100"));
                            planetsItem.Items.Add(string.Concat("Solar Insolation: ", planet.SolarInsolation, " W/m^2"));
                            planetsItem.Items.Add(string.Concat("Base Temperature: ", planet.BaseTemperature, " Kelvins"));
                            planetsItem.Items.Add(string.Concat("Surface Temperature: ", planet.AverageSurfaceTemperature, " Kelvins"));
                            planetsItem.Items.Add(string.Concat("Breathable Atmosphere: ", planet.BreathableAtmosphere));
                            planetsItem.Items.Add(string.Concat("Fauna BioDiversity: ", planet.FaunaBioDiversity));
                            planetsItem.Items.Add(string.Concat("Flora BioDiversity: ", planet.FloraBioDiversity));
                            planetsItem.Items.Add(string.Concat("Geologic Activity Level: ", planet.GeologicActivityLevel));
                            planetsItem.Items.Add(string.Concat("Landmass Ratio: ", planet.LandmassRatio));
                            planetsItem.Items.Add(string.Concat("Rotational Perioud: ", planet.RotationalPeriod));

                            TreeViewItem childMoons = new TreeViewItem {Header = $"Moon Count: {planet.ChildMoons.Count}"};
                            planetsItem.Items.Add(childMoons);

                            foreach (SolarBodies.Moon moon in planet.ChildMoons)
                            {
                                TreeViewItem moonItem = new TreeViewItem {Header = $"Moon: {moon.Id}"};
                                childMoons.Items.Add(moonItem);
                                
                                moonItem.Items.Add(string.Concat("ID: ", moon.Id));
                                moonItem.Items.Add(string.Concat("Km from Parent: ", moon.KilometersFromParent));
                                moonItem.Items.Add(string.Concat("Radius: ", moon.Radius, " Km"));
                                moonItem.Items.Add(string.Concat("Volume: ", moon.Volume, " Km^3"));
                                moonItem.Items.Add(string.Concat("Mass: ", moon.Mass, " Kg"));
                                moonItem.Items.Add(string.Concat("Gravity: ", moon.Gravity, " Meters/second"));
                                moonItem.Items.Add(string.Concat("AxialTilt: ", moon.AxialTilt, " Degrees"));
                                moonItem.Items.Add(string.Concat("Average Density: ", moon.AverageDensity, " grams/meter^3"));
                                moonItem.Items.Add(string.Concat("Atmospheric Density: ", moon.AtmosphericDensity, " Random number between 1 and 100"));
                                moonItem.Items.Add(string.Concat("Surface Temperature: ", moon.AverageSurfaceTemperature, " Kelvins"));
                                moonItem.Items.Add(string.Concat("Breathable Atmosphere: ", moon.BreathableAtmosphere));
                                moonItem.Items.Add(string.Concat("Fauna BioDiversity: ", moon.FaunaBioDiversity));
                                moonItem.Items.Add(string.Concat("Flora BioDiversity: ", moon.FloraBioDiversity));
                                moonItem.Items.Add(string.Concat("Geologic Activity Level: ", moon.GeologicActivityLevel));
                                moonItem.Items.Add(string.Concat("Landmass Ratio: ", moon.LandmassRatio));
                                moonItem.Items.Add(string.Concat("Rotational Perioud: ", moon.RotationalPeriod));
                                
                            }

                            TreeViewItem childRings = new TreeViewItem { Header = $"Ring Count: {planet.ChildRings.Count}" };
                            planetsItem.Items.Add(childRings);

                            foreach (SolarBodies.Ring childRing in planet.ChildRings)
                            {
                                TreeViewItem ringItem = new TreeViewItem {Header = $"Ring: {childRing.Id}"};
                                planetsItem.Items.Add(ringItem);
                            }
                        }
                    }
                }
            }
            starSystemCount.Header = string.Concat("Star System Count: ", worldGeneration._starSystemCount);
            planetCount.Header = string.Concat("Planet Count: ", worldGeneration._planetCount);
            moonCount.Header = string.Concat("Moon Count: ", worldGeneration._moonCount);
            moonACount.Header = string.Concat("Moon A Count: ", worldGeneration._moonACount);
            moonBCount.Header = string.Concat("Moon B Count: ", worldGeneration._moonBCount);
            moonCCount.Header = string.Concat("Moon C Count: ", worldGeneration._moonCCount);
        }
    }
}
