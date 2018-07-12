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

namespace plmOS.Core
{
    public class Session : IEquatable<Session>
    {
        public Manager Manager { get; private set; }

        public String UserID { get; private set; }

        public IEnumerable<ItemType> ItemTypes
        {
            get
            {
                return this.Manager.ItemTypes;
            }
        }

        public ItemType ItemType(String Name)
        {
            return this.Manager.ItemType(Name);
        }

        public ItemType ItemType(Guid ID)
        {
            return this.Manager.ItemType(ID);
        }

        public Item Create(ItemType ItemType)
        {
            return this.Manager.Create(ItemType, Guid.NewGuid());
        }

        public bool Equals(Session other)
        {
            if (other != null)
            {
                return this.UserID.Equals(other.UserID);
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
                if (obj is Session)
                {
                    return this.Equals((Session)obj);
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
            return this.UserID;
        }

        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

        internal Session(Manager Manager, String UserID)
        {
            this.Manager = Manager;
            this.UserID = UserID;
        }
    }
}
