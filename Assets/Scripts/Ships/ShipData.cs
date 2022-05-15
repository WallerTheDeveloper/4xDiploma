// class which stores ship data
namespace Ships
{
    public class ShipData
    {
        private string _shipPersonalCode;
        private int _healthpoints;
    
        public ShipData(string shipPersonalCode, int healthpoints)
        {
            _shipPersonalCode = shipPersonalCode;
            _healthpoints = healthpoints;
        }
    
        public string ShipPersonalCode { get => _shipPersonalCode; }
        public int HP { get => _healthpoints; }
    }
}
