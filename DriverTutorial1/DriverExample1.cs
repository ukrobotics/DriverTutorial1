/*
 * Copyright: UK Robotics Ltd
 * Created by: Mike Counsell
 * Created: 04 March 2010
 */
using System;
using System.Data;
using System.Xml;
using UKRobotics.Common.Exceptions;
using UKRobotics.Common.Xml;
using UKRobotics.SystemInterfaces.Devices;
using UKRobotics.SystemInterfaces.ErrorHandling;
using UKRobotics.SystemInterfaces.ScheduleController;

namespace UKRobotics.DriverTutorial1
{
    /// <summary>
    /// An example device driver
    /// </summary>
    public class DriverExample1 : DeviceBase
    {
        private static readonly StringXmlAttributeConst IPAddressXmlAttributeConstant = new StringXmlAttributeConst("ip-address");

        private string _deviceIPAddress = string.Empty;

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

        /// <summary>
        /// method to allow the device to read its config 
        /// params from a device element. Note that under 
        /// some circumstances, ie on new device creation, 
        /// the device will not be read from xml and hence 
        /// this method will not be invoked before bringing 
        /// the device online
        /// </summary>
        /// <param name="deviceElement">device element</param>
        public override void FromXmlElement(
            XmlElement deviceElement)
        {
            base.FromXmlElement(deviceElement);

            _deviceIPAddress = IPAddressXmlAttributeConstant.ReadOptional(
                deviceElement, 
                string.Empty);
        }

        /// <summary>
        /// method to allow the device to set its config 
        /// params to the device element
        /// </summary>
        /// <param name="deviceElement">device element</param>
        public override void ToXmlElement(
            XmlElement deviceElement)
        {
            base.ToXmlElement(deviceElement);

            IPAddressXmlAttributeConstant.Write(
                deviceElement,
                _deviceIPAddress);
        }

        
        /// <summary>
        /// A device operation method. Revolution 
        /// will automatically find this C# method 
        /// and allow it to be invoked from a schedule.
        /// </summary>
        /// <param name="deviceOperationContext">
        /// The IDeviceOperationContext which must always be 
        /// the first parameter for operation methods. 
        /// Revolution provides this data to the device at 
        /// runtime when an operation is invoked.
        /// </param>
        /// <param name="scriptName"></param>
        [DeviceOperation]
        public void RunScript(
            IDeviceOperationContext deviceOperationContext, 
            string scriptName)
        {
            Logger.Info(scriptName); // Just log the script name
        }

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

        /// <summary>
        /// This operation serves to demonstrate the main 
        /// points of performing error handling within a 
        /// device operation.
        /// Note that it is not always required to 
        /// implement "in device" error handling, often
        /// an exception can be thrown from the
        /// device operation method. The advantage
        /// to performing "in device" error handling
        /// is that it allows the device to control
        /// the resumption point rather than a retry from
        /// a user resulting in calling the method again.
        /// </summary>
        /// <param name="deviceOperationContext"></param>
        [DeviceOperation]
        public void ErrorHandlingDeviceOperationExample(
            IDeviceOperationContext deviceOperationContext)
        {
            IScheduleThread scheduleThread = deviceOperationContext.ScheduleThread;


            ErrorHandlerData<ErrorRecoveryActionType> errorData = new ErrorHandlerData<ErrorRecoveryActionType>(
                scheduleThread,
                "An example error has occured", // An error message
                null, 
                // list of recovery actions shown to user as appropriate to error
                ErrorRecoveryActionType.IgnoreErrorAndContinue, 
                ErrorRecoveryActionType.Retry
                );

            System.HandleError(errorData);

            // Note that the following code simply logs what action the user selected. In the real world
            // we would of course perform the chosen action.
            if (errorData.IsErrorRecoveryActionSpecified)
            {
                ErrorRecoveryActionType chosenAction = errorData.ErrorRecoveryAction;
                switch (chosenAction)
                {
                    case ErrorRecoveryActionType.Retry:
                        Logger.Info(string.Format("User requested action {0}", chosenAction));
                        break;

                    case ErrorRecoveryActionType.IgnoreErrorAndContinue:
                        Logger.Info(string.Format("User requested action {0}", chosenAction));
                        break;

                    default:
                        throw new InvalidEnumValueException(chosenAction);
                }
            }
            else
            {
                // handle the case that no action is chosen. a good option is to just throw upwards.
                throw new DeviceException("No error recovery action chosen by user!");
            }


        }

    }
}