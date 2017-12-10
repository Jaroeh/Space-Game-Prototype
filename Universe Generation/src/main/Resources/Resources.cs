using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Space_Explorer.main.Resources
{
    public class Resource
    {
        public readonly byte Id;
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
        
        public Resource(byte id, string name, string description, string state, float density, float meltingPoint, float boilingPoint, float abundance, byte generationChance, byte quality)
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
    }

    public static class ResourceOperations
    {
        public static SortedDictionary<byte, Resource> LoadResources(string resourceFileLocation)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(url: resourceFileLocation);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader: xmlTextReader);

            SortedDictionary<byte, Resource> resources = new SortedDictionary<byte, Resource>();
            XmlNode resourcesXmlNode = null;

            foreach (XmlNode childNode in xmlDocument.ChildNodes)
            {
                if (childNode.Name == @"Resources") resourcesXmlNode = childNode;
            }

            if (resourcesXmlNode == null) throw new FileLoadException(message: "Resources Node not found in Resources.xml file");

            try
            {
                foreach (XmlElement resourceXmlElement in resourcesXmlNode)
                {
                    byte id = byte.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "Id").Value);
                    string name = resourceXmlElement.Attributes.GetNamedItem(name: "Name").Value;
                    string description = resourceXmlElement.Attributes.GetNamedItem(name: "Description").Value;
                    string state = resourceXmlElement.Attributes.GetNamedItem(name: "State").Value;
                    float density = float.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "Density").Value);
                    float meltingPoint = float.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "MeltingPoint").Value);
                    float boilingPoint = float.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "BoilingPoint").Value);
                    float abundance = float.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "Abundance").Value);
                    byte generationChance = byte.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "GenerationChance").Value);
                    byte quality = byte.Parse(s: resourceXmlElement.Attributes.GetNamedItem(name: "Quality").Value);
                    
                    resources.Add(key: id, value: new Resource(id: id, name: name, description: description, state: state, density: density, meltingPoint: meltingPoint, boilingPoint: boilingPoint, abundance: abundance, generationChance: generationChance, quality: quality));
                }
            }
            catch (Exception e)
            {
                if (e.GetBaseException().GetType() == typeof(FormatException))
                {
                    MessageBox.Show(text: @"Error occured while attempting to load resources. A resource value could not be parsed to it's set type.");
                }
                throw;
            }

            return resources;
        }
    }
}
