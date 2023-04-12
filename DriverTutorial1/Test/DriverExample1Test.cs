/*
 * Copyright: UK Robotics Ltd
 * Created by: mico
 * Created: 05 March 2010
 */

using System.Xml;
using Moq;
using NUnit.Framework;
using UKRobotics.Common;
using UKRobotics.SystemInterfaces;
using UKRobotics.SystemInterfaces.Devices.Test;

namespace UKRobotics.DriverTutorial1.Test
{

    /// <summary>
    ///
    /// How to unit test your device...
    /// 
    /// </summary>
    [TestFixture]
    public class DriverExample1Test
    {


        [Test]
        public void TestFromXml0()
        {
            //////////////
            //Arrange
            string xml = @"<device ip-address=""192.168.0.123"" />";
            XmlDocument deviceDocument = XmlUtils.GetXmlDocument(xml);

            DriverExample1 driverExample1 = new DriverExample1();

            Mock<ISystem> systemMock = new Mock<ISystem>();
            driverExample1.DeviceContext = DeviceTestUtils.CreateDeviceContext(systemMock.Object, driverExample1);

            /////////////
            // Act
            driverExample1.FromXmlElement(deviceDocument.DocumentElement);

            /////////////
            // Assert
            Assert.AreEqual("192.168.0.123", driverExample1.DeviceIPAddress);
        }

        
    }
}