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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace plmOS.Core.Test
{
    [TestClass]
    public class Session
    {
        private Core.Session _session { get; set; }

        [TestInitialize()]
        public void Initialize()
        {
            string userid = "admin";

            Core.Manager manager = new Core.Manager();
            manager.LoadSchema(Assembly.GetExecutingAssembly().GetManifestResourceStream("plmOS.Core.Test.Resources.Schema.xml"));
            this._session = manager.Login(userid);
        }

        [TestMethod]
        public void Create()
        {
            string itemtypename = "Test";
            Core.ItemType itemtype = this._session.ItemType(itemtypename);
            Core.Item item = this._session.Create(itemtype);
            Assert.AreEqual(itemtype, item.ItemType);
        }
    }
}
