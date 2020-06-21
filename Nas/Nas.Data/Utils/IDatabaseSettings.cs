namespace Nas.API.Utils
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }    
        public string Collection { get; set; }
    }
}