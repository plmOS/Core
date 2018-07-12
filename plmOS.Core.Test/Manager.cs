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
    public class Manager
    {
        [TestMethod]
        public void LoadSchema()
        {
            string userid = "admin";
            string itemtypename = "Test";

            Core.Manager manager = new Core.Manager();
            manager.LoadSchema(Assembly.GetExecutingAssembly().GetManifestResourceStream("plmOS.Core.Test.Resources.Schema.xml"));
            Core.Session session = manager.Login(userid);
            Core.ItemType itemtype = session.ItemType(itemtypename);
            Assert.IsNotNull(itemtype);
            Assert.AreEqual(itemtypename, itemtype.Name);
        }

        [TestMethod]
        public void Login()
        {
            string userid = "admin";

            Core.Manager manager = new Core.Manager();
            Core.Session session = manager.Login(userid);
            Assert.IsNotNull(session);
            Assert.AreEqual(userid, session.UserID);
        }
    }
}
