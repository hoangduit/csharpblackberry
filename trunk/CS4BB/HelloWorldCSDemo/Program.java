//using System.Linq;  // Not supported yet
import net.rim.device.api.ui.*;
 
package HelloWorldCSDemo;

public class HelloWorldCSDemo extends UiApplication
{
public HelloWorldCSDemo()
{
HelloWorldMainScreen mainScreen = new HelloWorldMainScreen();
base.pushScreen(mainScreen);
}
 
static void Main(string[] args)
{
HelloWorldCSDemo app = new HelloWorldCSDemo();
app.enterEventDispatcher();
}
}
}
