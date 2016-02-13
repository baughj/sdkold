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

#pragma warning disable

namespace Hybrasyl.XSD
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.hybrasyl.com/XML/Nations", TypeName = "nation")]
    [XmlRootAttribute(Namespace = "http://www.hybrasyl.com/XML/Nations", IsNullable = false, ElementName = "nation")]
    public partial class Nation
    {
        private List<SpawnPoint> _spawnpoints;

        [XmlElementAttribute("name")]
        public string Name { get; set; }

        [XmlElementAttribute("description")]
        public string Description { get; set; }

        [XmlElementAttribute("default")]
        public object Default { get; set; }

        [XmlAttributeAttribute(AttributeName = "flag")]
        public byte Flag { get; set; }

        [XmlIgnore()]
        public bool NameSpecified { get; set; }

        [XmlIgnore()]
        public bool DescriptionSpecified { get; set; }

        [XmlIgnore()]
        public bool DefaultSpecified { get; set; }

        [XmlIgnore()]
        public bool SpawnpointsSpecified { get; set; }

        [XmlIgnore()]
        public bool FlagSpecified { get; set; }

        [XmlArray("spawnpoints")]
        [XmlArrayItemAttribute("spawnpoint", IsNullable = false, ElementName = "spawnpoint")]
        public List<SpawnPoint> Spawnpoints
        {
            get
            {
                if ((_spawnpoints == null))
                {
                    _spawnpoints = new List<SpawnPoint>();
                }
                return _spawnpoints;
            }
            set
            {
                _spawnpoints = value;
            }
        }

        public Nation()
        {
            _spawnpoints = new List<SpawnPoint>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Nations")]
    [XmlRootAttribute("SpawnPoint")]
    public partial class SpawnPoint
    {
        [XmlAttributeAttribute(AttributeName = "mapname")]
        public string Mapname { get; set; }

        [XmlAttributeAttribute(AttributeName = "x")]
        public byte X { get; set; }

        [XmlAttributeAttribute(AttributeName = "y")]
        public byte Y { get; set; }

        [XmlTextAttribute]
        public string Value { get; set; }

        [XmlIgnore()]
        public bool MapnameSpecified { get; set; }

        [XmlIgnore()]
        public bool XSpecified { get; set; }

        [XmlIgnore()]
        public bool YSpecified { get; set; }

        [XmlIgnore()]
        public bool ValueSpecified { get; set; }
    }
}

#pragma warning restore