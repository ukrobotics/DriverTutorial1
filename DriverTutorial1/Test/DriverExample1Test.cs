/*
 * Copyright: UK Robotics Ltd
 * Created by: mico
 * Created: 05 March 2010
 */

using NUnit.Framework;
using UKRobotics.SystemInterfaces.Devices.Test;

namespace UKRobotics.DriverTutorial1.Test
{
    [TestFixture]
    public class DriverExample1Test
    {


        [Test]
        public void Test0()
        {
            string xml = @"<device ip-address=""192.168.0.123"" />";

            DriverExample1 driverExample1 = new DriverExample1();
            DeviceTestUtils.TestToAndFromXml(driverExample1, xml);

            Assert.AreEqual("192.168.0.123", driverExample1.DeviceIPAddress);
        }

        
    }
}