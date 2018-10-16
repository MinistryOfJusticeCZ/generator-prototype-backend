using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
namespace SelectionsAlgorithms
{
    /// <summary>
    /// Atribut pro uložení informací o algoritmech přidělování případů. 
    /// </summary>
    /// <remarks>
    /// Každý algoritmus, který má být systémem používán, musí mít tento atribut a musí být povolený, viz vlastnost <see cref="AlgorithmDescription.Enabled"/>.
    /// </remarks>
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class AlgorithmDescription : Attribute
    {
        /// <summary>
        /// Vlastnost zpřístupňující identifikátor algoritmu.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Vlastnost zpřístupňující jméno algoritmu.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Vlastnost zpřístupňující stručný popis algoritmu. 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Vlastnost zpřístupňující povolení algoritmu. Hodnota <value>True</value> znamená, že algoritmus přidělení bude zpřístupněn.
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Konstruktor pro vytvořeni instance.
        /// </summary>
        /// <param name="name">Jméno algoritmu</param>
        /// <remarks>
        /// Pokud je použit bez zadání identifikátoru, tak je zde hodnota int.MinValue.
        /// Identifikátor algoritmu musí být explicitně uveden.
        /// </remarks>
        public AlgorithmDescription(string name)
            : this(name, false, int.MinValue)
        { }

        /// <summary>
        /// Konstruktor umožňující vytvoření daného popisu algoritmu. Algoritmus označený tímto atributem bude dafaultně nepřístupný.
        /// </summary>
        /// <param name="name">Jméno algoritmu.</param>
        public AlgorithmDescription(string name,int id)
            : this(name, false,id)
        { }


        /// <summary>
        /// Konstruktor umožňující vytvoření daného popisu algoritmu.
        /// </summary>
        /// <param name="name">Jméno algoritmu.</param>
        /// <param name="enabled">Přístupnost algoritmu.</param>
        public AlgorithmDescription(string name, bool enabled,int id)
        {
            ID = id;
            Name = name;
            Enabled = enabled;
        }
        /// <summary>
        /// Převod atributu na řetězec.
        /// </summary>
        /// <returns>Řetězec odpovídající atributu.</returns>
        public override string ToString()
        {
            return string.Format("ID:{0}\tName:{1}\tDescription:{2}\tEnabled:{3}", ID, Name, Description, Enabled);
        }

    }

  

}
