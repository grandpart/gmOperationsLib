//using System.Configuration;

//namespace Grandmark
//{
//    public class DataConnectionSection : ConfigurationSection
//    {
//        [ConfigurationProperty("DataConnection", IsDefaultCollection = true)]
//        public DataConnectionCollection DataConnections
//        {
//            get { return (DataConnectionCollection)this["DataConnection"]; }
//            set { this["DataConnection"] = value; }
//        }
//    }

//    [ConfigurationCollection(typeof(DataConnectionElement))]
//    public class DataConnectionCollection : ConfigurationElementCollection
//    {
//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new DataConnectionElement();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((DataConnectionElement)element).Name;
//        }

//        public DataConnectionElement GetConnectionByName(string aName)
//        {
//            return (DataConnectionElement)BaseGet(aName);
//        }
//    }

//    public class DataConnectionElement : ConfigurationElement
//    {
//        [ConfigurationProperty("name", IsKey = true, IsRequired= true)]
//        public string Name
//        {
//            get { return (string)this["name"]; }
//            set { this["name"] = value; }
//        }

//        [ConfigurationProperty("server", IsRequired = true, DefaultValue = "localhost")]    
//        public string Server
//        {
//            get { return (string)this["server"]; }
//            set { this["server"] = value; }
//        }

//        [ConfigurationProperty("base", IsRequired = true, DefaultValue = "zoomout")]    
//        public string Database
//        {
//            get { return (string)this["base"]; }
//            set { this["base"] = value; }
//        }

//        [ConfigurationProperty("logon", IsRequired = true, DefaultValue = "zooman")]    
//        public string UserID
//        {
//            get { return (string)this["logon"]; }
//            set { this["logon"] = value; }
//        }

//        [ConfigurationProperty("password", IsRequired = true, DefaultValue = "1Password!")]    
//        public string Password
//        {
//            get { return (string)this["password"]; }
//            set { this["password"] = value; }
//        }

//        [ConfigurationProperty("pooled", IsRequired = true, DefaultValue = true)]
//        public bool Pooled
//        {
//            get { return (bool)this["pooled"]; }
//            set { this["pooled"] = value; }
//        }
//    }
//}