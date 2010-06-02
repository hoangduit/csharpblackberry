import net.rim.device.api.ui.container.*;
import net.rim.device.api.ui.component.*;
import net.rim.device.api.ui.*;

public class HelloWorldMainScreen extends MainScreen
{
public HelloWorldMainScreen()
{
// Set the displayed title of the screen
super.setTitle(getAppTitle());
// Add a read only text field (RichTextField) to the screen.  The
// RichTextField is focusable by default. Here we provide a style
// parameter to make the field non-focusable.
super.add(new RichTextField("Hello World!", Field.NON_FOCUSABLE));
}
/***
* Displays a dialog box to the user with the text "Goodbye!" when the
* application is closed.
*/
public void close() 
{
// Display a farewell message before closing the application
Dialog.alert("Goodbye!");
super.close();
}
/// <summary>
/// Return the application title
/// </summary>
/// <returns></returns>
protected String getAppTitle()
{
return "Hello World Demo";
}
}
