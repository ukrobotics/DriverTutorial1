using UKRobotics.SystemInterfaces.Devices;
using UKRobotics.SystemInterfaces.Simulator;

namespace UKRobotics.DriverTutorial1
{

    
    /// <summary>
    /// This driver will load the 3d OBJ file from the given relative path in the ModelFileAttribute.
    /// Note that model files are held in a directory tree in the installation folder "models".
    /// 
    /// 
    /// </summary>
    [ModelFile(@"devices\drivertutorial\DriverExample1.obj")]     // this attribute tells the scheduler where it should load the 3D simulation model file from. 
                                                                // This is a relative path to the root defined in the configuration.
                                                                // You will find DriverExample1.obj and .mtl files in the Revolution installation
    public class DriverWith3DModelExample : DeviceBase
    {
        
    }


}