using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAME.Shared.Models.Objects
{
    public enum E_Resources
    {
        Alloy_Plate,
        Circuits,
        Control_Module,
        Ferrite,
        Gallium,    
        Morphics,
        Neural_Sensor,
        Neurodes,
        Orokin_Cell,
        Rubedo,
        Salvage,
        Plastids,
        Polymer_Bundle,
        Argon_Crystal,
        Oxium,
        Detonite_Ampule,
        Fieldron_Sample,
        Mutagen_Sample,
        Tellurium
    }

    public class Resource : Object
    {
         
        public Resource(E_Resources resource)
        {

        }

        public string Name
        {
            get { return base.Name; }
        }

        public string Type
        {
            get
            {
                return base.Type;
            }
        }
    }
}
