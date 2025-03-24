namespace apbd_2;

public class Container
{
    public double cargoWeight = 0;
    public double height;
    public double weight;
    public double depth;
    public string serialNumber;
    public double maxWeight;
    public static int _counter = 0;
    public int ID { get; set; }
    
    
    public void Empty()
    {
        cargoWeight = 0;
    }

    public void Fill(double amount)
    {
        var newWeight = amount + cargoWeight;
        if (newWeight + weight > maxWeight) throw new OverfillException();
        cargoWeight = newWeight;
    }

    public override string ToString()
    {
        return this.serialNumber;
    }

    public void Print()
    {
        Console.WriteLine("Serial number: " + serialNumber + " Depth: " + depth + " Height: " + height +
                          " Weight: " + weight  + " Cargo weight: " + cargoWeight + " Max weight: " + maxWeight);
    }
}

class FluidContainer : Container, IHazardNotifier
{
    private Fluid fluid = null;

    public FluidContainer(double height, double weight, double depth, double maxWeight) 
    {
        this.height = height;
        this.weight = weight;
        this.depth = depth;
        this.maxWeight = maxWeight;
        this.ID = Interlocked.Increment(ref _counter);
        this.serialNumber = "KON-L-"+ID;
    }

    public void notifyAboutHazard()
    {
        Console.WriteLine("Potential hazard at container ID: " + this.serialNumber);
    }

    public virtual void Fill(Fluid fluid, double amount)
    {
        var newWeight = amount + cargoWeight + weight;
        if (newWeight > maxWeight) throw new OverfillException();
        if (fluid.hazard && newWeight > 0.5 * this.maxWeight) notifyAboutHazard();
        else if (newWeight > 0.9 * this.maxWeight) notifyAboutHazard();
        this.cargoWeight = newWeight;

    }
    
}

class GasContainer : Container, IHazardNotifier
{
    public GasContainer(double height, double weight, double depth, double maxWeight, double pressure) 
    {
        this.height = height;
        this.weight = weight;
        this.depth = depth;
        this.maxWeight = maxWeight;
        this.pressure = pressure;
        this.ID = Interlocked.Increment(ref _counter);
        this.serialNumber = "KON-G-"+ID;
    }
    private double pressure;
    public void notifyAboutHazard()
    {
        Console.WriteLine("Potential hazard at container ID: " + this.serialNumber);
    }

    public virtual void Empty()
    {
        cargoWeight *= 0.05;
    }
    
    public virtual void Print()
    {
        Console.WriteLine("Serial number: " + serialNumber + " Depth: " + depth + " Height: " + height +
                          " Weight: " + weight  + " Cargo weight: " + cargoWeight + " Max weight: " + maxWeight + 
                          " Pressure: " + pressure);
    }
    
}

class FreezerContainer : Container
{
    private Food food;
    
    public FreezerContainer(double height, double weight, double depth, double maxWeight) 
    {
        this.height = height;
        this.weight = weight;
        this.depth = depth;
        this.maxWeight = maxWeight;
        this.ID = Interlocked.Increment(ref _counter);
        this.serialNumber = "KON-C-"+ID;
    }
    public virtual void Fill(Food food, double amount)
    {
        if (this.food != null && food.name != this.food.name)  throw new IllegalTypeException();
        this.food = food;
        var newWeight = amount + cargoWeight + weight;
        if (newWeight + weight > maxWeight) throw new OverfillException();
        cargoWeight = newWeight;
    }

    public virtual void Empty()
    {
        this.food = null;
        this.weight = 0;
    }

}
