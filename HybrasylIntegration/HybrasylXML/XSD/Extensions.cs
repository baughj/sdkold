using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.XSD
{
    public partial class ItemType
    {
        [System.Xml.Serialization.XmlIgnore]
        public bool IsVariant { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public ItemType ParentItem { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool Stackable
        {
            get { return Properties.Stackable != null; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public VariantType CurrentVariant { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<int, ItemType> Variants { get; set; }

        public int Id
        {
            get
            {
                unchecked
                {
                    if (Properties.Appearance.DisplayspriteSpecified && Properties.Appearance.Displaysprite > 0)
                    {
                        return 31 * Name.GetHashCode() * (Properties.Restrictions.Gender.GetHashCode() + 1) *
                        Properties.Appearance.Displaysprite.GetHashCode();
                    }
                    return 31 * Name.GetHashCode() * (Properties.Restrictions.Gender.GetHashCode() + 1);
                }
            }
        }
    }
    public partial class VariantType
    {
        public void ResolveVariant(ItemType itemType)
        {
            //Logger.DebugFormat("Logging some variant stuff.");
            if (Properties != null)
                Console.WriteLine("hi");
            foreach (var variantObject in Properties.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine("variantobject contains {0}", variantObject);
            }
        }
    }

    public partial class Castable
    {
        public int Id
        {
            get
            {
                unchecked
                {
                    if(Class != null)
                    {
                        return 31 * Name.GetHashCode() * (Class.GetHashCode() + 1);
                    }
                    return 31 * (Name.GetHashCode() + 1);
                }
            }
        }
    }
}

