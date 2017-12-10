using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Design_Planner
{
    class UnitConversions
    {
        //Method takes the weight of an object in Petagrams and converts it to a string that
        //shows the weight in kilograms in the format "amount Tri/Quad/Quintillion/etc. kg"
        public string ConvertPetagramsToKilogramDisplayString(decimal numToConvert)
        {
            string formattedString = null;

            int length = numToConvert.ToString().Length;
            int triplets = (length - 1)/3;

            decimal convertedNumber = Math.Round((numToConvert/(decimal) Math.Pow(1000, triplets)), 2);
            switch (triplets)
            {
                case 0:
                    formattedString = $"{numToConvert} Trillion Kg";
                    break;
                case 1:
                    formattedString = $"{convertedNumber} Quadrillion Kg";
                    break;
                case 2:
                    formattedString = $"{convertedNumber} Quintillion Kg";
                    break;
                case 3:
                    formattedString = $"{convertedNumber} Sextillion Kg";
                    break;
                case 4:
                    formattedString = $"{convertedNumber} Septillion Kg";
                    break;
                case 5:
                    formattedString = $"{convertedNumber} Octillion Kg";
                    break;
                case 6:
                    formattedString = $"{convertedNumber} Nonillion Kg";
                    break;
            }

            return formattedString;
        }
    }
}
