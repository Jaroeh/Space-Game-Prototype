using System.Collections.Generic;
using System.Xml;
// ReSharper disable SpecifyACultureInStringConversionExplicitly
// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 169


namespace Resources_List_XML_Converter
{
    internal class Program
    {
        public static void Main()
        {
            List<Resource> resources = Resource.InitializeResourceArray();

            XmlDocument resourcesXml = new XmlDocument();
            ConvertResourcesToXml(resourcesXml, resources);
            
            resourcesXml.Save("Resources.xml");
        }



        private static void ConvertResourcesToXml(XmlDocument resourcesXml, List<Resource> resources)
        {
            XmlNode docNode = resourcesXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            resourcesXml.AppendChild(docNode);
            XmlNode resourcesNode = resourcesXml.CreateElement("Resources");
            resourcesXml.AppendChild(resourcesNode);

            foreach (Resource resource in resources)
            {
                XmlNode resourceNode = resourcesXml.CreateElement(resource.Name);

                XmlAttribute id = resourcesXml.CreateAttribute("Id");
                id.Value = resource.Id.ToString();
                resourceNode.Attributes?.Append(id);

                XmlAttribute name = resourcesXml.CreateAttribute("Name");
                name.Value = resource.Name;
                resourceNode.Attributes?.Append(name);

                XmlAttribute description = resourcesXml.CreateAttribute("Description");
                description.Value = resource.Description;
                resourceNode.Attributes?.Append(description);

                XmlAttribute state = resourcesXml.CreateAttribute("State");
                state.Value = resource.State;
                resourceNode.Attributes?.Append(state);

                XmlAttribute density = resourcesXml.CreateAttribute("Density");
                density.Value = resource.Density.ToString();
                resourceNode.Attributes?.Append(density);

                XmlAttribute meltingPoint = resourcesXml.CreateAttribute("MeltingPoint");
                meltingPoint.Value = resource.MeltingPoint.ToString();
                resourceNode.Attributes?.Append(meltingPoint);

                XmlAttribute boilingPoint = resourcesXml.CreateAttribute("BoilingPoint");
                boilingPoint.Value = resource.BoilingPoint.ToString();
                resourceNode.Attributes?.Append(boilingPoint);

                XmlAttribute abundance = resourcesXml.CreateAttribute("Abundance");
                abundance.Value = resource.Abundance.ToString();
                resourceNode.Attributes?.Append(abundance);

                XmlAttribute generationChance = resourcesXml.CreateAttribute("GenerationChance");
                generationChance.Value = resource.GenerationChance.ToString();
                resourceNode.Attributes?.Append(generationChance);

                XmlAttribute quality = resourcesXml.CreateAttribute("Quality");
                quality.Value = resource.Quality.ToString();
                resourceNode.Attributes?.Append(quality);

                resourcesNode.AppendChild(resourceNode);
            }
        }
    }

    internal class Resource
    {
        public readonly ushort Id;
        public readonly string Name;
        public readonly string Description;
        public readonly string State;
        public readonly float Density;
        public readonly float MeltingPoint;
        public readonly float BoilingPoint;
        public readonly float Abundance;
        public readonly byte GenerationChance;
        public readonly byte Quality;
        public byte PercentTotalVolume;
        public double ResourceTotalVolume;

        private Resource(ushort id, string name, string description, string state, float density, float meltingPoint, float boilingPoint, float abundance, byte generationChance, byte quality)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            Quality = quality;
            Density = density;
            MeltingPoint = meltingPoint;
            BoilingPoint = boilingPoint;
            Abundance = abundance;
            GenerationChance = generationChance;
            Quality = quality;
        }

        public static List<Resource> InitializeResourceArray() //Id, Name, Description, Category, Density, MeltingPoint, BoilingPoint, Abundance, GenerationChance
        {

            List<Resource> resourceList = new List<Resource>
            {
                new Resource(0, "Plutonium", "Description", "Solid", 19.84F, 912.5F, 3501F, 0.00000000000000003F, 100, 128),
                new Resource(1, "Gold", "Description", "Solid", 19.282F, 1337.33F, 3129F, 0.000000004F, 100, 128),
                new Resource(2, "Neon", "Used for lasers", "Gas", 0.0008999F, 24.56F, 27.07F, 0.000000005F, 100, 128),
                new Resource(3, "Helium", "Description", "Gas", 0.0001785F, 0.956F, 4.22F, 0.000000008F, 100, 128),
                new Resource(4, "Iodine", "Description", "Solid", 4.93F, 386.85F, 457.4F, 0.00000045F, 100, 128),
                new Resource(5, "Tungstun", "Description", "Solid", 19.25F, 3695F, 5828F, 0.0000013F, 100, 128),
                new Resource(6, "Tin", "Description", "Solid", 7.287F, 505.08F, 2875F, 0.0000023F, 100, 128),
                new Resource(7, "Uranium", "Description", "Solid", 18.95F, 1405.3F, 4404F, 0.0000027F, 100, 128),
                new Resource(8, "Lead", "Description", "Solid", 11.342F, 600.61F, 2022F, 0.000014F, 100, 128),
                new Resource(9, "Nitrogen", "Description", "Gas", 0.0012506F, 63.15F, 77.36F, 0.000019F, 100, 128),
                new Resource(10, "Lithium", "Batteries", "Solid", 0.534F, 453.69F, 1560F, 0.00002F, 100, 128),
                new Resource(11, "Cobalt", "Batteries, SuperAlloys", "Solid", 8.86F, 1768F, 3200F, 0.000025F, 100, 128),
                new Resource(12, "Copper", "Electronics, Power generation/Transmission", "Solid", 8.96F, 1357.77F, 2835F, 0.00006F, 100, 128),
                new Resource(13, "Nickel", "Description", "Solid", 8.912F, 1728F, 3186F, 0.000084F, 100, 128),
                new Resource(14, "Carbon", "Carbon Fiber, ", "Solid", 2.267F, 3800F, 4300F, 0.0002F, 100, 128),
                new Resource(15, "Hydrogen", "Description", "Gas", 0.00008988F, 14.01F, 20.28F, 0.0014F, 100, 128),
                new Resource(16, "Titanium", "Description", "Solid", 4.54F, 1941F, 3560F, 0.00565F, 100, 128),
                new Resource(17, "Magnesium", "Explosives", "Solid", 1.738F, 923F, 1363F, 0.0233F, 100, 128),
                new Resource(18, "Iron", "Description", "Solid", 7.874F, 1811F, 3134F, 0.0563F, 100, 128),
                new Resource(19, "Aluminum", "Building Material, Vehicle/Airframes", "Solid", 2.698F, 933.47F, 2792F, 0.0823F, 100, 128),
                new Resource(20, "Silicon", "Computing", "Solid", 2.3296F, 1687F, 3538F, 0.282F, 100, 128),
                new Resource(21, "Oxygen", "Breathing", "Gas", 0.001429F, 54.36F, 90.2F, 0.361F, 100, 128),
                new Resource(22, "Water", "Description", "Liquid", 0.58F, 273.15F, 373F, 0.187621233F, 100, 128)
            };

            return resourceList;
        }
    }
}


