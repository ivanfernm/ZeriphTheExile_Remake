public interface IMelteable
{
   float MeltingStartTemperature { get;set;}
   float MeltingPoint { get;set;}
   float MeltingSpeed { get;set;}
   float currentTemperature { get;set;}
   
   bool IsMelted { get; set; }

    void Melt(IHeatEmmiter heatEmmiter);
    void Cold();
}
