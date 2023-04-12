# Revolution Driver Tutorial

## Intro
This repo contains examples to aid 3rd parties to create their own drivers or plugins for Revolution.

In Revolution, a driver ( also refered to as a device ) is a C# class that is a plugin to Revolution. 
A driver can control hardware but it can also simply be a plugin that can be called from a schedule operation to do non-hardware related
actions, EG reading or writing to a DB.  You can think of a driver as a plugin that can control hardware, or do anything in fact. 
It can be as simple as an empty class definition, or as complex as a plugin for a liquid handler backed by a LIMS/DB interface.

Revolution drivers are self configuring by using Attribute decoration for
- configuration properties, 
- schedule operations and their associated parameters
- simulation (3D cad files)


This repo provides two primary classes as follows:
- UKRobotics.DriverTutorial1.DriverExample1  - shows how to create a simple device together with associated configuration and also how to expose operations to the scheduler
- UKRobotics.DriverTutorial1.RobotExample1  - shows how to create a simple robot device


## Driver Configuration
Revolution drivers can be created without needing to code a UI class to support the driver by using our standard custom attributes to decorate your class properaties. 
This will allow the Revolution GUI to expose these properties to the user so that they can be set as required to configure things such as COM ports, IP addresses, DB connection strings.. etc etc
~~~
        [DeviceProperty("IP Address")]
        public string DeviceIPAddress
        {
            get { return _deviceIPAddress; }
            set
            {
                _deviceIPAddress = value;
                
                //inform Device Manager that config 
                // changed hence data requires saving
                FireDeviceConfigurationChanged();
            }
        }
~~~

By default Revolution stores configuration data into local XML files, however this can be overridden to allow you to save such values in a different place, such as a database if you prefer.


## Driver Operations
Operations are methods that perform tasks that are called from a schedule. In Revolution, schedules are the list of operations than run the system. Schedules define plate movement, device actions upon these 
plates and also program flow related tasks such as 'loops', 'while', 'if' etc.
Driver classes expose operations as regular C# methods that are decorated with the DeviceOperationAttribute as shown below in this simple example:

~~~
        /// <summary>
        /// A device operation method.
        /// </summary>
        /// <param name="deviceOperationContext">
        /// The IDeviceOperationContext which must always be 
        /// the first parameter for operation methods. 
        /// Revolution provides this data to the device at 
        /// runtime when an operation is invoked.
        /// </param>
        /// <param name="rpm">RPM</param>
        /// <param name="duration">Spin duration</param>
        [DeviceOperation]
        public void Spin(
            IDeviceOperationContext deviceOperationContext, 
            int rpm, TimeSpan duration )
        {
            Logger.Info($"Spin at rpm {rpm} for duration {duration.TotalSeconds}secs"); // log the parameters
        }
~~~

Revolution will introspect the device class and look for public methods with the DeviceOperation attribute and first parameter of type IDeviceOperationContext.  
Any following parameters are then exposed in the UI in the schedule editor to allow the user to define these as they wish.
At run time Revolution will pass in an instance of IDeviceOperationContext to the method. This is a rich interface by which the driver can access the 
system as a whole to access data on the plates in the system ( EG to read bar codes etc ). It can also access the current schedule thread to request
error handling and also write to the Revolution Event Database to record actions if required. A full explaination of IDeviceOperationContext is beyond the scope
of this simple demo class, please reach out to UK Robotics for more information/support.


## Unit Testing
We are big fans of unit testing and hope you are also!!!  
The core of Revolution has over 5000 NUnit unit tests and we always endevour to ensure that all parts of Revolution, including plugins, are easily testable.

See the class DriverExample1Test in this repo for an example on how to support unit testing of your device driver class, or see below:-
~~~
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
~~~


## More information
See the following for more information about Revolution and also driver development:
- [Revolution user docs](https://ukrobotics.tech/docs/revolution/revo-introduction/)
- [Revolution driver developer docs](https://ukrobotics.atlassian.net/wiki/spaces/RDD/overview?key=RDD)


## Feedback wanted!
Are we missing an example in this repo, please let us know!










