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




