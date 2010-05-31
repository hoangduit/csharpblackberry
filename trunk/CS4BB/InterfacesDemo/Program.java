import net.rim.device.api.ui.*;
 
package InterfacesDemo;

public class InterfacesDemo extends UiApplication implements IAddCalculate, ISubtractCalculate
{
public InterfacesDemo()
{
}
 
public int Add(int a, int b)
{
return a + b;
}
 
public int Subtract(int a, int b)
{
return a - b;
}
 
public static void main(String[] args)
{
InterfacesDemo demo = new InterfacesDemo();
demo.Add(1, 2);
demo.Subtract(10, 5);
}
}
