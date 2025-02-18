using LittleHorse.Sdk.Workflow.Spec;
using LittleHorse.Sdk.Common.Proto;

namespace Quickstart
{
	public static class QuickstartWorkflow
	{
		public static Workflow GetWorkflow()
		{
			void MyEntryPoint(WorkflowThread wf)
			{
				var firstName = wf.DeclareStr("first-name").Searchable().Required();
				var lastName = wf.DeclareStr("last-name").Searchable().Required();
				var ssn = wf.DeclareInt("ssn").Masked().Required();

				var identityVerified = wf.DeclareBool("identity-verified").Searchable();

				wf.Execute("verify-identity", firstName, lastName, ssn).WithRetries(3);

				var identityVerificationResult = wf.WaitForEvent("identity-verified").WithTimeout(60 * 60 * 24 * 3);

				wf.HandleError(identityVerificationResult, LHErrorType.Timeout, handler =>
				{
					handler.Execute("notify-customer-not-verified", firstName, lastName);
					// handler.Fail("customer-not-verified", "Unable to verify customer identity in time.");
				});

				identityVerified.Assign(identityVerificationResult);

				wf.DoIf(
					wf.Condition(identityVerified, Comparator.Equals, true),
					ifThread => ifThread.Execute("notify-customer-verified", firstName, lastName),
					elseThread => elseThread.Execute("notify-customer-not-verified", firstName, lastName)
				);
			}

			return new Workflow("quickstart", MyEntryPoint);
		}
	}
}