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
using System.Xml;

namespace plmOS.Core
{
    public class ItemType : IEquatable<ItemType>
    {
        public Manager Manager { get; private set; }

        public Guid ID { get; private set; }

        public String Name { get; private set; }

        public bool Equals(ItemType other)
        {
            if (other != null)
            {
                return this.ID.Equals(other.ID);
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is ItemType)
                {
                    return this.Equals((ItemType)obj);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        internal ItemType(Manager Manager, XmlNode Node)
        {
            this.Manager = Manager;

            XmlAttribute idattribute = Node.Attributes["id"];
            this.ID = Guid.Parse(idattribute.Value);

            XmlAttribute nameattribute = Node.Attributes["name"];
            this.Name = nameattribute.Value;
        }
    }
}
