using System;
using UKRobotics.SystemInterfaces.Devices;
using UKRobotics.SystemInterfaces.Objects.CommonObjectTypes;

namespace UKRobotics.DriverTutorial1
{
    public class DriverExampleLocationMonitor : DeviceBase, IDevice
    {



        protected override void HandleDeviceContextSet()
        {
            base.HandleDeviceContextSet();


            //
            // HANDLE SOMETHING ON THIS DEVICE ....
            ILocation location = DeviceLocations[0];
            location.CurrentObjectChanged += HandleObjectAtLocationChanged;


            //
            // WE CAN EVEN HANDLE SOMETHING ON ANOTHER DEVICE LOCATION IN THE SYSTEM
            ILocation echoSourceLocation = System.DeviceManager.GetDeviceLocation("EchoSource");
            echoSourceLocation.CurrentObjectChanged += HandleObjectAtLocationChanged;

            

        }

        private void HandleObjectAtLocationChanged(CurrentObjectChangeEventArgs args)
        {

            switch (args.ObjectChangeEventType)
            {
                case ObjectChangeEventType.Arrive:
                    PlateObjectData plate = args.Location.CurrentObject as PlateObjectData;
                    
                    Console.WriteLine($"PLATE {plate?.BarCode} ARRIVED AT {args.Location.Name}");

                    break;
                case ObjectChangeEventType.Depart:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }




    }
}