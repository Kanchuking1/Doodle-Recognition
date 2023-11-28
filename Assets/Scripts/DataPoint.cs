public class DataPoint
{
    public double a0, a1;
    public int diamondType;

    public DataPoint(double a0, double a1, int diamondType) 
    {
        this.a0 = a0;
        this.a1 = a1;
        this.diamondType = diamondType;
    }

    public override string ToString()
    {
        return "a0 " + a0 + "\na1 " + a1 + "\n data " + diamondType;
    }
}