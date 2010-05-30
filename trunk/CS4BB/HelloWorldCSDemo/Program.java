//using System.Linq;  // Not supported yet
import net.rim.device.api.ui.*;
 
package HelloWorldCSDemo;

class HelloWorldCSDemo : UiApplication
{
public HelloWorldCSDemo()
{
HelloWorldMainScreen mainScreen = new HelloWorldMainScreen();
pushScreen(mainScreen);
}
 
static void Main(string[] args)
{
HelloWorldCSDemo app = new HelloWorldCSDemo();
app.enterEventDispatcher();
}
}
}
