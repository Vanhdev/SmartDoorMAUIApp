using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Internal;
using MQTTnet.Protocol;

namespace MauiApp1.Services
{
    public class MQTTService
    {
        //static DeviceService? _deviceService;
        public static IMqttClient? mqttClient;

        //public MQTTService(DeviceService deviceService) 
        //{
        //    _deviceService = deviceService;
        //}

        public static IMqttClient Start(string host, int port)
        {
            string cid = Guid.NewGuid().ToString();
            mqttClient = new MqttFactory().CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                              .WithTcpServer(host, port)
                              .WithClientId(cid)
                              .WithCleanSession()
                              .Build();

            mqttClient.ConnectAsync(options).Wait();

            mqttClient.SubscribeAsync("ESP32/LOCK_RESPONSE");
            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var res = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                string[] infos = res.Split(',');
                if (!DeviceService.Device_State_Map.ContainsKey(infos[0]))
                {
                    DeviceService.Device_State_Map.Add(infos[0], infos[1]);
                }
                else
                {
                    DeviceService.Device_State_Map[infos[0]] = infos[1];
                }
                
                return Task.CompletedTask;
            };

            //mqttClient.ApplicationMessageReceivedAsync += async e =>
            //{
            //    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            //    string[] infos = payload.Split(',');

            //    await _deviceService.CreateAsync(new Models.Device { 
            //                                        Id = new Guid(),
            //                                        Name = infos[0],
            //                                        Type = infos[1]
            //                                    }) ;
            //};
            return mqttClient;
        }


        public async Task<MqttClientPublishResult> PublishAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
                    .Build();
            var result = await mqttClient.PublishAsync(message);

            return result;
        }
    }
}
