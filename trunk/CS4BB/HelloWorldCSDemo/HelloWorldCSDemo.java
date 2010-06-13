//using System.Linq;  // Not supported yet
//using System.IO;  // Not supported yet
import net.rim.device.api.ui.*;

package com.lennie;

public class HelloWorldCSDemo extends UiApplication
{
public HelloWorldCSDemo()
{
HelloWorldMainScreen mainScreen = new HelloWorldMainScreen();
super.pushScreen(mainScreen);
}
public static void main(String[] args)
{
HelloWorldCSDemo app = new HelloWorldCSDemo();
app.enterEventDispatcher();
}
}
