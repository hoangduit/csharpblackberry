//using System.Linq;  // Not supported yet
//using System.IO;  // Not supported yet
import net.rim.device.api.ui.*;
package HelloWorldCSDemo;

public class HelloWorldCSDemo extends UiApplication
{
public HelloWorldCSDemo()
{
HelloWorldMainScreen mainScreen = new HelloWorldMainScreen();
base.pushScreen(mainScreen);
}
public static void main(String[] args)
{
HelloWorldCSDemo app = new HelloWorldCSDemo();
app.enterEventDispatcher();
}
}
