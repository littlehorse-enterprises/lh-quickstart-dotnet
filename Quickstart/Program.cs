using Quickstart;
using LittleHorse.Sdk;
using LittleHorse.Sdk.Common.Proto;
using LittleHorse.Sdk.Worker;

var config = new LHConfig();
var client = config.GetGrpcClientInstance();

var request = new PutExternalEventDefRequest
{
    Name = "identity-verified"
};
Console.WriteLine("Registering ExternalEventDef");
client.PutExternalEventDef(request);

var tasks = new KnowYourCustomerTasks();

var identityVerificationWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "verify-identity", config);
var notifyCustomerVerifiedWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "notify-customer-verified", config);
var notifyCustomerNotVerifiedWorker = new LHTaskWorker<KnowYourCustomerTasks>(tasks, "notify-customer-not-verified", config);

Console.WriteLine("Registering TaskDefs");
identityVerificationWorker.RegisterTaskDef();
notifyCustomerVerifiedWorker.RegisterTaskDef();
notifyCustomerNotVerifiedWorker.RegisterTaskDef();

var workflow = QuickstartWorkflow.GetWorkflow();

Console.WriteLine("Registering WfSpec");
workflow.RegisterWfSpec(client);

Console.WriteLine("Starting workers");
identityVerificationWorker.Start();
notifyCustomerVerifiedWorker.Start();
notifyCustomerNotVerifiedWorker.Start();

Console.WriteLine("Workers started");
Console.WriteLine("Press enter to stop the workers");
Console.ReadLine();

Console.WriteLine("Stopping workers");
identityVerificationWorker.Close();
notifyCustomerVerifiedWorker.Close();
notifyCustomerNotVerifiedWorker.Close();
