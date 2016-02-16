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

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public ItemType ResolveVariant(ItemType itemType)
        {
            var variantItem = itemType.Clone();
            _resolve(variantItem, this);
            return variantItem;

        }

        private void _resolve<T1, T2>(T1 item, T2 variant)
        {
            foreach (var property in typeof (T2).GetProperties(BindingFlags.Public))
            {
                foreach (var attr in property.GetCustomAttributes(false))
                {
                    var itemProperty = typeof(T1).GetProperty(property.Name);
                    if (attr is VariantOverride)
                        // Simple override
                        itemProperty.SetValue(item, property.GetValue(variant));
                    if (attr is VariantAttribute)
                    {
                        var variantString = (string) property.GetValue(variant);
                        if (variantString.StartsWith("replace"))
                        {
                            var variants = variantString.Split(':');
                            itemProperty.SetValue(item, variants[1]);
                        }
                        else if (variantString.EndsWith("%"))
                        {
                            var pct = Convert.ToInt32(variantString.TrimEnd('%'));
                            var originalValue = Convert.ToInt32(itemProperty.GetValue(item));
                            itemProperty.SetValue(item, (originalValue * pct / 100));
                        }
                        else
                        {
                            // Just add it
                            var originalValue = Convert.ToInt32(itemProperty.GetValue(item));
                            var variantValue = Convert.ToInt32(property.GetValue(variant));
                            itemProperty.SetValue(item, originalValue + variantValue);
                        }
                    }
                    if (attr is VariantTraverse)
                    {
                        // Navigate sub-properties
                        _resolve(itemProperty, property);

                    }

                }
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
                    return 31 * (Name.GetHashCode() + 1);
                }
            }
        }

        public byte CastableLevel { get; set; }
    }
}