using System;
using System.Threading;
using UKRobotics.SystemInterfaces.Devices;
using UKRobotics.SystemInterfaces.Objects;
using UKRobotics.SystemInterfaces.Objects.CommonObjectTypes;
using UKRobotics.SystemInterfaces.Transfer;

namespace UKRobotics.DriverTutorial1
{


    /// <summary>
    /// This is a simple robot example. It extends TransferDeviceBase which provides most of the implementation of ITransferDevice except for //
    /// the code shown in this class.
    /// 
    /// 
    /// </summary>
    public class RobotExample1 : TransferDeviceBase
    {


        /// <summary>
        /// This is a robot, hence we return that this device is a Lifter.
        /// </summary>
        public override TransferDeviceType TransferDeviceType
        {
            get { return TransferDeviceType.Lifter; }
        }


        /// <summary>
        /// In this method we will move the robot towards the pick up location. We should not pick the plate in this method since it might not be in the pick position yet.
        /// It is allowed to do nothing in this method if required however this could harm system throughput and efficiency.
        /// This method should block until complete.
        /// </summary>
        /// <param name="sourceLocationName"></param>
        /// <param name="destinationLocationName"></param>
        /// <param name="transferContext"></param>
        public override void PrepareForTransfer(string sourceLocationName, string destinationLocationName, ITransferContext transferContext)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1)); // simulate the robot motion...
        }

        /// <summary>
        /// In this method we should pick the plate. We can also do any other tasks such as lid handling, barcode reading etc.
        /// We should finally move the plate close to the destination location but not place the plate down since the destination location
        /// might not be ready yet.
        /// This method should block until complete.
        /// </summary>
        /// <param name="sourceLocationName"></param>
        /// <param name="destinationLocationName"></param>
        /// <param name="transferContext"></param>
        public override void StartTransfer(string sourceLocationName, string destinationLocationName, ITransferContext transferContext)
        {
            // Get the object that we are to move
            IObjectData objectData = transferContext.ObjectDataContext.ObjectData;
            // Normally this will be a plate so we can cast safely to the plate type and so access the plate dimensions
            PlateObjectData plate = objectData as PlateObjectData;

            ILocation sourceLocation = System.DeviceManager.GetDeviceLocation(sourceLocationName);
            ILocation destinationLocation = System.DeviceManager.GetDeviceLocation(destinationLocationName);


            Thread.Sleep(TimeSpan.FromSeconds(1)); // simulate the robot motion...


            // In addition to physically moving the object we must also inform the scheduler of the plate movement.
            sourceLocation.CurrentObject = null;// the plate is no longer here since we will have picked it.


        }

        /// <summary>
        /// In this method we should place the plate at the destination location and retract the robot to a safe distance.
        /// This method should block until complete.
        /// </summary>
        /// <param name="sourceLocationName"></param>
        /// <param name="destinationLocationName"></param>
        /// <param name="transferContext"></param>
        public override void CompleteTransfer(string sourceLocationName, string destinationLocationName, ITransferContext transferContext)
        {
            // Get the object that we are to move
            IObjectData objectData = transferContext.ObjectDataContext.ObjectData;
            // Normally this will be a plate so we can cast safely to the plate type and so access the plate dimensions
            PlateObjectData plate = objectData as PlateObjectData;

            ILocation sourceLocation = System.DeviceManager.GetDeviceLocation(sourceLocationName);
            ILocation destinationLocation = System.DeviceManager.GetDeviceLocation(destinationLocationName);

            Thread.Sleep(TimeSpan.FromSeconds(1)); // simulate the robot motion...

            // In addition to physically moving the object we must also inform the scheduler of the plate movement.
            destinationLocation.CurrentObject = objectData;

        }


    }
}