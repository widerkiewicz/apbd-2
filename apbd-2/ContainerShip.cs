namespace apbd_2;

public class ContainerShip(double maxSpeed, int maxCapacity, double maxWeight)
{
    private double maxSpeed = maxSpeed;
    private int maxCapacity = maxCapacity;
    private double maxWeight = maxWeight;
    private List<Container> Containers = new List<Container>();
    private double currentWeight = 0.0;

    public void LoadContainer(Container container)
    {
        var newWeight = ((container.weight + container.cargoWeight) * 0.001) + currentWeight;
        if (newWeight > maxWeight) throw new ShipOverloadException();
        currentWeight = newWeight;
        Containers.Add(container);
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(Container container)
    {
        currentWeight -= container.weight * 0.001;
        Containers.Remove(container);
    }

    public bool RemoveContainer(string containerSerial)
    {
        Container toRemove = null;
        foreach (var container in Containers)
        {
            if (container.serialNumber == containerSerial) toRemove = container;
        }

        if (toRemove != null)
        {
            Containers.Remove(toRemove);
            return true;
        }
        return false;
    }

    public void Replace(string containerSerial, Container newContainer)
    {
        var removed = RemoveContainer(containerSerial);
        if (removed) LoadContainer(newContainer);
        else Console.WriteLine("Container not found");
    }

    public void MoveContainer(ContainerShip containerShip, Container container)
    {
        RemoveContainer(container);
        containerShip.LoadContainer(container);
    }

    public void Print()
    {
        Console.WriteLine("Max speed: " + maxSpeed + ", Max capacity: " + maxCapacity + ", Max weight: " + maxWeight +
                          ", Current weight: " + currentWeight);
        Console.WriteLine("Current cargo: ");
        foreach (var container in Containers) Console.WriteLine(container.ToString());
    }
}