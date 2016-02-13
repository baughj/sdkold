/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2016 Project Hybrasyl (info@hybrasyl.com)
 *
 */
 
using System.Collections.Generic;

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
            // if (Properties != null)
            //    Console.WriteLine("hi");
            //foreach (var variantObject in Properties.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            //{
            //    Console.WriteLine("variantobject contains {0}", variantObject);
            // }
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
                    return 31 * (Name.GetHashCode() + 1);
                }
            }
        }

        public byte CastableLevel { get; set; }
    }
}