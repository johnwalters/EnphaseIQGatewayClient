using Newtonsoft.Json;

namespace IQClientLib.Responses.Status
{
    public class Counters
    {
        public int main_CfgLoad { get; set; }
        public int main_CfgChanged { get; set; }
        public int main_taskUpdate { get; set; }
        public int MqttClient_publish { get; set; }
        public int MqttClient_respond { get; set; }
        public int MqttClient_msgarrvd { get; set; }
        public int MqttClient_create { get; set; }
        public int MqttClient_setCallbacks { get; set; }
        public int MqttClient_connect { get; set; }
        public int MqttClient_connect_err { get; set; }

        // The json response has MqttClient_connect_err in there twice with different casing. Same value.
        //public int MqttClient_connect_Err { get; set; }

        public int MqttClient_subscribe { get; set; }
        public int SSL_Keys_Create { get; set; }
        public int sc_hdlDataPub { get; set; }
        public int sc_SendStreamCtrl { get; set; }
        public int sc_SendDemandRspCtrl { get; set; }
        public int rest_Status { get; set; }
    }




}
