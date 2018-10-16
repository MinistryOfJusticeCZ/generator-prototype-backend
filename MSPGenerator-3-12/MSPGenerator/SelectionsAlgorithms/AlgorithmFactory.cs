using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;




namespace SelectionsAlgorithms
{
   
    /// <summary>
    /// Factory třída pro automatické vytváření a poskytování instancí algoritmů přidělování.
    /// </summary>
    /// <remarks>
    /// Pro použití na více vláknech musí každé vlákno vytvářet vlastní instanci této tovární metody.
    /// </remarks>
    /// <remarks>
    /// Tato třída slouží pro oddělení přístupu k algoritmům přidělování od služeb API.
    /// Automaticky prochází assembly a vyhledává implementované algoritmy , tj. všechny třídy označené atributem <see cref="AlgorithmDescription"/>, které zároveň instancuje.
    /// V principu nemusí být použita, ale zjednodušuje a zflexibilňuje použití algoritmů přidělování.
    /// </remarks>
    /// <example>
    /// AlgorithmFactory factory = new AlgorithmFactory();//vytvor si factory
    /// int id=0;//identifikator algoritmu
    /// var algoritmus=factory.GetSelectionAlgorithm(id);//vem algoritmus
    /// </example>
    public class AlgorithmFactory
    {
        /// <summary>
        /// Vnitřní tabulka vazeb mezi algoritmem a jeho popisem.
        /// Klíčem do tabulky je identifikátor algorimu a  hodnotou je dvojice algoritmus a jeho popis.
        /// </summary>
        Dictionary<int,Tuple<SelectionAlgorithm,AlgorithmDescription>> instantionlist = null;


        /// <summary>
        /// Konstruktor umožňující vytvoření továrny.
        /// </summary>
        /// <remarks>
        /// Při volání konstruktoru dochází k inicializaci vnitřních struktur a  vytvoření instancí implementovaných algoritmů <see cref="CreateInstances()"/>.  
        /// </remarks>
        public AlgorithmFactory()
        {
            CreateInstances();
        }

        /// <summary>
        /// Privátní metoda pro vytvoření interní tabulky pro svázání informací algoritmus s jeho popisem.
        /// </summary>
        /// <remarks>
        /// Hodnoty z této tabulky jsou poskytovány dalším metodám třídy. 
        /// Je zde využito reflexe, metoda prohledá aktuální assembly a použije všechny třídy s atributem <see cref="AlgorithmDescription"/>, které jsou povoleny.
        /// Od těchto tříd jsou vytvořeny instance.  Je prohledáváno pouze aktuální assembly, všechny algorimy přidělování musí být umístěny v tomto assembly.
        /// </remarks>
        private void CreateInstances() 
        {
            if(instantionlist==null)//delej pouze pokud jeste nebylo inicializovano
            {
                instantionlist = new  Dictionary<int, Tuple<SelectionAlgorithm,AlgorithmDescription>>();
                foreach(Type t in typeof(AlgorithmFactory).Assembly.GetTypes())//prohledej assembly
                {
                    AlgorithmDescription attribute=t.GetCustomAttribute<AlgorithmDescription>();//nalezni dany atribut
                    if((attribute!=null) && attribute.Enabled )//pokud zde opravdu je a je povoleny
                    {
                        var c=(SelectionAlgorithm)Activator.CreateInstance(t);//vytvor instanci
                        instantionlist.Add(attribute.ID,new Tuple<SelectionAlgorithm,AlgorithmDescription>(c,attribute));//pridej do tabulky
                    }
                }
            }
        }
        /// <summary>
        /// Iterátor přes implementované a povolené algortimy.
        /// </summary>
        public IEnumerable<SelectionAlgorithm> Algorithms //vlastnost 
        {
            get
            {
                CreateInstances();//vytvor instance, pokud existuji, metoda nic nevytvari
                return  from x in instantionlist.Values select x.Item1;//vrat iterator
            }
        }
        /// <summary>
        /// Iterátor přes popisy implementovaných a povolených aloritmů.
        /// </summary>
        public  IEnumerable<AlgorithmDescription> Descriptions
        {
          get
          {
                CreateInstances();//vytvor instance, pokud existuji, nic se nevytvari
                return from x in instantionlist.Values select x.Item2;//vrat iterator
          }
            
        }
        /// <summary>
        /// Vrací vybraný algoritmus přidělování na základě vstupního indexu, což je identifikátor algoritmu <see cref="AlgorithmDescription"/>.
        /// </summary>
        /// <param name="index">Index (identifikátor) algoritmu</param>
        /// <returns>SelectionAlgorithm</returns>
        public SelectionAlgorithm GetSelectionAlgorithm(int index)
        {
            CreateInstances();//vytvor instance, pokud existuji, nic se nevytvari
            Debug.WriteLine("Vracim index"+ index+ "   " + instantionlist[index].Item2.Name+" ");
            if (!instantionlist.ContainsKey(index))//kontroluj spravny index
                throw new KeyNotFoundException("Neznamy index algoritmu: " + index);
            return instantionlist[index].Item1; //vrat instanci daneho algoritmu
        }
    }
}
