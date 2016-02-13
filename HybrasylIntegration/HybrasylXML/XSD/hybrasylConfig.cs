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
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("hybrasylconfig", Namespace = "http://www.hybrasyl.com/XML/Config", IsNullable = false)]
    public partial class HybrasylConfig
    {
        [System.Xml.Serialization.XmlElementAttribute("logging")]
        public LogConfig Logging { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("datastore")]
        public DataStore Datastore { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("network")]
        public Network Network { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("access")]
        public Access Access { get; set; }

        [System.Xml.Serialization.XmlArray("boards")]
        [System.Xml.Serialization.XmlArrayItemAttribute("board", IsNullable = false, ElementName = "board")]
        public List<GlobalBoard> Boards { get; set; }

        public HybrasylConfig()
        {
            this.Boards = new List<GlobalBoard>();
            this.Access = new Access();
            this.Network = new Network();
            this.Datastore = new DataStore();
            this.Logging = new LogConfig();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("LogConfig")]
    public partial class LogConfig
    {
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "type")]
        [System.ComponentModel.DefaultValueAttribute("file")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "target")]
        [System.ComponentModel.DefaultValueAttribute("hybrasyl.log")]
        public string Target { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "level")]
        [System.ComponentModel.DefaultValueAttribute(LogLevel.info)]
        public LogLevel Level { get; set; }

        public LogConfig()
        {
            this.Type = "file";
            this.Target = "hybrasyl.log";
            this.Level = LogLevel.info;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("LogLevel")]
    public enum LogLevel
    {
        /// <remarks/>
        all = 0,

        /// <remarks/>
        debug = 30000,

        /// <remarks/>
        info = 40000,

        /// <remarks/>
        warn = 60000,

        /// <remarks/>
        error = 70000,

        /// <remarks/>
        fatal = 110000,

        /// <remarks/>
        off = Int32.MaxValue,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("GlobalBoardAccessList")]
    public partial class GlobalBoardAccessList
    {
        [System.Xml.Serialization.XmlElementAttribute("read")]
        public List<string> Read { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("write")]
        public List<string> Write { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("moderate")]
        public List<string> Moderate { get; set; }

        public GlobalBoardAccessList()
        {
            Read = new List<string>();
            Write = new List<string>();
            Moderate = new List<string>();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("GlobalBoard")]
    public partial class GlobalBoard
    {
        [System.Xml.Serialization.XmlElementAttribute("accesslist")]
        public GlobalBoardAccessList Accesslist { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token", AttributeName = "name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "displayname")]
        public string Displayname { get; set; }

        public GlobalBoard()
        {
            this.Accesslist = new GlobalBoardAccessList();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("Access")]
    public partial class Access
    {
        [System.Xml.Serialization.XmlElementAttribute("privileged")]
        public string Privileged { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("NetworkInfo")]
    public partial class NetworkInfo
    {
        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "bindaddress")]
        [System.ComponentModel.DefaultValueAttribute("127.0.0.1")]
        public string Bindaddress { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "port")]
        public ushort Port { get; set; }

        public NetworkInfo()
        {
            this.Bindaddress = "127.0.0.1";
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("Network")]
    public partial class Network
    {
        [System.Xml.Serialization.XmlElementAttribute("lobby")]
        public NetworkInfo Lobby { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("login")]
        public NetworkInfo Login { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("world")]
        public NetworkInfo World { get; set; }

        public Network()
        {
            this.World = new NetworkInfo();
            this.Login = new NetworkInfo();
            this.Lobby = new NetworkInfo();
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.hybrasyl.com/XML/Config")]
    [System.Xml.Serialization.XmlRootAttribute("DataStore")]
    public partial class DataStore
    {
        [System.Xml.Serialization.XmlElementAttribute(DataType = "token", ElementName = "username")]
        public string Username { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "token", ElementName = "password")]
        public string Password { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token", AttributeName = "type")]
        [System.ComponentModel.DefaultValueAttribute("redis")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "token", AttributeName = "host")]
        [System.ComponentModel.DefaultValueAttribute("localhost")]
        public string Host { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "port")]
        [System.ComponentModel.DefaultValueAttribute(typeof(ushort), "6379")]
        public ushort Port { get; set; }

        public DataStore()
        {
            this.Type = "redis";
            this.Host = "localhost";
            this.Port = ((ushort)(6379));
        }
    }
}

#pragma warning restore