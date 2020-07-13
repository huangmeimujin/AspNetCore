using LocationReports.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationReports.Events
{

    /// <summary>
    /// AMQP事件生产器
    /// </summary>
    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger logger;
        private AMQPOptions rabbitOptions;
        private ConnectionFactory connectionFactory;

        public const string QUEUE_LOCATIONRECORDED = "memberlocationrecorded";

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger,IOptions<AMQPOptions> amqpOptions)
        {
            this.logger = logger;
            this.rabbitOptions = amqpOptions.Value;
            connectionFactory = new ConnectionFactory() 
            { 
                UserName=rabbitOptions.Username,
                Password=rabbitOptions.Password,
                VirtualHost=rabbitOptions.VirtualHost,
                HostName=rabbitOptions.HostName,
                Uri=new Uri(rabbitOptions.Uri)
            };
            logger.LogInformation("AMQP事件发射器配置的Url{0}",rabbitOptions.Uri);
            
        }
        /// <summary>
        /// 把事件发布到由RabbitMQ支持的高级消息队列协议AMQP的队列中
        /// </summary>
        /// <param name="locationRecordedEvent"></param>
        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            //创建连接
            using (var conn=connectionFactory.CreateConnection())
            {
                //创建一个信息通道
                using (var channel = conn.CreateModel())
                {
                    //声明一个队列
                    channel.QueueDeclare(
                        queue:QUEUE_LOCATIONRECORDED,
                        durable:false,
                        exclusive:false,
                        autoDelete:false,
                        arguments:null);
                    var jsonPayLoad = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayLoad);
                    //发布事件
                    channel.BasicPublish(
                        exchange:"",
                        routingKey:QUEUE_LOCATIONRECORDED,
                        basicProperties:null,
                        body:body
                        
                        );
                }
               

            }
        }
    }
}
