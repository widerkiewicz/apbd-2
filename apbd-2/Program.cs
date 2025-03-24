// See https://aka.ms/new-console-template for more information
namespace apbd_2;
class Program
{
    static void Main(string[] args)
    {
        var ship = new ContainerShip(300.0, 5, 20.0);
        var fluidContainer = new FluidContainer(20.0,5.0,20.0,20.0);
        var gasContainer = new GasContainer(20.0, 5.0, 20.0, 20.0, 20.0);
        var freezerContainer = new FreezerContainer(20.0, 5.0, 20.0, 20.0);
        ship.LoadContainer(fluidContainer);
        var containers = new List<Container>(){gasContainer, freezerContainer};
        ship.LoadContainers(containers);
        ship.Print();
        ship.RemoveContainer(fluidContainer);
        ship.Print();
        Food banana = new Food("Bananas",13.3);
        Food chocolate = new Food("Chocolate",18);
        freezerContainer.Fill(banana, 5);
        var newContainer = new FluidContainer(20.0, 5.0, 20.0, 20.0);
        ship.Replace("KON-L-1", newContainer);
        ship.Replace("KON-G-2", newContainer);
        ship.Print();
        freezerContainer.Print();
        var newShip = new ContainerShip(300.0, 5, 20.0);
        ship.MoveContainer(newShip, freezerContainer);
        ship.Print();
        newShip.Print();
        try
        {
            freezerContainer.Fill(chocolate, 5);
        }
        catch (IllegalTypeException e)
        {
            Console.WriteLine("IllegalTypeException");
        }
        
        fluidContainer.Fill(10.0);
        try
        {
            fluidContainer.Fill(20.0);
            
        }
        catch (OverfillException e)
        {
            Console.WriteLine("OverfillException");
        }

        var container = new FluidContainer(20.0, 100000000.0, 20.0, 100000000.0);
        try
        {
            ship.LoadContainer(container);
        }
        catch (ShipOverloadException e)
        {
            Console.WriteLine("ShipOverloadException");
        }
        fluidContainer.Empty();
        fluidContainer.Fill(new Fluid("fluid",true),14.0);
        


    }
}