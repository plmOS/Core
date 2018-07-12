/*  
  plmOS Core provides a .NET library for managing PLM (Product Lifecycle Management) data.
  Copyright (C) 2018 Processwall Limited.
  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Affero General Public License as published
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.
  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU Affero General Public License for more details.
  You should have received a copy of the GNU Affero General Public License
  along with this program.  If not, see http://opensource.org/licenses/AGPL-3.0.
 
  Company: Processwall Limited
  Address: The Winnowing House, Mill Lane, Askham Richard, York, YO23 3NW, United Kingdom
  Tel:     +44 113 815 3440
  Email:   support@processwall.com
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace plmOS.Core
{
    public class Manager
    {
        private Dictionary<String, Session> SessionCache;

        public Session Login(String UserID)
        {
            if (!String.IsNullOrEmpty(UserID))
            {
                if (!this.SessionCache.ContainsKey(UserID))
                {
                    this.SessionCache[UserID] = new Session(this, UserID);
                }

                return this.SessionCache[UserID];
            }
            else
            {
                throw new ArgumentException("User ID must be specified");
            }
        }

        private Dictionary<Guid, ItemType> ItemTypeIDCache;
        private Dictionary<String, ItemType> ItemTypeNameCache;

        public void LoadSchema(Stream Schema)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Schema);
            XmlNode schemanode = doc.SelectSingleNode("schema");
            XmlNode itemtypesnode = schemanode.SelectSingleNode("itemtypes");

            foreach (XmlNode itemtypenode in itemtypesnode.SelectNodes("itemtype"))
            {
                ItemType itemtype = new ItemType(this, itemtypenode);
                this.ItemTypeIDCache[itemtype.ID] = itemtype;
                this.ItemTypeNameCache[itemtype.Name] = itemtype;
            }
        }

        internal IEnumerable<ItemType> ItemTypes
        {
            get
            {
                return this.ItemTypeIDCache.Values;
            }
        }

        internal ItemType ItemType(String Name)
        {
            if (this.ItemTypeNameCache.ContainsKey(Name))
            {
                return this.ItemTypeNameCache[Name];
            }
            else
            {
                throw new ArgumentException("Invalid ItemType Name");
            }
        }

        internal ItemType ItemType(Guid ID)
        {
            if (this.ItemTypeIDCache.ContainsKey(ID))
            {
                return this.ItemTypeIDCache[ID];
            }
            else
            {
                throw new ArgumentException("Invalid ItemType ID");
            }
        }

        private Dictionary<Guid, Item> ItemCache;

        internal Item Create(ItemType ItemType, Guid ID)
        {
            if (!this.ItemCache.ContainsKey(ID))
            {
                this.ItemCache[ID] = new Item(ItemType, ID);
            }

            return this.ItemCache[ID];
        }

        public Manager()
        {
            this.SessionCache = new Dictionary<String, Session>();
            this.ItemTypeIDCache = new Dictionary<Guid, ItemType>();
            this.ItemTypeNameCache = new Dictionary<String, ItemType>();
            this.ItemCache = new Dictionary<Guid, Item>();
        }
    }
}
