using LGAPIGateway.Models;
using LGAPIGateway.Resources;
using LGAPIGateway.Singletons;
using RestAPIService;

namespace LGAPIGateway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var handler = new RestAPIHandler();
            var system = new SystemResource();
            var engine = new AnalyticEngineResource();
            //engine.DebugReset();
            engine.OnUpdateEngine += system.GetEngineCount;
            //handler.EnableAuth("admin", "12345", RestAPIManager.AuthorizationHelper.AuthType.Basic);
            handler.AddResources(system);
            handler.AddResources(engine);
            handler.AddResources(new AuthorizationResource());
            handler.Init(PreDefineResources.inst.LGAPIPort);
            handler.Start();
            engine.Restore();
            System.Console.ReadLine();
        }
    }
}
