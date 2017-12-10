using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Space_Explorer.main.CelestialObjects;
using Space_Explorer.main.CelestialObjects.universe;
using Space_Explorer.main.Resources;

namespace Space_Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Globals.ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Globals.Resources = ResourceOperations.LoadResources(resourceFileLocation: Globals.ApplicationDirectory + Globals.ResourceFileLocation);
            Globals.Universe = new Universe(id: 0, name: "Universe1", description: "Placeholder Description");
            UpdateGalaxyCountLabels();
        }

        private void btn_GenerateNewUniverse_Click(object sender, EventArgs e)
        {
            ResetUniverseCounts();
            Globals.Universe = new Universe(id: 0, name: "Universe1", description: "Placeholder Description");
            UpdateGalaxyCountLabels();
        }

        private void UpdateGalaxyCountLabels()
        {
            lbl_GalaxyCount.Text = Universe.GalaxyCount.ToString();
            lbl_SectorCount.Text = Universe.SectorCount.ToString();
            lbl_StarSystemCount.Text = Universe.StarSystemCount.ToString();
            lbl_EmptyStarSystemCount.Text = Universe.EmptySystemCount.ToString();
            lbl_StarCount.Text = Universe.StarCount.ToString();
            lbl_PlanetCount.Text = Universe.PlanetCount.ToString();
            lbl_RingCount.Text = Universe.RingCount.ToString();
            lbl_AsteroidCount.Text = Universe.AsteroidCount.ToString();
            lbl_MoonCount.Text = Universe.MoonCount.ToString();
            lbl_MoonACount.Text = Universe.MoonACount.ToString();
            lbl_MoonBCount.Text = Universe.MoonBCount.ToString();
            lbl_MoonCCount.Text = Universe.MoonCCount.ToString();
        }

        private void ResetUniverseCounts()
        {
            Universe.GalaxyCount = 0;
            Universe.SectorCount = 0;
            Universe.StarSystemCount = 0;
            Universe.EmptySystemCount = 0;
            Universe.StarCount = 0;
            Universe.PlanetCount = 0;
            Universe.RingCount = 0;
            Universe.AsteroidCount = 0;
            Universe.MoonCount = 0;
            Universe.MoonACount = 0;
            Universe.MoonBCount = 0;
            Universe.MoonCCount = 0;
        }
    }

    public static class Globals
    {
        internal static string ApplicationDirectory;
        internal const string ResourceFileLocation = @"/Resources.xml";
        internal static SortedDictionary<byte, Resource> Resources;
        internal static Universe Universe;
    }


}
