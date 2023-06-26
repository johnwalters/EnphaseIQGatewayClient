namespace IQClientLib.Responses.Status
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Connection
    {
        public string mqtt_state { get; set; }
        public string prov_state { get; set; }
        public string auth_state { get; set; }
        public string sc_stream { get; set; }
        public string sc_debug { get; set; }
    }




}
