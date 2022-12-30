using EmpPostLibrary;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EmpPostService.Controllers;
namespace EmpPostService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            /*GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;*/
            ListenForIntegrationEvents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private static void ListenForIntegrationEvents()
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (mode, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JObject.Parse(message);
                var type = ea.RoutingKey;
                if (type == "jobPost.add")
                {
                    EmpPostController.empPostRepoAsync.Add(data["PostId"].Value<Int32>());
                }else if (type == "jobPost.delete")
                {
                    EmpPostController.empPostRepoAsync.Delete(data["PostId"].Value<Int32>());
                }
            };
            channel.BasicConsume(queue: "jobPost.empPostsvc", autoAck: true, consumer: consumer);
        }
    }
}
